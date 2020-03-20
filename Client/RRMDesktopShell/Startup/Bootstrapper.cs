using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Autofac;
using Caliburn.Micro;
using RRMDesktopShell.AttachedProperties;
using RRMDesktopShell.Helpers;
using RRMDesktopShell.ViewModels;

namespace RRMDesktopShell.Startup
{
    public class Bootstrapper : BootstrapperBase
    {

        private static IContainer Container;

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

            GetType().Assembly.GetTypes()
                .Where(type => type.IsClass)
                .Where(type => type.Name.EndsWith("ViewModel"))
                .ToList()
                .ForEach(viewModelType => { builder.RegisterType(viewModelType).AsSelf(); });

            Container = builder.Build();
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            var type = typeof(IEnumerable<>).MakeGenericType(service);
            return Container.Resolve(type) as IEnumerable<object>;
        }

        protected override object GetInstance(Type service, string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                if (Container.IsRegistered(service))
                    return Container.Resolve(service);
            }
            else
            {
                if (Container.IsRegisteredWithKey(key, service))
                    return Container.ResolveKeyed(key, service);
            }

            var msgFormat = "Could not locate any instances of contract {0}.";
            var msg = string.Format(msgFormat, key ?? service.Name);
            throw new Exception(msg);
        }

        protected override void BuildUp(object instance)
        {
            Container.InjectProperties(instance);
        }

    }
}