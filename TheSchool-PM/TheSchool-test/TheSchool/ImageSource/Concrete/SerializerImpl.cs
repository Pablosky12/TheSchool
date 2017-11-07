using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ImageSource.Concrete
{
    public class SerializerImp : ISerializer
    {
        public string ToJson<T>(T source)
        {
            

            string json = JsonConvert.SerializeObject(source, Formatting.Indented);
            return json;
        }

        public T ToObject<T>(string source)
        {
            T returnObject = JsonConvert.DeserializeObject<T>(source);
            return returnObject;
        }
    }
}
