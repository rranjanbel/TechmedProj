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
            if (diffResult != TimeSpan.Zero)
            {
                if (totalDays > 365)
                {
                    int year = (int)(totalDays / 365);
                    return year;
                }
                else if (totalDays < 365)
                {
                    int month = (int)(totalDays / 12);
                    return 365;
                }
                else
                {
                    return 0;
                }

            }
            else
                return 0;
        }

        public static long GetPatientNumber()
        {
            Setting setting = new Setting();
            Int64 currentNo = 0;
            var value = _teleMedecineContext.Settings.Select(a => a.PatientNumber);
            if (value == null)
            {
                currentNo = Convert.ToInt64(value);
                var setModel = _teleMedecineContext.Settings.FirstOrDefault();
                if (setModel != null)
                {
                    setting = (Setting)setModel;
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
            var value = _teleMedecineContext.Settings.Select(a => a.CaseFileNumber);
            if (value == null)
            {
                currentNo = Convert.ToInt64(value);
                var setModel = _teleMedecineContext.Settings.FirstOrDefault();
                if (setModel != null)
                {
                    setting = (Setting)setModel;
                    setting.CaseFileNumber = currentNo + 1;
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
