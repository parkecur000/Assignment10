using Assignment10.Models;
using Assignment10.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment10.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private BowlingLeagueContext _context { get; set; }

        public HomeController(ILogger<HomeController> logger, BowlingLeagueContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index(long? teamId, string teamName, int pageNum = 0)
        {
            int pageSize = 5;
            return View(new IndexViewModel
            {
                Bowlers = (_context.Bowlers
                .Where(b => b.TeamId == teamId || teamId == null)
                .OrderBy(b => b.BowlerFirstName)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize)
                .ToList()),
                //.FromSqlInterpolated($"SELECT * FROM Bowlers WHERE TeamID = {teamId} OR {teamId} IS NULL")
                //.ToList());

                PageNumberingInfo = new PageNumberingInfo
                {
                    NumItemsPerPage = pageSize,
                    CurrentPage = pageNum,

                    //If no team has been selected, then get the full count. Otherwise, only count the number of
                    //bowlers from the team that has been selected.
                    TotalNumItems = (teamId == null ? _context.Bowlers.Count() :
                        _context.Bowlers.Where(b => b.TeamId == teamId).Count())

                },

                TeamName = teamName
            });
                
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
