using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FypProject.CustomException;
using FypProject.Models;
using FypProject.Repository;
using FypProject.Utils;
using FypProject.ViewModel;

namespace FypProject.Services
{
    public class AppointmentScheduleService
    {
        private readonly IGenericRepository<OffDay> _offDayRepository;
        private readonly IGenericRepository<TimeSlot> _timeSlotRepository;
        private readonly IGenericRepository<SpecialHoliday> _spHolidayRepository;
        private readonly IGenericRepository<SlotDuration> _slotDurationRepository;
        IAppointmentRepository _apptRepository;

        public AppointmentScheduleService(IGenericRepository<OffDay> offDayRepository,
        IGenericRepository<SpecialHoliday> spHolidayRepository,
        IGenericRepository<Models.TimeSlot> timeSlotRepository,
        IGenericRepository<SlotDuration> slotDurationRepository,
        IAppointmentRepository apptRepository)
        {
            _offDayRepository = offDayRepository;
            _timeSlotRepository = timeSlotRepository;
            _spHolidayRepository = spHolidayRepository;
            _slotDurationRepository = slotDurationRepository;
            _apptRepository = apptRepository;
        }

        public AppointmentScheduleViewModel RetrieveApptSchedule()
        {
            AppointmentScheduleViewModel apptSchedule = new AppointmentScheduleViewModel();
            apptSchedule.offDayList = _offDayRepository.ToQueryable().ToList();
            apptSchedule.timeSlotList = _timeSlotRepository.ToQueryable().ToList();
            return apptSchedule;
        }

        public List<int> RetrieveOffDaySchedule()
        {
            var offDayList = _offDayRepository.ToQueryable().ToList();
            List<int> offDayArr = new List<int>();
            for (int i = 0; i < offDayList.Count; i++)
            {
                if (offDayList[i].isOffDay)
                {
                    if (offDayList[i].Id == 7)
                    {
                        offDayArr.Add(0);
                    }
                    else
                    {
                        offDayArr.Add(offDayList[i].Id);
                    }
                }
            }
            return offDayArr;
        }
        public AppointmentScheduleViewModel UpdateOffDaySchedule(int[] isChecked)
        {
            var apptScheduleList = _offDayRepository.ToQueryable().ToList();
            foreach (var n in apptScheduleList)
            {
                if (isChecked.Contains(n.Id)) n.isOffDay = false;
                else n.isOffDay = true;
            }

            int savechanges = _offDayRepository.SaveChanges(); //debugging purpose
            //Debug.WriteLine("savechange value=>"+ savechanges.ToString());
            var updatedScheduleList = new AppointmentScheduleViewModel
            {
                offDayList = apptScheduleList
            };
            return updatedScheduleList;
        }

        public AppointmentScheduleViewModel GetSpecialHoliday()
        {
            AppointmentScheduleViewModel apptSchedule = new AppointmentScheduleViewModel();
            apptSchedule.spHolidayList = _spHolidayRepository.ToQueryable().ToList();
            int count = 1;
            foreach (var cD in apptSchedule.spHolidayList)
            {
                cD.Id = count;
                count++;
            }
            return apptSchedule;
        }

        public void AddSpecialHoliday(SpecialHoliday obj)
        {
            var isExist = _spHolidayRepository.Where(c => obj.Date.Contains(c.Date)).FirstOrDefault();
            if (isExist != null) throw new BusinessException("Same date has exist.");
            else _spHolidayRepository.Add(obj);
        }
        public List<SpecialHoliday> GetSpecialHolidayList()
        {
            var formatDate = DateTime.Now.ToString("dd/MM/yyyy");
            var spHolidayList = _spHolidayRepository.ToQueryable().ToList().Where(c => DateTime.Parse(c.Date) >= DateTime.Parse(formatDate)).OrderBy(x => DateTime.Parse(x.Date)); // TODO: change date type
            return spHolidayList.ToList();
        }
        public List<TimeSlotHelper> LoadTimeSlot(string slot)
        {
            return TimeSlotHelper.ReturnSlot(slot);
        }

        public List<TimeSlotHelper> LoadSpecificTimeSlot(string date, int slot)
        {
            int duration = _slotDurationRepository.Where(c => c.isActive == true).FirstOrDefault().slotDuration; // retrieve duration per appt
            var timeSlot = _timeSlotRepository.Where(c => c.Id == slot).FirstOrDefault();
            var apptList = _apptRepository.ToQueryable().ToList().Where(c => DateTime.Parse(c.Date) == DateTime.Parse(date)).ToList(); //TODO: change date type
            var list = TimeSlotHelper.ReturnSpecificTimeSlot(apptList, timeSlot, duration);
            return list;
        }

        public void UpdateTimeSlot(TimeSlot obj)
        {
            TimeSlotHelper.UpdateTimeSlot(obj, _timeSlotRepository);
        }

        public int GetSlotDuration()
        {
            var slotDuration = _slotDurationRepository.Where(c => c.isActive == true).FirstOrDefault().Id;
            return slotDuration;
        }

        public int EditSlotDuration(int Id)
        {
            var slotDuration = _slotDurationRepository.ToQueryable();
            slotDuration.Where(c => c.Id == Id).FirstOrDefault().isActive = true;
            slotDuration.Where(c => c.Id != Id).FirstOrDefault().isActive = false;
            _slotDurationRepository.SaveChanges();
            return Id;
        }

    }
}
