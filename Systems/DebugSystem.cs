using System;
using System.Collections.Generic;
using System.Linq;
using HelloMonogame.Entities;
using HelloMonogame.Extensions;
using HelloMonogame.Models;
using HelloMonogame.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HelloMonogame.Systems;

public class DebugSystem(HelloMonogame helloMonogame, SpriteBatch spriteBatch) : Entity
{
    private Camera _camera = null!;
    
    private HelloMonogame _helloMonogame = helloMonogame;
    private SpriteBatch _spriteBatch = spriteBatch;

    public override void Initialize(List<Entity> entities)
    {
        base.Initialize(entities);

        _camera = entities.GetEntity<Camera>();
        var inputSystem = entities.GetEntity<InputSystem>();

        inputSystem.OnMouseClick += (sender, vector2) => { HandleClick(sender, vector2, entities); };
    }

    public override void Update(GameTime gameTime, List<Entity> entities)
    {
        base.Update(gameTime, entities);

        var _player = entities.GetEntity<Character>();
        
    }

    private void HandleClick(object? sender, Vector2 position, List<Entity> entities)
    {
        var instantiatedConveyorBelt = new ConveyorBelt(_helloMonogame, _spriteBatch, position);
        instantiatedConveyorBelt.Id = entities.Count;
        instantiatedConveyorBelt.Depth += (float)instantiatedConveyorBelt.Id / 10;
        
        entities.Add(instantiatedConveyorBelt);
    }
}