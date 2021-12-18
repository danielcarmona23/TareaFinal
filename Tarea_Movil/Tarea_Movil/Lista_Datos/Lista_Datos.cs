using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Tarea_Movil.Lista_Datos
{
    public class T_Contacto
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(255)]
        public string Nombre { get; set; }
        [MaxLength(255)]
        public string Apellidos { get; set; }
        [MaxLength(255)]
        public string edad { get; set; }
        [MaxLength(100)]
        public String Direccion { get; set; }

        [MaxLength(100)]
        public String puesto { get; set; }


    }
}
