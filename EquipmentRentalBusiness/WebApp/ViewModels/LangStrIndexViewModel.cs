using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ee.itcollege.Raul.Vesinurm.Contracts.Domain;

#pragma warning disable 1591
namespace WebApp.ViewModels
{
    public class LangStrIndexViewModel : IDomainEntityId
    {
        public Guid Id { get; set; }
        public ICollection<LangStrTranslationViewModel>? Translations { get; set; }

        [InverseProperty(nameof(CategoryCreateEditViewModel.Description))]
        public ICollection<CategoryCreateEditViewModel>? CategoryDescriptions { get; set; }
        
        public ICollection<PaymentTypeCreateEditViewModel>? PaymentTypeDescriptions { get; set; }
        
        public ICollection<ProductDescriptionCreateEditViewModel>? ProductDescriptions { get; set; }
    }
}