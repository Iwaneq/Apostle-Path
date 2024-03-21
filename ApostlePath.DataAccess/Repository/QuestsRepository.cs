using ApostlePath.DataAccess.Data;
using ApostlePath.DataAccess.Model;
using Microsoft.EntityFrameworkCore;

namespace ApostlePath.DataAccess.Repository
{
    public class QuestsRepository : IQuestsRepository
    {
        private readonly DataContext _dataContext;

        public QuestsRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task CreateQuest(Quest quest)
        {
            _dataContext.Quests.Add(quest);
            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteQuest(int id)
        {
            var questToDelete = await _dataContext.Quests.Where(quest => quest.Id == id).FirstOrDefaultAsync();

            if(questToDelete != null)
            {
                _dataContext.Quests.Remove(questToDelete);
            }

            await _dataContext.SaveChangesAsync();
        }

        public async Task<Quest> GetQuest(int id)
        {
            return await _dataContext.Quests.Where(quest => quest.Id == id).FirstOrDefaultAsync() ?? throw new KeyNotFoundException();
        }

        public IEnumerable<Quest> GetQuests()
        {
            return _dataContext.Quests;
        }

        public async Task UpdateQuest(Quest quest)
        {
            _dataContext.Update(quest);
            await _dataContext.SaveChangesAsync();
        }
    }
}
