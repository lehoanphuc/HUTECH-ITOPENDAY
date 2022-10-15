using JobFair.ViewModels;
using System.Collections.Generic;

namespace JobFair.Shared.MasterData
{
    public class CompanyData
    {
        // Static Data
        public static CompanyViewModel TMA = new CompanyViewModel
        {
            Name = "TMA Solutions",
            Logo = "tma.png"
        };

        public static CompanyViewModel SCC = new CompanyViewModel
        {
            Name = "SCC",
            Logo = "scc.png"
        };

        public static CompanyViewModel FPT = new CompanyViewModel
        {
            Name = "FPT Software",
            Logo = "fpt.png"
        };

        public static CompanyViewModel CMC = new CompanyViewModel
        {
            Name = "CMC Global",
            Logo = "cmc.png"
        };

        public static CompanyViewModel LIFULL = new CompanyViewModel
        {
            Name = "Lifull",
            Logo = "lifull.png"
        };

        public static CompanyViewModel HITACHI = new CompanyViewModel
        {
            Name = "Hitachi Vantara",
            Logo = "hitachi.png"
        };

        public static CompanyViewModel KMS = new CompanyViewModel
        {
            Name = "KMS Technology",
            Logo = "kms.png"
        };

        public static CompanyViewModel VITALIFY = new CompanyViewModel
        {
            Name = "Vitalify Asia",
            Logo = "vitalifyasia.png"
        };

        public static CompanyViewModel DXC = new CompanyViewModel
        {
            Name = "DXC Technology",
            Logo = "dxc.png"
        };

        public static CompanyViewModel FUJINET = new CompanyViewModel
        {
            Name = "Fujinet",
            Logo = "fujinet.png"
        };

        public static CompanyViewModel CISCO = new CompanyViewModel
        {
            Name = "Cisco Network Academy",
            Logo = "cisco.png"
        };

        public static CompanyViewModel SHTP = new CompanyViewModel
        {
            Name = "SHTP",
            Logo = "shtp.png"
        };

        public static CompanyViewModel HCA = new CompanyViewModel
        {
            Name = "HCA",
            Logo = "hca.png"
        };

        public static CompanyViewModel VNITO = new CompanyViewModel
        {
            Name = "VNITO",
            Logo = "vnito.png"
        };

        public static CompanyViewModel VINASA = new CompanyViewModel
        {
            Name = "VINASA",
            Logo = "vinasa.png"
        };

        public static CompanyViewModel INTEL = new CompanyViewModel
        {
            Name = "Intel",
            Logo = "intel.png"
        };

        public static CompanyViewModel ORCO = new CompanyViewModel
        {
            Name = "ORCO",
            Logo = "orco.png"
        };

        public static List<CompanyViewModel> GetCoOrganizer()
        {
            var list = new List<CompanyViewModel>();
            list.Add(SHTP);
            list.Add(HCA);
            list.Add(VNITO);
            list.Add(VINASA);

            return list;
        }

        /// <summary>
        /// Xem danh sách tài trợ học bổng
        /// </summary>
        /// <returns></returns>
        public static List<CompanyViewModel> GetSponsorsScholarship()
        {
            var list = new List<CompanyViewModel>();

            list.Add(DXC);
            list.Add(SCC);
            list.Add(ORCO);
            list.Add(KMS);
            list.Add(INTEL);
            list.Add(TMA);


            return list;
        }

        public static Dictionary<string, List<CompanyViewModel>> GetSponsors()
        {
            var result = new Dictionary<string, List<CompanyViewModel>>();

            // Tài trợ chính
            var list1 = new List<CompanyViewModel>();
            list1.Add(TMA);
            result.Add("Tài trợ chính", list1);

            // Tài trợ đồng hành
            var list3 = new List<CompanyViewModel>();
            list3.Add(SCC);
            list3.Add(FPT);
            list3.Add(DXC);
            list3.Add(FUJINET);
            list3.Add(HITACHI);
            list3.Add(CMC);

            result.Add("Tài trợ đồng hành", list3);

            // Tài trợ đồng hành & Học bổng
            var list2 = new List<CompanyViewModel>();
            list2.Add(KMS);
            list2.Add(SCC);
            list2.Add(DXC);
            list2.Add(CISCO);

            result.Add("Tài trợ Quà tặng & Học bổng", list2);


            // Doanh nghiệp tuyển dụng
            var list4 = new List<CompanyViewModel>();
            list4.Add(TMA);
            list4.Add(SCC);
            list4.Add(FPT);
            list4.Add(DXC);
            list4.Add(FUJINET);
            list4.Add(HITACHI);
            list4.Add(CMC);
            list4.Add(KMS);
            list4.Add(CISCO);

            result.Add("Doanh nghiệp tuyển dụng", list4);

            return result;
        }
    }
}
