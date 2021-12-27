using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nasrisite.MyModel
{
    public class participantmember
    {
        public int ID { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public decimal Experience { get; set; }

        [Required]
        public string JobTitle { get; set; }
    }
}