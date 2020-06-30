#pragma warning disable 1591
using System;
using ee.itcollege.Raul.Vesinurm.Contracts.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class ItemsBookedCreateEditViewModel : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid AppUserId { get; set; }
        
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        
        public Guid ItemId { get; set; }

        public Guid BookingId { get; set; }

        public SelectList? BookingSelectList { get; set; }
        public SelectList? ItemSelectList { get; set; }
    }
}