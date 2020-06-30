using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

using ee.itcollege.Raul.Vesinurm.Contracts.Domain;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class CategoryDAL : IDomainEntityId
    {
        public Guid Id { get; set; }

        public Guid DescriptionId { get; set; }
        public string Description { get; set; } = default!;
        
        public Guid? ParentCategoryId { get; set; }
        [JsonIgnore]
        public CategoryDAL? ParentCategory { get; set; }
        
        [JsonIgnore]
        public ICollection<CategoryDAL>? ChildCategories { get; set; }

        [JsonIgnore]
        public ICollection<ItemCategoryDAL>? ItemCategories { get; set; }
    }
   
}