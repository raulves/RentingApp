using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;

using ee.itcollege.Raul.Vesinurm.BLL.Base.Service;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using PublicApi.DTO.v1.InvoiceDTOs;

namespace BLL.App.Services
{
    public class InvoiceService : BaseEntityService<IAppUnitOfWork, IInvoiceRepository, IInvoiceServiceMapper, InvoiceDAL, InvoiceBLL>, IInvoiceService
    {
        public InvoiceService(IAppUnitOfWork uow) 
            : base(uow, uow.Invoices, new InvoiceServiceMapper())
        {
        }

        
        
        public string GetInvoiceNumber()
        {
            var invoiceNumber = UOW.Invoices.GetLastInvoiceNumber();
            if (invoiceNumber == null)
            {
                return "1";
            }

            var number = 1;
            int.TryParse(invoiceNumber, out number);
            var newInvoiceNumber = number + 1;
            return newInvoiceNumber.ToString();
        }

        public decimal CalculateInvoiceTotalWithoutVAT(List<BookingBLL> bookings)
        {
            decimal total = 0;
            foreach (var booking in bookings)
            {
                
                total += booking.BookingWithoutVat;
            }

            return total;
        }

        public decimal CalculateInvoiceVAT(List<BookingBLL> bookings)
        {
            decimal vat = 0;
            foreach (var booking in bookings)
            {
                vat += booking.Vat;
            }
            return vat;
        }

        public decimal CalculateInvoiceTotalWithVAT(List<BookingBLL> bookings)
        {
            decimal total = 0;
            foreach (var booking in bookings)
            {
                total += booking.BookingTotal;
            }
            return total;
        }
    }
}