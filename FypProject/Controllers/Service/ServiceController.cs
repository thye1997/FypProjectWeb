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
            try
            {
                var dataList = _serviceRepository.List().Where(c => c.isActive).ToList();
                var serviceType = serviceTypeRepository.List().ToList();
                foreach(var obj in dataList)
                {
                    obj.serviceType = serviceType.Where(c => c.Id == obj.typeId).FirstOrDefault();
                }
                return this.DataTableResult(dict, dataList);

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

   /* class ServiceCustomData
    {
        public int customId { set; get; }
        public int Id { set; get; }
        public string serviceType { set; get; }
        public string serviceName { set; get; }
        public string createdOn { set; get; }
        public string createdBy { set; get; }

    }*/
}
