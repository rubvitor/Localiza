using Microsoft.AspNetCore.Identity;
using NetDevPack.Domain;

namespace DivisorPrimo.Domain.Models
{
    public class User : IdentityUser, IAggregateRoot
    {
        public string Name { get; set; }
    }
}
