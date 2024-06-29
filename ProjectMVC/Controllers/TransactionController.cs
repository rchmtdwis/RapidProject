using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectMVC.ViewModels;
using ProjectMVC.Models;
using Newtonsoft.Json.Linq;

namespace ProjectMVC.Controllers
{
    public class TransactionController : Controller
    {
        private readonly HttpClient _httpClient;

        public TransactionController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ProductClient");
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("/api/transaction");
            if (!response.IsSuccessStatusCode)
            {
                // Handle error response
                return View("Error");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<List<TransactionViewModel>>(jsonResponse);

            return View(products);
        }

        [HttpGet("/Transaction/{id}")]
        [ActionName("Details")]
        public async Task<IActionResult> ProductDetails(int id)
        {
            var response = await _httpClient.GetAsync($"api/Transaction/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var transaction = JsonConvert.DeserializeObject<TransactionViewModel>(jsonResponse);

            return View(transaction);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTransactType(int id)
        {
            try
            {
                var response = await _httpClient.PutAsync($"api/Transaction/{id}", null);
                if (!response.IsSuccessStatusCode)
                {
                    return View("Error");
                }

                return RedirectToAction("Details", new { id = id });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
