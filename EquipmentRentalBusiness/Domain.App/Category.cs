using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.Raul.Vesinurm.Domain.Base;

namespace Domain.App
{
    public class Category : DomainEntityIdMetadata
    {
        
        public Guid DescriptionId { get; set; }
        [MinLength(1)]
        [MaxLength(64)]
        public LangStr? Description { get; set; }
        
        public Guid? ParentCategoryId { get; set; }

        public Category? ParentCategory { get; set; }
        
        public ICollection<Category>? ChildCategories { get; set; }

        public ICollection<ItemCategory>? ItemCategories { get; set; }
    }
    
}