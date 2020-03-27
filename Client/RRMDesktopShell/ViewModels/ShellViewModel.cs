using Caliburn.Micro;
using RRMDesktopShell.Events;

namespace RRMDesktopShell.ViewModels
{
    public class ShellViewModel : Conductor<object>,IHandle<LoggedInEvent>
    {

        private readonly SalesViewModel _salesViewModel;
        private readonly IEventAggregator _eventAggregator;

        public ShellViewModel(SalesViewModel salesViewModel,IEventAggregator eventAggregator)
        {
            _salesViewModel = salesViewModel;
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
            ActivateItem(IoC.Get<LoginViewModel>());
        }

        public void Handle(LoggedInEvent message)
        {
           ActivateItem(_salesViewModel); 
        }
    }
}