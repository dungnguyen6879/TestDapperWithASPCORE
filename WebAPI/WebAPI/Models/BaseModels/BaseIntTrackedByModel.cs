using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.BaseModels
{
    public class BaseIntTrackedByModel : BaseIntTrackedModel
    {        
        [Required]
        public Guid CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
    }
}
