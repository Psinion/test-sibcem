using System;
using NHibernate;

namespace Test.WPF.UI.Data.Repositories.Base
{
    public interface IUnitOfWork : IDisposable
    {
        ISession Session { get; }

        IUsersRepository UsersRepository { get; }
        ITuiViewModelsRepository TuiViewModelsRepository { get; }
        ITuiViewModelActionsRepository TuiViewModelActionsRepository { get; }
        ITuiPermissionsRepository TuiPermissionsRepository { get; }

        void BeginTransaction();

        void CommitTransaction();

        void RollbackTransaction();

        void OpenSession();

        void CloseSession();
    }
}