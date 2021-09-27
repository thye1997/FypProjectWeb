using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FypProject.Base;
using FypProject.Config;
using FypProject.CustomException;
using FypProject.Models;
using FypProject.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FypProject.Repository;



namespace FypProject.Controllers.Prescription
{
    [Authorize(AuthenticationSchemes = authenticationSchemes)]
    public class MedicineController : BasicController
    {
        private readonly IGenericRepository<Medicine> _medicineRepository;

        protected override string pageName { get; set; } = SystemData.View.MedicineIndex;

        public MedicineController(IGenericRepository<Medicine> medicineRepository)
        {
            _medicineRepository = medicineRepository;
        }
        public IActionResult Index()
        {
            return base.Index();
        }

        [HttpPost]
        public IActionResult AddMedicine(string medName, string medType)
        {            
                try
                {
                    var exist = _medicineRepository.List().Where(c => c.medName == medName && c.isActive).FirstOrDefault();
                if (exist != null) throw new BusinessException("Duplicate medicine name found.");
                    var medicine = new Medicine
                    {
                        createdBy = User.Identity.Name,
                        medName = medName,
                        Type = medType
                    };
                    _medicineRepository.Add(medicine);
                    return SetMessage(SystemData.ResponseStatus.Success, "Medicine added successfully.");
                }
                catch (Exception ex)
                {
                    return SetError(ex);
                }           
        }

        [HttpPost]
        public JsonResult DeleteMedicine(int Id)
        {
            try
            {
                var medicine = _medicineRepository.List().Where(c => c.Id == Id).FirstOrDefault();
                medicine.isActive = false;
                _medicineRepository.SaveChanges();
                return SetMessage(SystemData.ResponseStatus.Success, "Medicine deleted successfully.");
            }catch(Exception ex)
            {
                return SetError(ex);
            }
        }

        [HttpPost]
        public JsonResult LoadData()
        {
            try
            {              
                // getting all Customer data  
                var dataList = _medicineRepository.List().Where(c => c.isActive).ToList();
                return this.DataTableResult(dict, dataList);
            }
            catch (Exception e)
            {
                return SetError(e);
            }

        }
        [HttpPost]
        public JsonResult GetSpecificTypeMedicine(string Type)
        {
            try
            {
                var result = _medicineRepository.List().Where(c => c.Type == Type && c.isActive)
                        .Select(med => new MedicineListViewModel
                        {
                            Id = med.Id,
                            medName = med.medName
                        }).ToList();
                return SetMessage(data: new { list = result });
            }
            catch(Exception ex)
            {
               return SetError(ex);
            }
        }
    }
   /* class MedicineCustomData
    {
        public int customId { set; get; }
        public int Id { set; get; }
        public string Type { set; get; }
        public string medName { set; get; }
        public string createdOn { set; get; }
        public string createdBy { set; get; }

    }*/
}
