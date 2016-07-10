using Newtonsoft.Json;

namespace Catalog.Application.Contract.ViewModels
{
    public class ParametersViewModel
    {
        [JsonProperty("idProduto")]
        public int IdProduto { get; set; }

        [JsonProperty("idSku")]
        public int IdSku { get; set; }
    }
}
