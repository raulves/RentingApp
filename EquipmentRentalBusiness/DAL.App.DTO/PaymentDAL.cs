using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

using ee.itcollege.Raul.Vesinurm.Contracts.Domain;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class PaymentDAL : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid AppUserId { get; set; } = default!;
        [JsonIgnore]
        public AppUserDAL? AppUser { get; set; }
        
        public decimal Amount { get; set; }
        
        public DateTime PaymentDate { get; set; } = DateTime.Now;
        
        public Guid PaymentTypeId { get; set; } = default!;
        [JsonIgnore]
        public PaymentTypeDAL? PaymentType { get; set; }
        
        public Guid InvoiceId { get; set; } = default!;
        [JsonIgnore]
        public InvoiceDAL? Invoice { get; set; }
        
        public Guid? CompanyId { get; set; }
        [JsonIgnore]
        public CompanyDAL? Company { get; set; }
    }
    
}