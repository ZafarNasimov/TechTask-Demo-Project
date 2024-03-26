using ProductsWeb.Models;

public class ProductsService
{
    private static readonly HttpClient _httpClient;
    private static readonly string _baseUrl;

    static ProductsService()
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

        _baseUrl = configuration.GetConnectionString("DefaultConnection");
        _httpClient = new HttpClient();
    }

    public async Task<IEnumerable<ProductDto>> GetProductsAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<IEnumerable<ProductDto>>($"{_baseUrl}api/products");
        return response ?? Enumerable.Empty<ProductDto>();
    }

    public async Task<IEnumerable<ProductDto>> GetProductsByFilterAsync(string filter = "")
    {
        var response = await _httpClient.GetFromJsonAsync<IEnumerable<ProductDto>>($"{_baseUrl}api/products/filter?filter={filter}");
        return response ?? Enumerable.Empty<ProductDto>();
    }

    public async Task<ProductDto> GetProductByIdAsync(Guid id)
    {
        var response = await _httpClient.GetFromJsonAsync<ProductDto>($"{_baseUrl}api/products/{id}");
        return response ?? throw new Exception("Product not found");
    }

    public async Task<ProductDto> AddProductAsync(ProductDto productDto)
    {
        var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}api/products", productDto);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ProductDto>() ?? throw new Exception("Failed to add product");
    }

    public async Task UpdateProductAsync(ProductDto productDto)
    {
        var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}api/products", productDto);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteProductAsync(Guid id)
    {
        var response = await _httpClient.DeleteAsync($"{_baseUrl}api/products/{id}");
        response.EnsureSuccessStatusCode();
        if (response.Content != null)
        {
            await response.Content.ReadAsStringAsync();
        }
    }
}
