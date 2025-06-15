using MiraGames.Server.Database.Contexts;
using MiraGames.Server.Entities;
using MiraGames.Server.Entities.Enums;
using MiraGames.Server.Interfaces;
using MiraGames.Server.Services;

using PrintToConsoleDebug = System.Diagnostics.Debug;

namespace MiraGames.Server.Database.Scripts
{
    public static class InitPgScript
    {
        public static void Init(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var contex = scope.ServiceProvider.GetRequiredService<PostgresContext>();
            var token = scope.ServiceProvider.GetRequiredService<ITokenService<User>>();

            if (contex.Users.Any() || contex.Rates.Any() || contex.Payments.Any())
                return;

            var users = new User[3];

            for (int i = 0; i < users.Length; i++)
                users[i] = new User($"admin{i}", $"admin{i}_123");

            for (int i = 0; i < 3; i++)
                users.First().AddPayment(
                    i * 100, Currencies.Ruble, PaymentStatuses.Paid
                );

            for (int i = 0; i < 2; i++)
                users.Last().AddPayment(
                    i * 50, Currencies.Ruble, PaymentStatuses.Paid
                );

            var rate = new Rate(Currencies.Ruble, 10);

            contex.Users.AddRange(users);
            contex.Rates.Add(rate);

            foreach (var user in users)
            {
                var key = token.GenerateToken(user);

                PrintToConsoleDebug.WriteLine($"Token for {user.Email}:\n{key}");
            } 

            contex.SaveChanges();
        }
    }
}
