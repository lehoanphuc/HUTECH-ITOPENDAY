namespace JobFair.ViewModels
{
    public class PagedDataViewModel
    {
        public int TotalRecord { get; set; }
        public int TotalPage { get; set; }
        public dynamic Data { get; set; }
    }
}
