using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PublicApi.DTO.v1.BookingDTOs
{
    public class BookingCreateDTO
    {
        public string BookingNumber { get; set; } = default!;
        
        public DateTime BookingDate { get; set; }
        
        public DateTime BookingStartDay { get; set; }
        
        public DateTime BookingEndDay { get; set; }
        public int BookingPeriodDays { get; set; }
        public decimal PricePerDay { get; set; }
        public decimal VatPercent { get; set; }
        public decimal Vat { get; set; }
        public decimal BookingWithoutVat { get; set; }
        public decimal BookingTotal { get; set; }
        
        public Guid ItemId { get; set; }
        public Guid? ItemOwnerId { get; set; }
        public Guid? RenterId { get; set; }
        public Guid? ItemOwnerCompanyId { get; set; }
        public Guid? RenterCompanyId { get; set; }
        public Guid? InvoiceId { get; set; }
    }
}