using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace ApostlePath.ViewModel
{
    public class MainViewModel : ObservableObject
    {

        private ObservableCollection<CompactQuestViewModel> _quests = new ObservableCollection<CompactQuestViewModel>()
        {
            new CompactQuestViewModel()
            {
                Level = 1,
                Title = "Cold Shower",
                Progress = 0.4m
            },
            new CompactQuestViewModel()
            {
                Level = 2,
                Title = "Reading Books",
                Progress = 0.6m
            },
            new CompactQuestViewModel()
            {
                Level = 5,
                Title = "Gym",
                Progress = 0.1m
            }
        };
        public ObservableCollection<CompactQuestViewModel> Quests
        {
            get { return _quests; }
            set
            {
                _quests = value;
                OnPropertyChanged();
            }
        }
    }
}
