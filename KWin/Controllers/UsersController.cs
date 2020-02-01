using KWin.Models.Users;
using KWin.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KWin.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddBalanceToUsers()
        {
            var users = await usersService.GetAllUsersAsync();
            var userViewModels = new List<UserViewModel>();

            foreach (var user in users)
            {
                var userViewModel = new UserViewModel
                {
                    Username = user.UserName
                };

                userViewModels.Add(userViewModel);
            }

            return this.View(userViewModels);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddBalanceToUsers(string username, string amount)
        {
            await this.usersService.IncreaseUserBalanceByUsernameAsync(username, Decimal.Parse(amount));

            return this.Redirect("/Home/Index");
        }
    }
}
