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
    public class BoardController : ControllerBase
    {
        private readonly ILogger<BoardController> _logger;
        public BoardController(ILogger<BoardController> logger)
        {
            _logger = logger;
        }
        // GET: api/<BoardController>
        [HttpGet]
        public IEnumerable<Board> GetAll()
        {
            List<Board> board;
            using (var context = new d4n1ar52gnq7onContext())
            {
                board = context.Boards.OrderBy(i=>i.Boardid).Select(m => new Board
                {
                    Boardid = m.Boardid,
                    Title = m.Title,
                    Detail = m.Detail,
                    Typeid = m.Typeid,
                    IsShow = m.IsShow,
                    AddDate = m.AddDate,
                    Comments = context.Comments.Where(i => i.Boardid == m.Boardid).Select(j => new Comment
                    {
                        Commentid = j.Commentid,
                        Userid = j.Userid,
                        Boardid = j.Boardid,
                        Text = j.Text,
                        IsOwner = j.IsOwner,
                        Path = j.Path,
                        AddDate = j.AddDate,
                    }).ToList(),
                }).ToList();
            }

            foreach(Board b in board)
            {
                Comment c = b.Comments.LastOrDefault();
                List<Comment> c1 = new List<Comment>();
                c1.Add(c);
                b.Comments = c1;
            }
            board.Reverse();
            return board;
        }

        [HttpGet("show")]
        public List<Board> GetAllShow()
        {
            List<Board> board;
            board = GetAll().Where(i => i.IsShow == true).ToList();
            
            return board;
        }

        // GET api/<BoardController>/5
        [HttpGet("{id}")]
        public Board Get(int id)
        {
            Board b = GetAll().FirstOrDefault(i => i.Boardid == id);

            using (var context = new d4n1ar52gnq7onContext())
            {
                b.Comments = context.Comments.Where(i => i.Boardid == id).OrderBy(s => s.Commentid).ToList();
            }

            return b;
        }

        [HttpGet("type/{id}")]
        public IEnumerable<Board> GetByType(int id)
        {
            List<Board> b = GetAll().Where(i => i.Typeid == id).OrderBy(i=>i.Boardid).ToList();
            b.Reverse();
            return b;
        }


        [HttpGet("user/{id}")]
        public List<Board> GetByUser(int id)
        {
            List<Comment> c;
            List<Board> b = new List<Board>();
            using (var context = new d4n1ar52gnq7onContext())
            {
                c = context.Comments.Where(i => i.Userid == id && i.IsOwner == true).OrderBy(i=>i.Boardid).ToList();
            }

            foreach (Comment a in c)
            {
                b.Add(Get(a.Boardid));
            }
            b.Reverse();
            return b;
        }


        // POST api/<BoardController>
        [HttpPost("create")]
        public object Post(Board board)
        {
            var timeZones = TimeZoneInfo.GetSystemTimeZones();
            Console.WriteLine(timeZones);
            var t = DateTime.Now;
            board.AddDate = new DateTime(t.Year,t.Month,t.Day,t.Hour+7,t.Minute,t.Second,t.Millisecond);

            try
            {
                using (var context = new d4n1ar52gnq7onContext())
                {
                    context.Boards.Add(board);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }



            int id = 0;
            List<Board> b = GetAll().ToList();
            id = b[0].Boardid;
            return Ok(id);
        }

        // PUT api/<BoardController>/5
        [HttpPut("update")]
        public object update(Board board)
        {
            try
            {
                using (var context = new d4n1ar52gnq7onContext())
                {
                    var std = context.Boards.FirstOrDefault(s => s.Boardid == board.Boardid);

                    std.Title = board.Title;

                    std.Detail = board.Detail;

                    std.Typeid = board.Typeid;

                    std.IsShow = board.IsShow;

                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
            return Ok("update Sucessful");
        }

        // DELETE api/<BoardController>/5
        [HttpDelete("delete/{id}")]
        public object delete(int id)
        {
            try
            {
                using (var context = new d4n1ar52gnq7onContext())
                {
                    var del = context.Comments.Where(s => s.Boardid == id).ToList();
                    foreach (Comment a in del)
                    {
                        context.Comments.Remove(a);

                    }

                    var std = context.Boards.FirstOrDefault(s => s.Boardid == id);
                    context.Boards.Remove(std);
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
