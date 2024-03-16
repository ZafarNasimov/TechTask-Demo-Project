using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using SharedLib;
using Microsoft.AspNetCore.Mvc;

public static class ProductsService
{
    private static readonly HttpClient _httpClient;
    private static readonly string _connectionString;

    static ProductsService()
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

        _connectionString = configuration.GetConnectionString("DefaultConnection");
        _httpClient = new HttpClient();
    }

    public static async Task<IEnumerable<Product>> GetProductsAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<IEnumerable<Product>>($"{_connectionString}api/products");
        return response ?? Enumerable.Empty<Product>();
    }

    public static async Task<IEnumerable<Product>> GetProductsByFilterAsync(string filter = "")
    {
        var response = await _httpClient.GetFromJsonAsync<IEnumerable<Product>>($"{_connectionString}api/products/filter?filter={filter}");
        return response ?? Enumerable.Empty<Product>();
    }

    public static async Task<Product> GetProductByIdAsync(Guid id)
    {
        var response = await _httpClient.GetFromJsonAsync<Product>($"{_connectionString}api/products/{id}");
        return response ?? throw new Exception("Product not found");
    }

    public static async Task<Product> AddProductAsync(Product product)
    {
        var response = await _httpClient.PostAsJsonAsync($"{_connectionString}api/products", product);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Product>() ?? throw new Exception("Failed to add product");
    }

    public static async Task UpdateProductAsync(Product product)
    {
        var response = await _httpClient.PutAsJsonAsync($"{_connectionString}api/products", product);
        response.EnsureSuccessStatusCode();
    }

    public static async Task DeleteProductAsync(Guid id)
    {
        var response = await _httpClient.DeleteAsync($"{_connectionString}api/products/{id}");
        response.EnsureSuccessStatusCode();
        if (response.Content != null)
        {
            await response.Content.ReadAsStringAsync();
        }
    }
}
