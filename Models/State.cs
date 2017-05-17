using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CoreApiBooks.Repositories;

namespace CoreApiBooks.Models
{
    public class State : BaseEntity
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [ForeignKey("Country")]
        public int? CountryID { get; set; }

        public virtual Country Country { get; set; }
    }
}
