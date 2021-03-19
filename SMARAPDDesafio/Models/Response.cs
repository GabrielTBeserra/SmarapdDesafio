using System.Collections.Generic;
using SMARAPDDesafio.Models.enums;

namespace SMARAPDDesafio.Models
{
    public class Response
    {
        private readonly Dictionary<ResponseType, string> codeErros = new Dictionary<ResponseType, string>
            {{ResponseType.ERROR, "ERROR"}, {ResponseType.SUCESS, "SUCESS"}};

        public Response(ResponseType response)
        {
            Type = codeErros[response];
        }
        
        public string Type { get; }

        public string Message { get; set; }
    }
}