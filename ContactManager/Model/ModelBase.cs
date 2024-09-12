using System.ComponentModel.DataAnnotations;

namespace ContactManager.Model
{
    public class ModelBase
    {
        [Key]
        public Guid Id { get; set; }  // Primary key

        public DateTime CreationTimestamp { get; }

        public DateTime LastChangeTimestamp { get; set;}

        public ModelBase()
        {
            Id = Guid.NewGuid();
            CreationTimestamp = DateTime.Now;
        }
    }
}
