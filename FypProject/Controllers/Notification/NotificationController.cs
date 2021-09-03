using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FypProject.Base;
using FypProject.Config;
using FypProject.Models;
using FypProject.Utils;
using FypProject.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FypProject.Repository;


namespace FypProject.Controllers
{
    [Authorize(AuthenticationSchemes = authenticationSchemes)]
    public class NotificationController : BasicController
    {
        private readonly IGenericRepository<Notification> notiRepository;
        private readonly IGenericRepository<Account> accRepository;
        private readonly FirebaseNotificationHelper firebaseNotificationHelper;
        public NotificationController(IGenericRepository<Notification> notiRepository, IGenericRepository<Account> accRepository,
            FirebaseNotificationHelper firebaseNotificationHelper
            )
        {
            this.notiRepository = notiRepository;
            this.accRepository = accRepository;
            this.firebaseNotificationHelper = firebaseNotificationHelper;
        }
        protected override string pageName { get; set; } = SystemData.View.NotificationIndex;

        public IActionResult Index()
        {
            return base.Index();
        }

        public JsonResult NotificationList()
        {
            try
            {
                var dataList = notiRepository.List().ToList();
                return this.DataTableResult(dict, dataList);
            }
            catch (Exception e)
            {
                Debug.Write($"{e}");

                return SetError(e);
            }
        }

        public async Task<JsonResult> AddNotification(Notification obj)
        {
            try
            {
               await firebaseNotificationHelper.SendNotifcation(accRepository, obj);
                obj.createdBy = User.Identity.Name;
                notiRepository.Add(obj);
                return SetMessage(SystemData.ResponseStatus.Success, "Notification sent successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return SetError(ex);
            }
        }
    }
}
