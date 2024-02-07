﻿using System.Net;

namespace API_Facturacion.Models
{
    public class RespuestaAPI
    {
        public RespuestaAPI()
        {
            ErrorMessages = new List<string>();
        }
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public List<string> ErrorMessages { get; set; }
        public object Result { get; set; }
    }
}
