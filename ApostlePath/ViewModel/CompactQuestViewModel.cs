using ApostlePath.DataAccess.Repository;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ApostlePath.ViewModel
{
    public class CompactQuestViewModel : ObservableObject
    {
        private readonly IQuestsRepository _questsRepository;

        public IAsyncRelayCommand NavigateToQuestPageCommand { get; set; }

        public int Id { get; set; }


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

        public CompactQuestViewModel(IQuestsRepository questsRepository)
        {
            NavigateToQuestPageCommand = new AsyncRelayCommand(NavigateToQuestPage);
            _questsRepository = questsRepository;
        }

        private async Task NavigateToQuestPage()
        {
            var quest = await _questsRepository.GetQuest(Id);
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "Id", quest.Id },
                { "Title", quest.Title },
                { "Level", quest.Level },
                { "Experience", quest.Experience },
                { "Challenge", quest.Challenge},
                { "LastProgress", quest.LastProgress }
            };

            await Shell.Current.GoToAsync("QuestPage", parameters);
        }
    }
}
