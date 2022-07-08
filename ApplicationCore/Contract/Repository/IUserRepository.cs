using ApplicationCore.Entity;
using ApplicationCore.Model;

namespace ApplicationCore.Contract.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByEmail(string email);
        
    }
}
