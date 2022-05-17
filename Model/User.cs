using System;
using System.Collections.Generic;

#nullable disable

namespace petdiary.Model
{
    public partial class User
    {
        public User()
        {
            Comments = new HashSet<Comment>();
            Diaries = new HashSet<Diary>();
        }

        public int Userid { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Alias { get; set; }
        public int Titleid { get; set; }
        public DateTime? RegisDate { get; set; }

        public virtual Title Title { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Diary> Diaries { get; set; }
    }
}
