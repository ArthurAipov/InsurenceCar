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
    using System;
    using System.Collections.Generic;
    
    public partial class Model
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string ModelName { get; set; }
        public string Category { get; set; }
        public string Year { get; set; }
        public string ModelYear { get; set; }
        public string Price { get; set; }
    
        public virtual ICollection<Car> Car { get; set; }
    }
}
