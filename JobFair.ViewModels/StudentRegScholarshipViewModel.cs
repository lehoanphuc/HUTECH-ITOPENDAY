using JobFair.DomainModels;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;

namespace JobFair.ViewModels
{
    public class StudentRegScholarshipViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mã số sinh viên")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "Vui lòng nhập đúng định dạng email")]
        [Required(ErrorMessage = "Vui lòng nhập email")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Vui lòng nhập đúng định dạng số điện thoại")]
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập lớp")]
        public string Class { get; set; }

        public string Activities { get; set; }

        public List<int> Role { get; set; }

        [Range(1, 4, ErrorMessage = "Vui lòng nhập điểm hệ 4")]
        public double Point { get; set; }

        public string RoleName { get; set; }


        public StudentRegScholarshipViewModel() { }

        public StudentRegScholarshipViewModel(SCHOLARSHIP_STUDENT item)
        {
            this.Id = item.IdReg;
            this.Code = item.StudentCode;
            this.Name = item.StudentName;
            this.Email = item.StudentEmail;
            this.Phone = item.StudentPhone;
            this.Point = Math.Round(item.StudentPoint ?? 0, 2);
            this.Activities = item.StudentActivities;
            this.Class = item.StudentClass;
            this.Role = new List<int>();

            if (!string.IsNullOrEmpty(item.StudentRoles))
            {
                var listRolesString = item.StudentRoles.Split(',');
                foreach (var role in listRolesString)
                {
                    Role.Add(int.Parse(role));
                }
            }
        }

        public StudentRegScholarshipViewModel(NameValueCollection form)
        {
            foreach (var key in form.AllKeys)
            {
                var value = form.Get(key);
                switch (key.ToLower())
                {
                    case "studentcode":
                        Code = value;
                        break;
                    case "studentname":
                        Name = value;
                        break;
                    case "studentclass":
                        Class = value;
                        break;
                    case "studentemail":
                        Email = value;
                        break;
                    case "studentphone":
                        Phone = value;
                        break;
                    case "studentactivities":
                        Activities = value;
                        break;
                    case "studentpoint":
                        Point = double.Parse(value);
                        break;
                    case "studentrole":
                        if (Role is null)
                        {
                            Role = new List<int>();
                        }

                        var idsString = value.Split(',');
                        foreach (var id in idsString)
                        {
                            Role.Add(int.Parse(id));
                        }
                        break;
                }
            }
        }

    }
}
