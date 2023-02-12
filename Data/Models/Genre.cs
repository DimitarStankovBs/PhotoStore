using PhotoStore.Data.Models.Abstraction;
using System.Collections.Generic;

namespace PhotoStore.Data.Models
{
    public class Genre: BaseModel
    {
        public string Title { get; set; }
        public ICollection<Photo> Photo { get; set; }
        public Genre()
        {
            Photo = new HashSet<Photo>();            
        }

    }
}
