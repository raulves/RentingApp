using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ee.itcollege.Raul.Vesinurm.Contracts.Domain;

namespace DAL.App.DTO
{
    public class LangStrIndexDAL : IDomainEntityId
    {
        public Guid Id { get; set; }
        public ICollection<LangStrTranslation>? Translations { get; set; }
        
        public ICollection<CategoryDAL>? CategoryDescriptions { get; set; }
        
        public ICollection<PaymentTypeDAL>? PaymentTypeDescriptions { get; set; }
        
        public ICollection<ProductDescriptionDAL>? ProductDescriptions { get; set; }
    }
}