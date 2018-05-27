using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PyDeployer.Common.Mappers;
using Environment = PyDeployer.Common.Entities.Environment;

namespace PyDeployer.Web.Converters
{
    public class JsonEnvironmentConverter : JsonConverter<Common.Entities.Environment>
    {
        public override void WriteJson(JsonWriter writer, Environment value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value.ToViewModel());
        }

        public override Environment ReadJson(JsonReader reader, Type objectType, Environment existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            return null;
        }
    }
}
