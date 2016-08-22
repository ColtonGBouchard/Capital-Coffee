using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalCoffee.Data.Models
{
    public class ProfilePicture
    {
        public int ProfilePictureId { get; set; }
        public int UserId { get; set; }
        public byte[] Picture { get; set; }
        public string MimeType { get; set; }
        public User User { get; set; }
    }
}
