using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ToDoAppNTierBLL.Interfaces;
using ToDoAppNTierDTO;
using ToDoAppNTierUI.ActionFilters;
using ToDoAppNTierUI.Extensions;
using ToDoAppNTierUI.Models;

namespace ToDoAppNTierUI.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IToDoService _toDoService;

        public HomeController(ILogger<HomeController> logger, IToDoService toDoService)
        {
            _logger = logger;
            _toDoService = toDoService;
        }

        [LoggedUser]
        public async Task<IActionResult> Index()
        {
            int id = int.Parse(HttpContext.Session.GetString("userId"));
            var response = await _toDoService.GetFilteredList(x => x.UserId == id);

            return View(response.Data);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Create()
        {
            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "Regular", Value = "Regular", Selected = true });
            items.Add(new SelectListItem { Text = "Urgent", Value = "Urgent" });
            items.Add(new SelectListItem { Text = "Medium", Value = "Medium" });
            ViewBag.Priorty = items;

            return View(new ToDoCreateDto());
        }
        [HttpPost]
        public async Task<IActionResult> Create(ToDoCreateDto model)
        {
            model.UserId =int.Parse(HttpContext.Session.GetString("userId"));
            var response = await _toDoService.CreateAsync(model);
            return this.ResponseRedirectToAction(response, "Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            var response = await _toDoService.GetByIdAsync<ToDoUpdateDto>(id);

            return this.ResponseView(response);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ToDoUpdateDto model)
        {
            var response = await _toDoService.Update(model);

            return this.ResponseRedirectToAction(response,"Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response=await _toDoService.Remove(id);
            return this.ResponseRedirectToAction(response, "Index");
        }
        public async Task<IActionResult> Completed()
        {
            var response = await _toDoService.GetFilteredList(x => x.IsCompleted ==true && x.UserId == int.Parse(HttpContext.Session.GetString("userId")));
            return View(response.Data);
        }

        public async Task<IActionResult> Ongoing()
        {
            var response = await _toDoService.GetFilteredList(x => x.IsStarted==true && x.IsCompleted==false && x.UserId == int.Parse(HttpContext.Session.GetString("userId")));
            return View(response.Data);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult NotFound(int code)
        {
            return View();
        }
    }
}
