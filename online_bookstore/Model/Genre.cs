using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace online_bookstore.Model
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        // Removed the [Index] attribute as it is not valid in EF Core. Instead, use Fluent API in DbContext to configure the index.
        public string Name { get; set; }

        // Navigation property for one-to-many relationship with Book
        public ICollection<Book>? Books { get; set; } = new List<Book>();
    }
}