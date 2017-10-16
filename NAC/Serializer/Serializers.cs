using System.Collections.Generic;
using Newtonsoft.Json;

namespace NAC.Serializer

{
    public class Serializers
    {
        public T convertObject<T>(string response)
        {
            return JsonConvert.DeserializeObject<T>(response);
        }

        public List<T> convertList<T>(string response)
        {
            return JsonConvert.DeserializeObject<List<T>>(response);
        }
    }
}
