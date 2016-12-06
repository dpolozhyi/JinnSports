﻿using System.Collections.Generic;

namespace JinnSports.WEB.Views.ViewModels
{
    public class TeamViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IList<ResultViewModel> Result { get; set; }
    }
}