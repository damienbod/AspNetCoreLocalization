using System.ComponentModel.DataAnnotations;

namespace AspNet5Localization.Model
{
    using AspNet5Localization.Controllers;

    public class Box
    {
        public long Id { get; set; }

        public double Height { get; set; }

        public double Width { get; set; }

        [Required(ErrorMessageResourceName = "BoxLengthRequired", ErrorMessageResourceType = typeof(AmazingResource))]
        [Range(1.0, 100.0, ErrorMessageResourceName = "BoxLengthRange", ErrorMessageResourceType = typeof(AmazingResource))]
        public double Length { get; set; }
    }
}
