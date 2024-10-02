using System;
using System.Collections.Generic;
using System.Linq;
using HelloMonogame.Enums;
using HelloMonogame.Models.Contracts;
using Microsoft.Xna.Framework;

namespace HelloMonogame.Systems;

public abstract class System : IUpdatable
{
    private Dictionary<MessageType, Delegate> _listens = new();
    public virtual void Update(GameTime gameTime, Dictionary<MessageType, object> messages)
    {
        foreach (var message in messages)
            if (_listens.TryGetValue(message.Key, out var value))
                value.DynamicInvoke(message.Key, message);
    }

    public void Register(MessageType messageType, Delegate function)
    {
        _listens[messageType] = function;
    }
}