using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkRelations.DataModel
{
    public class Author
    {
        public Author()
        {
            Books = new HashSet<Book>();
        }

        public Guid AuthorId { get; set; }
        public string Name { get; set; } 

        public virtual ICollection<Book> Books { get; set; }
    }
}
