using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BLL.App.DTO.Identity;

using ee.itcollege.Raul.Vesinurm.Contracts.Domain;

namespace BLL.App.DTO
{
    public class InvoiceBLL :IDomainEntityId
    {
        public Guid Id { get; set; } = default!;
        
        public Guid AppUserId { get; set; } = default!;
        public AppUserBLL? AppUser { get; set; }
        
        public string InvoiceNumber { get; set; } = default!;
        
        public DateTime InvoiceDate { get; set; } = DateTime.Now;
        
        public decimal VatPercent { get; set; }
        
        public decimal Vat { get; set; }
        
        public decimal InvoiceWithoutVat { get; set; }
        
        public decimal InvoiceTotal { get; set; }

        public ICollection<BookingBLL>? Bookings { get; set; }

        public ICollection<PaymentBLL>? Payments { get; set; }
        
        public Guid? CompanyId { get; set; }

        public CompanyBLL? Company { get; set; }
    }
    
}