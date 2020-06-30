#pragma warning disable 1591
using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.Raul.Vesinurm.Contracts.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class ItemViewVM : IDomainEntityId
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = default!;
        
        public byte[]? Image { get; set; }
        
        public string AddressLine { get; set; } = default!;
        
        public decimal Price { get; set; }

        
    }
}