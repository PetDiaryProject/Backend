using System;
using System.Collections.Generic;

#nullable disable

namespace petdiary.Model
{
    public partial class Title
    {
        public Title()
        {
            Users = new HashSet<User>();
        }

        public int Titleid { get; set; }
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
