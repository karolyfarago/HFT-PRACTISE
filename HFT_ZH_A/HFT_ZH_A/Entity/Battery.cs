using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HFT_ZH_A.Entity {

    public enum BatteryType { 
        Main,
        Secondary
    }

    public class Battery {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public BatteryType Type { get; set; }

        public int Caliber { get; set; }

        [ForeignKey(nameof(Warship))]
        public int WarshipId { get; set; }

        public Warship Warship { get; set; }

    }
}
