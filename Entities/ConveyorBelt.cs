using System.Numerics;
using HelloMonogame.Models;
using HelloMonogame.Utilities;

namespace HelloMonogame.Entities;

public class ConveyorBelt : Entity
{
    public ConveyorBelt(Vector2 screenPosition)
    {
        Position = WorldUtilities.ScreenToWorldCoordinates(screenPosition);
    }
}