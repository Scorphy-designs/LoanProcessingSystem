//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LoanProcessingSystem.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class EnquiryTable
    {
        public int EnquiryId { get; set; }
        public string EmailId { get; set; }
        public decimal PhoneNo { get; set; }
        public string PropertyType { get; set; }
        public System.DateTime Date { get; set; }
        public string Message { get; set; }
    }
}