using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1.CategoryDTOs
{
    public class CategoryCreateDTO
    {
        
        [MaxLength(128)] public string Description { get; set; } = default!;
        
        public Guid? ParentCategoryId { get; set; }
    }
}