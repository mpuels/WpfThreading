using Prism.Unity;
using Microsoft.Practices.Unity;
using System.Configuration;
using System.Windows;
using WpfThreading.Views;
using WpfThreading.Db;
using WpfThreading.Reports;
using WpfThreading.Services;

namespace WpfThreading
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            //Container.RegisterInstance<IDbGateway>(new DbGatewayDemo());
            Container.RegisterInstance<IDbGateway>(
                new DbGatewayPgSql(
                    ConfigurationManager
                        .ConnectionStrings["TOPODATA"]
                        .ConnectionString));

            Container.RegisterType<IReportGenerator, ReportGenerator>(
                new InjectionConstructor(
                    Container.Resolve<IDbGateway>()));
        }
    }
}
