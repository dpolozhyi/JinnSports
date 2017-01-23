using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PredictorBalancer.Models
{
    public class PredictorList
    {
        private IList<Predictor> predictors;

        public PredictorList()
        {
            predictors = new List<Predictor>();
        }

        public void Add(string baseUrl, string controllerUrn, int timeoutSec)
        {
            int id = 0;

            if (predictors.Count > 0)
            {
                id = predictors.Max(p => p.Id) + 1;
            }

            Predictor predictor = new Predictor(baseUrl, controllerUrn, timeoutSec);
            predictor.Id = id;

            predictors.Add(predictor);
        }

        public ICollection<Predictor> GetAll()
        {
            return predictors;
        }

        public Predictor GetById(int id)
        {
            return predictors.FirstOrDefault(p => p.Id == id);
        }

        public void Delete(int id)
        {
            Predictor predictor = predictors.FirstOrDefault(p => p.Id == id);
            predictors.Remove(predictor);
        }
    }
}