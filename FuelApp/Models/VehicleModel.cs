using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FuelApp.Utility;
using static FuelApp.Utility.EnumUtil;

namespace FuelApp.Models
{
    
    public class VehicleModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid GID { get; set; }

        public Guid UserGID { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public string Licensplate { get; set; }

        
        [Required]
        [EnumDataType(typeof(EnumUtil.Fuel))]
        public Fuel FuelType { get; set; }
        
        [Required]
        public int FuelTankSize { get; set; }

        
        [NotMapped]
        public string GetVehicleIdentification
        {
            get
            {
                return Brand + " - " + Model + " (" + Licensplate + ")";
            }
        }
        
    }
}
