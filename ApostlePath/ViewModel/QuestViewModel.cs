using ApostlePath.DataAccess.Model;
using ApostlePath.DataAccess.Repository;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ApostlePath.ViewModel
{
    [QueryProperty(nameof(Id), "Id")]
    [QueryProperty(nameof(Title), "Title")]
    [QueryProperty(nameof(Level), "Level")]
    [QueryProperty(nameof(Experience), "Experience")]
    [QueryProperty(nameof(Challenge), "Challenge")]
    [QueryProperty(nameof(LastProgress), "LastProgress")]
    public class QuestViewModel : ObservableObject
    {
        private readonly IQuestsRepository _questsRepository;

        public IAsyncRelayCommand MarkAsDoneCommand { get; set; }

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


        private int _experience;
        public int Experience
        {
            get { return _experience; }
            set
            {
                _experience = value;
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


        private DateTime _lastProgress;
        public DateTime LastProgress
        {
            get { return _lastProgress; }
            set
            {
                _lastProgress = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsMarkAsDoneButtonVisible));
            }
        }

        public bool IsMarkAsDoneButtonVisible => DateTime.UtcNow.Date - LastProgress.Date != TimeSpan.FromDays(0);

        public QuestViewModel(IQuestsRepository questsRepository)
        {
            _questsRepository = questsRepository;

            MarkAsDoneCommand = new AsyncRelayCommand(MarkAsDone);
        }

        private async Task MarkAsDone()
        {
            LastProgress = DateTime.UtcNow.Date;
            Experience += 1;
            if(Experience == 7)
            {
                Level += 1;
                Experience = 0;
            }

            Quest quest = new Quest()
            {
                Id = Id,
                Title = Title,
                Challenge = Challenge,
                Experience = Experience,
                LastProgress = LastProgress,
                Level = Level
            };

            await _questsRepository.UpdateQuest(quest);

            await Shell.Current.GoToAsync("..");
        }
    }
}
