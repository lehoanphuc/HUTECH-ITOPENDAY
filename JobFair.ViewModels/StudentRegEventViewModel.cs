using JobFair.DomainModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobFair.ViewModels
{
    public class StudentRegEventViewModel
    {
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

        public List<int> EventIDs { get; set; }

        public StudentRegEventViewModel() { }

        public StudentRegEventViewModel(STUDENT_EVENT item)
        {
            Code = item.StudentCode;
            Name = item.StudentName;
            Phone = item.StudentPhone;
            Email = item.StudentEmail;
            Class = item.StudentClass;
        }
    }
}
