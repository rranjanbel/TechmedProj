using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TechMed.DL.Models;

namespace TechMed.BL.CommanClassesAndFunctions
{
    public static class UtilityMaster
    {
        public static int Age = 0;
        private static TeleMedecineContext _teleMedecineContext = new TeleMedecineContext();
        private static TimeZoneInfo India_Standard_Time = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        public static string GetAgeOfPatient(DateTime dateOfBirth)
        {
            DateTime dtToday = GetLocalDateTime().Date;
            DateTime dtOfBirth = dateOfBirth.Date;
            TimeSpan diffResult = dtToday - dtOfBirth;
            double totalDays = diffResult.TotalDays;

            if (totalDays > 365)
            {
                int year = (int)(totalDays / 365.2425);
                return (year + " Years").ToString(); 
            }
            else
            {
                return (totalDays + " Days").ToString();
            }
        }

        public static int GetAgeInYearOnly(DateTime dateOfBirth)
        {
            DateTime dtToday = GetLocalDateTime().Date;
            DateTime dtOfBirth = dateOfBirth.Date;
            TimeSpan diffResult = dtToday - dtOfBirth;
            double totalDays = diffResult.TotalDays;

            if (totalDays > 365)
            {
                int year = (int)(totalDays / 365.2425);
                return year ;
            }
            else
            {
                return 0;
            }
        }

        public static string GetDetailsAgeOfPatient(DateTime dateOfBirth)
        {
            DateTime dtToday = GetLocalDateTime().Date;
            DateTime dtOfBirth = dateOfBirth.Date;
            TimeSpan diffResult = dtToday - dtOfBirth;
            double totalDays = diffResult.TotalDays;

            if (totalDays > 365)
            {
                int year = (int)(totalDays / 365.2425);
                return (year +":Years").ToString();
            }
            else if (totalDays < 365 && totalDays >= 30)
            {
                int month = (int)(totalDays / 30);
                return (month + ":Months").ToString();
            }
            else if (totalDays < 30)
            {
                int day = (int)(totalDays);
                return (day + ":Days").ToString();
            }
            else
            {
                return ("0").ToString();
            }


        }

        public static long GetPatientNumber()
        {
            Setting setting = new Setting();
            Int64 currentNo = 0;
            Int64 patientSerNo = 0;
            patientSerNo = _teleMedecineContext.Settings.Select(a => a.PatientNumber).FirstOrDefault();
            if (patientSerNo > 0)
            {
                currentNo = patientSerNo;
                setting = _teleMedecineContext.Settings.FirstOrDefault();
                if (setting != null)
                {
                    setting.PatientNumber = currentNo + 1;
                }
                try
                {
                    _teleMedecineContext.Entry(setting).State = EntityState.Modified;
                    var result = _teleMedecineContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    throw;
                }
                return currentNo + 1;
            }
            return 0;
        }
        public static long GetCaseFileNumber()
        {
            Setting setting = new Setting();
            Int64 currentNo = 0;
            Int64 patientSerNo = 0;
            patientSerNo = _teleMedecineContext.Settings.Select(a => a.CaseFileNumber).FirstOrDefault();
            if (patientSerNo > 0)
            {
                currentNo = patientSerNo;
                setting = _teleMedecineContext.Settings.FirstOrDefault();
                if (setting != null)
                {
                    setting.PatientNumber = currentNo + 1;
                }
                try
                {
                    _teleMedecineContext.Entry(setting).State = EntityState.Modified;
                    var result = _teleMedecineContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    throw;
                }
                return currentNo + 1;
            }
            return 0;
        }

        public static string SaveFileFromBase64(string imgBase64Str, string rootPath, string relativePath, string extention, int docTypeId = 1)
        {
            try
            {
                string path = string.Empty;
                string saveFileName = string.Empty;
                string contentRootPath = rootPath;
                //string path = @"\\MyStaticFiles\\Images\\Patients\\";
                path = contentRootPath + relativePath;

                //Create unique name of the file     

                var myfilename = string.Format(@"{0}", Guid.NewGuid());

                //Generate unique filename
                //string filepath = path + myfilename + ".jpeg";// png
                //string[] extentionName =new string[7] {".pdf",".png",".jpeg", ".jpg", ".docx",".doc",".txt" };
                string filepath = string.Empty;
                if (docTypeId == 1)
                {
                    filepath = path + myfilename + extention;
                    saveFileName = myfilename + extention;
                }
                else
                {
                    filepath = path + myfilename + ".pdf";
                    saveFileName = myfilename + ".pdf";
                }

                var bytess = Convert.FromBase64String(imgBase64Str);
                using (var imageFile = new FileStream(filepath, FileMode.Create))
                {
                    imageFile.Write(bytess, 0, bytess.Length);
                    imageFile.Flush();
                }

                return saveFileName;
            }
            catch (Exception ex)
            {
                string exception = ex.Message;
                return "";
            }

        }

        public static string ConvertToBase64(IFormFile file)
        {
            string strBase64 = string.Empty;
            try
            {

                if (file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        strBase64 = Convert.ToBase64String(fileBytes);
                        // act on the Base64 data
                    }
                }
                return strBase64;
            }
            catch (Exception ex)
            {
                string exception = ex.Message;
                return strBase64;
            }

        }

        public static DateTime GetLocalDateTime()
        {
            DateTime dateTime = DateTime.Now;

            try
            {
                dateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, India_Standard_Time);
            }
            catch (Exception)
            {
                throw;
            }

            return dateTime;
        }
        public static DateTime ConvertToLocalDateTime(DateTime incommingDate)
        {
            DateTime dateTime = DateTime.Now;

            try
            {
                dateTime = TimeZoneInfo.ConvertTimeFromUtc(incommingDate, India_Standard_Time);
                dateTime = dateTime.Date;
            }
            catch (Exception)
            {
                throw;
            }

            return dateTime;
        }

        public static bool SendSMS(string mobileNo, string message, string apiKey, string sender, string url)
        {
            if (url == null)
            {
                url = "https://api.textlocal.in/send/?apikey=";
            }
            string absoluteUrl = url + apiKey + "&number=" + mobileNo + "&message=" + message + "&sender=" + sender; ;
            StreamWriter streamWriter = null;
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";
            httpRequest.ContentLength = Encoding.UTF8.GetByteCount(absoluteUrl);
            httpRequest.ContentType = "application/x-www-form-urlencoded";
            try
            {
                streamWriter = new StreamWriter(httpRequest.GetRequestStream());
                streamWriter.Write(absoluteUrl);
            }
            catch (Exception ex)
            {
                string exceptionMessage = ex.Message;
                return false;
            }
            finally
            {
                streamWriter.Close();
            }
            return true;
        }

        public static string DownloadFile(string relativePath, string? contentRootPath)
        {
            try
            {
                string strBase64 = string.Empty;
                string filePath = string.Empty;
                 
                if (relativePath != null)
                {
                    //filePath = "https://telemed-ang-dev.azurewebsites.net"+ relativePath;
                    var provider = new FileExtensionContentTypeProvider();
                    if (!provider.TryGetContentType(filePath, out var contentType))
                    {
                        contentType = "application/octet-stream";
                    }

                    string path = @"\\MyStaticFiles\\Images\\Doctor\\Test_Signature.jpg";
                    filePath = contentRootPath + path;
                    if (File.Exists(filePath))
                    {
                        var bytes = System.IO.File.ReadAllBytesAsync(filePath);

                        strBase64 = Convert.ToBase64String(bytes.Result);
                        return strBase64;
                    }
                    else
                    {
                        return "";
                    }
                    
                   
                }
                else
                {
                    return "";
                }
            }
            catch(Exception ex)
            {
                
                throw;
            }
           
        }
    }
}
