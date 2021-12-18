using SQLite;
using Tarea_Movil.Base_Datos;
using Tarea_Movil.Lista_Datos;
using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Tarea_Movil.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page_detail : ContentPage
    {
        public int IdSeleccionado;
        public string NomSeleccionado, ApSeleccionado, edadSeleccionado, dirSeleccionado, pueSeleccionado;
        private SQLiteAsyncConnection conexion;
        IEnumerable<T_Contacto> ResultadoDelete;
        IEnumerable<T_Contacto> ResultadoUpdate;
        public Page_detail(int id, string nom, string ap, string edad, string dir, string puesto)
        {
            InitializeComponent();
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();
            IdSeleccionado = id;
            NomSeleccionado = nom;
            ApSeleccionado = ap;
            edadSeleccionado = edad;
            dirSeleccionado = dir;
            pueSeleccionado = puesto;
            btn_actualizar.Clicked += Btn_actualizar_Clicked;
            btn_eliminar.Clicked += Btn_eliminar_Clicked;   
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            lblMensaje.Text = " ID :" + IdSeleccionado;
            txtNombre.Text = NomSeleccionado;
            txtApellidos.Text = ApSeleccionado;
            txtedad.Text = edadSeleccionado;
            txtdireccion.Text = dirSeleccionado;
            txtpuesto.Text = pueSeleccionado;
        }
        private void Btn_eliminar_Clicked(object sender, EventArgs e)
        {
            var rutaDB = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "AgendaSQLite.db3");
            var db = new SQLiteConnection(rutaDB);
            ResultadoDelete = Delete(db, IdSeleccionado);
            DisplayAlert("Confirmación", "El contacto se eliminó correctamente", "OK");
            Limpiar();
        }
        private void Btn_actualizar_Clicked(object sender, EventArgs e)
        {
            var rutadb = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "AgendaSQLite.db3");
            var db = new SQLiteConnection(rutadb);
            ResultadoUpdate = Update(db, txtNombre.Text, txtApellidos.Text, txtedad.Text, txtdireccion.Text, txtpuesto.Text, IdSeleccionado);
            DisplayAlert("Confirmación", "El contacto se acualizó correctamente", "OK");
        }
        public static IEnumerable<T_Contacto> Delete(SQLiteConnection db, int id)
        {
            return db.Query<T_Contacto>("Delete FROM T_CONTACTO WHERE Id = ?", id);
        }
        public static IEnumerable<T_Contacto> Update(SQLiteConnection db, string nombre, string apellidos, string edad, string direccion, string puesto, int id)
        {
            return db.Query<T_Contacto>("UPDATE T_Contacto SET Nombre = ?, Apellidos = ?, edad = ? direccion = ? puesto = ? WHERE Id =?", nombre, apellidos, edad, direccion, puesto, id);
        }
        public void Limpiar()
        {
            lblMensaje.Text = "";
            txtNombre.Text = "";
            txtApellidos.Text = "";
            txtedad.Text = "";
            txtdireccion.Text = "";
            txtpuesto.Text = "";
        }
    }
}