using ApostlePath.DataAccess.Repository;

namespace ApostlePath.Services
{
    public class QuestProgressChecker : IQuestProgressChecker
    {
        private readonly IQuestsRepository _questsRepository;

        public QuestProgressChecker(IQuestsRepository questsRepository)
        {
            _questsRepository = questsRepository;
        }

        public async Task CheckProgress()
        {
            var quests = _questsRepository.GetQuests().ToList();

            foreach(var quest in quests)
            {
                if((DateTime.UtcNow.Date - quest.LastProgress.Date).Days > 1)
                {
                    if(quest.LastProgress.Date == quest.LastChecked.Date)
                    {
                        quest.Experience -= (DateTime.UtcNow.Date - quest.LastChecked.Date).Days - 1;
                    }
                    else
                    {
                        quest.Experience -= (DateTime.UtcNow.Date - quest.LastChecked.Date).Days;   
                    }
                    
                    while(quest.Experience < 0)
                    {
                        if(quest.Level == 0)
                        {
                            if(quest.Experience < 0) quest.Experience = 0;
                            break;
                        }
                        quest.Level--;
                        quest.Experience = 7 + quest.Experience;
                    }

                    quest.LastChecked = DateTime.UtcNow.Date;

                    await _questsRepository.UpdateQuest(quest);
                }
            }
        }
    }
}
