using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FooBlog.Core.Entities;
using FooBlog.Data;
using System.Threading;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;

namespace FooBlog.API.Controllers
{
    public class BlogsController : ODataController
    {
        private readonly FooBlogContext _context;

        public BlogsController(FooBlogContext context)
        {
            _context = context;
        }

        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_context.Blogs.Include(b => b.Comments));
        }

        [EnableQuery]
        public IActionResult Get(int key)
        {
            var blog = _context.Blogs.Include(b => b.Comments).FirstOrDefault(p => p.ID == key);
            if (blog == null)
            {
                return NotFound($"Not found product with id = {key}");
            }

            return Ok(blog);
        }

        public async Task<IActionResult> Post([FromBody] Blog blog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync();

            return Created(blog);
        }

        public async Task<IActionResult> Put([FromODataUri] int key, [FromBody] Blog blog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Entry(blog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(key))
                {
                    return NotFound();
                }

                throw;
            }

            return Updated(blog);
        }

        public async Task<IActionResult> Delete([FromODataUri] int key)
        {
            var blog = await _context.Blogs.FindAsync(key);

            if (blog == null)
            {
                return NotFound();
            }

            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
            return NoContent();
        }



        [ODataRoute("blogs({key})/comments")]
        public async Task<IActionResult> PostComment([FromODataUri] int key, [FromBody] Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var blog = _context.Blogs.Include(b => b.Comments).Where(b => b.ID == key).FirstOrDefault();

            blog.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return Updated(blog);
        }

        [Route("{key}/comments")]
        public ActionResult<IEnumerable<Comment>> GetComments([FromODataUri] int key)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_context.Blogs.Find(key).Comments);
        }

        private bool Exists(int id)
        {
            return _context.Blogs.Any(b => b.ID == id);
        }
    }
}
