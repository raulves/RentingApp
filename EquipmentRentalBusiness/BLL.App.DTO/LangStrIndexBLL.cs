using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ee.itcollege.Raul.Vesinurm.Contracts.Domain;

namespace BLL.App.DTO
{
    public class LangStrIndexBLL : IDomainEntityId
    {
        public Guid Id { get; set; }
        public ICollection<LangStrTranslation>? Translations { get; set; }

        
        public ICollection<CategoryBLL>? CategoryDescriptions { get; set; }
        
        public ICollection<PaymentTypeBLL>? PaymentTypeDescriptions { get; set; }
        
        public ICollection<ProductDescriptionBLL>? ProductDescriptions { get; set; }
    }
}