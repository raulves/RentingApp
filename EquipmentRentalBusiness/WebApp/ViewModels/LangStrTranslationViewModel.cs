#pragma warning disable 1591
using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.Raul.Vesinurm.Contracts.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class LangStrTranslationViewModel : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        [MaxLength(5)] public string Culture { get; set; } = default!;
        [MaxLength(10240)] public string Value { get; set; } = default!;

        public Guid LangStrId { get; set; } = default!;
        public LangStrIndexViewModel? LangStr { get; set; }

        public SelectList? LangStrSelectList { get; set; }
    }
}