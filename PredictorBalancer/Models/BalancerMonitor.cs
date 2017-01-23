using AutoMapper;
using PredictorDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace PredictorBalancer.Models
{
    public class BalancerMonitor
    {
        private static BalancerMonitor instance;

        public PredictorList Predictors { get; private set; }

        private Task update;
        

        private BalancerMonitor()
        {

        }

        public static BalancerMonitor GetInstance()
        {
            if (instance == null)
            {
                instance = new BalancerMonitor();
                instance.RunUpdate();
                instance.IsAwalable = true;
            }

            return instance;
        }

        public Package Package { get; private set; }
        public bool IsAwalable { get; private set; }

        public void SendIncomingEvents(PackageDTO package)
        {
            Package = Mapper.Map<PackageDTO, Package>(package);

            int predictorsCount = CountAwailablePredictors();
            int size = (package.IncomigEvents.Count() / predictorsCount) + 1;
            int count = 0;

            foreach (Predictor predictor in GetAwailablePredictors())
            {
                predictor.SendIncomingEvents(package.IncomigEvents.Skip(count * size).Take(size));
                count++;
            }
        }

        public void RunUpdate()
        {
            update = new Task(() =>
                {
                    while (true)
                    {
                        UpdateStatus();
                        Thread.Sleep(1000 * 60 * 5); // 5 minute pause;
                    }
                });
            update.Start();
        }

        private void UpdateStatus()
        {
            int count = 0;
            Task[] tasks = new Task[Predictors.GetAll().Count]; 

            foreach (Predictor predictor in Predictors.GetAll())
            {
                tasks[count] = Task.Factory.StartNew(() => predictor.UpdateStatus());
                count++;
            }

            Task.WaitAll(tasks);
        }

        private int CountAwailablePredictors()
        {
            return Predictors.GetAll().Where(p => p.CurrentStatus == Predictor.Status.Awailable).Count();
        }

        private IEnumerable<Predictor> GetAwailablePredictors()
        {
            return Predictors.GetAll().Where(p => p.CurrentStatus == Predictor.Status.Awailable);
        }
    }
}