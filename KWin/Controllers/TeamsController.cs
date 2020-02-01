using KWin.Models.Teams;
using KWin.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KWin.Controllers
{
    public class TeamsController : Controller
    {
        private readonly ITeamsService teamsService;

        public TeamsController(ITeamsService teamsService)
        {
            this.teamsService = teamsService;
        }

        [Authorize]
        public async Task<IActionResult> AllTeams()
        {
            var teams = await teamsService.GetAllTeamsAsync();
            var teamsForView = new List<TeamViewModel>();

            foreach (var team in teams)
            {
                var teamViewModel = new TeamViewModel
                {
                    Name = team.Name,
                    League = team.League
                };

                teamsForView.Add(teamViewModel);
            }

            return this.View(teamsForView);
        }
    }
}
