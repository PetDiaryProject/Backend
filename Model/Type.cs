using System;
using System.Collections.Generic;

#nullable disable

namespace petdiary.Model
{
    public partial class Type
    {
        public Type()
        {
            Boards = new HashSet<Board>();
        }

        public int Typeid { get; set; }
        public string Name { get; set; }
        public string NameEng { get; set; }

        public virtual ICollection<Board> Boards { get; set; }
    }
}
