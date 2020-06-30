using System;
using System.ComponentModel.DataAnnotations;

using ee.itcollege.Raul.Vesinurm.Contracts.Domain;

namespace PublicApi.DTO.v1.CategoryDTOs
{
    public class CategoryDTO : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        [MaxLength(128)] public string Description { get; set; } = default!;
        
        public Guid? ParentCategoryId { get; set; }
    }
}