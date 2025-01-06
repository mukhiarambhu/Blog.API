using System.ComponentModel.DataAnnotations;

namespace Blog.API.Models.Dto
{
    public class UpdateBlogRequest
    {
        [Required(ErrorMessage = "Title is Required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "ShortDescription is Required")]
        public string ShortDescription { get; set; }

        [Required(ErrorMessage = "Content is Required")]
        public string Content { get; set; }

        [Required(ErrorMessage = "FeaturedImageUrl is Required")]
        public string FeaturedImageUrl { get; set; }

        [Required(ErrorMessage = "UrlHandle is Required")]
        public string UrlHandle { get; set; }

        [Required(ErrorMessage = "Author is Required")]
        public DateTime PublishedDate { get; set; }

        [Required(ErrorMessage = "Author is Required")]
        public string Author { get; set; }

        [Required(ErrorMessage = "isVisible is Required")]
        public bool isVisible { get; set; }

        public Guid[] categories { get; set; }
    }
}
