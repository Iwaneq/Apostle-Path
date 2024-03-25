using ApostlePath.View;

namespace ApostlePath
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("QuestPage", typeof(QuestPage));
            Routing.RegisterRoute("LevelsInfoPage", typeof(LevelsInfoPage));
            Routing.RegisterRoute("CreateQuestPage", typeof(CreateQuestPage));
            Routing.RegisterRoute("AskForName", typeof(AskForNameView));
        }
    }
}
