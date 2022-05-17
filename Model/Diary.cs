using System;
using System.Collections.Generic;

#nullable disable

namespace petdiary.Model
{
    public partial class Diary
    {
        public int Diaryid { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public string Path { get; set; }
        public int Userid { get; set; }
        public DateTime? AddDate { get; set; }

        public virtual User User { get; set; }
    }
}
