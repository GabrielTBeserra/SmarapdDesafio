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

        /// <summary>
        ///     Retorna apenas a salas que possua algum horario agendado
        /// </summary>
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

        /// <summary>
        ///     Retorna todas as salas
        /// </summary>
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


        /// <summary>
        ///     Retorna uma sala conforme um id fornecido.
        /// </summary>
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

        /// <summary>
        ///     Cria uma nova sala
        /// </summary>
        public async Task<Response> Create()
        {
            var room = new Room();
            _context.Add(room);
            await _context.SaveChangesAsync();
            return new Response(ResponseType.SUCESS) {Message = "Sala adicionado com sucesso!"};
        }

        /// <summary>
        ///     Apaga um sala conforme um id fornecido
        /// </summary>
        public async Task<Response> Delete(int id)
        {
            try
            {
                var obj = await _context.Rooms.FindAsync(id);
                _context.Rooms.Remove(obj);
                await _context.SaveChangesAsync();
                return new Response(ResponseType.SUCESS) {Message = "Sala excluida com sucesso!"};
            }
            catch (DbUpdateException e)
            {
                return new Response(ResponseType.ERROR) {Message = "Nao foi possivel apagar essa sala"};
            }
        }
    }
}