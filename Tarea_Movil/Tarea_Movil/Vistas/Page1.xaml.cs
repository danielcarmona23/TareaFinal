using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SQLite;
using Tarea_Movil.Lista_Datos;
using System.IO;
using Tarea_Movil.Base_Datos;

namespace Tarea_Movil.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        private SQLiteAsyncConnection conexion;
        public Page1()
        {
            InitializeComponent();
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();
            btnBuscar.Clicked += BtnBuscar_Clicked;
            btnRegistrar.Clicked += BtnRegistrar_Clicked;
        }

        private void BtnRegistrar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Page_Registrar());
        }

        private void BtnBuscar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var rutaDB = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "AgendaSQLite.db3");
                var db = new SQLiteConnection(rutaDB);
                db.CreateTable<T_Contacto>();
                IEnumerable<T_Contacto> resultado = SELECT_WHERE(db, txtNombre.Text);
                if (resultado.Count() > 0)
                {
                    Navigation.PushAsync(new Page_lista());
                    DisplayAlert("Aviso", "Existen contactos con este nombre", "OK");
                }
                else
                {
                    DisplayAlert("Aviso", "No existen contactos con este nombre", "OK");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private IEnumerable<T_Contacto> SELECT_WHERE(SQLiteConnection db, string nombre)
        {
            return db.Query<T_Contacto>("SELECT * FROM T_Contacto WHERE Nombre=?", nombre);
        }
    }
}