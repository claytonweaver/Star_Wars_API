using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StarWars_API_Lab.Models;

namespace StarWars_API_Lab.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }



        
        
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Search(string searchData, string searchCategory)
        {

            string searchUrl = $"{searchCategory}/?search={searchData}";

            var client = new HttpClient();
            client.BaseAddress = new Uri("https://swapi.co/api/");
            var response = await client.GetAsync(searchUrl);
            

            if (searchCategory == "people")
            {
                var result = await response.Content.ReadAsAsync<People>();


                ViewBag.StarWarsThing = result;

                return View("Result");
            }
            else if(searchCategory == "planets")
            {
                var result = await response.Content.ReadAsAsync<Planets>();

                ViewBag.StarWarsThing = result;


                return View("Result");
            }

            return View();

            

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

