using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using FypProject.Base;
using FypProject.Config;
using FypProject.CustomException;
using FypProject.Models;
using FypProject.Models.DBContext;
using FypProject.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FypProject.Repository;


namespace FypProject.Controllers
{
    [Authorize(AuthenticationSchemes = authenticationSchemes)]
    public class ServiceController : BasicController
    {
        private readonly IGenericRepository<Service> _serviceRepository;
        private readonly IGenericRepository<ServiceType> serviceTypeRepository;

        protected override string pageName { get; set; } = SystemData.View.ServiceIndex;

        public ServiceController(IGenericRepository<Service> serviceRepository,
            IGenericRepository<ServiceType> serviceTypeRepository
            )
        {
            _serviceRepository = serviceRepository;
            this.serviceTypeRepository = serviceTypeRepository;
        }
        public IActionResult Index()
        {
            //ServiceViewModel userViewModel = new ServiceViewModel();
            //userViewModel.serviceList = (List<Service>)_serviceRepository.GetServiceList();
            return base.Index();
        }

        [HttpPost]
        public JsonResult AddService(string serviceName, int typeId)
        {
                try
                {
                    var exist = _serviceRepository.List().Where(c => c.serviceName == serviceName && c.isActive).FirstOrDefault();
                    var serviceType = serviceTypeRepository.List().Where(c => c.Id == typeId).FirstOrDefault();
                if (exist != null) throw new BusinessException("Duplicate service name found."); 
                    var service = new Service
                    {
                        createdBy = User.Identity.Name,
                        serviceName = serviceName,
                        typeId = serviceType.Id
                    };
                    _serviceRepository.Add(service);
                    return SetMessage(SystemData.ResponseStatus.Success, "Service added successfully.");
                }
                catch (Exception ex)
                {
                return SetError(ex);
                }            
        }

        [HttpPost]
        public JsonResult LoadData()
        {
            List<ServiceCustomData> customData = new List<ServiceCustomData>();
            string start =null;
            string length =null;
            int pageSize=0, skip=0;
            try
            {
                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
                int recordsTotal = 0;
                // getting all Customer data  
                var customerData = _serviceRepository.List().Where(c => c.isActive).ToList();

                List<int> countId = new List<int>();
                int count = 1;
                foreach(var cD in customerData)
                {
                    customData.Add(new ServiceCustomData { customId=count, Id = cD.Id, serviceType=serviceTypeRepository.List().Where(c=>c.Id == cD.typeId).FirstOrDefault().TypeName, serviceName= cD.serviceName, createdBy=cD.createdBy, createdOn = cD.createdOn});
                    count++;
                }
                base.dataLoad(ref start, ref length, ref pageSize, ref skip);

                //total number of rows counts   
                recordsTotal = customerData.Count;
                //Paging 

                //Returning Json Data  
                var data = customData.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw= draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data});

            }
            catch (Exception ex)
            {
                return SetError(ex);
            }

        }
        [HttpPost]
        public JsonResult DeleteService(int Id)
        {
            try
            {
                var service = _serviceRepository.List().Where(c => c.Id == Id).FirstOrDefault();
                service.isActive = false;
                _serviceRepository.SaveChanges();
                return SetMessage(SystemData.ResponseStatus.Success, "Service deleted successfully.");
            }
            catch(Exception ex)
            {
               return SetError(ex);
            }

        }
    }

     class ServiceCustomData
    {
        public int customId { set; get; }
        public int Id { set; get; }
        public string serviceType { set; get; }
        public string serviceName { set; get; }
        public string createdOn { set; get; }
        public string createdBy { set; get; }

    }
}
