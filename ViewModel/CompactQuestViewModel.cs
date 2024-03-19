using CommunityToolkit.Mvvm.ComponentModel;

namespace ApostlePath.ViewModel
{
    public class CompactQuestViewModel : ObservableObject
    {

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
    }
}
