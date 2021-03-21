using System;
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

        /// <summary>
        ///     Adiciona um novo agendamento
        /// </summary>
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

        /// <summary>
        ///     Atualiza um agendamento
        /// </summary>
        public async Task<Response> UpdateAsync(Scheduling scheduling)
        {
            var hasAny = await _context.Schedulings.AnyAsync(x => x.Id == scheduling.Id);
            if (!hasAny) return new Response(ResponseType.ERROR) {Message = "Nao foi possivel atualizar o cadastro!"};

            try
            {
                var isExits = await _context.Schedulings.AnyAsync(sc =>
                    sc.RoomId != scheduling.RoomId &&
                    (sc.EndTime <= scheduling.EndTime && sc.StartTime >= scheduling.StartTime ||
                     scheduling.EndTime <= sc.EndTime && scheduling.StartTime >= sc.StartTime));

                if (!isExits)
                {
                    _context.Update(scheduling);
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

        /// <summary>
        ///     Remove um agendamento conforme o id fornecido
        /// </summary>
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

        /// <summary>
        ///     Responsavel por devolver um agendamento
        ///     para popular a tela de edicao de agendamento
        ///     Recebe o ID do agendamento e o ID da sala
        /// </summary>
        public async Task<Scheduling> GetFromId(int id, int roomid)
        {
            var scheduling = await _context.Schedulings.FirstOrDefaultAsync(a => a.Id == id && a.RoomId == roomid);
            scheduling.EndTime = scheduling.EndTime.ToLocalTime();
            scheduling.StartTime = scheduling.StartTime.ToLocalTime();
            return scheduling;
        }
    }
}