using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SMARAPDDesafio.Models;
using SMARAPDDesafio.Services;

namespace SMARAPDDesafio.Controllers
{
    [ApiController]
    [Route("agendamento")]
    public class ScheduleController
    {
        private readonly SchedulingService _schedulingService;

        public ScheduleController(SchedulingService schedulingService)
        {
            _schedulingService = schedulingService;
        }

        /// <summary>
        ///     End point para criar um novo agendamento
        /// </summary>
        [HttpPost("insert")]
        public async Task<Response> Insert([FromBody] Scheduling scheduling)
        {
            scheduling.EndTime = scheduling.EndTime.ToLocalTime();
            scheduling.StartTime = scheduling.StartTime.ToLocalTime();
            return await _schedulingService.AddSchedule(scheduling);
        }


        /// <summary>
        ///     End point para apagar um agendamento atrazve do id
        /// </summary>
        [HttpDelete("delete")]
        public async Task<Response> Remove(int id)
        {
            return await _schedulingService.RemoveAsync(id);
        }

        /// <summary>
        ///     End point para atualizar um agendamento
        /// </summary>
        [HttpPost("update")]
        public async Task<Response> Update([FromBody] Scheduling scheduling)
        {
            scheduling.EndTime = scheduling.EndTime.ToLocalTime();
            scheduling.StartTime = scheduling.StartTime.ToLocalTime();
            return await _schedulingService.UpdateAsync(scheduling);
        }

        /// <summary>
        ///     End point para obter um agendamento pelo id do agendamento e o id da sala
        /// </summary>
        [HttpGet("get")]
        public async Task<Scheduling> GetFromId(int id, int roomid)
        {
            return await _schedulingService.GetFromId(id, roomid);
        }
    }
}