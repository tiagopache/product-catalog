using Catalog.Infrastructure.Data.Base;
using System;

namespace Catalog.Model
{
    public class Notification : BaseIdEntity
    {
        public string Tipo { get; set; }

        public DateTime DataEnvio { get; set; }

        public int IdProduto { get; set; }

        public int IdSku { get; set; }
    }
}
