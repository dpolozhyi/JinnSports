﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JinnSports.BLL.Interfaces;
using PredictorDTO;
using JinnSports.BLL.Service;

namespace JinnSports.WEB.Areas.Admin.Controllers
{
    public class PredictionsController : ApiController
    {
        private readonly PredictionsService predictionsService;

        public PredictionsController(PredictionsService predictionsService)
        {
            this.predictionsService = predictionsService;
        }

        public void PostPredictions(IEnumerable<PredictionDTO> predictions)
        {
            predictionsService.SavePredictions(predictions);
        }
    }
}
