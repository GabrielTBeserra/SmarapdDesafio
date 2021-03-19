using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SMARAPDDesafio.Data;
using SMARAPDDesafio.Models;
using SMARAPDDesafio.Models.enums;

namespace SMARAPDDesafio.Services
{
    public class RoomService
    {
        private readonly SmarapdDesafioContext _context;

        public RoomService(SmarapdDesafioContext context)
        {
            _context = context;
        }

        // Retorna apenas a salas que possuem algum horario agendado
        public List<Room> JustScheduled()
        {
            var result = (from a in _context.Rooms
                join al in _context.Schedulings on a.RoomId equals al.RoomId
                select new
                {
                    al.Title,
                    a.RoomId,
                    al.Id,
                    al.StartTime,
                    al.EndTime
                }).ToList();

            var openWith =
                new Dictionary<int, Room>();

            foreach (var item in result)
                if (!openWith.ContainsKey(item.RoomId))
                    openWith.Add(item.RoomId, new Room
                    {
                        RoomId = item.RoomId, Schedulings = new List<Scheduling>
                        {
                            new Scheduling(item.RoomId)
                            {
                                Title = item.Title,
                                Id = item.Id,
                                EndTime = item.EndTime,
                                StartTime = item.StartTime
                            }
                        }
                    });
                else
                    openWith[item.RoomId].Schedulings.Add(new Scheduling(item.RoomId)
                    {
                        Id = item.Id,
                        EndTime = item.EndTime,
                        StartTime = item.StartTime
                    });


            return openWith.Values.ToList();
        }

        // Retorna todas as salas 
        public List<Room> FindAll()
        {
            var result = (from a in _context.Rooms
                from al in _context.Schedulings
                select new
                {
                    a.RoomId,
                    SRoomId = al.RoomId,
                    al.Title,
                    al.Id,
                    al.StartTime,
                    al.EndTime
                }).ToList();

            var dictionaryRooms =
                new Dictionary<int, Room>();

            foreach (var item in result)
                if (!dictionaryRooms.ContainsKey(item.RoomId))
                {
                    var r = new Room
                    {
                        RoomId = item.RoomId
                    };


                    if (item.RoomId == item.SRoomId)
                        r.Schedulings = new List<Scheduling>
                        {
                            new Scheduling(item.RoomId)
                            {
                                Title = item.Title,
                                Id = item.Id,
                                EndTime = item.EndTime,
                                StartTime = item.StartTime
                            }
                        };
                    dictionaryRooms.Add(item.RoomId, r);
                }
                else
                {
                    if (item.RoomId == item.SRoomId)
                        if (dictionaryRooms[item.RoomId].Schedulings == null)
                        {
                            dictionaryRooms[item.RoomId].Schedulings = new List<Scheduling>
                            {
                                new Scheduling(item.RoomId)
                                {
                                    Title = item.Title,
                                    Id = item.Id,
                                    EndTime = item.EndTime,
                                    StartTime = item.StartTime
                                }
                            };
                        }
                        else
                        {
                            if (!dictionaryRooms[item.RoomId].Schedulings.Any(asd => asd.Id == item.Id))
                                dictionaryRooms[item.RoomId].Schedulings.Add(new Scheduling(item.RoomId)
                                {
                                    Title = item.Title,
                                    Id = item.Id,
                                    EndTime = item.EndTime,
                                    StartTime = item.StartTime
                                });
                        }
                }


            return dictionaryRooms.Values.ToList();
        }


        // Retorna uma sala especifica
        public async Task<Room> FindFromId(int id)
        {
            var result = (from a in _context.Rooms
                join al in _context.Schedulings on a.RoomId equals al.RoomId
                where a.RoomId == id
                select new
                {
                    al.Title,
                    a.RoomId,
                    al.Id,
                    al.StartTime,
                    al.EndTime
                }).ToList();


            var r = await _context.Rooms.FirstOrDefaultAsync(r => r.RoomId == id);
            r.Schedulings = new List<Scheduling>();
            foreach (var item in result)
                r.Schedulings.Add(new Scheduling(item.RoomId)
                {
                    Title = item.Title,
                    Id = item.Id,
                    EndTime = item.EndTime,
                    StartTime = item.StartTime
                });


            return await _context.Rooms.FirstOrDefaultAsync(r => r.RoomId == id);
        }

        public async Task<Response> InsertAsync(Room room)
        {
            _context.Add(room);
            await _context.SaveChangesAsync();
            return new Response(ResponseType.SUCESS) {Message = "Nova Sala inserida com sucesso!"};
        }

        public async Task<Room> Create()
        {
            var room = new Room();
            _context.Add(room);
            await _context.SaveChangesAsync();
            return room;
        }
    }
}