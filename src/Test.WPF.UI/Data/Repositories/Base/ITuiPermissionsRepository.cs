using System.Collections.Generic;
using Test.WPF.UI.Data.Models;

namespace Test.WPF.UI.Data.Repositories.Base
{
    public interface ITuiPermissionsRepository : IRepository<TuiPermission>
    {
        IList<TuiPermission> GetPermissions(int userId, int viewModelId);

        void RemoveAllPermissions(int userId, int viewModelId);

        void SavePermissions(ICollection<TuiPermission> permissions);
    }
}