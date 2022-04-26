using System;
using System.Data;
using System.Runtime.CompilerServices;
using NHibernate;
using NHibernate.Cfg;
using Test.WPF.UI.Data.Models;

namespace Test.WPF.UI.Data
{
    public static class NHibernateHelper
    {
        private static Configuration configuration;
        private static ISessionFactory sessionFactory;

        public static ISessionFactory SessionFactory
        {
            get => sessionFactory;
        }

        public static ISession OpenSession()
        {
            return sessionFactory.OpenSession();
        }

        public static void Configure(Configuration conf)
        {
            configuration = conf;
            sessionFactory = conf.BuildSessionFactory();
        }
    }
}
