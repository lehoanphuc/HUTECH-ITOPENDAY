using JobFair.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace JobFair.Services.BUS
{
    public class CandidateBUS : BaseBUS
    {
        public CandidateFilterViewModel GetFilter(int? idCompany)
        {
            var result = new CandidateFilterViewModel();

            var companyBUS = new CompanyBUS();
            var listCompany = companyBUS.GetList();

            // Add total
            result.Companies.AddRange(listCompany);
            var jobTitlesModel = db.JOBTITLEs.AsNoTracking().ToList();
            foreach (var jobTitleModel in jobTitlesModel)
            {
                result.JobTitles.Add(new JobTitleViewModel(jobTitleModel));
            }

            if (idCompany.HasValue)
            {
                result.Companies = result.Companies.Where(x => x.Id == idCompany.Value).ToList();

                var companyModel = result.Companies.First();
                result.JobTitles = result.JobTitles.Where(x => companyModel.JobTitleIds.Any(i => i == x.Id)).ToList();
            }

            return result;
        }

        public List<CandidateViewModel> GetList(int? idCompany, int? idJobTitle, bool showData = true)
        {
            var list = new List<CandidateViewModel>();

            var listDB = db.STUDENT_JOB.AsNoTracking()
                .Where(x => (idCompany == null || x.IdCompany == idCompany.Value) &&
                            (idJobTitle == null || x.IdTitle == idJobTitle.Value) &&
                            x.USER.IsDeleted != true &&
                            x.COMPANY_JOB.IsDeleted != true);

            int index = 0;
            foreach (var model in listDB)
            {
                var candidateModel = new CandidateViewModel(model.USER);
                candidateModel.JobTitle = model.COMPANY_JOB.JOBTITLE.TitleName;
                candidateModel.Index = ++index;
                if (!showData)
                {
                    candidateModel.Email = candidateModel.Email.Substring(0, 4) + "****";
                    candidateModel.Phone = candidateModel.Phone.Substring(0, 4) + "****";
                }

                list.Add(candidateModel);
            }

            return list;
        }

        public Dictionary<(string, string), int> GetStat(int? idCompany)
        {
            var result = new Dictionary<(string, string), int>();

            var listDB = db.COMPANY_JOB.AsNoTracking()
                .Where(x => x.IsDeleted != true &&
                            (idCompany == null || x.IdCompany == idCompany.Value));
            
            foreach (var item in listDB)
            {
                result[(item.COMPANY.CompanyName, item.JOBTITLE.TitleName)] = item.STUDENT_JOB.Count;
            }

            return result;
        }

        public bool CheckCandidate(int idCandidate, int idCompany)
        {
            return db.STUDENT_JOB
                .AsNoTracking()
                .Any(x => x.IdUser == idCandidate &&
                            x.IdCompany == idCompany &&
                            x.USER.IsDeleted != true &&
                            x.COMPANY_JOB.IsDeleted != true);
        }
    }
}
