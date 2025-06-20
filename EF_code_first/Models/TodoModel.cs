using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EF_code_first.Models
{
    public class TodoModel
    {
        public int ID { get; set; }
        [DisplayName("Zadatak")]
        [DataType(DataType.Text)]
        [StringLength(100)]
        [Required]
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
    }
}
