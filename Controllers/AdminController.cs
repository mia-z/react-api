using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ShopAPI.API.Entities;
using ShopAPI.API.Services;

namespace ShopAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ShopContext _context;

        public AdminController(ShopContext context)
        {
            _context = context;
        }

        [HttpPost("category/create/")]
        public async Task<ActionResult> CreateCategory()
        {
            try
            {
                var reader = new StreamReader(Request.Body, Encoding.UTF8);
                var s = await reader.ReadToEndAsync();
                var c = JsonConvert.DeserializeObject<Category>(s);
                var img = new ByteImage
                {
                    Data = c.Data,
                    Name = $"{c.Name}-image"
                };
                await _context.ByteImages.AddAsync(img);
                await _context.SaveChangesAsync();
                var cat = c;
                cat.ImageId = img.id;
                await _context.Categories.AddAsync(cat);
                await _context.SaveChangesAsync();
                reader.Dispose();
                return CreatedAtAction("CreateCategory", cat);
            } catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500);
            }
        }

        [HttpPost("category/remove/{id}")]
        public async Task<ActionResult> RemoveCategory(int id)
        {
            try
            {
                var del = _context.Categories
                    .Where(x => x.id == id)
                    .FirstOrDefault();
                if (del == null)
                    return NotFound();
                else
                {
                    var img = await _context.ByteImages
                        .Where(x => x.id == id)
                        .FirstOrDefaultAsync();
                    _context.ByteImages.Remove(img);
                    _context.Categories
                        .Remove(del);
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500);
            }
        }

        [HttpPost("brand/create/")]
        public async Task<ActionResult> CreateBrand()
        {
            try
            {
                var reader = new StreamReader(Request.Body, Encoding.UTF8);
                var s = await reader.ReadToEndAsync();
                var b = JsonConvert.DeserializeObject<Brand>(s);
                var img = new ByteImage
                {
                    Data = b.Data,
                    Name = $"{b.Name}-image{DateTime.UtcNow.Millisecond}"
                };
                await _context.ByteImages.AddAsync(img);
                await _context.SaveChangesAsync();
                var brand = b;
                brand.ImageId = img.id;
                await _context.Brands.AddAsync(brand);
                await _context.SaveChangesAsync();
                reader.Dispose();
                return CreatedAtAction("CreateBrand", brand);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500);
            }
        }

        [HttpPost("brand/remove/{id}")]
        public async Task<ActionResult> RemoveBrand(int id)
        {
            try
            {
                var del = _context.Brands
                    .Where(x => x.id == id)
                    .FirstOrDefault();
                if (del == null)
                    return NotFound();
                else
                {
                    var img = await _context.ByteImages
                        .Where(x => x.id == id)
                        .FirstOrDefaultAsync();
                    _context.ByteImages.Remove(img);
                    _context.Brands
                        .Remove(del);
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500);
            }
        }

        [HttpPost("product/add/")]
        public async Task<ActionResult> AddProduct()
        {
            try
            {
                var reader = new StreamReader(Request.Body, Encoding.UTF8);
                var s = await reader.ReadToEndAsync();
                var p = JsonConvert.DeserializeObject<Product>(s);
                await _context.Products.AddAsync(p);
                await _context.SaveChangesAsync();
                foreach (var t in p.Tags)
                    await _context.Tags.AddAsync(new Tag
                    {
                        Name = t,
                        ProductId = p.id
                    });
                await _context.ByteImages.AddAsync(new ByteImage
                {
                    ProductId = p.id,
                    Data = p.Data,
                    Name = $"{p.Name}-image"
                });
                await _context.SaveChangesAsync();
                reader.Dispose();
                return CreatedAtAction("AddProduct", p);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500);
            }
        }

        [HttpPost("product/remove/{id}")]
        public async Task<ActionResult> RemoveProduct(int id)
        {
            try
            {
                var del = _context.Products
                    .Where(x => x.id == id)
                    .FirstOrDefault();
                if (del == null)
                    return NotFound();
                else
                {
                    var tags = await _context.Tags
                        .Where(x => x.id == id)
                        .ToListAsync();
                    foreach (var t in tags)
                        _context.Tags.RemoveRange(tags);
                    var img = await _context.ByteImages
                        .Where(x => x.id == id)
                        .FirstOrDefaultAsync();
                    _context.ByteImages.Remove(img);
                    _context.Products
                        .Remove(del);
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500);
            }
        }
    }
}