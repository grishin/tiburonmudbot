using System.Collections.Generic;

namespace TiburonMUD.Engine.Models
{
    public class World
    {
        public ICollection<Room> Rooms { get; set; }
    }
}
