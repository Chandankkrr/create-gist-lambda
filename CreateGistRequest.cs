using Newtonsoft.Json;

namespace create_gist
{
    public class CreateGistRequest
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("public")]
        public bool Public { get; set; }

        [JsonProperty("files")]
        public Files Files { get; set; }
    }
}
