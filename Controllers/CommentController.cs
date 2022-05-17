using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using petdiary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace petdiary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ILogger<CommentController> _logger;
        public CommentController(ILogger<CommentController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        public IEnumerable<Comment> GetAll()
        {
            List<Comment> comment;
            using (var context = new d4n1ar52gnq7onContext())
            {
                comment = context.Comments.OrderBy(i => i.Commentid).Select(m => new Comment
                {
                    Commentid = m.Commentid,
                    Userid = m.Userid,
                    Boardid = m.Boardid,
                    Text = m.Text,
                    IsOwner = m.IsOwner,
                    Path = m.Path,
                    AddDate = m.AddDate,
                    User = context.Users.Where(i => i.Userid == m.Userid).Select(h => new User
                    {
                        Userid = h.Userid,
                        Username = h.Username,
                        Password = h.Password,
                        Firstname = h.Firstname,
                        Lastname = h.Lastname,
                        Email = h.Email,
                        Alias = h.Alias,
                        RegisDate = h.RegisDate,
                        Title = context.Titles.Where(t => t.Titleid == h.Titleid).Select(k => new Title
                        {
                            Titleid = k.Titleid,
                            Name = k.Name,
                        }).ToList()[0],
                    }).ToList()[0],
                }).ToList();

                return comment;
            }
        }

        [HttpGet("{id}")]
        public object Get(int id)
        {
            Comment c = GetAll().FirstOrDefault(i => i.Commentid == id);

            return c;
        }

        [HttpGet("byBoard/{id}")]
        public object GetByBoard(int id)
        {
            List<Comment> c = GetAll().Where(i => i.Boardid == id).OrderBy(i=>i.Commentid).ToList();

            return c;
        }

        [HttpGet("byUser/{id}")]
        public object GeyByUser(int id)
        {
            List<Comment> c = GetAll().Where(i => i.Userid == id).ToList();

            return c;
        }


        [HttpPost("create")]
        public object Post(Comment comment)
        {

            var t = DateTime.Now;
            comment.AddDate = new DateTime(t.Year, t.Month, t.Day, t.Hour + 7, t.Minute, t.Second, t.Millisecond);
            comment.Path = "image/comment/";
            try
            {
                using (var context = new d4n1ar52gnq7onContext())
                {
                    context.Comments.Add(comment);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
            List<Comment> c = GetAll().ToList();
            int l = c.Count();
            Comment c1 = c[l - 1];

            return Ok (c1);
        }

        [HttpPut("update")]
        public object update(Comment comment)
        {
            try
            {
                using (var context = new d4n1ar52gnq7onContext())
                {
                    var std = context.Comments.FirstOrDefault(s => s.Commentid == comment.Commentid);
                    std.Text = comment.Text;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
            return Ok("update Sucessful");
        }

        [HttpDelete("delete/{id}")]
        public object delete(int id)
        {
            try
            {
                using (var context = new d4n1ar52gnq7onContext())
                {
                    var std = context.Comments.FirstOrDefault(s => s.Commentid == id);
                    context.Comments.Remove(std);
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
