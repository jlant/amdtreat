using AMDTreat.Commands;
using AMDTreat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AMDTreat.ViewModels
{
    public class CustomDialogViewModel : PropertyChangedBase
    {

        private Uri _image;
        public Uri Image
        {
            get { return _image; }
            set
            {
                ChangeAndNotify(ref _image, value, nameof(Image));
            }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                ChangeAndNotify(ref _message, value, nameof(Message));
            }
        }

        private readonly ICommand _closeCommand;
        public ICommand CloseCommand
        {
            get { return _closeCommand; }
        }

        public CustomDialogViewModel(Action<CustomDialogViewModel> closeHandler)
        {
            _closeCommand = new SimpleCommand
            {
                ExecuteDelegate = o => closeHandler(this)
            };
        }
    }
}
