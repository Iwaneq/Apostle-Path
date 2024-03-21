using ApostlePath.DataAccess.Model;

namespace ApostlePath.DataAccess.Repository
{
    public interface IQuestsRepository
    {
        IEnumerable<Quest> GetQuests();
        Task<Quest> GetQuest(int id);
        Task UpdateQuest(Quest quest);
        Task CreateQuest(Quest quest);
        Task DeleteQuest(int id);
    }
}
