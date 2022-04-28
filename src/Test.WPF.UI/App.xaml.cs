using NHibernate.Cfg;
using System.Windows;
using Test.WPF.UI.Data;
using Test.WPF.UI.Data.Models;

namespace Test.WPF.UI
{
    public partial class App : Application
    {
        App()
        {
            InitializeComponent();

            Configuration configuration = new Configuration()
                    .Configure()
                    .AddAssembly(typeof(User).Assembly)
                ;

            NHibernateHelper.Configure(configuration);
        }

        
    }
}
