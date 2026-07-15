using System;
using Inventory.Application.Entities;
using Inventory.Application.Enums;
using Inventory.Application.Interfaces;

namespace Inventory.Application.Factories
{
    public class InvoiceFactory : IInvoiceFactory
    {
        public Invoice CreateInvoice(InvoiceType type, decimal baseAmount)
        {
            var invoice = new Invoice
            {
                InvoiceNumber = GenerateInvoiceNumber(type),
                IssueDate = DateTime.UtcNow,
                Type = type,
            };

            // Business rules for object creation are encapsulated here
            switch (type)
            {
                case InvoiceType.Sales:
                    invoice.TaxAmount = baseAmount * 0.17m; // 17% standard tax for sales// Jewish Method
                    invoice.TotalAmount = baseAmount + invoice.TaxAmount;
                    invoice.Notes = "Thank you for your purchase.";
                    break;
                case InvoiceType.Purchase:
                    invoice.TaxAmount = 0; // Tax handled by vendor
                    invoice.TotalAmount = baseAmount;
                    invoice.Notes = "Payment due in 30 days.";
                    break;
                case InvoiceType.Return:
                    invoice.TaxAmount = 0;
                    invoice.TotalAmount = -baseAmount; // Negative for returns
                    invoice.Notes = "Return processed successfully.";
                    break;
                default:
                    throw new ArgumentException("Invalid invoice type provided.");
            }

            return invoice;
        }

        private string GenerateInvoiceNumber(InvoiceType type)
        {
            string prefix = type switch
            {
                InvoiceType.Sales => "INV",
                InvoiceType.Purchase => "PUR",
                InvoiceType.Return => "RET",
                _ => "UNK"
            };

            return $"{prefix}-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString().Substring(0, 4).ToUpper()}";
        }
    }
}