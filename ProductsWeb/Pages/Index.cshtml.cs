using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SharedLib;
using System;

public class IndexModel : PageModel
{
    public IndexModel() 
    {
        
    }

    public IList<Product> Products { get; set; }

    public async Task OnGetAsync()
    {
        Products = (IList<Product>)await ProductsService.GetProductsAsync();
    }

    public async Task<IActionResult> OnPostAsync(string filter)
    {
        if (!string.IsNullOrEmpty(filter))
        {
            Products = (IList<Product>)await ProductsService.GetProductsByFilterAsync(filter);
        }
        else
        {
            Products = (IList<Product>)await ProductsService.GetProductsAsync();
        }
        
        return Page();
    }

    public async Task<IActionResult> OnPostDelete(Guid id)
    {
        await ProductsService.DeleteProductAsync(id);
        return RedirectToPage("/Index");
    }

}
