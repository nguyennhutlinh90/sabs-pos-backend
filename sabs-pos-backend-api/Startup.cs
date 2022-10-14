using CoreLib.PasswordHasher;
using CoreLib.SqlExtensions;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

using Newtonsoft.Json;

using sabs_pos_backend_api.Models;

using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Claims;

namespace sabs_pos_backend_api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VV";
                options.SubstitutionFormat = "VV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .WithExposedHeaders("Content-Disposition")
                        .SetIsOriginAllowed(origin => true);
                });
            });

            services.AddMvc()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss.fff";
                    //options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Ignore;
                    //options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Latest);

            var appSettings = Configuration.GetSettings();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(appSettings.Configuration.JwtTokenKey.ToBytes()),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = async (context) =>
                    {
                        if (context.Principal != null)
                        {
                            var uuid = context.Principal.FindFirstValue(ClaimType.LOGIN_UUID);
                            if (!string.IsNullOrEmpty(uuid))
                            {
                                var srvAccount = context.HttpContext.RequestServices.GetService<IAccountService>();
                                var account = await srvAccount.Get<AccountRes>(QF.EQ("uuid", uuid), include: QI.IC("roles", "uuid role_uuid"));
                                if (!account.IsNull())
                                {
                                    var password = context.Principal.FindFirstValue(ClaimType.PASSWORD);
                                    var role_uuid = context.Principal.FindFirstValue(ClaimType.ROLE_UUID);
                                    if (account.password_hash == password && account.role_uuid == role_uuid)
                                    {
                                        context.Success();
                                        return;
                                    }
                                }
                            }
                        }
                        context.Fail("Unauthorized");
                    }
                };
            });

            services.AddSqlHandler((configBuider) =>
            {
                configBuider.ConnectionString = appSettings.Configuration.ConnectionString;
                configBuider.UpdateError = (error) =>
                {
                    error.Function.TableColumnsNotFound = (prefix, table, columns) =>
                    {
                        return string.Format("{0} field(s) ({1}) were not found", prefix, string.Join(", ", columns));
                    };
                    error.Function.TableColumnsRequired = (prefix, table) =>
                    {
                        return string.Format("{0} table '{1}' field(s) are required", prefix, table);
                    };
                    error.Prefix.Select = "Cursor";
                };
            });

            services.AddSingleton<IAppSettings>(appSettings);
            services.AddSingleton<IPasswordHasher, PasswordHasher>();

            services.AddSingleton<IAccountService, AccountService>();
            services.AddSingleton<IActivityLogService, ActivityLogService>();
            services.AddSingleton<IEmployeeService, EmployeeService>();
            services.AddSingleton<IMailService, MailService>();
            services.AddSingleton<IPOSDeviceService, POSDeviceService>();
            services.AddSingleton<IRoleService, RoleService>();
            services.AddSingleton<IStoreService, StoreService>();

            // Register hosted services
            services.AddHostedService<DatabaseHostedService>();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigureOptions>();
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter into field the word 'Bearer' following by space and JWT",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                options.MapType<DateTime>(() => new OpenApiSchema { Type = "string", Format = "date-time", Example = new OpenApiString(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")) });

                options.OperationFilter<AuthorizeOperationFilter>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger(options =>
            {
                options.PreSerializeFilters.Add((swagger, req) =>
                {
                    swagger.Servers = new List<OpenApiServer>() { new OpenApiServer() { Url = $"https://{req.Host}" } };
                });
            });
            app.UseSwaggerUI(options =>
            {
                foreach (var desc in apiVersionDescriptionProvider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"../swagger/{desc.GroupName}/swagger.json", desc.ApiVersion.ToString());
                    options.DocExpansion(DocExpansion.List);
                }
            });
        }
    }
}
