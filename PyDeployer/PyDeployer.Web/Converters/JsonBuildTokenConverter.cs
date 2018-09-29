using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PyDeployer.Common.Entities;
using PyDeployer.Common.Mappers;

namespace PyDeployer.Web.Converters
{
    public class JsonBuildTokenConverter : JsonConverter<BuildToken>
    {
        public override void WriteJson(JsonWriter writer, BuildToken value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value.ToViewModel());
        }

        public override BuildToken ReadJson(JsonReader reader, Type objectType, BuildToken existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            return null; // We shouldn't ever try to deserialize directly to a build token, that's
                         //     what its viewmodel is for
        }
    }
}
