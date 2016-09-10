using System.Linq;

namespace TiburonMUD.Engine.Models
{
    public class Player
    {
        public Room CurrentRoom { get; private set; }

        private readonly World _world;

        public Player(World world, string roomId)
        {
            _world = world;
            Relocate(roomId);
        }

        public void Relocate(string id)
        {
            CurrentRoom = _world.Rooms.FirstOrDefault(x => x.Id == id);
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
