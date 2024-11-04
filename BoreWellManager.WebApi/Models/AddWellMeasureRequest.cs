namespace BoreWellManager.WebApi.Models
{
    public class AddWellMeasureRequest
    {
        public decimal? Debi { get; set; }
        public decimal? StaticLevel { get; set; }
        public decimal? DynamicLevel { get; set; }
    }
}
