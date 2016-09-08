using System.Collections.Generic;

namespace TiburonMUD.Engine.Models
{
    public class Room
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Dictionary<Direction, string> Exits { get; set; }
    }
}
