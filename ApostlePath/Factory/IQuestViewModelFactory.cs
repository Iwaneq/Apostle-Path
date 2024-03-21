using ApostlePath.ViewModel;

namespace ApostlePath.Factory
{
    public interface IQuestViewModelFactory
    {
        public CompactQuestViewModel CreateCompactQuestViewModel(int id, string title, int level, decimal progress);
    }
}
