using ApostlePath.DataAccess.Repository;
using ApostlePath.Factory;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace ApostlePath.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        private readonly IQuestsRepository _questsRepository;
        private readonly IQuestViewModelFactory _questViewModelFactory;

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

        public MainViewModel(IQuestsRepository questsRepository, IQuestViewModelFactory questViewModelFactory)
        {
            _questsRepository = questsRepository;
            _questViewModelFactory = questViewModelFactory;

            LoadQuests();
        }

        private void LoadQuests()
        {
            var quests = _questsRepository.GetQuests().ToList();

            foreach(var q  in quests)
            {
                Quests.Add(_questViewModelFactory.CreateCompactQuestViewModel(q.Id, q.Title, q.Level, q.Experience / 7m));
            }
        }
    }
}
