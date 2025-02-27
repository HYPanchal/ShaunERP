#nullable enable
using System;
using System.Collections.Generic;

namespace Core
{
    // Lead: Represents an unqualified potential customer.
    public class Lead : BaseEntity
    {
        // Basic Information
        public string? LeadOwner { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Title { get; set; }
        public string? Company { get; set; }

        // Contact Information
        public string? Phone { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public string? SecondaryEmail { get; set; }
        public string? Fax { get; set; }
        public string? Website { get; set; }

        // Lead Details
        public string? LeadSource { get; set; }
        public string? Industry { get; set; }
        public decimal? AnnualRevenue { get; set; }
        public bool? EmailOptOut { get; set; }
        public string? LeadStatus { get; set; }
        public int? NoOfEmployees { get; set; }
        public string? Rating { get; set; }

        // Social & Communication
        public string? SkypeId { get; set; }
        public string? Twitter { get; set; }

        // Address
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public string? Country { get; set; }

        // Additional Details
        public string? Description { get; set; }
        public string? UserId { get; set; }
        public string? UserLevel { get; set; }

        // Qualification Status (indicates if the lead has been converted to a prospect)
        public bool IsQualified { get; set; }

        // Demographic & Preference Details
        public string? PreferredContactMethod { get; set; }
        public string? Gender { get; set; }
        public string? Language { get; set; }
    }

    // Prospect: A qualified lead that is actively engaged.
    public class Prospect : BaseEntity
    {
        // Basic Account Information
        public string? CompanyName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        // Reference to the originating Lead (if applicable)
        public int? LeadId { get; set; }
        public Lead? Lead { get; set; }

        // Associated Contacts
        public ICollection<Contact> Contacts { get; set; } = new List<Contact>();

        // Associated Deals/Opportunities
        public ICollection<Opportunity> Opportunities { get; set; } = new List<Opportunity>();
    }

    // Contact: Represents individuals associated with either a prospect or a customer.
    public class Contact : BaseEntity
    {
        // Basic Information
        public string? ContactOwner { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Title { get; set; }
        public string? Department { get; set; }
        public DateTime? DateOfBirth { get; set; }

        // Account & Reporting Information
        public string? AccountName { get; set; }
        public string? VendorName { get; set; }
        public string? ReportingTo { get; set; }

        // Contact Details
        public string? Email { get; set; }
        public string? SecondaryEmail { get; set; }
        public string? Phone { get; set; }
        public string? Mobile { get; set; }
        public string? HomePhone { get; set; }
        public string? Fax { get; set; }
        public string? Assistant { get; set; }
        public string? AsstPhone { get; set; }

        // Source and Ownership
        public string? LeadSource { get; set; }
        public string? UserId { get; set; }
        public string? UserLevel { get; set; }
        public string? SkypeId { get; set; }
        public bool? EmailOptOut { get; set; }
        public string? Twitter { get; set; }

        // Mailing Address
        public string? MailingStreet { get; set; }
        public string? MailingCity { get; set; }
        public string? MailingState { get; set; }
        public string? MailingZip { get; set; }
        public string? MailingCountry { get; set; }

        // Other Address
        public string? OtherStreet { get; set; }
        public string? OtherCity { get; set; }
        public string? OtherState { get; set; }
        public string? OtherZip { get; set; }
        public string? OtherCountry { get; set; }

        // Additional Details
        public string? Description { get; set; }

        // Associations
        public int? ProspectId { get; set; }
        public Prospect? Prospect { get; set; }
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }

        // Demographic & Preference Details
        public string? PreferredContactMethod { get; set; }
        public string? Gender { get; set; }
        public string? Language { get; set; }
    }

    // Opportunity: Represents a sales opportunity (aligned with Zoho's Deal screen).
    public class Opportunity : BaseEntity
    {
        // Deal Basic Details
        public string? DealOwner { get; set; }
        public string? DealName { get; set; }
        public string? AccountName { get; set; }
        public string? Type { get; set; }
        public string? NextStep { get; set; }
        public string? LeadSource { get; set; }
        public string? ContactName { get; set; }
        public string? UserId { get; set; }
        public string? UserLevel { get; set; }

        // Financial Details
        public decimal Amount { get; set; }
        public DateTime ClosingDate { get; set; }
        public decimal? ExpectedRevenue { get; set; }
        public double? Probability { get; set; }  // Represented as a percentage

        // Sales Pipeline Details
        public string? Pipeline { get; set; }
        public string? Stage { get; set; }
        public string? CampaignSource { get; set; }

        // Additional Details
        public string? Description { get; set; }

        // Opportunity Enhancements
        public DateTime? ActualCloseDate { get; set; }
        public decimal? ActualRevenue { get; set; }
        public string? WinLossReason { get; set; }
        public string? Competitor { get; set; }

        // Associations with Prospect or Customer
        public int? ProspectId { get; set; }
        public Prospect? Prospect { get; set; }
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }
    }

    // Customer: Represents a converted prospect after a successful deal.
    public class Customer : BaseEntity
    {
        // Basic Customer/Account Information
        public string? CompanyName { get; set; }
        public string? PrimaryContact { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        // Associated Contacts
        public ICollection<Contact> Contacts { get; set; } = new List<Contact>();

        // Historical Deals/Opportunities
        public ICollection<Opportunity> Opportunities { get; set; } = new List<Opportunity>();

        // Customer Financial & Billing Details
        public string? BillingAddress { get; set; }
        public string? PaymentTerms { get; set; }
        public decimal? CreditLimit { get; set; }
        public string? TaxId { get; set; }
    }

    // Task: Represents a to-do or activity, as per Zoho's Task screen.
    public class Task : BaseEntity
    {
        public string? TaskOwner { get; set; }
        public string? Subject { get; set; }
        public DateTime? DueDate { get; set; }
        public string? TaskTo { get; set; } // Could reference a Contact or Lead
        public string? TaskType { get; set; } // E.g., Deal, Product, Quote, etc.
        public string? Priority { get; set; }
        public string? Reminder { get; set; }
        public string? Repeat { get; set; }
        public string? Description { get; set; }

        // Task Status
        public TaskStatus Status { get; set; }
    }

    // Meeting: Represents a scheduled meeting or event, aligned with Zoho's Meetings screen.
    public class Meeting : BaseEntity
    {
        public string? Title { get; set; }
        public string? Location { get; set; }
        public bool AllDay { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string? Host { get; set; }
        public ICollection<string> Participants { get; set; } = new List<string>(); // Could be user IDs or emails
        public string? RelatedTo { get; set; } // E.g., Lead, Contact, Deal, etc.
        public string? Repeat { get; set; }
        public DateTime? ParticipantsReminder { get; set; }
        public DateTime? Reminder { get; set; }

        // Meeting Enhancements
        public string? Agenda { get; set; }
        public string? MeetingType { get; set; }
    }

    // Enum for Task Status
    public enum TaskStatus
    {
        NotStarted,
        InProgress,
        Completed,
        Deferred
    }
}
