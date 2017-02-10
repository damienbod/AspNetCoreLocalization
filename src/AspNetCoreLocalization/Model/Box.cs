using System.ComponentModel.DataAnnotations;
    
namespace AspNet5Localization.Model
{
    public class Box
    {
        public long Id { get; set; }

        public double Height { get; set; }

        public double Width { get; set; }

        [Required(ErrorMessage = "BoxLengthRequired")]
        [Range(1.0, 100.0, ErrorMessage = "BoxLengthRange")]
        public double Length { get; set; }
    }
}
