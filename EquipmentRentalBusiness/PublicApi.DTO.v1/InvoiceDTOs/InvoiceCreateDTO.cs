using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1.InvoiceDTOs
{
    public class InvoiceCreateDTO
    {
        [MaxLength(64)]
        public string InvoiceNumber { get; set; } = default!;
        public DateTime InvoiceDate { get; set; }
        public decimal VatPercent { get; set; }
        public decimal Vat { get; set; }
        public decimal InvoiceWithoutVat { get; set; }
        public decimal InvoiceTotal { get; set; }
    }
}