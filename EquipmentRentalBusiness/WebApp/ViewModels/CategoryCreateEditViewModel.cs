#pragma warning disable 1591
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.Raul.Vesinurm.Contracts.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class CategoryCreateEditViewModel : IDomainEntityId
    {

        public Guid Id { get; set; } = default!;

        public Guid DescriptionId { get; set; }
        
        [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Resources.Views.Shared.Common))]
        [MaxLength(64, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Views.Shared.Common))]
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Views.Shared.Common))]
        [Display(Name = nameof(Description), ResourceType = typeof(Resources.Domain.Category.Category))]
        public string Description { get; set; } = default!;
        
        [Display(Name = nameof(ParentCategoryId), ResourceType = typeof(Resources.Domain.Category.Category))]
        public Guid? ParentCategoryId { get; set; }

        public CategoryCreateEditViewModel? ParentCategory { get; set; }
        
        [Display(Name = nameof(ChildCategories), ResourceType = typeof(Resources.Domain.Category.Category))]
        public ICollection<CategoryCreateEditViewModel>? ChildCategories { get; set; }

        public ICollection<ItemCategoriesCreateEditViewModel>? ItemCategories { get; set; }

        public SelectList? ParentCategorySelectList { get; set; }
    }
}