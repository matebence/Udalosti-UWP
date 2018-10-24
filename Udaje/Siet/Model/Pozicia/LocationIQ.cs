namespace Udalosti.Udaje.Siet.Model.Pozicia
{
    class LocationIQ
    {
        public LocationIQ(Pozicia address)
        {
            this.address = address;
        }

        public Pozicia address { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
