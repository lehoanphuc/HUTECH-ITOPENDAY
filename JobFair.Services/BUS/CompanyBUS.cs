using JobFair.DomainModels;
using JobFair.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JobFair.Services.BUS
{
    public class CompanyBUS : BaseBUS
    {
        /// <summary>
        /// Get list in db
        /// </summary>
        /// <returns></returns>
        public List<CompanyViewModel> GetList()
        {
            var list = new List<CompanyViewModel>();

            // Search by vendor and env
            var listDB = db.COMPANies.AsNoTracking();

            // Convert to object
            foreach (var item in listDB.ToList())
            {
                list.Add(new CompanyViewModel(item));
            }

            return list;
        }


        /// <summary>
        /// Lấy danh sách các công ty có tuyển việc làm
        /// </summary>
        /// <returns></returns>
        public List<CompanyViewModel> GetListHaveJob()
        {
            var list = new List<CompanyViewModel>();

            // Search by vendor and env
            var listDB = db.COMPANies.AsNoTracking()
                .Where(x => x.COMPANY_JOB.Count > 0);

            // Convert to object
            foreach (var item in listDB.ToList())
            {
                list.Add(new CompanyViewModel(item));
            }

            return list;
        }

        public CompanyViewModel Get(int id)
        {
            var model = db.COMPANies.AsNoTracking()
                .Include("COMPANY_JOB")
                .Where(x => x.IdCompany == id)
                .FirstOrDefault();
            if (model is null) throw new Exception("Not found");
            return new CompanyViewModel(model);
        }

        public CompanyViewModel GetByName(string name)
        {
            name = name.ToLower();
            var model = db.COMPANies.AsNoTracking()
                .Include("COMPANY_JOB")
                .Where(x => x.CompanyName.ToLower().Equals(name))
                .FirstOrDefault();
            if (model is null) throw new Exception("Not found");
            return new CompanyViewModel(model);
        }

        /// <summary>
        /// Save or create new data
        /// base on id, if id is 0 its mean create new data, otherwise save data
        /// </summary>
        /// <param name="data"></param>
        /// <exception cref="NotImplementedException"></exception>
        public int Save(CompanyViewModel data)
        {
            var model = db.COMPANies.Where(x => x.IdCompany == data.Id).FirstOrDefault();
            
            if (model is null) {
                // Create new
                model = new COMPANY();
                db.COMPANies.Add(model);
            }

            // Check the name
            if (db.COMPANies.AsNoTracking().Any(x => x.IdCompany != data.Id && x.CompanyName.ToLower().Equals(data.Name.ToLower())))
            {
                throw new Exception("Tên công ty này đã tồn tại, vui lòng chọn một tên khác");
            }

            // Save data
            model.CompanyVideo = data.Video;
            model.CompanyLink = data.LinkJD;
            model.CompanyName = data.Name;
            model.MeetingLink = data.MeetingLink;
            model.CompanyDescription = data.Description;
            model.ShowIndex = (model.ShowIndex ?? 0) + 1;

            // Clear and add job title
            foreach (var currentJobTitle in model.COMPANY_JOB)
            {
                currentJobTitle.IsDeleted = true;
            }

            // Add new
            foreach (var item in data.JobTitleIds)
            {
                var existingJobtitle = model.COMPANY_JOB.Where(x => x.IdTitle == item).FirstOrDefault();

                if (existingJobtitle is null)
                {
                    model.COMPANY_JOB.Add(new COMPANY_JOB
                    {
                        IdTitle = item
                    });
                }
                else
                {
                    existingJobtitle.IsDeleted = false;
                }
            }

            // Save changes db
            db.SaveChanges();

            // Return id primary key
            return model.IdCompany;
        }
    }
}
