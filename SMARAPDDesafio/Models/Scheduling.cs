using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

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
        [BindProperty]
        public int Id { get; set; }
        [BindProperty]
        public int RoomId { get; set; }
        [BindProperty]
        public string Title { get; set; }
        [BindProperty]
        public DateTime StartTime { get; set; }
        [BindProperty]
        public DateTime EndTime { get; set; }
    }
}