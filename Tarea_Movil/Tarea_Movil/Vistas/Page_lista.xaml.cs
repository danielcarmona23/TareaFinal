using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SQLite;
using Tarea_Movil.Lista_Datos;
using System.Collections.ObjectModel;
using System.IO;
using Tarea_Movil.Base_Datos;
namespace Tarea_Movil.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page_lista : ContentPage
    {
        private SQLiteAsyncConnection conexion;
        private ObservableCollection<T_Contacto> TablaContacto;
        public Page_lista()
        {
            InitializeComponent();
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();
            ListaContactos.ItemSelected += ListaContactos_ItemSelected;
        }

        private void ListaContactos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var Obj = (T_Contacto)e.SelectedItem;
            var item = Obj.Id.ToString();
            var edad = Obj.edad;
            var nom = Obj.Nombre;
            var ap = Obj.Apellidos;
            var dir = Obj.Direccion;
            var puesto = Obj.puesto;
            int ID = Convert.ToInt32(item);
            try
            {
                Navigation.PushAsync(new Page_detail(ID, nom, ap, edad, dir, puesto));
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected async override void OnAppearing()
        {
            var ResulRegistros = await conexion.Table<T_Contacto>().ToListAsync();
            TablaContacto = new ObservableCollection<T_Contacto>(ResulRegistros);
            ListaContactos.ItemsSource = TablaContacto;
            base.OnAppearing();
        }
    }
}