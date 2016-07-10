using Catalog.Application.Contract.ViewModels;
using System.Collections.Generic;

namespace Catalog.Application.Contract.Contracts
{
    public interface IApplicationServiceBase<TContract> where TContract : BaseViewModel
    {
        IList<TContract> Get(string includeProperties = null);
    }
}
