using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.BL.Adapters;
using TechMed.DL.Models;

namespace TechMed.BL.CommanClassesAndFunctions
{
    public class SettingMaster: ISettingMaster
    {        
        private readonly TeleMedecineContext _teleMedecineContext ;
        public SettingMaster(TeleMedecineContext teleMedecineContext)
        {
            this._teleMedecineContext = teleMedecineContext;
        }
        public long GetPatientNumber()
        {
            Setting setting = new Setting();
            Int64 currentNo = 0;
            Int64 patientSerialNo = _teleMedecineContext.Settings.FirstOrDefault().PatientNumber;
            if (patientSerialNo >= 0)
            {
                currentNo = patientSerialNo;
                setting = _teleMedecineContext.Settings.FirstOrDefault();
                if (setting != null)
                {
                    //setting = (Setting)setModel;
                    setting.PatientNumber = (currentNo + 1);
                }
                try
                {
                    this._teleMedecineContext.Entry(setting).State = EntityState.Modified;
                    var result = this._teleMedecineContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    throw;
                }
                return (currentNo + 1);
            }
            return 0;
        }
        public long GetCaseFileNumber()
        {
            Setting setting = new Setting();
            Int64 currentNo = 0;
            var value = _teleMedecineContext.Settings.Select(a => a.CaseFileNumber);
            if (value != null)
            {
                currentNo = Convert.ToInt64(value);
                var setModel = _teleMedecineContext.Settings.FirstOrDefault();
                if (setModel != null)
                {
                    setting = (Setting)setModel;
                    setting.CaseFileNumber = currentNo+1;
                }
                try
                {
                    this._teleMedecineContext.Entry(setting).State = EntityState.Modified;
                    var result = this._teleMedecineContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    throw;
                }
                return (currentNo + 1);
            }
            return 0;
        }
    }
}
