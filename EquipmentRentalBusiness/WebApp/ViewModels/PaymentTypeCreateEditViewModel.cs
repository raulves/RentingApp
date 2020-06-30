#pragma warning disable 1591

using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.Raul.Vesinurm.Contracts.Domain;

namespace WebApp.ViewModels
{
    public class PaymentTypeCreateEditViewModel : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid DescriptionId { get; set; }
        
        [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Resources.Views.Shared.Common))]
        [MaxLength(64, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Views.Shared.Common))]
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Views.Shared.Common))]
        [Display(Name = nameof(Description), ResourceType = typeof(Resources.Domain.PaymentType.PaymentType))]
        public string Description { get; set; } = default!;
    }
}