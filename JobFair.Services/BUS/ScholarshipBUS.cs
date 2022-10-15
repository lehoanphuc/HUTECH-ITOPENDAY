using JobFair.DomainModels;
using JobFair.Shared.MasterData;
using JobFair.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JobFair.Services.BUS
{
    public class ScholarshipBUS : BaseBUS
    {
        public List<StudentRegScholarshipViewModel> GetList()
        {
            var list = new List<StudentRegScholarshipViewModel>();

            // Search by vendor and env
            var listDB = db.SCHOLARSHIP_STUDENT.AsNoTracking()
                .Where(x => x.IsDeleted != true);

            // Convert to object
            var listRole = StudentRoleData.GetList();
            foreach (var item in listDB.ToList())
            {
                var model = new StudentRegScholarshipViewModel(item);

                if (model.Role != null)
                {
                    var role = listRole.Where(x => model.Role.Any(i => i == x.Value)).ToList().Select(x => x.Name);
                    model.RoleName = string.Join(", ", role.ToArray());
                }

                list.Add(model);
            }

            return list;
        }

        public void Delete(int id)
        {
            var model = db.SCHOLARSHIP_STUDENT.Where(x => x.IdReg == id).FirstOrDefault();
            if (model is null)
            {
                throw new Exception("Không tìm thấy thông tin đăng ký này");
            }

            model.IsDeleted = true;
            db.SaveChanges();
        }

        public int Reg(StudentRegScholarshipViewModel data)
        {
            if (data.Role is null || data.Role.Count < 1)
            {
                throw new Exception("Vui lòng chọn một đối tượng xét học bổng");
            }

            var listRoles = StudentRoleData.GetList();
            foreach (var checkRole in data.Role)
            {
                if (!listRoles.Any(x => x.Value == checkRole))
                {
                    throw new Exception("Không tìm thấy đối tượng xét học bổng, vui lòng làm mới lại trang");
                }
            }

            var model = new SCHOLARSHIP_STUDENT
            {
                StudentCode = data.Code,
                StudentName = data.Name,
                StudentEmail = data.Email,
                StudentPhone = data.Phone,
                StudentActivities = data.Activities,
                StudentRoles = String.Join(",", data.Role.ToArray()),
                StudentPoint = data.Point,
                StudentClass = data.Class,
                TimeReg = DateTime.Now
            };

            db.SCHOLARSHIP_STUDENT.Add(model);
            db.SaveChanges();

            return model.IdReg;
        }
    }
}
