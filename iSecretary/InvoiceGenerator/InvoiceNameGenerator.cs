using System;

namespace Invoices
{
    public class InvoiceNameGenerator
    {
        public static string GetName(int number, DateTime now)
        {
            return string.Format("{0}-{1}", number.ToString("000"), now.ToString("MMyyyy"));
        }
    }
}