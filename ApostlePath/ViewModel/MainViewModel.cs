using ApostlePath.DataAccess.Dictionary;
using ApostlePath.DataAccess.Repository;
using ApostlePath.Factory;
using ApostlePath.View;
using CommunityToolkit.Maui.Views;
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
        public IAsyncRelayCommand NavigateToLevelsInfoPageCommand { get; set; }
        public IAsyncRelayCommand NavigateToCreateQuestPageCommand {  get; set; }

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
        public string Name => Preferences.Get("Name", "");
        public string Quote => QuotesDictionary.Quotes.ElementAt(QuoteIndex).Key;
        public string Verse => QuotesDictionary.Quotes.ElementAt(QuoteIndex).Value;

        private int _quoteIndex;
        public int QuoteIndex
        {
            get { return _quoteIndex; }
            set
            {
                _quoteIndex = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Quote));
                OnPropertyChanged(nameof(Verse));
            }
        }

        public MainViewModel(IQuestsRepository questsRepository, IQuestViewModelFactory questViewModelFactory)
        {
            _questsRepository = questsRepository;
            _questViewModelFactory = questViewModelFactory;

            ReloadQuestsCommand = new RelayCommand(LoadQuests);
            NavigateToLevelsInfoPageCommand = new AsyncRelayCommand(NavigateToLevelsInfoPage);
            NavigateToCreateQuestPageCommand = new AsyncRelayCommand(NavigateToCreateQuestPage);

            //Check if name has been already set
            if (!Preferences.Default.ContainsKey("Name"))
            {
                Shell.Current.GoToAsync("/AskForName");
            }

            LoadQuote();
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
            OnPropertyChanged(nameof(Name));
        }

        private void LoadQuote()
        {
            var random = new Random();
            QuoteIndex = random.Next(0, QuotesDictionary.Quotes.Count - 1);
        }

        private string GetDisciplineTitle()
        {
            return DisciplineTitleDictionary.DisciplineTitles.Where(x => x.Key <= DisciplineLevel).LastOrDefault().Value;
        }

        private async Task NavigateToLevelsInfoPage()
        {
            await Shell.Current.GoToAsync("LevelsInfoPage");
        }

        private async Task NavigateToCreateQuestPage()
        {
            await Shell.Current.GoToAsync("CreateQuestPage");
        }
    }
}
