using Newtonsoft.Json;

namespace FrogApp
{
    public class ParticleResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("connected")]
        public bool Connected { get; set; }

        [JsonProperty("return_value")]
        public int ReturnValue { get; set; }
    }
}