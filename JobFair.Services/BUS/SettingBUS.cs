using JobFair.DomainModels;
using JobFair.Shared.Constants;
using JobFair.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFair.Services.BUS
{
    public class SettingBUS : BaseBUS
    {
        public SettingBUS()
        {
        }

        public void Save(string key, string value)
        {
            var setting = db.SETTINGs.Where(x => x.SettingKey.Equals(key)).FirstOrDefault();
            if (setting is null)
            {
                setting = new SETTING();
                setting.SettingKey = key;
                db.SETTINGs.Add(setting);
            }

            setting.SettingValue = value;
            db.SaveChanges();
        }

        public T GetValueByKey<T>(string key)
        {
            var setting = db.SETTINGs.AsNoTracking().Where(x => x.SettingKey.Equals(key)).FirstOrDefault();

            if (setting is null)
                throw new Exception("Not found setting");

            return (T)Convert.ChangeType(setting.SettingValue, typeof(T));
        }

        public T GetObjectFromJsonByKey<T>(string key)
        {
            var setting = db.SETTINGs.AsNoTracking().Where(x => x.SettingKey.Equals(key)).FirstOrDefault();

            if (setting is null)
                throw new Exception("Not found setting");

            return JsonConvert.DeserializeObject<T>(setting.SettingValue);
        }

        public List<SponsorViewModel> GetListSponsor()
        {
            try
            {
                var sponsors = GetObjectFromJsonByKey<List<SponsorViewModel>>(SettingKeys.SETTING_SPONSOR.ToString());

                // Get list company
                var listCompany = db.COMPANies.AsNoTracking().ToList();

                // Get data from company service
                var companyBUS = new CompanyBUS();

                foreach (var sponsor in sponsors)
                {
                    if (sponsor is null)
                    {
                        continue;
                    }

                    sponsor.Company = new List<CompanyViewModel>();
                    foreach (var companyName in sponsor.CompanyName)
                    {
                        var companyModel = listCompany
                            .Where(x => x.CompanyName.ToLower().Equals(companyName.ToLower()))
                            .FirstOrDefault();

                        if (companyModel != null)
                        {
                            sponsor.Company.Add(new CompanyViewModel(companyModel));
                        }
                    }
                }

                return sponsors;
            }
            catch
            {
                // Any error, return empty list
                // Prevent null exception
                return new List<SponsorViewModel>();
            }
        }
    }
}
