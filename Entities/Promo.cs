using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Promo : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;


        [MaxLength(50)]
        public string Code { get; set; } = null!;
        public string PromoType { get; set; }

        public string Value { get; set; }

        public DateTime? ValidTill { get; set; }
    }









}
