using NHibernate;

namespace Test.WPF.UI.Data.Repositories.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        protected ITransaction Transaction;

        public ISession Session { get; protected set; }

        public UnitOfWork()
        {
            Session = NHibernateHelper.OpenSession();
        }

        public UnitOfWork(ISession session)
        {
            Session = session;
        }

        #region Public Methods

        public void BeginTransaction()
        {
            Transaction = Session.BeginTransaction();
        }

        public void CommitTransaction()
        {
            Transaction.Commit(); ;
            CloseTransaction(); ;
        }

        public void RollbackTransaction()
        {
            Transaction.Rollback(); ;

            CloseTransaction();
            CloseSession();
        }

        public void Dispose()
        {
            if (Transaction != null)
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
            Transaction.Dispose();
            Transaction = null;
        }

        private void CloseSession()
        {
            Session.Close();
            Session.Dispose();
            Session = null;
        }

        #endregion
    }
}