using System;
using System.ComponentModel.DataAnnotations;

using ee.itcollege.Raul.Vesinurm.Contracts.Domain;

namespace PublicApi.DTO.v1.InvoiceDTOs
{
    public class InvoiceDTO : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid AppUserId { get; set; }
        
        public Guid? CompanyId { get; set; }

        [MaxLength(64)]
        public string InvoiceNumber { get; set; } = default!;
        public DateTime InvoiceDate { get; set; }
        public decimal VatPercent { get; set; }
        public decimal Vat { get; set; }
        public decimal InvoiceWithoutVat { get; set; }
        public decimal InvoiceTotal { get; set; }
        
    }
}