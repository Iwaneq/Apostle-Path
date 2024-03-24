﻿using ApostlePath.View;

namespace ApostlePath
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("QuestPage", typeof(QuestPage));
            Routing.RegisterRoute("LevelsInfoPage", typeof(LevelsInfoPage));
        }
    }
}
