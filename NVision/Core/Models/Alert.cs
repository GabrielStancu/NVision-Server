using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    public class Alert : Model
    {
        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        [ForeignKey("Watcher")]
        public int WatcherId { get; set; }
        public Watcher Watcher { get; set; }
        public string Message { get; set; }
        public bool? WasTrueAlert { get; set; }
    }
}
