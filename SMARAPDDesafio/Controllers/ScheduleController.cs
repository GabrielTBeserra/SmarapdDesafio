using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SMARAPDDesafio.Data;
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

        [HttpPost("insert")]
        public async Task<Response> Insert([FromBody] Scheduling scheduling)
        {
            return await _schedulingService.AddSchedule(scheduling);
        }


        [HttpDelete("delete")]
        public async Task<Response> Remove(int id)
        {
            return await _schedulingService.RemoveAsync(id);
        }

        [HttpPost("update")]
        public async Task<Response> Update([FromBody] Scheduling scheduling)
        {
            return await _schedulingService.UpdateAsync(scheduling);
        }
    }
}