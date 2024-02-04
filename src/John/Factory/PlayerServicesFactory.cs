using gtfo_demons.John.Domain;
using gtfo_demons.John.Infrastructure;

namespace gtfo_demons.John.Factory;

public static class PlayerServicesFactory
{
    private static IPlayerInteractions _playerInteractions;

    public static IPlayerInteractions GetPlayerInteractions() => _playerInteractions ??= new PlayerInteractions();
}