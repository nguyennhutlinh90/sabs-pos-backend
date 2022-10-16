using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;

namespace sabs_pos_backend_api
{
    public static class ObjectExtension
    {
        public static bool IsNull(this object input)
        {
            if (input == null)
                return true;

            var instance = Activator.CreateInstance(input.GetType());
            var jsonInput = JsonConvert.SerializeObject(input);
            var jsonInstance = JsonConvert.SerializeObject(instance);
            return jsonInput == jsonInstance;
        }

        public static bool IsNull(this IEnumerable<object> inputs)
        {
            return inputs == null || !inputs.Any(i => !i.IsNull());
        }

        public static TResult MapTo<TResult>(this object input) where TResult : class
        {
            if (input.IsNull())
                return Activator.CreateInstance<TResult>();

            var jsonSerialize = JsonConvert.SerializeObject(input);
            return JsonConvert.DeserializeObject<TResult>(jsonSerialize, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            });
        }

        public static IEnumerable<TResult> MapTo<TResult>(this IEnumerable<object> inputs) where TResult : class
        {
            if (inputs.IsNull())
                return new List<TResult>();

            var jsonSerialize = JsonConvert.SerializeObject(inputs);
            return JsonConvert.DeserializeObject<IEnumerable<TResult>>(jsonSerialize, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            });
        }
    }
}
