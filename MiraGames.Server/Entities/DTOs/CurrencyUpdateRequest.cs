using MiraGames.Server.Entities.Enums;

namespace MiraGames.Server.Entities.DTOs
{
    public record struct CurrencyUpdateRequest(
        Currencies Currency, 
        decimal NewValue
    );
}
