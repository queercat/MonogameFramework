using System.Collections.Generic;
using HelloMonogame.Models.Contracts;
using Microsoft.Xna.Framework;
using IDrawable = HelloMonogame.Models.Contracts.IDrawable;
using IUpdateable = HelloMonogame.Models.Contracts.IUpdateable;

namespace HelloMonogame.Models;

public class Entity : ILoadable, IDrawable, IUpdateable
{
    public Vector2 Position { get; set; }
    public float Depth { get; set; } = 0.0f;

    private readonly List<IDrawable> _drawables = [];
    private readonly List<IUpdateable> _updatables = [];
    private readonly List<ILoadable> _loadables = [];

    private bool _isInitialized = false;
    
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

    public virtual void Update(GameTime gameTime, List<Entity> entities)
    {
        if (!_isInitialized)
            Initialize(entities);
        
        foreach (var updatable in _updatables)
        {
            updatable.Update(gameTime, entities);
        }
    }

    public virtual void Initialize(List<Entity> entities)
    {
        if (_isInitialized)
            return;
        
        _isInitialized = true;
    }
}