using app14;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace app14
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Reporte : ContentPage
    {
        private List<Calificacion> calificaciones;

        public Reporte()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Mostrar el ActivityIndicator
            LoadingActivityIndicator.IsRunning = true;
            LoadingActivityIndicator.IsVisible = true;

            // Realizar una consulta a la base de datos para obtener las calificaciones
            calificaciones = await ObtenerCalificacionesDesdeBaseDeDatosAsync();

            // Establecer la lista de calificaciones como origen de datos del ListView
            NotasListView.ItemsSource = calificaciones;

            // Ocultar el ActivityIndicator
            LoadingActivityIndicator.IsRunning = false;
            LoadingActivityIndicator.IsVisible = false;
        }



        private async Task<List<Calificacion>> ObtenerCalificacionesDesdeBaseDeDatosAsync()
        {
            try
            {
                string connectionString = @"data source=10.0.0.220;initial catalog=NOTAS;user id=jhoan;password=jhoan;";
                List<Calificacion> calificaciones = new List<Calificacion>();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    string query = "SELECT * FROM Calificaciones";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Calificacion calificacion = new Calificacion
                            {
                                ID = reader.GetInt32(reader.GetOrdinal("ID")),
                                Profesor = reader.GetString(reader.GetOrdinal("Profesor")),
                                Matricula = reader.GetString(reader.GetOrdinal("Matricula")),
                                Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                Asistencia = reader.GetDecimal(reader.GetOrdinal("Asistencia")),
                                TrabajoPractico = reader.GetDecimal(reader.GetOrdinal("TrabajoPractico")),
                                ExamenParcial = reader.GetDecimal(reader.GetOrdinal("ExamenParcial")),
                                ExamenFinal = reader.GetDecimal(reader.GetOrdinal("ExamenFinal")),
                                NotaFinal = reader.GetDecimal(reader.GetOrdinal("NotaFinal"))
                            };
                            calificaciones.Add(calificacion);
                        }
                    }
                }

                return calificaciones;
            }
            catch (Exception ex)
            {
                // Manejar errores aquí
                await DisplayAlert("Error", "Error al obtener las calificaciones desde la base de datos: " + ex.Message, "Aceptar");
                return new List<Calificacion>();
            }
        }


    }
}