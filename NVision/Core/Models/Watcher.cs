using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    public class Watcher : User
    {
        [InverseProperty("Watcher")]
        public IEnumerable<Subject> Subjects { get; set; }
        public new UserType UserType { get; } = UserType.Watcher;
        public string PhoneNumber { get; set; }
    }
}
