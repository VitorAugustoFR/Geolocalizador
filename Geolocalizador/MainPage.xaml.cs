using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Geolocalizador
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private async void btLocalizacao_Clicked(object sender, EventArgs e)
        {
            try
            {
                //puxando a localização
                var localizacao = await Geolocation.GetLocationAsync(new GeolocationRequest()
                {
                    DesiredAccuracy = GeolocationAccuracy.Best
                });
                if (localizacao != null)
                {
                    lblLatitude.Text = "Latitude: " + localizacao.Latitude.ToString();
                    lblLongitude.Text = "Longitude: " + localizacao.Longitude.ToString();
                }
            }
            //error messages
            catch (FeatureNotSupportedException fnsEx) 
            {
                await DisplayAlert("Erro!", fnsEx.Message, "OK");
            }
            catch (PermissionException pEx)
            {
                await DisplayAlert("Erro!", pEx.Message, "OK");
            }
            catch (Exception eEx)
            {
                await DisplayAlert("Erro!", eEx.Message, "OK");
            }
        }

        public async Task MostrarMapa()
        {
            //recebe localização
            var localizacao = await Geolocation.GetLocationAsync(new GeolocationRequest()
            {
                DesiredAccuracy = GeolocationAccuracy.Best
            });

            var localinfo = new Location(localizacao.Latitude, localizacao.Longitude);

            //opções para mostrar no mapa
            var options = new MapLaunchOptions { Name = "Meu local..." };
            //Abre o mapa
            await Map.OpenAsync(localinfo, options);
        }

        private async void btVerMapa_Clicked(object sender, EventArgs e)
        {
            await MostrarMapa();
        }
    }
}
