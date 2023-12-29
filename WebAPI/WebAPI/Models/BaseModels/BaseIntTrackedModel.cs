using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.BaseModels
{
    public class BaseIntTrackedModel : BaseModelInt
    {
        [Required]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedOn { get; set; } = DateTime.UtcNow;
    }
}
