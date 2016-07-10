using Newtonsoft.Json;

namespace Catalog.Application.Contract.ViewModels
{
    public class DimensionViewModel
    {
        [JsonProperty("altura")]
        public double? Altura { get; set; }

        [JsonProperty("largura")]
        public double? Largura { get; set; }

        [JsonProperty("comprimento")]
        public double? Comprimento { get; set; }

        [JsonProperty("peso")]
        public double? Peso { get; set; }
    }
}
