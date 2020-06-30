using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BLL.App.DTO.Identity;

using ee.itcollege.Raul.Vesinurm.Contracts.Domain;

namespace BLL.App.DTO
{
    public class CategoryBLL : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;

        public Guid DescriptionId { get; set; }
        public string Description { get; set; } = default!;
        
        public Guid? ParentCategoryId { get; set; }

        public CategoryBLL? ParentCategory { get; set; }
        
        public ICollection<CategoryBLL>? ChildCategories { get; set; }

        public ICollection<ItemCategoryBLL>? ItemCategories { get; set; }
    }
    
}