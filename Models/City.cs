using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CoreApiBooks.Repositories;

namespace CoreApiBooks.Models
{
    public class City
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [ForeignKey("State")]
        public int? StateID { get; set; }

        public virtual State State { get; set; }
    }
}
