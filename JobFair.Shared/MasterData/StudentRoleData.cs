using System.Collections.Generic;

namespace JobFair.Shared.MasterData
{
    public class StudentRoleData
    {
        public bool IsShow { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }

        public static IEnumerable<StudentRoleData> GetList()
        {
            // Dont remove the data please
            yield return new StudentRoleData
            {
                Value = 0,
                IsShow = true,
                Name = "Sinh viên"
            };

            yield return new StudentRoleData
            {
                Value = 1,
                IsShow = true,
                Name = "Ban cán sự lớp, các CLB"
            };

            yield return new StudentRoleData
            {
                Value = 2,
                IsShow = true,
                Name = "BCH Đoàn Hội"
            };

            yield return new StudentRoleData
            {
                Value = 3,
                IsShow = true,
                Name = "Sinh viên 5 tốt"
            };

            yield return new StudentRoleData
            {
                Value = 4,
                IsShow = true,
                Name = "Sinh viên nghiên cứu khoa học"
            };

            yield return new StudentRoleData
            {
                Value = 5,
                IsShow = true,
                Name = "Sinh viên vượt khó/ảnh hưởng bởi dịch Covid-19"
            };
        }
    }
}
