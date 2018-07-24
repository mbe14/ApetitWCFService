using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using ApetitWCFService.Model;

namespace ApetitWCFService
{
	[ServiceContract]
	public interface IService1
    {
		[OperationContract, WebGet]
        List<MenuData> GetApetitData();
    }
}
