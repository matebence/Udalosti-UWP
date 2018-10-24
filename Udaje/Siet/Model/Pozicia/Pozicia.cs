using System;

namespace Udalosti.Udaje.Siet.Model.Pozicia
{
    class Pozicia
    {
        public Pozicia(string city_district, string city, string state, string postcode, string country, string country_code)
        {
            this.city_district = city_district;
            this.city = city;
            this.state = state;
            this.postcode = postcode;
            this.country = country;
            this.country_code = country_code;
        }

        public String city_district { get; set; }
        public String city { get; set; }
        public String state { get; set; }
        public String postcode { get; set; }
        public String country { get; set; }
        public String country_code { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}