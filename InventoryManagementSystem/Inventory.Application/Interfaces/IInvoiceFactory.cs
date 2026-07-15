using Inventory.Application.Entities;
using Inventory.Application.Enums;

namespace Inventory.Application.Interfaces
{
    public interface IInvoiceFactory
    {
        Invoice CreateInvoice(InvoiceType type, decimal baseAmount);
    }
}