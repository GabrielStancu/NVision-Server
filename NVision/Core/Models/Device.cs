using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    public class Device : Model
    {
        public Guid SerialNumber { get; set; }
        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}
