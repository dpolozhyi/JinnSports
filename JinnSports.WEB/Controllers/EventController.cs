﻿using JinnSports.BLL.Dtos;
using JinnSports.BLL.Interfaces;
using JinnSports.BLL.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace JinnSports.WEB.Controllers
{
    public class EventController : Controller
    {
        public ActionResult Index()
        {
            string url = string.Format("/api/Event/LoadEvents");
            return this.View((object)url);
        }

    }
}