using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workers_Manager.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string TaskCategory { get; set; } = String.Empty;
        public string TaskDescription { get; set; } = String.Empty;
        public int WorkerId { get; set; }
    }
}
