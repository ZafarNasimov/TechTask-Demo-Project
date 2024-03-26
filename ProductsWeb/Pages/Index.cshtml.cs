using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductsWeb.Models;

public class IndexModel : PageModel
{
    private readonly ProductsService _productsService;

    public IndexModel(ProductsService productsService)
    {
        _productsService = productsService;
    }

    public IList<ProductDto> Products { get; set; }

    public async Task OnGetAsync()
    {
        Products = (IList<ProductDto>)await _productsService.GetProductsAsync();
    }

    public async Task<IActionResult> OnPostAsync(string filter)
    {
        if (!string.IsNullOrEmpty(filter))
        {
            Products = (IList<ProductDto>)await _productsService.GetProductsByFilterAsync(filter);
        }
        else
        {
            Products = (IList<ProductDto>)await _productsService.GetProductsAsync();
        }
        
        return Page();
    }

    public async Task<IActionResult> OnPostDelete(Guid id)
    {
        await _productsService.DeleteProductAsync(id);
        return RedirectToPage("/Index");
    }

}
