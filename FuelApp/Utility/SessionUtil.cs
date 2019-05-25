using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuelApp.Utility
{
    public static class SessionUtil
    {
        public static string SessionGuidName
        {
            get
            {
                return "_userGID";
            }
        }

        public static string SessionVehicleSet
        {
            get
            {
                return "_vehiclesIsSet";
            }
        }

    }
}
