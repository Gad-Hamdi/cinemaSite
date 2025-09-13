namespace cinemaSite.ViewModels
{
    public class FilterVM
    {
        public string? Name { get; set; }
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
        public int? CategoryId { get; set; }
        public bool IsHot { get; set; }
    }
}
