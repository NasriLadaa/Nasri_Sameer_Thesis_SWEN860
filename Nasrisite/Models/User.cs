//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Nasrisite.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        public int UserId { get; set; }
        public string UserGUIDId { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserHometown { get; set; }
        public string UserPhoneNumber { get; set; }
        public string UserEmail { get; set; }
        public bool UserEmailConfirmation { get; set; }
        public System.DateTime UserCreationDate { get; set; }
        public bool UserActivation { get; set; }
        public string UserProfilePhoto { get; set; }
        public double UserRate { get; set; }
        public string UserBio { get; set; }
    }
}
