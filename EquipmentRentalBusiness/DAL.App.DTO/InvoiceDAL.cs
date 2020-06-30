using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

using ee.itcollege.Raul.Vesinurm.Contracts.Domain;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class InvoiceDAL : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid AppUserId { get; set; } = default!;
        [JsonIgnore]
        public AppUserDAL? AppUser { get; set; }
        
        public string InvoiceNumber { get; set; } = default!;
        
        public DateTime InvoiceDate { get; set; } = DateTime.Now;
        
        public decimal VatPercent { get; set; }
        
        public decimal Vat { get; set; }
        
        public decimal InvoiceWithoutVat { get; set; }
        
        public decimal InvoiceTotal { get; set; }

        [JsonIgnore]
        public ICollection<BookingDAL>? Bookings { get; set; }

        [JsonIgnore]
        public ICollection<PaymentDAL>? Payments { get; set; }
        
        public Guid? CompanyId { get; set; }
        [JsonIgnore]
        public CompanyDAL? Company { get; set; }
    }
    
}