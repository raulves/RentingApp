#pragma warning disable 1591
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

using ee.itcollege.Raul.Vesinurm.Contracts.Domain;

namespace WebApp.ViewModels
{
    public class LangStrViewModel : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public string CurrentValue { get; set; } = default!;
        public int CultureCount { get; set; }
    }
}