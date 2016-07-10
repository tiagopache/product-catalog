using Catalog.Model;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Catalog.Application.Contract.ViewModels
{
    public class ProductViewModel : BaseViewModel<ProductViewModel, Product>
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }

        [JsonProperty("nomeReduzido")]
        public string NomeReduzido { get; set; }

        [JsonProperty("idParceiro")]
        public int IdParceiro { get; set; }

        [JsonProperty("codigo")]
        public string Codigo { get; set; }

        [JsonProperty("modelo")]
        public string Modelo { get; set; }

        [JsonProperty("ean")]
        public string Ean { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("foraDeLinha")]
        public bool ForaDeLinha { get; set; }

        [JsonProperty("preco")]
        public decimal Preco { get; set; }

        [JsonProperty("precoDe")]
        public decimal? PrecoDe { get; set; }

        [JsonProperty("disponivel")]
        public bool Disponivel { get; set; }

        [JsonProperty("estoque")]
        public int Estoque { get; set; }

        [JsonProperty("ativo")]
        public bool Ativo { get; set; }

        [JsonProperty("dimensoes")]
        public DimensionViewModel Dimensoes { get; set; }

        private IList<ImageItemViewModel> _imagens;
        [JsonProperty("imagens")]
        public IList<ImageItemViewModel> Imagens
        {
            get
            {
                if (_imagens == null)
                    _imagens = new List<ImageItemViewModel>();

                return _imagens;
            }
            set
            {
                _imagens = value;
            }
        }

        private IList<GroupViewModel> _grupos;
        [JsonProperty("grupos")]
        public IList<GroupViewModel> Grupos
        {
            get
            {
                if (_grupos == null)
                    _grupos = new List<GroupViewModel>();

                return _grupos;
            }
            set
            {
                _grupos = value;
            }
        }

        public override ProductViewModel ToContract(Product entity)
        {
            var result = base.ToContract(entity);

            if (result != null)
            {
                result.Codigo = entity.SKU;

                result.Dimensoes = new DimensionViewModel()
                {
                    Altura = entity.Altura,
                    Comprimento = entity.Comprimento,
                    Largura = entity.Largura,
                    Peso = entity.Peso
                };
            }

            return result;
        }

        public override Product ToEntity(ProductViewModel viewModel)
        {
            var result = base.ToEntity(viewModel);

            if (result != null)
            {
                result.SKU = viewModel.Codigo;

                result.Altura = viewModel.Dimensoes?.Altura;
                result.Comprimento = viewModel.Dimensoes?.Comprimento;
                result.Largura = viewModel.Dimensoes?.Largura;
                result.Peso = viewModel.Dimensoes?.Peso;
            }

            return result;
        }
    }
}
