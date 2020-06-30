using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1.BookingDTOs
{
    public class BookingEditDTO
    {
        public Guid Id { get; set; }
        public string BookingNumber { get; set; } = default!;
        [DataType(DataType.Date)]
        public DateTime BookingDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime BookingStartDay { get; set; }
        [DataType(DataType.Date)]
        public DateTime BookingEndDay { get; set; }
        public int BookingPeriodDays { get; set; }
        public decimal PricePerDay { get; set; }
        public decimal VatPercent { get; set; }
        public decimal Vat { get; set; }
        public decimal BookingWithoutVat { get; set; }
        public decimal BookingTotal { get; set; }
    }
}