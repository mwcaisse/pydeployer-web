using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PyDeployer.Common.Entities;
using PyDeployer.Common.Mappers;

namespace PyDeployer.Web.Converters
{
    public class JsonApplicationEnvironmentTokenConverter : JsonConverter<ApplicationEnvironmentToken>
    {
        public override void WriteJson(JsonWriter writer, ApplicationEnvironmentToken value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value.ToViewModel());
        }

        public override ApplicationEnvironmentToken ReadJson(JsonReader reader, Type objectType, ApplicationEnvironmentToken existingValue,
            bool hasExistingValue, JsonSerializer serializer)
        {
            return null;
        }
    }
}
