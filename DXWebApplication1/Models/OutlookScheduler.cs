using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace DXWebApplication1.Models {
    public class OutlookScheduler {
        public IEnumerable Appointments {
            get {
                return OutlookDataProvider.GetAppointments();
            }
        }

        public IEnumerable Resources {
            get {
                return OutlookDataProvider.GetResources();
            }
        }
    }
}