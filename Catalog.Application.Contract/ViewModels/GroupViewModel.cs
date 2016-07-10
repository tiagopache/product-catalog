using Newtonsoft.Json;
using System.Collections.Generic;

namespace Catalog.Application.Contract.ViewModels
{
    public class GroupViewModel
    {
        [JsonProperty("nome")]
        public string Nome { get; set; }

        private IList<AttributeItemViewModel> _atributos;
        [JsonProperty("atributos")]
        public IList<AttributeItemViewModel> Atributos
        {
            get
            {
                if (_atributos == null)
                    _atributos = new List<AttributeItemViewModel>();

                return _atributos;
            }
            set
            {
                _atributos = value;
            }
        }
    }
}
