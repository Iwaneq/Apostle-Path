using ApostlePath.ViewModel;

namespace ApostlePath.Factory
{
    public class QuestViewModelFactory : IQuestViewModelFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public QuestViewModelFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public CompactQuestViewModel CreateCompactQuestViewModel(int id, string title, int level, decimal progress)
        {
            var viewModel = _serviceProvider.GetRequiredService<CompactQuestViewModel>();
            viewModel.Id = id;
            viewModel.Title = title;
            viewModel.Level = level;
            viewModel.Progress = progress;

            return viewModel;
        }
    }
}
