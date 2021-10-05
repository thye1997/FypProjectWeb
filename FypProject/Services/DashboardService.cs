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
        private readonly IGenericRepository<AccountProfile> _accProfileRepository;
        private readonly IAppointmentRepository _apptRepository;
        public DashboardService(
            IGenericRepository<AccountProfile> accProfileRepository,
            IAppointmentRepository apptRepository)
        {
            _accProfileRepository = accProfileRepository;
            _apptRepository = apptRepository;
        }

        public DashBoardApiViewModel RetrievePatientMobileDashboardData(int accId)
        {
            var userIdList = _accProfileRepository.Where(c => c.accountId == accId).Select(c=>c.userId).ToList();
            int[] inqueueStatus = new int[ ]{ (int)AppointmentStatus.InQueue };
            var apptList = _apptRepository.Where(c => userIdList.Contains(c.userId)).ToList();
            var result = _apptRepository.GetAppointmentList(inqueueStatus).AsEnumerable().Where(c => userIdList.Contains(c.userId)).OrderBy(c => DateTime.Parse(c.Date)).FirstOrDefault();

            var inqueueAppt = (AppointmentData)null;
            if(result != null)
            {
                inqueueAppt = new AppointmentData
                {
                    ApptId = result.Id,
                    FullName = result.FullName,
                    Service = result.Service,
                    Date = result.Date,
                    ApptStatusString = result.Status,
                    StartTime = result.StartTime,
                    EndTime = result.EndTime,
                };
            }

            List<int> passApptStatus = new List<int>() {(int)AppointmentStatus.Completed, (int)AppointmentStatus.Cancelled};
            return new DashBoardApiViewModel
            {
                UpcomingCount = apptList.Where(c => c.Status == (int)AppointmentStatus.Confirmed).Count(),
                PastCount = apptList.Where(c => passApptStatus.Contains(c.Status)).Count(),
                NoShowCount = apptList.Where(c => c.Status == (int)AppointmentStatus.NoShow).Count(),
                ApptData = inqueueAppt
            };
        }

        public DashboardViewModel RetrieveWebApptDashboardDataCount()
        {
            var apptList = _apptRepository.ToQueryable();
            List<int> passApptStatus = new List<int>() { (int)AppointmentStatus.Completed, (int)AppointmentStatus.Cancelled };
            var model = new DashboardViewModel
            {
                totalUpcoming = apptList.Where(c => c.Status == (int)AppointmentStatus.Confirmed).Count(),
                totalPast = apptList.Where(c => passApptStatus.Contains(c.Status)).Count(),
                totalNoShow = apptList.Where(c => c.Status == (int)AppointmentStatus.NoShow).Count(),
            };

            return model;

    }
    }
}
