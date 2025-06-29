using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsApi.Data;
using ProductsApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace ProductsApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize] // Suojaa kaikki metodit
	public class ProductsController : ControllerBase
	{
		private readonly AppDbContext _context;

		public ProductsController(AppDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
		{
			return await _context.Products.ToListAsync();
		}

		[HttpPost]
		public async Task<ActionResult<Product>> CreateProduct(Product product)
		{
			_context.Products.Add(product);
			await _context.SaveChangesAsync();
			return CreatedAtAction(nameof(GetProducts), new { id = product.Id }, product);
		}
	}
}
