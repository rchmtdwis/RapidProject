using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectMVC.ViewModels;
using System.Collections.Generic;
using System.Net.Http;
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
}
