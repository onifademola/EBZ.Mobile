using Autofac;
using EBZ.Mobile.Services;
using EBZ.Mobile.ServicesInterface;
using System;

namespace EBZ.Mobile.AppStart
{
    public class AppContainer
    {
        private static IContainer _container;


        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            //ViewModels
            //builder.RegisterType<LoginViewModel>();

            //services - data

            //services - general
            builder.RegisterType<ConnectionService>().As<IConnectionService>();
            builder.RegisterType<CustomerDataService>().As<ICustomerDataService>();
            builder.RegisterType<SalesDataService>().As<ISalesDataService>();
            builder.RegisterType<NavigationService>().As<INavigationService>();
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<ValidationService>().As<IValidationService>();
            builder.RegisterType<AuthenticationService>().As<IAuthenticationService>();
            builder.RegisterType<DialogService>().As<IDialogService>();
            builder.RegisterType<SettingsService>().As<ISettingsService>().SingleInstance();

            //General
            builder.RegisterType<GenericService>().As<IGenericService>();

            _container = builder.Build();
        }


        public static object Resolve(Type typeName)
        {
            return _container.Resolve(typeName);
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}
