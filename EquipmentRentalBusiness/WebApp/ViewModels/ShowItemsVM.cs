#pragma warning disable 1591
using System;
using System.Collections.Generic;
using ee.itcollege.Raul.Vesinurm.Contracts.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class ShowItemsVM
    {
        public IEnumerable<ItemViewVM>? Items { get; set; }
        public SelectList? CategorySelectList { get; set; }

        public Guid CategoryId { get; set; } = new Guid("00000000-0000-0000-0000-000000000000");

    }
}