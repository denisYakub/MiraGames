using Microsoft.EntityFrameworkCore;
using MiraGames.Server.Database.Contexts;
using MiraGames.Server.Entities;
using MiraGames.Server.Interfaces;

namespace MiraGames.Server.Database.Repositories
{
    public class UserRepository(PostgresContext context) : IRepository<User>
    {
        public void Add(User entity) =>
            context.Users.Add(entity);

        public User Get(int id)
        {
            throw new NotImplementedException();
        }

        public User Get(Guid id)
        {
            var user = context.Users.FirstOrDefault(u => u.Id == id);

            return user ?? throw new BadHttpRequestException($"No user with this id: {id}");
        }

        public IEnumerable<User> GetAll() =>
            context.Users.Include(u => u.Payments);

        public void Save() =>
            context.SaveChanges();

        public void Update(User entity) =>
            context.Users.Update(entity);
    }
}
