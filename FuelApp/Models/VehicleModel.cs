using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FuelApp.Models
{
    public enum Fuel
    {
        Gasoline,
        Diesel,
        Electricity,
        Hydrogen,
        Other
    }
    public class VehicleModel
    {
        //TODO - add decorate for DB ID
        public int Id { get; set; }
        public Guid GID { get; set; }

        public Guid UserGID { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        [EnumDataType(typeof(Fuel))]
        public Fuel FuelType { get; set; }
        
        [Required]
        public int FuelTankSize { get; set; }

    }
}
