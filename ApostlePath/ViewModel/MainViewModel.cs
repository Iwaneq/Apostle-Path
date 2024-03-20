using ApostlePath.DataAccess.Repository;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace ApostlePath.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        private readonly IQuestsRepository _questsRepository;

        private ObservableCollection<CompactQuestViewModel> _quests = new ObservableCollection<CompactQuestViewModel>();
        public ObservableCollection<CompactQuestViewModel> Quests
        {
            get { return _quests; }
            set
            {
                _quests = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel(IQuestsRepository questsRepository)
        {
            _questsRepository = questsRepository;

            LoadQuests();
        }

        private void LoadQuests()
        {
            var quests = _questsRepository.GetQuests();

            foreach(var q  in quests)
            {
                Quests.Add(new CompactQuestViewModel()
                {
                    Title = q.Title,
                    Level = q.Level,
                    Progress = q.Experience / 7
                });
            }

            //OnPropertyChanged(nameof(Quests));
        }
    }
}
