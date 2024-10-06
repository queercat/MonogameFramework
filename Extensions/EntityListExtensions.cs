using System;
using System.Collections.Generic;
using System.Linq;
using HelloMonogame.Models;

namespace HelloMonogame.Extensions;

public static class EntityListExtensions
{
    public static T GetEntity<T>(this List<Entity> entities) where T : Entity
    {
        var entity = entities.First(e => e is T) as T;
        
        if (entity == null)
            throw new Exception($"Entity of type {typeof(T)} not found.");
        
        return entity;
    }
}