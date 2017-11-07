using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageSource
{
    public class Photo
    {
        public int Id { get; set; }
        [JsonProperty(PropertyName = "AlbumId")]
        public int PhotoBook { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
    }
}
