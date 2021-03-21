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

        /// <summary>
        ///     End point para obter todas salas que possuem pelo menos um agendamento
        /// </summary>
        [HttpGet("salasagendadas")]
        public List<Room> GetAll()
        {
            return _roomService.JustScheduled();
        }

        /// <summary>
        ///     End point para obter todas a salas
        /// </summary>
        [HttpGet("listall")]
        public List<Room> FindAllFull()
        {
            return _roomService.FindAll();
        }

        /// <summary>
        ///     End point para obter uma sala atravez de um id
        /// </summary>
        [HttpGet("get")]
        public async Task<Room> GetFromId(int id)
        {
            return await _roomService.FindFromId(id);
        }

        /// <summary>
        ///     End point para criar uma nova sala
        /// </summary>
        [HttpGet("create")]
        public async Task<Response> Create()
        {
            return await _roomService.Create();
        }

        /// <summary>
        ///     End point para apagar um sala conforme o id informado-
        /// </summary>
        [HttpDelete("delete")]
        public async Task<Response> Delete(int id)
        {
            return await _roomService.Delete(id);
        }
    }
}