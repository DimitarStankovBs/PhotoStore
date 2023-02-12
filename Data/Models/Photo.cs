using PhotoStore.Data.Models.Abstraction;
using System;

namespace PhotoStore.Data.Models
{
    public class Photo: BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int Price { get; set; }
        public string BuyerName { get; set; }
        public string BuyerAddress { get; set; }
        public string BuyerPhone { get; set; }
        public DateTime PurchseDate { get; set; }
        public Genre Genre { get; set; }
        public Guid GenreId { get; set; }
       
    }
}
