using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FuelApp.Models
{
    public class FuelingModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid GID { get; set; }

        public Guid VehicleGID { get; set; }

        public string GasStation { get; set; }
        public float FuelAmount { get; set; }
        public float TotalPrice { get; set; }
        public int Mileage { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "dd-mm-yyyy")]
        public DateTime FuelingDate { get; set; }
        [NotMapped]
        public string VehicleName { get; set; }
    }
}
