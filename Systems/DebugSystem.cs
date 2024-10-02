using System;
using System.Collections.Generic;
using HelloMonogame.Enums;

namespace HelloMonogame.Systems;

public class DebugSystem : System
{
    public DebugSystem()
    {
        Register(MessageType.InputDown, PrintKeyboard);
        Register(MessageType.InputRight, PrintKeyboard);
        Register(MessageType.InputDown, PrintKeyboard);
        Register(MessageType.InputLeft, PrintKeyboard);
    }

    private static void PrintKeyboard(MessageType messageType, object value)
    {
        Console.WriteLine($"This works! {messageType.ToString()} with value ${value} gotten!");
    }
}