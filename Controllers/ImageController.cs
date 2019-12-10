using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopAPI.API.Entities;
using ShopAPI.API.Services;

namespace ShopAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly ShopContext _context;

        public ImageController(ShopContext context)
        {
            _context = context;
        }

        [DisableRequestSizeLimit]
        [HttpPost]
        public async Task<ActionResult> AddNewImage()
        {
            using (var reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                try
                {
                    var s = await reader.ReadToEndAsync();
                    var b = JsonConvert.DeserializeObject<ByteImage>(s);
                    await _context.ByteImages.AddAsync(b);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return StatusCode(500);
                }
            }          
        }

        [HttpPost]
        [Route("{id}")]
        public async Task<ActionResult> DeleteImage(int id)
        {
            try
            {
                var del = _context.ByteImages
                    .Where(x => x.id == id)
                    .FirstOrDefault();
                if (del == null)
                    return NotFound();
                else
                {
                    _context.ByteImages
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