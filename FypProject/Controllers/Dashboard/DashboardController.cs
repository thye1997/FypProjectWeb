using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FypProject.Base;
using FypProject.Models;
using FypProject.Services;
using FypProject.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FypProject.Controllers
{
    [Authorize(AuthenticationSchemes =authenticationSchemes)]
    public class DashboardController : BasicController
    {
        protected override string pageName { get; set; }
        private readonly DashboardService dashboardService;
        public DashboardController (DashboardService dashboardService)
        {
            this.dashboardService = dashboardService;
        }

        public IActionResult Index()
        {
            return View(dashboardService.RetrieveWebApptDataCount());                         
        }
    }
}
