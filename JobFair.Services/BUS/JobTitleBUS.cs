using JobFair.DomainModels;
using JobFair.Shared.Utilities;
using JobFair.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFair.Services.BUS
{
    public class JobTitleBUS : BaseBUS
    {
        public JobTitleBUS()
        {
        }

        /// <summary>
        /// Get list in db
        /// </summary>
        /// <returns></returns>
        public List<JobTitleViewModel> GetList()
        {
            var list = new List<JobTitleViewModel>();

            // Search by vendor and env
            var listDB = db.JOBTITLEs.AsNoTracking().OrderBy(x => x.TitleName);

            // Convert to object
            foreach (var item in listDB.ToList())
            {
                list.Add(new JobTitleViewModel(item));
            }

            return list;
        }

        public int Save(JobTitleViewModel data)
        {
            // Validate data
            ValidateData.Validate(data, _throw: true);

            // Get and save model
            var model = db.JOBTITLEs.Where(x => x.IdTitle == data.Id).FirstOrDefault();

            if (model is null)
            {
                // Create new
                model = new JOBTITLE();
                db.JOBTITLEs.Add(model);
            }

            // Check the name
            if (db.JOBTITLEs.AsNoTracking().Any(x => x.IdTitle != data.Id && x.TitleName.ToLower().Equals(data.Title.ToLower())))
            {
                throw new Exception("Tên job title này đã tồn tại, vui lòng chọn một tên khác");
            }

            // Save data
            model.TitleName = data.Title;

            // Save changes db
            db.SaveChanges();

            // Return id primary key
            return model.IdTitle;
        }

        public JobTitleViewModel Get(int id)
        {
            var model = db.JOBTITLEs.Where(x => x.IdTitle == id).FirstOrDefault();
            if (model is null) throw new Exception("Not found");
            return new JobTitleViewModel(model);
        }
    }
}
