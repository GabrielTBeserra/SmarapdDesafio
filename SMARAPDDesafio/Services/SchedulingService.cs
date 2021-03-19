using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SMARAPDDesafio.Data;
using SMARAPDDesafio.Models;
using SMARAPDDesafio.Models.enums;

namespace SMARAPDDesafio.Services
{
    public class SchedulingService
    {
        private readonly SmarapdDesafioContext _context;

        public SchedulingService(SmarapdDesafioContext context)
        {
            _context = context;
        }

        public async Task<List<Room>> FindAll()
        {
            return await _context.Rooms.ToListAsync();
        }

        public async Task<Room> FindFromId(int id)
        {
            return await _context.Rooms.FirstOrDefaultAsync(r => r.RoomId == id);
        }


        public async Task<Response> AddSchedule(Scheduling scheduling)
        {
            var isExits = await _context.Schedulings.AnyAsync(sc =>
                sc.RoomId == scheduling.RoomId &&
                (sc.EndTime <= scheduling.EndTime && sc.StartTime >= scheduling.StartTime ||
                 scheduling.EndTime <= sc.EndTime && scheduling.StartTime >= sc.StartTime));


            if (!isExits)
            {
                await _context.AddAsync(scheduling);
                await _context.SaveChangesAsync();

                return new Response(ResponseType.SUCESS) {Message = "Agendamento realizado com sucesso!"};
            }


            return new Response(ResponseType.ERROR)
                {Message = "O agendamento nao foi realizado, ja possui uma agendamento nessa datas!"};
        }


        public async Task<Response> UpdateAsync(Scheduling obj)
        {
            var hasAny = await _context.Schedulings.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny) return new Response(ResponseType.ERROR) {Message = "Nao foi possivel atualizar o cadastro!"};

            try
            {
                var isExits = await _context.Schedulings.AnyAsync(sc =>
                    sc.RoomId == obj.RoomId &&
                    (sc.EndTime <= obj.EndTime && sc.StartTime >= obj.StartTime ||
                     obj.EndTime <= sc.EndTime && obj.StartTime >= sc.StartTime));

                if (!isExits)
                {
                    _context.Update(obj);
                    await _context.SaveChangesAsync();
                    return new Response(ResponseType.SUCESS) {Message = "Agendamento atualizado com sucesso!"};
                }

                return new Response(ResponseType.ERROR)
                    {Message = "As datas nao podem ser as mesmas de outro agendamento!"};
            }
            catch (Exception e)
            {
                return new Response(ResponseType.ERROR) {Message = e.Message};
            }
        }


        public async Task<Response> RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Schedulings.FindAsync(id);
                _context.Schedulings.Remove(obj);
                await _context.SaveChangesAsync();
                return new Response(ResponseType.SUCESS) {Message = "Agendamento apagado com sucesso!"};
            }
            catch (Exception e)
            {
                return new Response(ResponseType.ERROR) {Message = e.Message};
            }
        }
    }
}