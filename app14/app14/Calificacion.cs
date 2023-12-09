using System;
using System.Collections.Generic;
using System.Text;

namespace app14
{
    public class Calificacion
    {
        public int ID { get; set; }
        public string Profesor { get; set; }

        public string Matricula { get; set; }
        public string Nombre { get; set; }
        public decimal Asistencia { get; set; }
        public decimal TrabajoPractico { get; set; }
        public decimal ExamenParcial { get; set; }
        public decimal ExamenFinal { get; set; }

        public decimal NotaFinal { get; set; }
    }
}
