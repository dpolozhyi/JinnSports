﻿using PredictorDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinnSports.BLL.Interfaces
{
    public interface IPredictionsService
    {
        void SavePredictions(IEnumerable<PredictionDTO> predictions);
    }
}
