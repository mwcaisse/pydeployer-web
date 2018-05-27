using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PyDeployer.Common.Entities;
using PyDeployer.Common.Mappers;

namespace PyDeployer.Web.Converters
{
    public class JsonApplicationConverter : JsonConverter<Application>
    {
        public override void WriteJson(JsonWriter writer, Application value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value.ToViewModel());
        }

        public override Application ReadJson(JsonReader reader, Type objectType, Application existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            return null; // We shouldn't ever try to deserialize directly to an application, that's
                         //     what its viewmodel is for
        }
    }
}
