using System.Linq;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;



using Domain.App;
using ee.itcollege.Raul.Vesinurm.DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class InvoiceRepository : EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Invoice, InvoiceDAL>, IInvoiceRepository
    {
        public InvoiceRepository(AppDbContext dbContext) : base(dbContext, 
            new DALMapper<Invoice, InvoiceDAL>())
        {
        }

        public string? GetLastInvoiceNumber()
        {
            var invoices = RepoDbSet.AsNoTracking().OrderByDescending(a => a.InvoiceNumber).ToListAsync();
            if (invoices.Result.Count == 0)
            {
                return null;
            }
            return invoices.Result.FirstOrDefault().InvoiceNumber;
        }
    }
}