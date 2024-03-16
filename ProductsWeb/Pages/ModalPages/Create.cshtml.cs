using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SharedLib;

namespace ProductsWeb.Pages.ModelPages
{
    public class CreateModel : PageModel
    {
        public async Task<IActionResult> OnPost()
        {
            var product = new Product();

            string name = Request.Form["Name"];
            string description = Request.Form["Description"];

            product.Id = Guid.NewGuid();
            product.Name = name;
            product.Description = description;

            try
            {
                await ProductsService.AddProductAsync(product);
            }
            catch (Exception)
            {
                return RedirectToPage("/Error");
            }

            return RedirectToPage("/Index");
        }

    }
}
