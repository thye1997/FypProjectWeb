using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FypProject.ApiViewModel;
using FypProject.Models;
using FypProject.Repository;
using FypProject.ViewModel;
using static FypProject.Config.SystemData;

namespace FypProject.Services
{
    public class DashboardService
    {
        private readonly IGenericRepository<AccountProfile> accProfileRepository;
        private readonly IAppointmentRepository apptRepository;
        public DashboardService(
            IGenericRepository<AccountProfile> accProfileRepository,
            IAppointmentRepository apptRepository)
        {
            this.accProfileRepository = accProfileRepository;
            this.apptRepository = apptRepository;
        }

        public DashBoardApiViewModel RetrievePatientDataCount(int accId)
        {
            List<int> userId = new List<int>();

            var userIdList = accProfileRepository.List().Where(c => c.accountId == accId).ToList();
            if (userIdList != null)
            {
                foreach(var n in userIdList)
                {
                    userId.Add(n.userId);
                }
            }
            int[] inqueueStatus = new int[ ]{ (int)AppointmentStatus.InQueue };
            var apptList = apptRepository.List().Where(c => userId.Contains(c.userId)).ToList();
            var result = apptRepository.GetAppointmentList(inqueueStatus).Where(c => userId.Contains(c.userId)).OrderBy(c => DateTime.Parse(c.Date)).FirstOrDefault();

            var inqueueAppt = (AppointmentData)null;
            if(result != null)
            {
                inqueueAppt = new AppointmentData
                {
                    apptId = result.Id,
                    FullName = result.FullName,
                    service = result.Service,
                    date = result.Date,
                    apptStatusString = result.Status,
                    startTime = result.StartTime,
                    endTime = result.EndTime,
                };
            }

            List<int> passApptStatus = new List<int>() {(int)AppointmentStatus.Completed, (int)AppointmentStatus.Cancelled};
            return new DashBoardApiViewModel
            {
                upcomingCount = apptList.Where(c => c.Status == (int)AppointmentStatus.Confirmed).Count(),
                pastCount = apptList.Where(c => passApptStatus.Contains(c.Status)).Count(),
                noShowCount = apptList.Where(c => c.Status == (int)AppointmentStatus.NoShow).Count(),
                apptData = inqueueAppt
            };
        }

        public DashboardViewModel RetrieveWebApptDataCount()
        {
            var model = (DashboardViewModel)null;
            var apptList = apptRepository.List().ToList();

            List<int> passApptStatus = new List<int>() { (int)AppointmentStatus.Completed, (int)AppointmentStatus.Cancelled };

            model = new DashboardViewModel
            {
                totalUpcoming = apptList.Where(c => c.Status == (int)AppointmentStatus.Confirmed).Count(),
                totalPast = apptList.Where(c => passApptStatus.Contains(c.Status)).Count(),
                totalNoShow = apptList.Where(c => c.Status == (int)AppointmentStatus.NoShow).Count(),
            };

            return model;

    }
    }
}
