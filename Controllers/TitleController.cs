using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using petdiary.Model;

namespace project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TitleController : ControllerBase
    {
        private readonly ILogger<TitleController> _logger;
        public TitleController(ILogger<TitleController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        public IEnumerable<Title> getAll()
        {
            List<Title> title;
            using (var context = new d4n1ar52gnq7onContext())
            {
                title = context.Titles.OrderBy(t=>t.Titleid).ToList();
            }
            return title;
        }

        [HttpGet("{id}")]
        public Title getById(int id)
        {
            Title t = getAll().FirstOrDefault(i => i.Titleid == id);
            
            return t;
        }

        [HttpPost("create")]
        public object create(Title title)
        {
            try
            {
                using (var context = new d4n1ar52gnq7onContext())
                {
                    context.Titles.Add(title);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
            return Ok("create success ");
        }

        [HttpPut("update")]
        public object update(Title title)
        {
            try
            {
                using (var context = new d4n1ar52gnq7onContext())
                {
                    var std = context.Titles.FirstOrDefault(s => s.Titleid == title.Titleid);
                    std.Name = title.Name;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
            return Ok("update success");
        }
        [HttpDelete("delete/{id}")]
        public object delete(int id)
        {
            try
            {
                using (var context = new d4n1ar52gnq7onContext())
                {
                    var std = context.Titles.FirstOrDefault(s => s.Titleid == id);
                    context.Titles.Remove(std);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
            return Ok("delete success");
        }
    }
}
