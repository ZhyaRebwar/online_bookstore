using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace online_bookstore.Model
{
    public class Author
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string? Description { get; set; }

        // Navigation property
        public ICollection<Book>? Books { get; set; } = new List<Book>();
    }
}