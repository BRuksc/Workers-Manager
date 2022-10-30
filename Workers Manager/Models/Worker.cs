using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workers_Manager.Models
{
    public class Worker
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Surname { get; set; } = String.Empty;
        public int? UnfinishedTasks { get; set; }
        public List<Task> Tasks { get; set; } = new List<Task>();
    }
}
