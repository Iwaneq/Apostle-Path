using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ApostlePath.ViewModel
{
    public class CompactQuestViewModel : ObservableObject
    {
        public IAsyncRelayCommand NavigateToQuestPageCommand { get; set; }


        private string _title = string.Empty;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }


        private int _level;
        public int Level
        {
            get { return _level; }
            set
            {
                _level = value;
                OnPropertyChanged();
            }
        }


        private decimal _progress;
        public decimal Progress
        {
            get { return _progress; }
            set
            {
                _progress = value;
                OnPropertyChanged();
            }
        }

        public CompactQuestViewModel()
        {
            NavigateToQuestPageCommand = new AsyncRelayCommand(NavigateToQuestPage);
        }

        private async Task NavigateToQuestPage()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "Title", Title },
                { "Level", Level },
                { "Experience", 3 },
                { "Challenge", "TEST" },
                { "LastProgress", DateTime.UtcNow.AddDays(-2).Date }
            };

            await Shell.Current.GoToAsync("QuestPage", parameters);
        }
    }
}
