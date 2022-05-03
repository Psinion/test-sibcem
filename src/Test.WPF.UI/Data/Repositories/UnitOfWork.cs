using NHibernate;
using Test.WPF.UI.Data.Repositories.Base;

namespace Test.WPF.UI.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ITransaction transaction;

        private IUsersRepository usersRepository;
        private ITuiViewModelsRepository tuiViewModelsRepository;
        private ITuiViewModelActionsRepository tuiViewModelActionsRepository;
        private ITuiPermissionsRepository tuiPermissionsRepository;

        public ISession Session { get; protected set; }
        public IUsersRepository UsersRepository => usersRepository ?? (usersRepository = new UsersRepository(this));
        public ITuiViewModelsRepository TuiViewModelsRepository => tuiViewModelsRepository ?? (tuiViewModelsRepository = new TuiViewModelsRepository(this));
        public ITuiViewModelActionsRepository TuiViewModelActionsRepository => tuiViewModelActionsRepository ?? (tuiViewModelActionsRepository = new TuiViewModelActionsRepository(this));
        public ITuiPermissionsRepository TuiPermissionsRepository => tuiPermissionsRepository ?? (tuiPermissionsRepository = new TuiPermissionsRepository(this));

        public UnitOfWork()
        {
            OpenSession();
        }

        public UnitOfWork(ISession session)
        {
            Session = session;
        }

        #region Public Methods

        public void BeginTransaction()
        {
            transaction = Session.BeginTransaction();
        }

        public void CommitTransaction()
        {
            transaction.Commit(); ;
            CloseTransaction(); ;
        }

        public void RollbackTransaction()
        {
            transaction.Rollback(); ;

            CloseTransaction();
            CloseSession();
        }

        public void OpenSession()
        {
            Session = NHibernateHelper.OpenSession();
        }

        public void CloseSession()
        {
            Session.Close();
            Session.Dispose();
            Session = null;
        }

        public void Dispose()
        {
            if (transaction != null)
            {
                CommitTransaction();
            }

            if (Session != null)
            {
                Session.Flush();
                CloseSession(); ;
            }
        }

        #endregion

        #region Private Methods

        private void CloseTransaction()
        {
            transaction.Dispose();
            transaction = null;
        }

        #endregion
    }
}