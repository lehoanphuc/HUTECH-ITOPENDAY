using JobFair.DomainModels;
using JobFair.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JobFair.Services.BUS
{
    public class FindJobBUS : BaseBUS
    {
        /// <summary>
        /// Get list in db
        /// </summary>
        /// <returns></returns>
        public List<CompanyJobViewModel> GetList(string username, List<int> jobTitleIDs, List<int> companyIDs)
        {
            // Get user
            int userID = -1;

            if (!string.IsNullOrEmpty(username))
            {
                var user = db.USERs.Where(x => x.Username.Equals(username)).FirstOrDefault();

                if (user is null)
                {
                    throw new Exception("Không tìm thấy người dùng này, vui lòng thử lại sau");
                }

                userID = user.IdUser;
            }

            // Lấy setting cho button interview
            bool allowInterview = false;
            var bus = new SettingBUS();
            try
            {
                allowInterview = bus.GetValueByKey<bool>(JobFair.Shared.Constants.SettingKeys.ALLOW_INTERVIEW.ToString());
            }
            catch
            {
                // Do nothing
            }

            var list = new List<CompanyJobViewModel>();

            // Search by vendor and env
            var listDB = db.COMPANY_JOB.AsNoTracking()
                .Include("COMPANY")
                .Include("JOBTITLE")
                .Where(x => x.IsDeleted != true &&
                (jobTitleIDs.Count == 0 || jobTitleIDs.Any(i => i == x.IdTitle)) &&
                (companyIDs.Count == 0 || companyIDs.Any(i => i == x.IdCompany)));

            // Convert to object
            foreach (var item in listDB)
            {
                var job = new CompanyJobViewModel(item);
                list.Add(job);
                if (userID > 0)
                {
                    if (item.STUDENT_JOB.Any(x => x.IdUser == userID))
                    {
                        job.IsApplied = true;
                    }
                }

                // Button interview
                if (allowInterview)
                {
                    job.LinkInterview = item.COMPANY.MeetingLink;
                }
            }

            return list;
        }

        public string GetInterview(string username, int idCompany)
        {
            // Get user
            var user = db.USERs.Where(x => x.Username.Equals(username)).FirstOrDefault();

            if (user is null)
            {
                throw new Exception("Không tìm thấy người dùng này, vui lòng thử lại sau");
            }

            if (!user.STUDENT_JOB.Any(x => x.IdCompany == idCompany))
            {
                throw new Exception("Bạn phải nộp CV vào công ty này trước khi vào phòng phỏng vấn");
            }

            // Dont care error because line above have already prevent null exception 
            var company = db.COMPANies.Where(x => x.IdCompany == idCompany).First();
            return company.MeetingLink;
        }

        public void RemoveCV(string username, int idCompany, int idJobTitle)
        {
            // Get user
            var user = db.USERs.Where(x => x.Username.Equals(username)).FirstOrDefault();

            if (user is null)
            {
                throw new Exception("Không tìm thấy người dùng này, vui lòng thử lại sau");
            }

            var oldCV = db.STUDENT_JOB.Where(x => x.IdUser == user.IdUser &&
            x.IdCompany == idCompany &&
            x.IdTitle == idJobTitle).FirstOrDefault();

            if (oldCV != null)
            {
                db.STUDENT_JOB.Remove(oldCV);
                db.SaveChanges();
            }
        }

        public void SubmitCV(string username, int idCompany, int idJobTitle)
        {
            // Get user
            var user = db.USERs.Where(x => x.Username.Equals(username)).FirstOrDefault();

            if (user is null)
            {
                throw new Exception("Không tìm thấy người dùng này, vui lòng thử lại sau");
            }

            // Check job and company is exist
            if (!db.COMPANY_JOB.Any(x => x.IsDeleted != true &&
            x.IdCompany == idCompany &&
            x.IdTitle == idJobTitle))
            {
                throw new Exception("Dữ liệu không hợp lệ, vui lòng thử lại sau");
            }

            // Check if exist cv in company with job title, skip
            if (user.STUDENT_JOB.Any(x => x.IdCompany == idCompany && x.IdTitle == idJobTitle))
            {
                // Skip, dont warning any error
                return;
            }

            // Insert new row
            var newCV = new STUDENT_JOB
            {
                DateApplied = DateTime.Now,
                IdCompany = idCompany,
                IdTitle = idJobTitle,
                IdUser = user.IdUser
            };

            db.STUDENT_JOB.Add(newCV);
            db.SaveChanges();
        }
    }
}
