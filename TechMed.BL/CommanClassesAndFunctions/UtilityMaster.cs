using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.DL.Models;

namespace TechMed.BL.CommanClassesAndFunctions
{
    public static class UtilityMaster
    {
        public static int Age = 0;
        private static TeleMedecineContext _teleMedecineContext = new TeleMedecineContext();
        public static int GetAgeOfPatient(DateTime dateOfBirth)
        {
            DateTime dtToday = DateTime.Now.Date;
            DateTime dtOfBirth = dateOfBirth.Date;
            TimeSpan diffResult = dtToday - dtOfBirth;
            double totalDays = diffResult.TotalDays;

            if (totalDays > 365)
            {
                int year = (int)(totalDays / 365);
                return year;
            }
            else
            {
                return 0;
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
    }
}
