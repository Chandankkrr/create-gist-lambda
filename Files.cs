using Newtonsoft.Json;

namespace create_gist
{
    public class Files
    {
        [JsonProperty("hello_world.js")]
        public File HelloWorldJs { get; set; }
    }
}
