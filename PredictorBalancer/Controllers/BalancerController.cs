using PredictorBalancer.Models;
using PredictorDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PredictorBalancer.Controllers
{
    public class BalancerController : ApiController
    {
        BalancerMonitor instance;

        public void Post([FromBody]PackageDTO package)
        {
            instance = BalancerMonitor.GetInstance();
            instance.SendIncomingEvents(package);
        }
    }
}
