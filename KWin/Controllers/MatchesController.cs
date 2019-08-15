using KWin.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KWin.Controllers
{
    public class MatchesController : Controller
    {
        public MatchesController(IMatchesService matchesService)
        {

        }

        public IActionResult AllMatches()
        {
            return this.View();
        }
    }
}
