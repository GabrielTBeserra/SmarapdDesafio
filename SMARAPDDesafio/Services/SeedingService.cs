using System;
using System.Collections.Generic;
using System.Linq;
using SMARAPDDesafio.Data;
using SMARAPDDesafio.Models;

namespace SMARAPDDesafio.Services
{
    public class SeedingService
    {
        private readonly SmarapdDesafioContext _context;

        public SeedingService(SmarapdDesafioContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            // Caso ja possua alguma sala criada, nao adiciona novas
            if (_context.Rooms.Any()) return;

            var r1 = new Room
            {
                RoomId = 1, Schedulings = new List<Scheduling>
                {
                    new Scheduling(1)
                    {
                        EndTime = Convert.ToDateTime("01/12/2005"),
                        StartTime = Convert.ToDateTime("01/12/2010"),
                        Title = "Teste de title"
                    },
                    new Scheduling(1)
                    {
                        EndTime = Convert.ToDateTime("01/12/1998"),
                        StartTime = Convert.ToDateTime("01/12/1997"),
                        Title = "Tesasdte de title"
                    }
                }
            };
            var r2 = new Room {RoomId = 2, Schedulings = new List<Scheduling>(2)};
            var r3 = new Room {RoomId = 3, Schedulings = new List<Scheduling>(3)};
            var r4 = new Room
            {
                RoomId = 4, Schedulings = new List<Scheduling>(4)
                {
                    new Scheduling(4)
                    {
                        Title = "Consultoria", EndTime = Convert.ToDateTime("01/12/1998"),
                        StartTime = Convert.ToDateTime("01/12/1997")
                    }
                }
            };
            var r5 = new Room {RoomId = 5, Schedulings = new List<Scheduling>(5)};
            var r6 = new Room {RoomId = 6, Schedulings = new List<Scheduling>(6)};
            var r7 = new Room {RoomId = 7, Schedulings = new List<Scheduling>(7)};
            var r8 = new Room {RoomId = 8, Schedulings = new List<Scheduling>(8)};
            var r9 = new Room {RoomId = 9, Schedulings = new List<Scheduling>(9)};
            var r10 = new Room {RoomId = 10, Schedulings = new List<Scheduling>(10)};


            _context.Rooms.AddRange(r1, r2, r3, r4, r5, r6, r7, r8, r9, r10);
            _context.SaveChanges();
        }
    }
}