using System;
using Godot;

namespace gtfo_demons.Gameplay;

public static class GameplayFactory
{
    public static IGameplayManager GetGameplayManagerOrDefault(SceneTree getTree)
    {
        
        return getTree.Root.GetChild(0).GetChild<IGameplayManager>(0);
    }
}