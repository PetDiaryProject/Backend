using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using petdiary.Model;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace petdiary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeController : ControllerBase
    {
        private readonly ILogger<TypeController> _logger;
        public TypeController(ILogger<TypeController> logger)
        {
            _logger = logger;
        }

        // GET: api/<TypeController>
        [HttpGet]
        public IEnumerable<Model.Type> GetAll()
        {
            List<Model.Type> type;
            using (var context = new d4n1ar52gnq7onContext())
            {
                type = context.Types.OrderBy(t => t.Typeid).ToList();
            }
            return type;
        }

        // GET api/<TypeController>/5
        [HttpGet("{id}")]
        public Model.Type Get(int id)
        {
            Model.Type t = GetAll().FirstOrDefault(i => i.Typeid == id);

            return t;
        }

        // POST api/<TypeController>
        [HttpPost("create")]
        public object Post( Model.Type type)
        {
            try
            {
                using (var context = new d4n1ar52gnq7onContext())
                {
                    context.Types.Add(type);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
            return Ok("create successful");
        }

        // PUT api/<TypeController>/5
        [HttpPut("update")]
        public object update(Model.Type type)
        {
            try
            {
                using (var context = new d4n1ar52gnq7onContext())
                {
                    var std = context.Types.FirstOrDefault(s => s.Typeid == type.Typeid);
                    std.Name = type.Name;
                    std.NameEng = type.NameEng;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
            return Ok("update Sucessful");
        }

        // DELETE api/<TypeController>/5
        [HttpDelete("delete/{id}")]
        public object delete(int id)
        {
            try
            {
                using (var context = new d4n1ar52gnq7onContext())
                {
                    var std = context.Types.FirstOrDefault(s => s.Typeid == id);
                    context.Types.Remove(std);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
            return Ok("delete sucessful");
        }
    }
}
