using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterBot.Core.Models
{
    public class News
    {
        public int Id { get; set; }
        public bool IsApproved { get; set; }
        public string DecisionById { get; set; }
        public string ParaphrasedById { get; set; }
        public string OriganalNews { get; set; }
        public string ParaphrasdNews { get; set; }
        public string NewsSource { get; set; }
        public string Comment { get; set; }
        public DateTime GetNewsTime { get; set; }
        public DateTime DecisionTime { get; set; }
        public DateTime ParaphrasingTime { get; set; }
        public ApplicationUser DecisionBy { get; set; }
        public ApplicationUser ParaphrasedBy { get; set; }
    }
}
