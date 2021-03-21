using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SMARAPDDesafio.Models;
using SMARAPDDesafio.Services;

namespace SMARAPDDesafio.Controllers
{
    [ApiController]
    [Route("salas")]
    public class RoomController
    {
        private readonly RoomService _roomService;

        public RoomController(RoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet("salasagendadas")]
        public List<Room> GetAll()
        {
            return _roomService.JustScheduled();
        }

        [HttpGet("listall")]
        public List<Room> FindAllasd()
        {
            return _roomService.FindAll();
        }

        [HttpGet("get")]
        public async Task<Room> GetFromId(int id)
        {
            return await _roomService.FindFromId(id);
        }

        [HttpGet("create")]
        public async Task<Response> Create()
        {
            return await _roomService.Create();
        }

        [HttpDelete("delete")]
        public async Task<Response> Delete(int id)
        {
            return await _roomService.Delete(id);
        }
    }
}