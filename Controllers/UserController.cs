using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using petdiary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }
        /*
        [HttpGet]
        public IEnumerable<User> getAll()
        {
            List<User> user;
            using (var context = new d4n1ar52gnq7onContext())
            {
                user = context.Users.ToList();
            }
            return user;
        }
        */

        [HttpGet]
        public List<User> getAll()
        {
            List<User> user;
            using (var context = new d4n1ar52gnq7onContext())
            {
                user = context.Users.OrderBy(u => u.Userid).Select(m => new User
                {
                    Userid = m.Userid,
                    Username = m.Username,
                    Password = m.Password,
                    Firstname = m.Firstname,
                    Lastname = m.Lastname,
                    Email = m.Email,
                    Alias = m.Alias,
                    RegisDate = m.RegisDate,
                    Title = context.Titles.Where(t => t.Titleid == m.Titleid).Select(h => new Title
                    {
                        Titleid = h.Titleid,
                        Name = h.Name,
                    }).ToList()[0],
                    Diaries = context.Diaries.Where(d => d.Userid == m.Userid).Select(f => new Diary
                    {
                        Diaryid = f.Diaryid,
                        Title = f.Title,
                        Detail = f.Detail,
                        AddDate = f.AddDate,
                        Path = f.Path,
                    }).ToList(),
                }).ToList();
            }
            return user;
        }
        

        [HttpGet("{id}")]
        public User getById(int id)
        {
            User u = getAll().FirstOrDefault(i => i.Userid == id);

            return u;
        }

        [HttpPost("login")]
        public object login(User user)
        {
            User u = getAll().FirstOrDefault(i => i.Username == user.Username && i.Password == user.Password);

            if (u == null)
                return NotFound();

            var a = new
            {
                Userid = u.Userid,
                Title = u.Title.Name,
                Firstname = u.Firstname,
                Lastname = u.Lastname,
                Email = u.Email,
                Alias = u.Alias,
                RegisDate = u.RegisDate
            };
           
            return a;
        }

        [HttpPost("register")]
        public object create(User user)
        {
            User u = getAll().FirstOrDefault(i => i.Username == user.Username || i.Email == user.Email);

            if (u != null)
                return NotFound("Username or Email is already used");

            var t = DateTime.Now;
            user.RegisDate = new DateTime(t.Year, t.Month, t.Day, t.Hour + 7, t.Minute, t.Second, t.Millisecond);

            try
            {
                using (var context = new d4n1ar52gnq7onContext())
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return NotFound("create failed " + e.Message);
            }

            List<User> lu = getAll();
            int l = lu.Count();
            u = lu[l-1];

            return Ok(u);
        }

        [HttpPut("update")]
        public object update(User user)
        {
            try
            {
                using (var context = new d4n1ar52gnq7onContext())
                {
                    var std = context.Users.FirstOrDefault(s => s.Userid == user.Userid);
                    std.Password = user.Password;
                    std.Firstname = user.Firstname;
                    std.Lastname = user.Lastname;
                    std.Alias = std.Alias;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
            return Ok("Update Profile Successful");
        }
        [HttpDelete("delete/{id}")]
        public object delete(int id)
        {
            try
            {
                using (var context = new d4n1ar52gnq7onContext())
                {
                    var std = context.Users.FirstOrDefault(s => s.Userid == id);
                    context.Users.Remove(std);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
            return Ok("Remove User Successful");
        }
    }
}
