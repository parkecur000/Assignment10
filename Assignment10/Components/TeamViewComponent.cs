using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment10.Components
{
    public class TeamViewComponent : ViewComponent
    {
        private BowlingLeagueContext _context;
        public TeamViewComponent (BowlingLeagueContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            
            return View(_context.Teams
                //.Select(x=> x.TeamName)
                .Distinct()
                .OrderBy(x=> x)
                //.ToList()
                );
        }
    }
}
