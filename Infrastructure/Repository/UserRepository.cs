using ApplicationCore.Contract.Repository;
using ApplicationCore.Entity;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Model;

namespace Infrastructure.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {

        public UserRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<User> GetById(int id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }


        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }

    }
}
