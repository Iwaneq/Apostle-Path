using ApostlePath.DataAccess.Model;
using ApostlePath.DataAccess.Repository;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ApostlePath.ViewModel
{
    public class CreateQuestViewModel : ObservableObject
    {
        private readonly IQuestsRepository _questsRepository;

        public IAsyncRelayCommand CreateQuestCommand { get; set; }

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


        private string _challenge = string.Empty;
        public string Challenge
        {
            get { return _challenge; }
            set
            {
                _challenge = value;
                OnPropertyChanged();
            }
        }

        public CreateQuestViewModel(IQuestsRepository questsRepository)
        {
            _questsRepository = questsRepository;

            CreateQuestCommand = new AsyncRelayCommand(CreateQuest);
        }

        private async Task CreateQuest()
        {
            Quest quest = new Quest()
            {
                Title = Title,
                Challenge = Challenge,
                Level = 0,
                Experience = 0
            };

            await _questsRepository.CreateQuest(quest);

            await Shell.Current.GoToAsync("..");
        }
    }
}
