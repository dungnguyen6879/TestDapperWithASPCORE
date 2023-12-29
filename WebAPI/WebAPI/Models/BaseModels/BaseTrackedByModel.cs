using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.BaseModels
{
    public class BaseTrackedByModel : BaseTrackedModel
    {        
        [Required]
        public Guid CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
    }
}
