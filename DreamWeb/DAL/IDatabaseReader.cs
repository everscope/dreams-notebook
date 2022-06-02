using DreamWeb.DAL.Entities;

namespace DreamWeb.DAL
{
    public interface IDatabaseReader
    {
        public Task<UserAccount> GetUserAccountByUsernameAsync(string username);
        public Task AddDreamAsync(Dream dream);
        public Task DeleteDreamAsync(string username, string dreamId);
        public Task<Dream> GetDreamByIdAsync(string id);
    }
}
