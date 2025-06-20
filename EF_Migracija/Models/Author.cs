using System.ComponentModel;

namespace EF_Migracija.Models
{
    public class Author
    {
        public int AuthorId { get; set; }

        [DisplayName("Autor")]
        public string Name { get; set; }
        public string Bio { get; set; }
    }
}
