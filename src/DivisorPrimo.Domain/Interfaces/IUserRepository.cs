using DivisorPrimo.Domain.Models;
using NetDevPack.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DivisorPrimo.Domain.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        void Add(User User);
        Task<IEnumerable<User>> GetAll();
        Task<User> GetByEmail(string email);
        Task<User> GetByEmailOrUsername(string emailuser);
        Task<User> GetById(string id);
        Task<User> GetByUsername(string username);
        void Remove(User User);
        void Update(User User);
        Task<bool> ValidatePassword(string emailOrusername, string password);
    }
}