using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF_Migracija.Models
{
    public class Book
    {
        [DisplayName("ID knjige")]
        public int BookId { get; set; }
        [DisplayName("Naslov knjige")]
        public string Title { get; set; }
        [DisplayName("Autor")]
        [ForeignKey("AuthorId")]
        public Author Author { get; set; }
        public int AuthorId { get; set; }
        public DateTime DateOfPublish { get; set; }
    }
}
