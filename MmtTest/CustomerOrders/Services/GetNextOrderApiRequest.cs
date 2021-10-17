using System.ComponentModel.DataAnnotations;

namespace CustomerOrders.Services
{
    public class GetNextOrderApiRequest
    {
        [Required]
        public string User { get; set; }
        [Required]
        public string CustomerId { get; set; }
    }
}