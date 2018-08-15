namespace Udalosti.Udaje.Siet.Model.Pozicia
{
    class Pozicia
    {
        public Pozicia(string country, string regionName, string city)
        {
            this.country = country;
            this.regionName = regionName;
            this.city = city;
        }

        public string country { get; set; }
        public string regionName { get; set; }
        public string city { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
