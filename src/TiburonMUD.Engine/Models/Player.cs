using System.Linq;

namespace TiburonMUD.Engine.Models
{
    public class Player
    {
        public string Name { get; private set; }
        public World World { get; private set; }
        public Room CurrentRoom { get; private set; }

        

        public Player(string name, World world, string roomId)
        {
            Name = name;
            World = world;

            Relocate(roomId);
        }

        public void Relocate(string id)
        {
            CurrentRoom = World.Rooms.FirstOrDefault(x => x.Id == id);
        }

        public bool Move(Direction direction)
        {
            string newRoomId;
            if (CurrentRoom.Exits.TryGetValue(direction, out newRoomId))
            {
                Relocate(newRoomId);

                return true;
            }

            return false;
        }
    }
}
