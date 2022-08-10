using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorCrud.Models.Response
{
    public class Respuesta<T>
    {
        public int Exito { get; set; } //Recibimos un entero para saber si se recibio algo por get
        public string Mensaje { get; set; } //Mandamos mensaje de error o exito
        public T Data { get; set; } //Se tendran datos
        public Respuesta() //Constructor para tener default Exito como 0
        {
            this.Exito = 0;
        }
    }
}
