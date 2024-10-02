using System.Collections.Generic;
using HelloMonogame.Enums;
using HelloMonogame.Models.Contracts;
using Microsoft.Xna.Framework;

namespace HelloMonogame.Models;

public class Entity : ILoadable, IDrawable, IUpdatable
{
    public Vector2 Position { get; set; }
    
    public readonly List<IDrawable> _drawables = [];
    public readonly List<IUpdatable> _updatables = [];
    public readonly List<ILoadable> _loadables = [];

    public void AddUpdatable(IUpdatable updatable)
    {
        _updatables.Add(updatable);
    }
    
    public void AddLoadable(ILoadable loadable)
    {
        _loadables.Add(loadable);
    }
    
    public void AddDrawable(IDrawable drawable)
    {
        _drawables.Add(drawable);
    }
    
    public void Add(object obj)
    {
        if (obj is IUpdatable updatable)
            AddUpdatable(updatable);
        if (obj is ILoadable loadable)
            AddLoadable(loadable);
        if (obj is IDrawable drawable)
            AddDrawable(drawable);
    }
    
    public void Load()
    {
        foreach (var loadable in _loadables)
        {
            loadable.Load();
        }
    }

    public void Draw()
    {
        foreach (var drawable in _drawables)
        {
            drawable.Draw();
        }
    }

    public virtual void Update(GameTime gameTime, Dictionary<MessageType, object> messages)
    {
        foreach (var updatable in _updatables)
        {
            updatable.Update(gameTime, messages);
        }
    }
}