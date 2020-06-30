using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1.ImageDTOs
{
    public class ImageCreateDTO
    {
        [MaxLength(4096)]
        public string? Picture { get; set; }

       
        public Guid ItemId { get; set; }
    }
}