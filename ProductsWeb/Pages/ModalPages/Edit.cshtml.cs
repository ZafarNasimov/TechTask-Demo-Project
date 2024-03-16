using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SharedLib;
using System.Diagnostics;

namespace ProductsWeb.Pages.ModelPages
{
    public class EditModel : PageModel
    {
        public Product product = new();

        public async Task<IActionResult> OnGet(Guid id)
        {
            product = await ProductsService.GetProductByIdAsync(id);
            return Page();
        }

        public async Task<IActionResult> OnPost(Guid id)
        {
            string name = Request.Form["Name"];
            string descrpition = Request.Form["Description"];

            product = await ProductsService.GetProductByIdAsync(id);
            
            product.Name = name;
            product.Description = descrpition;
            await ProductsService.UpdateProductAsync(product);

            return RedirectToPage("/Index");
        }
    }
}
