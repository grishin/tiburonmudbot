using System.Collections.Generic;
using TiburonMUD.Engine.Models;

namespace TiburonMUD.Engine
{
    public class WorldBuilder
    {
        public World Build()
        {
            var world = new World
            {
                Rooms = new List<Room>
                {
                    new Room
                    {
                        Id = "R_Entrance",
                        Name = "Entrance",
                        Description = "You are in front of a white house. There is a glass door to the west.",
                        Exits = new Dictionary<Direction, string>
                        {
                            {Direction.West, "R_Stairs"}
                        }
                    },
                    new Room
                    {
                        Id = "R_Stairs",
                        Name = "Stairs",
                        Description = "Stairs lead down here. There is a glass door to the east.",
                        Exits = new Dictionary<Direction, string>
                        {
                            {Direction.East, "R_Entrance"}
                        }
                    }
                }
            };


            return world;
        }
    }
}
