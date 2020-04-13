using Caliburn.Micro;

namespace RRMCustomControls.ViewModels
{
    //           StatusInfoView
    public class StatusInfoViewModel : Screen
    {
        public string Message { get; set; }
        public string Header { get; set; }
        public void UpdateMessage(string header, string message)
        {
            Header = header;
            Message = message;

            NotifyOfPropertyChange(() => Header);
            NotifyOfPropertyChange(() => Message);
        }

        public StatusInfoViewModel()
        {

        }

        public void Close()
        {
            TryClose();
        }
    }
}
