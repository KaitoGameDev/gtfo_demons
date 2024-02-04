using Godot;

namespace gtfo_demons.Gameplay;

public static class GameplayFactory
{
    private static IGameplayManager _gameplayManager;

    public static IGameplayManager GetPlayerManager() => _gameplayManager ??= new DefaultGameplayManager();
}