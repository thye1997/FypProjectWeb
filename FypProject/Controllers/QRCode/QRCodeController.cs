using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FypProject.Base;
using FypProject.Config;
using FypProject.Utils;
using FypProject.ViewModel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using FypProject.Repository;


namespace FypProject.Controllers
{
    [Authorize(AuthenticationSchemes = authenticationSchemes)]
    public class QRCodeController : BasicController
    {
        private readonly IGenericRepository<FypProject.Models.QRCode> qrCodeRepository;

        public QRCodeController(IGenericRepository<FypProject.Models.QRCode> qrCodeRepository)
        {
            this.qrCodeRepository = qrCodeRepository;
        }
        protected override string pageName { get; set; } = SystemData.View.QRCodeIndex;

        public IActionResult Index()
        {
            return base.Index();
        }

        [HttpPost]
        public JsonResult GetQRCodeList()
        {
            string start = null;
            string length = null;
            int pageSize = 0, skip = 0;
            try
            {
                int recordsTotal = 0;
                // getting all Customer data  
                var result = qrCodeRepository.List().ToList();
                var formattedResult = new List<QRCodeViewModel>();
                var count = 1;
                foreach(var n in result)
                {
                    formattedResult.Add(new QRCodeViewModel
                    {
                        Id = n.Id,
                        FileName = n.FileName,
                        createdBy = n.createdBy,
                        isActive = n.isActive,
                        createdOn = n.createdOn
                    });                 
                    count++;
                }
                recordsTotal = formattedResult.Count;
                base.dataLoad(ref start, ref length, ref pageSize, ref skip);
                //Returning Json Data  
                var data = formattedResult.Skip(skip).Take(pageSize).ToList();
                return Json(new { recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception e)
            {
                Debug.Write($"{e}");

                return SetError(e);
            }
        }

        [HttpPost]
        public IActionResult GenerateQRCode()
        {
                MemoryStream memoryStream = new MemoryStream();
                string randomString = RandomHelper.RandomUniqueString();
                string fileName = $"QRCode_{DateTime.Now.ToString("dd/MM/yyyy")}.pdf";
                var isExist = true;
                while (isExist)
                {
                if (qrCodeRepository.List().Where(c => c.UniqueString == randomString).Any() == true)
                {
                    isExist = true;
                    randomString = RandomHelper.RandomUniqueString(); }
                else isExist = false;
                }
                var existQR = qrCodeRepository.List().Where(c => c.isActive == true).FirstOrDefault();
                if (existQR != null)
                {
                    existQR.isActive = false;
                    qrCodeRepository.SaveChanges();
                }
                var qrCode = new Models.QRCode
                {
                    UniqueString = randomString,
                    createdBy = User.Identity.Name,
                    isActive = true,
                    FileName = fileName
                };
                qrCodeRepository.Add(qrCode);
                var qrCodeImage = CreateQRCode(randomString);
                Document doc = new Document(PageSize.A4);
                using (var pdfWriter = PdfWriter.GetInstance(doc, memoryStream))
                {
                    pdfWriter.CloseStream = false;
                    doc.Open();
                    iTextSharp.text.Image pdfImage = iTextSharp.text.Image.GetInstance(qrCodeImage, System.Drawing.Imaging.ImageFormat.Jpeg);
                    doc.Add(pdfImage);
                    doc.Close();
                    byte[] byteInfo = memoryStream.ToArray();
                    memoryStream.Write(byteInfo, 0, byteInfo.Length);
                    memoryStream.Position = 0;
                    return File(memoryStream, "application/pdf", fileName);
                   //return ; //Return as file result
                }
        }

        private System.Drawing.Image CreateQRCode(string randomString)
        {   
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(randomString, QRCodeGenerator.ECCLevel.Q);
            QRCoder.QRCode qrCode = new QRCoder.QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            System.Drawing.Image image = qrCodeImage;
            return image;         
        }

        [HttpGet]
        public IActionResult DownloadSpecificQRCode(int Id)
        {
            MemoryStream memoryStream = new MemoryStream();
      
            var qrCode = qrCodeRepository.List().Where(c => c.Id == Id && c.isActive).FirstOrDefault();
            if(qrCode != null)
            {
                var qrCodeImage = CreateQRCode(qrCode.UniqueString);
                Document doc = new Document(PageSize.A4);
                using (var pdfWriter = PdfWriter.GetInstance(doc, memoryStream))
                {
                    pdfWriter.CloseStream = false;
                    doc.Open();
                    iTextSharp.text.Image pdfImage = iTextSharp.text.Image.GetInstance(qrCodeImage, System.Drawing.Imaging.ImageFormat.Jpeg);
                    doc.Add(pdfImage);
                    doc.Close();
                    byte[] byteInfo = memoryStream.ToArray();
                    memoryStream.Write(byteInfo, 0, byteInfo.Length);
                    memoryStream.Position = 0;
                    return File(memoryStream, "application/pdf", qrCode.FileName); //Return as file result
                }
            }
            return null;                    
        }

        [HttpGet]
        public IActionResult GenerateTest()
        {
            MemoryStream memoryStream = new MemoryStream();

            var qrCode = qrCodeRepository.List().Where(c => c.isActive).FirstOrDefault();
            if (qrCode != null)
            {
                var qrCodeImage = CreateQRCode(qrCode.UniqueString);
                Document doc = new Document(PageSize.A4);
                using (var pdfWriter = PdfWriter.GetInstance(doc, memoryStream))
                {
                    pdfWriter.CloseStream = false;
                    doc.Open();
                    iTextSharp.text.Image pdfImage = iTextSharp.text.Image.GetInstance(qrCodeImage, System.Drawing.Imaging.ImageFormat.Jpeg);
                    doc.Add(pdfImage);
                    doc.Close();
                    byte[] byteInfo = memoryStream.ToArray();
                    memoryStream.Write(byteInfo, 0, byteInfo.Length);
                    memoryStream.Position = 0;
                    return File(memoryStream, "application/pdf", qrCode.FileName); //Return as file result
                }
            }
            return null;
        }
    }
}
