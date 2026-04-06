using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tggargamel.Models
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long TgId { get; set; }
        public string? Password { get; set; }
        public string? Login { get; set; }
    }
}


