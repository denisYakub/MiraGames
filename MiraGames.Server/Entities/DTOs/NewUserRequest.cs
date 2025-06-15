namespace MiraGames.Server.Entities.DTOs
{
    public record struct NewUserRequest(
        string Email, 
        string Password
    );
}
