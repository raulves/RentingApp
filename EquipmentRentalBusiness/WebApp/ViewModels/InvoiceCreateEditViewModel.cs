#pragma warning disable 1591
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BLL.App.DTO;
using ee.itcollege.Raul.Vesinurm.Contracts.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class InvoiceCreateEditViewModel : IDomainEntityId
    {
        public IEnumerable<BookingsCreateEditViewModel>? Bookings { get; set; }
        
        public Guid Id { get; set; }

        public Guid AppUserId { get; set; }

        public Guid? CompanyId { get; set; }

        [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Resources.Views.Shared.Common))]
        [MaxLength(64, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Views.Shared.Common))]
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Views.Shared.Common))]
        public string InvoiceNumber { get; set; } = default!;
        public DateTime InvoiceDate { get; set; }
        public decimal VatPercent { get; set; }
        public decimal Vat { get; set; }
        public decimal InvoiceWithoutVat { get; set; }
        public decimal InvoiceTotal { get; set; }

        public SelectList? CompanySelectList { get; set; }
        public SelectList? PaymentTypeSelectList { get; set; }

        [Display(Name = nameof(PaymentTypeId), ResourceType = typeof(Resources.Domain.Invoice.Invoice))]
        public Guid PaymentTypeId { get; set; }
    }
}