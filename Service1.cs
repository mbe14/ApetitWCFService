using System;
using System.Collections.Generic;
using ApetitWCFService.Model;
using ApetitWCFService.Repository;

namespace ApetitWCFService
{
	public class Service1 : IService1
    {
		public List<MenuData> GetApetitData()
        {
            MenuRepository mrep = new MenuRepository();
            List<MenuData> lmi = new List<MenuData>();

            string f1 = null, f2 = null;
            string selectedDay = DateTime.Now.DayOfWeek.ToString();
            switch (selectedDay)
            {
                case "Monday":
                    f1 = "8";
                    f2 = "9";
                    break;
                case "Tuesday":
                    f1 = "13";
                    f2 = "14";
                    break;
                case "Wednesday":
                    f1 = "18";
                    f2 = "19";
                    break;
                case "Thursday":
                    f1 = "22";
                    f2 = "23";
                    break;
                case "Friday":
                    f1 = "26";
                    f2 = "27";
                    break;
                default:
                    break;
            }

            var list = mrep.GetFeluri(f1);
            list.AddRange(mrep.GetFeluri(f2));
            list.AddRange(mrep.GetOtherMenuData());

            lmi = list;
            Logger.WriteErrorLog("Data from Apetit was loaded.");
            return lmi;
        }
    }
}
