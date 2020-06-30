#pragma warning disable 1591
using System;
using ee.itcollege.Raul.Vesinurm.Contracts.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class PaymentCreateEditViewModel : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        
        public Guid PaymentTypeId { get; set; }

        public Guid InvoiceId { get; set; }

        public Guid AppUserId { get; set; }

        public Guid? CompanyId { get; set; }
        
        
        public SelectList? CompanySelectList { get; set; }
        public SelectList? InvoiceSelectList { get; set; }
        public SelectList? PaymentTypeSelectList { get; set; }
        
    }
}