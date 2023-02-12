using System;

namespace PhotoStore.Data.Models.Abstraction
{
    public class BaseModel
    {
        public Guid Id { get; set; }
        public BaseModel()
        {
            Id = Guid.NewGuid();
        }
    }
}
