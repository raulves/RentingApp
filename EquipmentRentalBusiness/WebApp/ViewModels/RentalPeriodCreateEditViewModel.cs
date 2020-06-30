#pragma warning disable 1591

using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.Raul.Vesinurm.Contracts.Domain;

namespace WebApp.ViewModels
{
    public class RentalPeriodCreateEditViewModel : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Resources.Views.Shared.Common))]
        [MaxLength(64, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Views.Shared.Common))]
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Views.Shared.Common))]
        [Display(Name = nameof(Description), ResourceType = typeof(Resources.Domain.RentalPeriod.RentalPeriod))]
        public string Description { get; set; } = default!;
        
        [Display(Name = nameof(PeriodStart), ResourceType = typeof(Resources.Domain.RentalPeriod.RentalPeriod))]
        public int PeriodStart { get; set; }
        
        [Display(Name = nameof(PeriodEnd), ResourceType = typeof(Resources.Domain.RentalPeriod.RentalPeriod))]
        public int PeriodEnd { get; set; }
    }
}