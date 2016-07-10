using Catalog.Infrastructure.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Model
{
    public class Product : BaseIdEntity
    {
        public string Nome { get; set; }

        public string NomeReduzido { get; set; }

        [Key, Column(Order = 1)]
        public int IdParceiro { get; set; }

        [Key, Column(Order = 2)]
        public string SKU { get; set; }

        public string Modelo { get; set; }

        public string Ean { get; set; }

        public string Url { get; set; }

        public bool ForaDeLinha { get; set; }

        public decimal Preco { get; set; }

        public decimal? PrecoDe { get; set; }

        public bool Disponivel { get; set; }

        public int Estoque { get; set; }

        public bool Ativo { get; set; }

        public double? Altura { get; set; }

        public double? Largura { get; set; }

        public double? Comprimento { get; set; }

        public double? Peso { get; set; }
    }
}
