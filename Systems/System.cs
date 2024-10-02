using System;
using System.Collections.Generic;
using System.Linq;
using HelloMonogame.Enums;
using HelloMonogame.Models.Contracts;
using Microsoft.Xna.Framework;
using IUpdateable = HelloMonogame.Models.Contracts.IUpdateable;

namespace HelloMonogame.Systems;

public abstract class System : IUpdateable
{
    private Dictionary<MessageType, Delegate> _listens = new();
    public virtual void Update(GameTime gameTime, Dictionary<MessageType, object> messages)
    {
        foreach (var message in messages)
            if (_listens.TryGetValue(message.Key, out var value))
                value.DynamicInvoke(message.Key, message.Value);
    }

    protected void Register(MessageType messageType, Delegate function)
    {
        _listens[messageType] = function;
    }
}