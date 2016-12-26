﻿using System.Web.Mvc;
using JinnSports.BLL.Interfaces;
using JinnSports.BLL.Service;
using JinnSports.BLL.Dtos;

namespace JinnSports.WEB.Controllers
{
    public class TeamDetailsController : Controller
    {
        private ITeamService teamService;
        public TeamDetailsController()
        {
            this.teamService = new TeamService();
        }

        public ActionResult Details(int id = 0)
        {
            if (id != 0)
            {
                TeamDto team = this.teamService.GetTeamById(id);
                return this.View(team);
            }
            else
            {
                return this.HttpNotFound();
            }
        }
    }
}