using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopAPI.API.Entities;
using ShopAPI.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ShopContext _context;
        public ProductsController(ShopContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAll()
        {
            return Ok(await _context.Products
                .ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            return Ok(await _context.Products
                .Where(x => x.id == id)
                .FirstOrDefaultAsync());
        }

        [HttpGet("brands/{brand}")]
        async public Task<ActionResult<List<Product>>> GetByBrand(string brand)
        {
            var b = await _context.Brands
                .Where(x => x.Name.ToLower() == brand.ToLower())
                .FirstOrDefaultAsync();
            if (b == null)
                return NotFound();
            else
                return Ok(await _context.Products
                    .Where(x => x.BrandId == b.id)
                    .ToListAsync());
        }

        [HttpGet("category/{category}")]
        async public Task<ActionResult<List<Product>>> GetByCategory(string category)
        {
            var c = await _context.Categories
                .Where(x => x.Name.ToLower() == category.ToLower())
                .FirstOrDefaultAsync();
            if (c == null)
                return NotFound();
            else 
                return Ok(await _context.Products
                    .Where(x => x.CategoryId == c.id)
                    .ToListAsync());
        }
    }
}
