#nullable enable

using System;
using System.Collections.Generic;

namespace Core.Finance // 🔥 Correct Namespace for Finance Module
{
    // ✅ Company Master (For Finance Module)
    public class CompanyMaster : BaseEntity
    {
        public string? Name { get; set; }
        public string? LegalName { get; set; }
        public string? TaxId { get; set; } // GST, VAT, or TIN
        public string? Address { get; set; }
        public string? ContactNumber { get; set; }
        public string? Email { get; set; }
        public string? Currency { get; set; } // Example: USD, INR, EUR
        public decimal Balance { get; set; }

        public ICollection<Account> Accounts { get; set; } = new List<Account>();
        public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
        public ICollection<Bill> Bills { get; set; } = new List<Bill>();
    }

    // ✅ General Ledger & Accounts
    public class Account : BaseEntity // 🔥 Moved Above References
    {
        public int CompanyMasterId { get; set; }
        public CompanyMaster? CompanyMaster { get; set; }

        public string? Name { get; set; }
        public string? AccountNumber { get; set; }
        public AccountType Type { get; set; } // Asset, Liability, Revenue, Expense
        public decimal Balance { get; set; }
    }

    public enum AccountType
    {
        Asset,
        Liability,
        Revenue,
        Expense,
        Equity
    }

    public class JournalEntry : BaseEntity
    {
        public int CompanyMasterId { get; set; }
        public CompanyMaster? CompanyMaster { get; set; }

        public DateTime EntryDate { get; set; }
        public string? Reference { get; set; }
        public ICollection<JournalEntryLine> JournalEntryLines { get; set; } = new List<JournalEntryLine>();
    }

    public class JournalEntryLine : BaseEntity
    {
        public int JournalEntryId { get; set; }
        public JournalEntry? JournalEntry { get; set; }

        public int AccountId { get; set; }
        public Account? Account { get; set; } // 🔥 Now Account is Defined Before Being Used

        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
    }

    // ✅ Banking & Reconciliation
    public class BankAccount : BaseEntity
    {
        public int CompanyMasterId { get; set; }
        public CompanyMaster? CompanyMaster { get; set; }

        public string? BankName { get; set; }
        public string? AccountNumber { get; set; }
        public decimal Balance { get; set; }
    }

    public class BankTransaction : BaseEntity
    {
        public int CompanyMasterId { get; set; }
        public CompanyMaster? CompanyMaster { get; set; }

        public int BankAccountId { get; set; }
        public BankAccount? BankAccount { get; set; }

        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string? Description { get; set; }
    }

    public class BankReconciliation : BaseEntity
    {
        public int CompanyMasterId { get; set; }
        public CompanyMaster? CompanyMaster { get; set; }

        public int BankAccountId { get; set; }
        public BankAccount? BankAccount { get; set; }

        public decimal BankBalance { get; set; }
        public decimal SystemBalance { get; set; }
        public DateTime ReconciliationDate { get; set; }
        public string? Notes { get; set; }
    }

    // ✅ Accounts Receivable (Invoices & Customers)
    public class FinanceCustomer : BaseEntity
    {
        public int CompanyMasterId { get; set; }
        public CompanyMaster? CompanyMaster { get; set; }

        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }

    public class Invoice : BaseEntity
    {
        public int CompanyMasterId { get; set; }
        public CompanyMaster? CompanyMaster { get; set; }

        public int FinanceCustomerId { get; set; }
        public FinanceCustomer? FinanceCustomer { get; set; }

        public DateTime InvoiceDate { get; set; }
        public decimal Amount { get; set; }
        public bool IsPaid { get; set; }
    }

    // ✅ Accounts Payable (Vendor Bills)
    public class Vendor : BaseEntity
    {
        public int CompanyMasterId { get; set; }
        public CompanyMaster? CompanyMaster { get; set; }

        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }

    public class Bill : BaseEntity
    {
        public int CompanyMasterId { get; set; }
        public CompanyMaster? CompanyMaster { get; set; }

        public int VendorId { get; set; }
        public Vendor? Vendor { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Amount { get; set; }
        public bool IsPaid { get; set; }
    }

    // ✅ Tax Management
    public class TaxRate : BaseEntity
    {
        public int CompanyMasterId { get; set; }
        public CompanyMaster? CompanyMaster { get; set; }

        public string? TaxName { get; set; } // Example: GST, VAT, TDS
        public decimal Rate { get; set; } // Example: 18% GST
    }

    public class TaxTransaction : BaseEntity
    {
        public int CompanyMasterId { get; set; }
        public CompanyMaster? CompanyMaster { get; set; }

        public int TaxRateId { get; set; }
        public TaxRate? TaxRate { get; set; }

        public int InvoiceId { get; set; }
        public Invoice? Invoice { get; set; }

        public decimal TaxAmount { get; set; }
    }
}
