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
    [ActionName("Delete")]
    public async Task<IActionResult> EditProduct(int id)
    {
        var response = await _httpClient.GetAsync($"api/product/{id}");
        if (!response.IsSuccessStatusCode)
        {
            return View("Error");
        }

        var jsonRespone =await response.Content.ReadAsStringAsync();
        var product = JsonConvert.DeserializeObject<ProductViewModel>(jsonRespone);
        return View(product);
    }

    [HttpPost]
    [ActionName("Delete")]
    public async Task<IActionResult> DeleteProduct(int ProductId)
    {
        try
        {
            // Send HTTP DELETE request to delete the product
            var response = await _httpClient.DeleteAsync($"api/product/{ProductId}");

            if (!response.IsSuccessStatusCode)
            {
                // Handle error response
                return View("Error");
            }

            // Redirect to Index action after successful deletion
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            // Handle exceptions and return an error view or message
            return View("Error");
        }
    }
}
