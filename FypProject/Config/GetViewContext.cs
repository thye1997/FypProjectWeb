using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FypProject.Config
{
    public class GetViewContext
    {
        ViewContext viewContext;

        public GetViewContext(ViewContext viewContext)
        {
            this.viewContext = viewContext;
        }
        public string returnTitle()
        {
            string activeController = viewContext.RouteData.Values["Controller"].ToString();
            string activeAction = viewContext.RouteData.Values["Action"].ToString();

           string pageName = validatePageTitle(activeController, activeAction);

            return pageName;

        }

        public string validatePageTitle(string controllerName, string actionName)
        {

             if (controllerName == "Appointment")
            {
                if(actionName == "AppointmentSchedule")
                {
                    return SystemData.PageTitle.AppointmentSchedule;
                }
                else if (actionName == "AppointmentDetail")
                {
                    return SystemData.PageTitle.AppointmentDetail;
                }
            }
            else if(controllerName == "User")
            {    if(actionName == SystemData.ActionName.UserDetail)
                {
                    return SystemData.PageTitle.PatientDetail;
                }
                return SystemData.PageTitle.PatientList;
            }

            return PageTitleMapping.pageTitleDictionary(controllerName, actionName);


        }
    }
}
