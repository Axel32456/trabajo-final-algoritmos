using System;

namespace tpfinal
{
    public class Dato
    {
        public int ocurrencia { get; set; }
        public string texto { get; set; }
        public string descripcion { get; set; }

        public Dato(int ocurrencia, string texto)
        {
            this.ocurrencia = ocurrencia;
            this.texto = texto;
        }
        public Dato(int ocurrencia, string texto, string descripcion)
        {
            this.ocurrencia = ocurrencia;
            this.texto = texto;
            this.descripcion = descripcion;
        }
        public override string ToString()
        {
            if (texto != null)
            {
                return "(" +ocurrencia +") " + texto;
            }

            else
            {
                return "";
            }
        }
    }
}