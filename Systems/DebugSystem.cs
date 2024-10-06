using System;
using System.Collections.Generic;
using HelloMonogame.Extensions;
using HelloMonogame.Models;
using HelloMonogame.Utilities;
using Microsoft.Xna.Framework;

namespace HelloMonogame.Systems;

public class DebugSystem : Entity
{
    private Camera _camera = null!;
    
    public override void Initialize(List<Entity> entities)
    {
        base.Initialize(entities);

        _camera = entities.GetEntity<Camera>();
        var inputSystem = entities.GetEntity<InputSystem>();

        inputSystem.OnMouseClick += HandleClick;
    }

    private void HandleClick(object? sender, Vector2 position)
    {
        Console.WriteLine(WorldUtilities.ScreenToWorldCoordinates(position + _camera.Position));
    }
}