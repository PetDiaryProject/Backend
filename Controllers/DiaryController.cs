using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using petdiary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace petdiary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiaryController : ControllerBase
    {
        private readonly ILogger<DiaryController> _logger;
        public DiaryController(ILogger<DiaryController> logger)
        {
            _logger = logger;
        }

        // GET: api/<DiaryController>
        [HttpGet]
        public IEnumerable<Diary> GetAll()
        {
            List<Diary> diary;
            using (var context = new d4n1ar52gnq7onContext())
            {
                diary = context.Diaries.ToList();
            }
            return diary;
        }

        // GET api/<DiaryController>/5
        [HttpGet("{id}")]
        public Diary Get(int id)
        {
            Diary d = GetAll().FirstOrDefault(i => i.Diaryid == id);

            return d;
        }

        [HttpGet("user/{id}")]
        public IEnumerable<Diary> GetByUser(int id)
        {
            List<Diary> d = GetAll().Where(i => i.Userid == id).OrderBy(i=>i.Diaryid).ToList();
            d.Reverse();
            return d;
        }


        // POST api/<DiaryController>
        [HttpPost("create")]
        public object Post( Diary diary)
        {
            var t = DateTime.Now;
            diary.AddDate = new DateTime(t.Year, t.Month, t.Day, t.Hour + 7, t.Minute, t.Second, t.Millisecond);
            diary.Path = "image/diary/";
            try
            {
                using (var context = new d4n1ar52gnq7onContext())
                {
                    context.Diaries.Add(diary);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

            List<Diary> d = GetAll().ToList();
            int l = d.Count();
            Diary d1 = d[l - 1];

            return Ok(d1);
        }

        // PUT api/<DiaryController>/5
        [HttpPut("update")]
        public object update(Diary diary)
        {
            
            try
            {
                using (var context = new d4n1ar52gnq7onContext())
                {
                    var std = context.Diaries.FirstOrDefault(s => s.Diaryid == diary.Diaryid);
                    std.Title = diary.Title;
                    std.Detail = diary.Detail;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
            return Ok("update Sucessful");
        }

        // DELETE api/<DiaryController>/5
        [HttpDelete("delete/{id}")]
        public object delete(int id)
        {
            try
            {
                using (var context = new d4n1ar52gnq7onContext())
                {
                    var std = context.Diaries.FirstOrDefault(s => s.Diaryid == id);
                    context.Diaries.Remove(std);
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
