using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Http;
using Newtonsoft.Json;
using ProjectMVC.Models;
using ProjectMVC.ViewModels;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;

namespace ProjectMVC.Controllers
{
    public class HomeController : Controller
    {

        private HttpClient _httpClient;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ProductClient");

        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("/api/product");
            if (!response.IsSuccessStatusCode)
            {
                // Handle error response
                return View("Error");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<List<Product>>(jsonResponse);

            return View(products);
        }

        
    }
}
