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
                        Description = "Stairs lead down here. There is a glass door to the east that leads outside.",
                        Exits = new Dictionary<Direction, string>
                        {
                            {Direction.East, "R_Entrance"},
                            {Direction.Up, "R_UpperStairs"},
                            { Direction.Down, "R_Hallway"}
                        }
                    },
                     new Room
                    {
                        Id = "R_UpperStairs",
                        Name = "Upper stairs",
                        Description = "Nothing of interest here. Stairs lead down.",
                        Exits = new Dictionary<Direction, string>
                        {
                            {Direction.Down, "R_Stairs"}
                        }
                    },
                    new Room
                    {
                        Id = "R_Hallway",
                        Name = "Hallway",
                        Description = "Racks of shoes are all around. Stairs lead up. A corridor with white walls goes west from here.",
                        Exits = new Dictionary<Direction, string>
                        {
                            {Direction.Up, "R_Stairs"},
                            {Direction.West, "R_Corridor1p1"}
                        }
                    },
                    new Room
                    {
                        Id = "R_Corridor1p1",
                        Name = "Corridor",
                        Description = "A well-hidden camera watches your moves. To the east is a hallway. To the south is DP Room. A corridor with white walls lead north.",
                        Exits = new Dictionary<Direction, string>
                        {
                            {Direction.East, "R_Hallway"},
                            {Direction.South, "R_DPRoom"},
                            { Direction.North, "R_Corridor1p2"}
                        }
                    },
                    new Room
                    {
                        Id = "R_DPRoom",
                        Name = "DP Room",
                        Description = "A number of computer desks stand around the walls. You may return to the corridor to the north or take a visit to Managers Room west of here.",
                        Exits = new Dictionary<Direction, string>
                        {
                            {Direction.North, "R_Corridor1p1"},
                            {Direction.West, "R_ManagersRoom"}
                        }
                    },
                    new Room
                    {
                        Id = "R_ManagersRoom",
                        Name = "Managers Room",
                        Description = "A grand room with lots of computer desks. You may go east to DP Room or north to another corridor.",
                        Exits = new Dictionary<Direction, string>
                        {
                            {Direction.East, "R_DPRoom"},
                             {Direction.North, "R_Corridor2p1"}
                        }
                    },
                    new Room
                    {
                        Id = "R_Corridor2p1",
                        Name = "Corridor",
                        Description = "A large black cabinet with all kinds of stuff is here. To the south is Managers Room. To the west there is a glass door. Corridor leads north.",
                        Exits = new Dictionary<Direction, string>
                        {
                            {Direction.South, "R_ManagersRoom"},
                             {Direction.West, "R_Boss"},
                            { Direction.North, "R_Corridor2p2"}
                        }
                    },
                    new Room
                    {
                        Id = "R_Boss",
                        Name = "Boss Room",
                        Description = "Two computer desks and a locker full of iBeacons stand here. You may return east to the corridor.",
                        Exits = new Dictionary<Direction, string>
                        {
                            {Direction.East, "R_Corridor2p1"},
                            { Direction.Down, "R_SecretPassage"}
                        }
                    },
                    new Room
                    {
                        Id = "R_Corridor2p2",
                        Name = "Corridor",
                        Description = "Corridor leads north. There is a glass door to the west.",
                        Exits = new Dictionary<Direction, string>
                        {
                            {Direction.West, "R_Accounting"},
                             { Direction.South, "R_Corridor2p1"},
                              { Direction.North, "R_Corridor2p3"}
                        }
                    },
                    new Room
                    {
                        Id = "R_Accounting",
                        Name = "Accounting Room",
                        Description = "A room is filled with cabinets and lockers mostly with extra-secret papers. An exit to the corridor is to the east.",
                        Exits = new Dictionary<Direction, string>
                        {
                            {Direction.East, "R_Corridor2p2"}
                        }
                    },
                    new Room
                    {
                        Id = "R_Corridor2p3",
                        Name = "Corridor",
                        Description = "Corridor ends here. A printer is standing on the desk making loud noise. Corridor leads south. A kitchen room is to the east. A glass door labeled 'Сrutches&Bikes Inc.' is on the western side.",
                        Exits = new Dictionary<Direction, string>
                        {
                            {Direction.West, "R_WebProg"},
                             { Direction.East, "R_Kitchen"},
                              { Direction.South, "R_Corridor2p2"}
                        }
                    },
                    new Room
                    {
                        Id = "R_WebProg",
                        Name = "Developers Room",
                        Description = "A bunch of computer desks are here. There is a poster of Lady Gaga on the wall. An opened glass door leads east.",
                        Exits = new Dictionary<Direction, string>
                        {
                             { Direction.East, "R_Corridor2p3"}
                        }
                    },
                    new Room
                    {
                        Id = "R_Kitchen",
                        Name = "Kitchen",
                        Description = "A well-equipped kitchen. A boiler stands in the corner. You may go west to the corridor or south to another passage.",
                        Exits = new Dictionary<Direction, string>
                        {
                             { Direction.West, "R_Corridor2p3"},
                            { Direction.South, "R_Corridor1p2"}
                        }
                    },
                    new Room
                    {
                        Id = "R_Corridor1p2",
                        Name = "Corridor",
                        Description = "You are in the nothern part of a corridor. To the north is a kitchen. To the east there is a blue door.",
                        Exits = new Dictionary<Direction, string>
                        {
                             { Direction.North, "R_Kitchen"},
                             { Direction.South, "R_Corridor1p1"},
                            {  Direction.East, "R_ToiletSplit"}
                        }
                    },
                    new Room
                    {
                        Id = "R_ToiletSplit",
                        Name = "A Split",
                        Description = "A small room with two blue doors leading in the northern and southern directions. Another blue door to the west is an exit to the corridor.",
                        Exits = new Dictionary<Direction, string>
                        {
                             { Direction.West, "R_Corridor1p2"},
                             { Direction.South, "R_ToiletM"},
                            { Direction.North, "R_ToiletF"}
                        }
                    },
                    new Room
                    {
                        Id = "R_ToiletM",
                        Name = "Toilet",
                        Description = "It is pitch black. Someone has turned the lights off! You may go back north.",
                        Exits = new Dictionary<Direction, string>
                        {
                             { Direction.North, "R_ToiletSplit"}
                        }
                    },
                    new Room
                    {
                        Id = "R_ToiletF",
                        Name = "Toilet",
                        Description = "It is pitch black. Someone has turned the lights off! You may go back south.",
                        Exits = new Dictionary<Direction, string>
                        {
                             { Direction.South, "R_ToiletSplit"}
                        }
                    },
                    new Room
                    {
                        Id = "R_ToiletF",
                        Name = "Toilet",
                        Description = "It is pitch black. You are likely to be eaten by a grue.",
                        Exits = new Dictionary<Direction, string>
                        {
                             { Direction.South, "R_ToiletSplit"}
                        }
                    },
                    new Room
                    {
                        Id = "R_SecretPassage",
                        Name = "Secret Passage",
                        Description = @"You go down a secret stairway to the cellar. 

It is pitch black here. There is a glowing red portal to another dimension to the north.",
                        Exits = new Dictionary<Direction, string>
                        {
                             { Direction.North, "R_BeyondPortal"},
                             { Direction.Up, "R_Boss"},
                        }
                    },
                    new Room
                    {
                        Id = "R_BeyondPortal",
                        Name = "Beyond portal",
                        Description = @"Oh, sorry. There is nothing here. Come back later.",
                        Exits = new Dictionary<Direction, string>
                        {
                             { Direction.South, "R_SecretPassage"}
                        }
                    }


                }
            };


            return world;
        }
    }
}
