using System;
using System.Collections.Generic;
using BLL.App.DTO;
using ee.itcollege.Raul.Vesinurm.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;

namespace Contracts.BLL.App.Services
{
    public interface IInvoiceService : IBaseEntityService<InvoiceBLL>, IInvoiceRepositoryCustom<InvoiceBLL>
    {
        // TODO : add custom methods
        string GetInvoiceNumber();
        decimal CalculateInvoiceTotalWithoutVAT(List<BookingBLL> bookings);
        decimal CalculateInvoiceVAT(List<BookingBLL> bookings);
        decimal CalculateInvoiceTotalWithVAT(List<BookingBLL> bookings);
    }
}