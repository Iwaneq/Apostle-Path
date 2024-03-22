using ApostlePath.DataAccess.Dictionary;
using ApostlePath.DataAccess.Repository;
using ApostlePath.Factory;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace ApostlePath.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        private readonly IQuestsRepository _questsRepository;
        private readonly IQuestViewModelFactory _questViewModelFactory;

        public IRelayCommand ReloadQuestsCommand { get; set; }

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

        public int DisciplineLevel => Quests.Sum(x => x.Level);
        public string DisciplineTitle => GetDisciplineTitle();

        public MainViewModel(IQuestsRepository questsRepository, IQuestViewModelFactory questViewModelFactory)
        {
            _questsRepository = questsRepository;
            _questViewModelFactory = questViewModelFactory;

            ReloadQuestsCommand = new RelayCommand(LoadQuests);
        }

        private void LoadQuests()
        {
            var quests = _questsRepository.GetQuests().ToList();

            Quests.Clear();

            foreach(var q  in quests)
            {
                Quests.Add(_questViewModelFactory.CreateCompactQuestViewModel(q.Id, q.Title, q.Level, q.Experience / 7m));
            }

            OnPropertyChanged(nameof(DisciplineLevel));
            OnPropertyChanged(nameof(DisciplineTitle));
        }

        private string GetDisciplineTitle()
        {
            return DisciplineTitleDictionary.DisciplineTitles.Where(x => x.Key <= DisciplineLevel).LastOrDefault().Value;
        }
    }
}
