using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using ProjectMVC.DTOs;
using ProjectMVC.ViewModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

public class ProductController : Controller
{
    private readonly HttpClient _httpClient;

    public ProductController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("ProductClient");
    }

    public async Task<IActionResult> Index()
    {
        var response = await _httpClient.GetAsync("api/Product");
        if (!response.IsSuccessStatusCode)
        {
            // Handle error response
            return View("Error");
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var products = JsonConvert.DeserializeObject<List<ProductViewModel>>(jsonResponse);

        return View(products);
    }
    
    [HttpGet("/Product/{id}")]
    [ActionName("Details")]
    public async Task<IActionResult> ProductDetails(int id)
    {
        var response = await _httpClient.GetAsync($"api/Product/{id}");
        if (!response.IsSuccessStatusCode)
        {
            return View("Error");
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var product = JsonConvert.DeserializeObject<ProductViewModel>(jsonResponse);

        return View(product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> BuyProduct(ProductViewModel model)
    {
        // Validate input parameters as needed
        if (model.Quantity <= 0)
        {
            ModelState.AddModelError("Quantity", "Quantity must be greater than zero.");
            return RedirectToAction("ProductDetails", new { id = model.Id });
        }

        // Set TransactionType to "Add"
        model.TransactionType = "Add";

        var createTransactionDTO = new CreateTransactionDTO
        {
            ProductId = model.Id,
            Quantity = model.Quantity,
            TransactionType = model.TransactionType,
        };

        var json = JsonConvert.SerializeObject(createTransactionDTO);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("api/Transaction", content);
        if (!response.IsSuccessStatusCode)
        {
            return View("Error");
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var transaction = JsonConvert.DeserializeObject<TransactionViewModel>(jsonResponse);

        return RedirectToAction("Details", "Transaction", new { id = transaction.TransactionId });
    }

}
