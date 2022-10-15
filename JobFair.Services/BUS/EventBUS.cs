using JobFair.DomainModels;
using JobFair.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JobFair.Services.BUS
{
    public class EventBUS : BaseBUS
    {
        public EventBUS()
        {
        }

        /// <summary>
        /// Get list in db
        /// </summary>
        /// <returns></returns>
        public List<EventViewModel> GetList(bool showAvailable = false)
        {
            var list = new List<EventViewModel>();

            // Search by vendor and env
            var listDB = db.EVENTs.AsNoTracking()
                .Where(x => (showAvailable == false || (showAvailable == true && x.IsShow == true)) &&
                x.IsDeleted != true)
                .OrderBy(x => x.EventTime);

            // Convert to object
            foreach (var item in listDB)
            {
                list.Add(new EventViewModel(item));
            }

            return list;
        }

        public void Delete(int id)
        {
            var model = db.EVENTs.Where(x => x.IdEvent == id).FirstOrDefault();
            if (model is null)
            {
                throw new Exception("Không tìm thấy hội thảo này");
            }

            model.IsDeleted = true;
            db.SaveChanges();
        }

        /// <summary>
        /// Đăng ký event
        /// Dữ liệu trùng MSSV sẽ được clear
        /// </summary>
        /// <param name="data"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Reg(StudentRegEventViewModel data)
        {
            if (data.EventIDs is null || data.EventIDs.Count < 1)
            {
                throw new Exception("Vui lòng chọn ít nhất một sự kiện muốn tham gia");
            }

            // Xóa danh sách cũ
            var listOldData = db.STUDENT_EVENT.Where(x => x.StudentCode.Equals(data.Code)).ToList();
            if (listOldData.Count > 0)
            {
                db.STUDENT_EVENT.RemoveRange(listOldData);
            }

            // Nạp danh sách mới
            foreach (var item in data.EventIDs)
            {
                var eventDB = db.EVENTs.Where(x => x.IdEvent == item).FirstOrDefault();
                if (eventDB is null || eventDB.IsShow != true)
                {
                    throw new Exception($"Sự kiện có mã số {item} không tồn tại, vui lòng làm mới lại trang");
                }

                eventDB.STUDENT_EVENT.Add(new STUDENT_EVENT
                {
                    StudentCode = data.Code,
                    StudentName = data.Name,
                    StudentEmail = data.Email,
                    StudentPhone = data.Phone,
                    StudentClass = data.Class
                });
            }

            db.SaveChanges();
        }

        public EventViewModel Get(int id)
        {
            var model = db.EVENTs.AsNoTracking()
                .Where(x => x.IdEvent == id)
                .FirstOrDefault();
            if (model is null) throw new Exception("Not found");
            return new EventViewModel(model);
        }

        /// <summary>
        /// Save or create new data
        /// base on id, if id is 0 its mean create new data, otherwise save data
        /// </summary>
        /// <param name="data"></param>
        /// <exception cref="NotImplementedException"></exception>
        public int Save(EventViewModel data)
        {
            var model = db.EVENTs.Where(x => x.IdEvent == data.Id).FirstOrDefault();

            if (model is null)
            {
                // Create new
                model = new EVENT();
                db.EVENTs.Add(model);
            }

            // Save data
            data.SetDBModel(model);

            // Save changes db
            db.SaveChanges();

            // Return id primary key
            return model.IdEvent;
        }

        public void ToggleStatusEvent(int id)
        {
            var model = db.EVENTs.Where(x => x.IdEvent == id).FirstOrDefault();

            if (model is null) throw new Exception("Not found");

            model.IsShow = !(model.IsShow ?? false);

            db.SaveChanges();
        }

        /// <summary>
        /// Lấy danh sách sinh viên tham gia hội thảo
        /// </summary>
        /// <returns></returns>
        public List<StudentRegEventViewModel> GetListStudent(int id)
        {
            var list = new List<StudentRegEventViewModel>();

            // Search by vendor and env
            var listDB = db.STUDENT_EVENT.AsNoTracking()
                .Where(x => x.IdEvent == id)
                .OrderBy(x => x.IdReg);

            // Convert to object
            foreach (var item in listDB.ToList())
            {
                list.Add(new StudentRegEventViewModel(item));
            }

            return list;
        }
    }
}
