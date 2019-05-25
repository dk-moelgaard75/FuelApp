using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace FuelApp.Models
{
    public class FuelingModel
    {
        //TODO - add decorate for DB ID
        public int Id { get; set; }
        public Guid GID { get; set; }

        public Guid VehicleGID { get; set; }

        public string GasStation { get; set; }
        public float FuelAmount { get; set; }
        public float TotalPrice { get; set; }
        public int Mileage { get; set; }
        [NotMapped]
        public string VehicleName { get; set; }
    }
}
