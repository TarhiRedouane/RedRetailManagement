using System.Collections.Generic;
using Caliburn.Micro;
using RRMCustomControls.ViewModels;

namespace RRMCustomControls.Services
{
    public class DialogService : IDialogService
    {
        private readonly StatusInfoViewModel _statusInfoViewModel;
        private readonly IWindowManager _windowManager;

        public DialogService(StatusInfoViewModel statusInfoViewModel, IWindowManager windowManager)
        {
            _statusInfoViewModel = statusInfoViewModel;
            _windowManager = windowManager;
        }
        public void Message(string title,string header, string message)
        {
            var settings = new Dictionary<string, object> {{"Title", title}};
            _statusInfoViewModel.UpdateMessage(header, message);
            _windowManager.ShowDialog(_statusInfoViewModel, null, settings);
        }
    }
}
