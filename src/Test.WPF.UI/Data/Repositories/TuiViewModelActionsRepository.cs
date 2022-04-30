using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Criterion;
using Test.WPF.UI.Data.Models;
using Test.WPF.UI.Data.Repositories.Base;

namespace Test.WPF.UI.Data.Repositories
{
    public class TuiViewModelActionsRepository : Repository<TuiViewModelAction>, ITuiViewModelActionsRepository
    {
        public TuiViewModelActionsRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IList<TuiViewModelAction> GetFreeActionsOfViewModel(int viewModelId, int userId)
        {
            var vmActions = Session.CreateSQLQuery(
                    @"SELECT vma.*
                        FROM TUI_VIEW_MODEL_ACTIONS vma
                        WHERE vma.VIEW_MODEL = ?
                        ORDER BY vma.""NAME""")
                .AddEntity(typeof(TuiViewModelAction))
                .SetInt32(0, viewModelId)
                .List<TuiViewModelAction>();
            var userActions = Session.CreateSQLQuery(
                    @"SELECT vma.*
                        FROM TUI_VIEW_MODEL_ACTIONS vma
                        LEFT JOIN TUI_PERMISSIONS p ON p.VIEW_MODEL_ACTION = vma.ID
                        WHERE p.USER_ID = ? AND vma.VIEW_MODEL = ?")
                .AddEntity(typeof(TuiViewModelAction))
                .SetInt32(0, userId)
                .SetInt32(1, viewModelId)
                .List<TuiViewModelAction>();
            return vmActions.Except(userActions).ToList();
        }
    }
}