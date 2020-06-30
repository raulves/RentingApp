using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ee.itcollege.Raul.Vesinurm.Contracts.Domain;

namespace BLL.App.DTO
{
    public class LangStr : IDomainEntityId
    {
        public Guid Id { get; set; }

        public string CurrentValue { get; set; } = default!;
        public int CultureCount { get; set; }

    }
}