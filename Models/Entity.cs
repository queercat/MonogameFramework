using System.Collections.Generic;
using HelloMonogame.Enums;
using HelloMonogame.Models.Contracts;
using Microsoft.Xna.Framework;
using IUpdateable = HelloMonogame.Models.Contracts.IUpdateable;

namespace HelloMonogame.Models;

public class Entity : ILoadable, IDrawable, IUpdateable
{
    public Vector2 Position { get; set; }

    private readonly List<IDrawable> _drawables = [];
    private readonly List<IUpdateable> _updatables = [];
    private readonly List<ILoadable> _loadables = [];

    public void AddUpdatable(IUpdateable updateable)
    {
        _updatables.Add(updateable);
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
        if (obj is IUpdateable updatable)
            AddUpdatable(updatable);
        if (obj is ILoadable loadable)
            AddLoadable(loadable);
        if (obj is IDrawable drawable)
            AddDrawable(drawable);
    }
    
    public virtual void Load()
    {
        foreach (var loadable in _loadables)
        {
            loadable.Load();
        }
    }

    public virtual void Draw()
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