using Newtonsoft.Json;

namespace create_gist
{
    public class File
    {
        [JsonProperty("content")]
        public string Content { get; set; }
    }
}
