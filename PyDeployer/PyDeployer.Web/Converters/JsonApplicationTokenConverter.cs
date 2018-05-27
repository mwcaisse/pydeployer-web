using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PyDeployer.Common.Entities;
using PyDeployer.Common.Mappers;

namespace PyDeployer.Web.Converters
{
    public class JsonApplicationTokenConverter : JsonConverter<ApplicationToken>
    {
        public override void WriteJson(JsonWriter writer, ApplicationToken value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value.ToViewModel());
        }

        public override ApplicationToken ReadJson(JsonReader reader, Type objectType, ApplicationToken existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            return null;
        }
    }
}
