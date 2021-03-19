using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMARAPDDesafio.Models
{
    public class Scheduling
    {
        public Scheduling()
        {
        }
        
        

        public Scheduling(int roomId, DateTime startTime, DateTime endTime)
        {
            RoomId = roomId;
            StartTime = startTime;
            EndTime = endTime;
        }

        public Scheduling(int roomId)
        {
            RoomId = roomId;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int RoomId { get; set; }
        
        public string Title { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}