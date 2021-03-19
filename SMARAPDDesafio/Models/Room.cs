using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMARAPDDesafio.Models
{
    public class Room
    {
        public Room()
        {
        }

        public Room(int roomId, ICollection<Scheduling> schedulings)
        {
            RoomId = roomId;
            Schedulings = schedulings;
        }

        public Room(int roomId)
        {
            RoomId = roomId;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoomId { get; set; }

        public ICollection<Scheduling> Schedulings { get; set; }
    }
}