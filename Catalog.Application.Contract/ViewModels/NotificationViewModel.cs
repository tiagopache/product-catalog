using Catalog.Model;
using Newtonsoft.Json;
using System;

namespace Catalog.Application.Contract.ViewModels
{
    public class NotificationViewModel : BaseViewModel<NotificationViewModel, Notification>
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("tipo")]
        public string Tipo { get; set; }

        [JsonProperty("dataEnvio")]
        public DateTime DataEnvio { get; set; }

        [JsonProperty("parametros")]
        public ParametersViewModel Parametros { get; set; }

        public override NotificationViewModel ToContract(Notification entity)
        {
            var result = base.ToContract(entity);

            if (result != null)
            {
                result.Parametros = new ParametersViewModel()
                {
                    IdProduto = entity.IdProduto,
                    IdSku = entity.IdSku
                };
            }

            return result;
        }

        public override Notification ToEntity(NotificationViewModel viewModel)
        {
            var result = base.ToEntity(viewModel);

            if (result != null)
            {
                result.IdProduto = viewModel.Parametros.IdProduto;
                result.IdSku = viewModel.Parametros.IdSku;
            }

            return result;
        }
    }
}
