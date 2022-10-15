namespace JobFair.ViewModels
{
    /// <summary>
    /// Result/Return model for Business Logic Layer return data with message
    /// </summary>
    public class ResultViewModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }
    }
}
