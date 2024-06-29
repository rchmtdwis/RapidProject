using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

    [HttpGet("/Product/Edit/{id}")]
    [ActionName("Edit")]
    public async Task<IActionResult> EditProduct(int id)
    {
        var response = await _httpClient.GetAsync($"api/product/{id}");
        if (!response.IsSuccessStatusCode)
        {
            return View("Error");
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var product = JsonConvert.DeserializeObject<ProductViewModel>(jsonResponse);
        return View(product);
    }

    [HttpPost("/Product/Edit/{id}")]
    [ActionName("Edit")]
    public async Task<IActionResult> EditProduct(int id, ProductViewModel product)
    {
        try
        {
            var jsonContent = JsonConvert.SerializeObject(product);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/product/{id}", content);
            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }

            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            // Handle exceptions and return an error view or message
            return View("Error");
        }
    }

    [HttpGet("/Product/Delete/{id}")]
    [ActionName("Delete")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var response = await _httpClient.GetAsync($"api/product/{id}");
        if (!response.IsSuccessStatusCode)
        {
            return View("Error");
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var product = JsonConvert.DeserializeObject<ProductViewModel>(jsonResponse);
        return View(product);
    }

    [HttpPost("/Product/Delete/{id}")]
    [ActionName("Delete")]
    public async Task<IActionResult> DeleteProductConfirmed(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/product/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }

            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            // Handle exceptions and return an error view or message
            return View("Error");
        }
    }
}
