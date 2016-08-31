using System.ComponentModel.DataAnnotations;

namespace CapitalCoffee.Data.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
