using System.ComponentModel.DataAnnotations;
using Shipping.Core.Model;

namespace Shipping.DTO.Branch
{
    public class BranchDTO
    {
        [Required]
        public string Name { get; set; }
        public bool isDeleted { get; set; } = false;
        public DateTime DateTime { get; set; } = DateTime.Now;
        public bool status { get; set; } = true;

    }
}
