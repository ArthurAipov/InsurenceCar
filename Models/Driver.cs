//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InsurenceCar.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    
    public partial class Driver
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronic { get; set; }
        public string PassportData { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string DriverLicense { get; set; }
        public System.DateTime Experience { get; set; }
        public System.DateTime DateOfBirth { get; set; }
        public bool BlackList { get; set; }


        [JsonIgnore]
        public User User
        {
            get
            {
                return DBConnection.Users.FirstOrDefault(u => u.Id == Id);
            }
            set
            {
                UserId = value.Id;
            }
        }

        public virtual ICollection<Car> Car { get; set; }
        public virtual ICollection<Casco> Casco { get; set; }
        public virtual ICollection<EmergencyApplication> EmergencyApplication { get; set; }
        public virtual ICollection<Osago> Osago { get; set; }
    }
}