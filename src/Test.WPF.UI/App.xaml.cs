using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Test.WPF.UI.Data;
using Test.WPF.UI.Data.Models;
using Test.WPF.UI.Data.Repositories;

namespace Test.WPF.UI
{
    public partial class App : Application
    {
        App()
        {
            InitializeComponent();

            NHibernateHelper.Configuration = new Configuration()
                    .Configure()
                    .AddAssembly(typeof(User).Assembly)
                ;

            Repository<User> rep = new Repository<User>(new NHibernateHelper());

            rep.Add(null);
        }
    }
}
