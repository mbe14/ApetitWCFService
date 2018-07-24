using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using ApetitWCFService.Model;
using HtmlAgilityPack;

namespace ApetitWCFService.Repository
{
	internal class MenuRepository
    {
		public List<MenuData> GetFeluri(string f)
        {
            List<MenuData> fullmenu = new List<MenuData>();

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            try
            {
                var fel1 = (HttpWebRequest)WebRequest.Create("http://apetit-catering.ro/category.php?id_category=" + f);
                fel1.Method = "GET";

                using (var response = (HttpWebResponse)fel1.GetResponse())
                {
                    int response_code = (int)response.StatusCode;
                    string response_descr = (string)response.StatusDescription;
                    Logger.WriteErrorLog("HttpWebResponse StatusCode: " + response_code + " " + response_descr);
                    using (var stream = response.GetResponseStream())
                    {
                        doc.Load(stream, Encoding.GetEncoding("iso-8859-9"));
                    }
                }
            }
            catch (WebException ex)
            {
                Logger.WriteErrorLog(ex.Message);
            }

            if (doc.DocumentNode.SelectNodes("//tr[@class='row']") == null)
            {
                Logger.WriteErrorLog("Document is empty! No data on the Apetit page. Maybe site is updating?");
            }
            
            else
            {
                foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//tr[@class='row']"))
                {
                    string allt1 = node.InnerText;
                    fullmenu.Add(ProcessString(allt1));
                }
            }

            return fullmenu;
        }

        public List<MenuData> GetOtherMenuData()
        {
            var list = GetFeluri("33");
            list.AddRange(GetFeluri("35"));
            list.AddRange(GetFeluri("36"));
            list.AddRange(GetFeluri("37"));
            list.AddRange(GetFeluri("38"));
            list.AddRange(GetFeluri("39"));
            //list.AddRange(GetFeluri("40"));

            return list;
        }

        private MenuData ProcessString(string str)
        {
            MenuData mi = new MenuData();
            str = str.Replace("      ", string.Empty).Replace("ï»¿", string.Empty).Replace("Â", string.Empty).Trim('\n').TrimEnd(null);
            string sub = str.TrimEnd(null);

            string[] array = sub.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            List<string> lst = new List<string>(array);
            lst.RemoveAll(item => String.IsNullOrWhiteSpace(item));
            lst.RemoveAll(item => item.Contains("Recomandarea"));

            if (lst.Count == 6)
            {
                lst[5] = lst[5].Replace(",", ".");
                mi.Weight = Convert.ToDecimal(lst[4].Remove(lst[4].Length - 2).Trim());
                mi.ValNut = lst[3].Trim();
                mi.Price = Convert.ToDecimal(lst[5].Remove(lst[5].Length - 3).Trim());
                mi.Code = (lst[0].ToString().Length > 3) ? lst[0].Substring(0, 3) : lst[0].Trim();
                mi.Description = lst[1].Trim();
                mi.Ingredients = lst[2].Trim();
            }

            else if (lst.Count == 5)
            {
                lst[4] = lst[4].Replace(",", ".");
                mi.Weight = Convert.ToDecimal(lst[3].Remove(lst[3].Length - 2).Trim());
                mi.ValNut = null;
                mi.Price = Convert.ToDecimal(lst[4].Remove(lst[4].Length - 3).Trim());
                mi.Code = (lst[0].ToString().Length > 3) ? lst[0].Substring(0, 3) : lst[0].Trim();
                mi.Description = lst[1].Trim();
                mi.Ingredients = lst[2].Trim();
            }

            else if (lst.Count == 4)
            {
                mi.Code = (lst[0].ToString().Length > 3) ? lst[0].Substring(0, 3) : lst[0].Trim();
                mi.Description = lst[1].Trim();
                mi.Weight = Convert.ToDecimal(lst[2].Remove(lst[2].Length - 2).Trim());
                mi.Price = Convert.ToDecimal(lst[3].Remove(lst[3].Length - 3).Trim());
            }

            return mi;
        }
    }
}
