using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ApostlePath.ViewModel
{
    public class AskForNameViewModel : ObservableObject
    {
        public IAsyncRelayCommand SetNameCommand { get; set; }

        private string _name = string.Empty;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public AskForNameViewModel()
        {
            SetNameCommand = new AsyncRelayCommand(SetName);
        }

        private async Task SetName()
        {
            Preferences.Set("Name", Name);
            await Shell.Current.GoToAsync("..");
        }
    }
}
