using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DivisorPrimo.Infra.Data.Context;
using DevExpress.Data.ODataLinq.Helpers;
using DivisorPrimo.Domain.Interfaces;
using DivisorPrimo.Domain.Models;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;

namespace DivisorPrimo.Infra.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        protected readonly DivisorPrimoContext Db;
        protected readonly DbSet<User> DbSet;

        public UserRepository(DivisorPrimoContext context)
        {
            Db = context;
            DbSet = Db.Set<User>();
        }

        public IUnitOfWork UnitOfWork => Db;

        public async Task<User> GetById(string id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<User> GetByUsername(string username)
        {
            return await DbSet.AsNoTracking().Where(u => u.UserName.Equals(username)).FirstOrDefaultAsync();
        }

        public async Task<User> GetByEmail(string email)
        {
            return await DbSet.AsNoTracking().Where(u => u.Email.Equals(email)).FirstOrDefaultAsync();
        }

        public async Task<User> GetByEmailOrUsername(string emailuser)
        {
            return await DbSet.AsNoTracking().Where(u => u.Email.Equals(emailuser) || u.UserName.Equals(emailuser)).FirstOrDefaultAsync();
        }

        public async Task<bool> ValidatePassword(string emailOrusername, string password)
        {
            return await DbSet.AsNoTracking().AnyAsync(u => (u.Email.Equals(emailOrusername)
                                                          || u.UserName.Equals(emailOrusername))
                                                           && u.PasswordHash.Equals(password));
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public void Add(User User)
        {
            DbSet.Add(User);
        }

        public void Update(User User)
        {
            DbSet.Update(User);
        }

        public void Remove(User User)
        {
            DbSet.Remove(User);
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}
