using System.Collections;
using System.Collections.Generic;
using Test.WPF.UI.Data.Models;
using Test.WPF.UI.Data.Repositories.Base;

namespace Test.WPF.UI.Data.Repositories
{
    public class TuiPermissionsRepository : Repository<TuiPermission>, ITuiPermissionsRepository
    {
        public TuiPermissionsRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IList<TuiPermission> GetPermissions(int userId, int viewModelId)
        {
            return Session.CreateSQLQuery(
                    @"SELECT p.ID, p.VIEW_MODEL_ACTION, p.USER_ID, p.DATE_EXPIRE 
                        FROM TUI_PERMISSIONS p
                        JOIN TUI_VIEW_MODEL_ACTIONS vma ON vma.ID = p.VIEW_MODEL_ACTION
                        WHERE p.USER_ID = ? AND vma.VIEW_MODEL = ?
                        ORDER BY p.ID")
                .AddEntity(typeof(TuiPermission))
                .SetInt32(0, userId)
                .SetInt32(1, viewModelId)
                .List<TuiPermission>();
        }
    }
}