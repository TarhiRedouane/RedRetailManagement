using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Autofac;
using Caliburn.Micro;
using RRMCustomControls.AttachedProperties;
using RRMCustomControls.Services;
using RRMDesktopShell.Helpers;
using RRMDesktopShell.Library.Api;
using RRMDesktopShell.Library.Models;
using RRMDesktopShell.ViewModels;

namespace RRMDesktopShell.Startup
{
    public class Bootstrapper : BootstrapperBase
    {

        private static IContainer _container;

        public Bootstrapper()
        {
            Initialize();

            ConventionManager.AddElementConvention<PasswordBox>(
                BoundPasswordProperty.ValueProperty, "Password", "PasswordChanged");
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        protected override void Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<WindowManager>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<EventAggregator>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<ApiHelper>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<LoggedInUserModel>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<ConfigHelper>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<ProductApi>()
                .AsImplementedInterfaces()
                .InstancePerDependency();

            builder.RegisterType<SaleApi>()
                .AsImplementedInterfaces()
                .InstancePerDependency();

            builder.RegisterType<DialogService>()
                .AsImplementedInterfaces()
                .InstancePerDependency();

            builder.RegisterModule(new AutoMapperModule());

            var assemblies = Loader.CreateAssemblies(GetType().Assembly);

            foreach (var assembly in assemblies)
            {
                assembly.GetTypes()
                    .Where(type => type.IsClass)
                    .Where(type => type.Name.EndsWith("ViewModel"))
                    .ToList()
                    .ForEach(viewModelType => { builder.RegisterType(viewModelType).AsSelf(); });
            }
            _container = builder.Build();
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            var type = typeof(IEnumerable<>).MakeGenericType(service);
            return _container.Resolve(type) as IEnumerable<object>;
        }

        protected override object GetInstance(Type service, string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                if (_container.IsRegistered(service))
                    return _container.Resolve(service);
            }
            else
            {
                if (_container.IsRegisteredWithKey(key, service))
                    return _container.ResolveKeyed(key, service);
            }

            var msgFormat = "Could not locate any instances of contract {0}.";
            var msg = string.Format(msgFormat, key ?? service.Name);
            throw new Exception(msg);
        }

        protected override void BuildUp(object instance)
        {
            _container.InjectProperties(instance);
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            return Loader.CreateAssemblies(GetType().Assembly);
        }
    }
}