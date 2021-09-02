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
            return View("MedicineIndex");
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
            string start = null;
            string length = null;
            int pageSize = 0, skip = 0;
            List<MedicineCustomData> customData = new List<MedicineCustomData>();
            try
            {
                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
              
                int recordsTotal = 0;

                // getting all Customer data  
                var medicineData = _medicineRepository.List().Where(c => c.isActive).ToList();
                List<int> countId = new List<int>();
                int count = 1;
                foreach (var cD in medicineData)
                {
                    customData.Add(new MedicineCustomData { customId = count, Type= cD.Type, Id = cD.Id, medName = cD.medName, createdBy = cD.createdBy, createdOn = cD.createdOn });
                    count++;
                }
                base.dataLoad(ref start, ref length, ref pageSize, ref skip);

                //total number of rows counts   
                recordsTotal = medicineData.Count;
                //Paging 

                //Returning Json Data  
                var data = customData.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

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
    class MedicineCustomData
    {
        public int customId { set; get; }
        public int Id { set; get; }
        public string Type { set; get; }
        public string medName { set; get; }
        public string createdOn { set; get; }
        public string createdBy { set; get; }

    }
}
