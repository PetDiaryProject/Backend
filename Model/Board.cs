using System;
using System.Collections.Generic;

#nullable disable

namespace petdiary.Model
{
    public partial class Board
    {
        public Board()
        {
            Comments = new HashSet<Comment>();
        }

        public int Boardid { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public int Typeid { get; set; }
        public bool IsShow { get; set; }
        public DateTime? AddDate { get; set; }

        public virtual Type Type { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
