using System;
using System.Collections.Generic;

#nullable disable

namespace petdiary.Model
{
    public partial class Comment
    {
        public int Commentid { get; set; }
        public int Userid { get; set; }
        public int Boardid { get; set; }
        public string Text { get; set; }
        public bool IsOwner { get; set; }
        public string Path { get; set; }
        public DateTime? AddDate { get; set; }

        public virtual Board Board { get; set; }
        public virtual User User { get; set; }
    }
}
