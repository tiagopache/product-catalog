using Newtonsoft.Json;

namespace Catalog.Application.Contract.ViewModels
{
    public class ImageItemViewModel
    {
        [JsonProperty("menor")]
        public string Menor { get; set; }

        [JsonProperty("maior")]
        public string Maior { get; set; }

        [JsonProperty("zoom")]
        public string Zoom { get; set; }

        [JsonProperty("ordem")]
        public int Ordem { get; set; }
    }
}
