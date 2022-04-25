using System;
using System.Data;
using NHibernate;
using NHibernate.Cfg;
using Test.WPF.UI.Data.Models;

namespace Test.WPF.UI.Data
{
    public class NHibernateHelper : IDisposable
    {
        private readonly ISessionFactory sessionFactory;
        private ISession session;
        private ITransaction transaction;

        public static Configuration Configuration { get; set; }

        public ISession CurrentSession
        {
            get
            {
                if (session != null)
                {
                    return session;
                }

                session = sessionFactory.OpenSession();
                transaction = session.BeginTransaction();

                return session;
            }
        }

        public NHibernateHelper()
        {
            sessionFactory = Configuration.BuildSessionFactory();
        }

        ~NHibernateHelper()
        {
            Dispose();
        }

        public void PerformCommit()
        {
            try
            {
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Dispose()
        {
            if (!sessionFactory.IsClosed)
            {
                sessionFactory.Close();
            }
        }
    }
}
