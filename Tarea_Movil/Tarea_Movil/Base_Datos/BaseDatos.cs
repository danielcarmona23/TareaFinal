using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Tarea_Movil.Base_Datos
{
    public interface ISQLiteDB
    {
        SQLiteAsyncConnection GetConnection();
    }
}
