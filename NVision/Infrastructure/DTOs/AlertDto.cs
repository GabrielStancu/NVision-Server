using Core.Models;
using System;

namespace Infrastructure.DTOs
{
    public class AlertDto
    {
        public int SubjectId { get; set; }
        public DateTime AlertMoment { get; set; }
        public string Message { get; set; }
    }
}
