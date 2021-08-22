using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FypProject.Config
{
    public  class PageTitleMapping
    {

        public static string pageTitleDictionary(string controllerName, string actionName)
        {

            IDictionary<string, string> configMapping = new Dictionary<string, string>();
            configMapping.Add("Account", SystemData.PageTitle.Dashboard);
            configMapping.Add("Dashboard", SystemData.PageTitle.Dashboard);
            configMapping.Add("Appointment", SystemData.PageTitle.AppointmentList);
            configMapping.Add("QRCode", SystemData.PageTitle.QRCode);
            configMapping.Add("SystemUser", SystemData.PageTitle.SystemUser);
            configMapping.Add("Medicine", SystemData.PageTitle.Medicine);
            configMapping.Add("Service", SystemData.PageTitle.Service);
            configMapping.Add("Notification", SystemData.PageTitle.Notification);
            return configMapping[controllerName];
        }
    }
}
