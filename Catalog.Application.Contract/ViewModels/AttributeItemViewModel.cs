using Newtonsoft.Json;

namespace Catalog.Application.Contract.ViewModels
{
    public class AttributeItemViewModel
    {
        [JsonProperty("nome")]
        public string Nome { get; set; }

        [JsonProperty("valor")]
        public string Valor { get; set; }
    }
}
