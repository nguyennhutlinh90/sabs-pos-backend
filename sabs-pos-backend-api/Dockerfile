#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["sabs-pos-backend-api/sabs-pos-backend-api.csproj", "sabs-pos-backend-api/"]
RUN dotnet restore "sabs-pos-backend-api/sabs-pos-backend-api.csproj"
COPY . .
WORKDIR "/src/sabs-pos-backend-api"
RUN dotnet build "sabs-pos-backend-api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "sabs-pos-backend-api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "sabs-pos-backend-api.dll"]