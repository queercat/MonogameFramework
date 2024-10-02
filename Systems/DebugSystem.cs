using System;
using System.Collections.Generic;
using HelloMonogame.Enums;
using Microsoft.Xna.Framework;

namespace HelloMonogame.Systems;

public class DebugSystem : System
{
    public DebugSystem()
    {
        Register(MessageType.InputDown, PrintKeyboard);
        Register(MessageType.InputRight, PrintKeyboard);
        Register(MessageType.InputDown, PrintKeyboard);
        Register(MessageType.InputLeft, PrintKeyboard);
        Register(MessageType.MousePosition, PrintMouse);
    }

    private static void PrintKeyboard(MessageType messageType, bool value)
    {
        // Console.WriteLine($"This works! {messageType.ToString()} with value ${value} gotten!");
    }

    private static void PrintMouse(MessageType messageType, Point value)
    {
        Console.WriteLine(value);
    }
}