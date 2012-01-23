using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace ReservationsCommons.Implementation {
    public class ConfigReader : ReservationsCommons.IConfigReader {

        #region IConfigReader Members

        public int MaxCapacity {
            get {
                int value;
                if (!int.TryParse(ConfigurationManager.AppSettings["MaxCapacity"], out value)) {
                    value = 100; //Default value
                }
                return value;
            }
        }

        public decimal BookingTop {
            get {
                decimal value;
                if (!decimal.TryParse(ConfigurationManager.AppSettings["BookingTop"], out value)) {
                    value = 1; //Default value
                }
                return value;
            }
        }

        #endregion

    }
}
