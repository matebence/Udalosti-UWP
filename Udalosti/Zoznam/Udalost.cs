namespace Udalosti.Udalosti.Zoznam
{
    class Udalost
    {
        public string obrazok { get; set; }
        public string den { get; set; }
        public string mesiac { get; set; }
        public string nazov { get; set; }
        public string mesto { get; set; }
        public string miesto { get; set; }
        public string cas { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
