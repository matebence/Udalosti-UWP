using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Udalosti.Udaje.Nastavenia;
using Windows.Devices.Geolocation;

namespace Udalosti.Nastroje
{
    class Lokalizator
    {
        public static async Task<Dictionary<string, double>> zistiPolohuAsync()
        {
            Debug.WriteLine("Metoda zistiPolohu bola vykonana");

            Geoposition geo;
            try
            { 
                var pozicia = new Geolocator();
                pozicia.DesiredAccuracy = PositionAccuracy.High;
                geo = await pozicia.GetGeopositionAsync(maximumAge: TimeSpan.FromSeconds(Nastavenia.DLZKA_REQUESTU), timeout: TimeSpan.FromSeconds(1));
            }
            catch(Exception e)
            {
                geo = null;
                Debug.WriteLine("Lokalizator.cs CHYBA:"+e.Message);
                await DialogOznameni.kommunikaciaAsync("Chyba", "GPS je vypnutý alebo prístup k aktuálnej polohe bol odmietnutý", "Pokračovať bez GPS", false, null);
            }

            Dictionary<string, double> poloha;
            if (geo != null)
            {
                poloha = new Dictionary<string, double>();
                poloha.Add("zemepisnaSirka", geo.Coordinate.Point.Position.Latitude);
                poloha.Add("zemepisnaDlzka", geo.Coordinate.Point.Position.Longitude);
            }
            else
            {
                poloha = null;
            }

            return poloha;
        }
    }
}