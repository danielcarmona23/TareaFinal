using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SQLite;
using Tarea_Movil.Lista_Datos;
using Tarea_Movil.Base_Datos;

namespace Tarea_Movil.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page_Registrar : ContentPage
    {
        private SQLiteAsyncConnection conexion;
        public Page_Registrar()
        {
            InitializeComponent();
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();
            btnGuardar.Clicked += BtnGuardar_Clicked;
        }

        private void BtnGuardar_Clicked(object sender, EventArgs e)
        {
            var DatosContacto = new T_Contacto
            {
                Nombre = txtNombre.Text,
                Apellidos = txtApellidos.Text,
                edad = txtedad.Text,
                Direccion = txtdireccion.Text,
                puesto = txtpuesto.Text
            };
            conexion.InsertAsync(DatosContacto);
            limpiarFormulario();
            DisplayAlert("Confirmación", "El contacto se registró correctamente", "OK");
        }
        private void limpiarFormulario()
        {
            txtNombre.Text = "";
            txtApellidos.Text = "";
            txtedad.Text = "";
            txtdireccion.Text = "";
            txtpuesto.Text = "";
        }
    }

}