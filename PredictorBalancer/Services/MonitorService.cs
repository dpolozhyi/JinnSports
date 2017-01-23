using AutoMapper;
using PredictorBalancer.Models;
using PredictorBalancer.ViewModels;
using PredictorDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PredictorBalancer.Services
{
    public class MonitorService
    {
        private BalancerMonitor monitor;

        public IEnumerable<PredictorViewModel> GetPredicors()
        {
            monitor = BalancerMonitor.GetInstance();
            IEnumerable<Predictor> predictors = monitor.Predictors.GetAll();

            IEnumerable<PredictorViewModel> viewPredictors = Mapper.Map<IEnumerable<Predictor>, IEnumerable<PredictorViewModel>>(predictors);
            return viewPredictors;
        }

        public void SendPredictions(IEnumerable<PredictionDTO> predictions)
        {
            monitor = BalancerMonitor.GetInstance();
            ApiConnection<IEnumerable<PredictionDTO>> connection = new ApiConnection<IEnumerable<PredictionDTO>>(monitor.Package.CallBackURL, 
                                                                            monitor.Package.CallBackController, monitor.Package.CallBackTimeout);
            connection.Send(predictions);
        }
    }
}