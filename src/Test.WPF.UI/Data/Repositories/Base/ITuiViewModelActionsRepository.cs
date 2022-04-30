using System.Collections;
using System.Collections.Generic;
using Test.WPF.UI.Data.Models;

namespace Test.WPF.UI.Data.Repositories.Base
{
    public interface ITuiViewModelActionsRepository : IRepository<TuiViewModelAction>
    {
        IList<TuiViewModelAction> GetFreeActionsOfViewModel(int viewModelId, int userId);
    }
}