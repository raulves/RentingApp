using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using ee.itcollege.Raul.Vesinurm.Domain.Base;

namespace Domain.App
{
    public class Invoice : DomainEntityIdMetadataUser<AppUser>
    {
        [MinLength(1)]
        [MaxLength(64)]
        public string InvoiceNumber { get; set; } = default!;
        
        public DateTime InvoiceDate { get; set; }
        
        public decimal VatPercent { get; set; }
        
        public decimal Vat { get; set; }
        
        public decimal InvoiceWithoutVat { get; set; }
        
        public decimal InvoiceTotal { get; set; }

        public ICollection<Booking>? Bookings { get; set; }

        public ICollection<Payment>? Payments { get; set; }
        
        public Guid? CompanyId { get; set; }

        public Company? Company { get; set; }
    }
    
}