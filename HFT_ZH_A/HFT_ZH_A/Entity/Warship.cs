using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HFT_ZH_A.Entity {

    
    public class Warship {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MinLength(0)]
        [MaxLength(1000)]
        [Required]
        public double Length { get; set; }

        public int Displacement { get; set; }

        public ICollection<Battery> Batteries { get; set; }
        public virtual Battery Battery { get; set; }
        public Warship()
        {
            Batteries = new HashSet<Battery>();
        }

    }

}
