using System;
namespace ApetitWCFService.Model
{
    public class MenuData
    {
		public string Code { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public decimal Weight { get; set; }
        public string ValNut { get; set; }
        public string Ingredients { get; set; }
    }
}
