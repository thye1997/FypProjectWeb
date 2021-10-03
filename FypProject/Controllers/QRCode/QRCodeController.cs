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
        private readonly IGenericRepository<Models.QRCode> _qrCodeRepository;

        public QRCodeController(IGenericRepository<Models.QRCode> qrCodeRepository)
        {
            _qrCodeRepository = qrCodeRepository;
        }
        protected override string pageName { get; set; } = SystemData.View.QRCodeIndex;

        public IActionResult Index()
        {
            return base.Index();
        }

        public JsonResult GetQRCodeList()
        {
            try
            {
                var dataList = _qrCodeRepository.ToQueryable();
                return this.DataTableResult(dict, dataList);
            }
            catch (Exception e)
            {
                Debug.Write($"{e}");

                return SetError(e);
            }
        }

        public IActionResult GenerateQRCode()
        {
                MemoryStream memoryStream = new MemoryStream();
                string randomString = RandomHelper.RandomUniqueString();
                string fileName = $"QRCode_{DateTime.Now.ToString("dd/MM/yyyy")}.pdf";
                var isExist = true;
                while (isExist)
                {
                if (_qrCodeRepository.Where(c => c.UniqueString == randomString).Any() == true)
                {
                    isExist = true;
                    randomString = RandomHelper.RandomUniqueString(); }
                else isExist = false;
                }
                var existQR = _qrCodeRepository.Where(c => c.isActive == true).FirstOrDefault();
                if (existQR != null)
                {
                    existQR.isActive = false;
                    _qrCodeRepository.SaveChanges();
                }
                var qrCode = new Models.QRCode
                {
                    UniqueString = randomString,
                    createdBy = User.Identity.Name,
                    isActive = true,
                    FileName = fileName
                };
                _qrCodeRepository.Add(qrCode);
                var qrCodeImage = CreateQRCode(randomString);
                Document doc = new Document(PageSize.A4);
            try
            {
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
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return File(memoryStream, "application/pdf", fileName);
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

        public IActionResult DownloadSpecificQRCode(int Id)
        {
      
            var qrCode = _qrCodeRepository.Where(c => c.Id == Id && c.isActive).FirstOrDefault();
            if(qrCode != null)
            {
                Document doc = new Document(PageSize.A4);
                var memoryStream = new MemoryStream();
                var qrCodeImage = CreateQRCode(qrCode.UniqueString);
                    using(var pdfWriter = PdfWriter.GetInstance(doc, memoryStream))
                    {
                        doc.Open();
                        iTextSharp.text.Image pdfImage = iTextSharp.text.Image.GetInstance(qrCodeImage, System.Drawing.Imaging.ImageFormat.Jpeg);
                        doc.Add(pdfImage);
                        pdfWriter.CloseStream = false;
                        doc.Close();
                        byte[] byteInfo = memoryStream.ToArray();
                        memoryStream.Write(byteInfo, 0, byteInfo.Length);
                        memoryStream.Position = 0;
                        return File(memoryStream, "application/pdf", qrCode.FileName); //Return as file result
                    }                    
            }
            return null;                    
        }

        public IActionResult GenerateTest()
        {
            var qrCode = _qrCodeRepository.Where(c => c.isActive).FirstOrDefault();
            if (qrCode != null)
            {
                var qrCodeImage = CreateQRCode(qrCode.UniqueString);
                using (var memoryStream = new MemoryStream())
                {
                    Document doc = new Document(PageSize.A4);
                    var pdfWriter = PdfWriter.GetInstance(doc, memoryStream);
                    doc.Open();
                    iTextSharp.text.Image pdfImage = iTextSharp.text.Image.GetInstance(qrCodeImage, System.Drawing.Imaging.ImageFormat.Jpeg);
                    doc.Add(pdfImage);
                    pdfWriter.CloseStream = false;
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
