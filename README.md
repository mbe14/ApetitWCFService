# ApetitWCFService
Self hosted, simple WCF service that pulls menu data from catering site

## Getting Started
  This Project targets .NET Framework 4.7 but if you want to compile using Visual Studio with an older .NET Framework version, you can change it to meet your requirements. Tested with .NET Framework 4.5.2.
  
  After targeting a different .NET Framework version, make sure to update the HtmlAgilityPack refferences and you are ready to go.
  
  Before running the project, please make sure to start Visual Studio in administrator mode.
  
  
  ## What it does
  
  This project starts a WCF service hosted in a console application.
  
  The service address is declared as following:
    
    string baseAddress = "http://" + Environment.MachineName + ":8000/Service1";
    
   The path of the method to be invoked is:
   
    http://localhost:8000/Service1/GetApetitData
    
   When invoked, this service will scrape the daily menu data from [Apetit Catering](www.apetit-catering.ro) using [Html Agility Pack](http://html-agility-pack.net/) and apply the retrieved and processed response, on the following model:
   
     public string Code { get; set; }
     public decimal Price { get; set; }
     public string Description { get; set; }
     public decimal Weight { get; set; }
     public string ValNut { get; set; }
     public string Ingredients { get; set; }
     
   In order to catch possible errors, a log file is created and can be viewed at:
     
     AppDomain.CurrentDomain.BaseDirectory + "/LogFile.txt"
