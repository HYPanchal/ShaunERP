#nullable enable

using System;
using System.Collections.Generic;

namespace Core
{
    // Represents a Company/Organization in the system.
    public class Company : BaseEntity
    {
        public string? Name { get; set; }
        public string? Domain { get; set; }  // Example: company1.com, company2.com
        public string? Description { get; set; }

        // List of users associated with the company.
        public ICollection<CompanyUser> CompanyUsers { get; set; } = new List<CompanyUser>();

        // List of modules/subdomains available for the company.
        public ICollection<CompanyModule> CompanyModules { get; set; } = new List<CompanyModule>();
    }

    // Represents a User (Both ShaunERP System Admins & Company Users).
    public class User : BaseEntity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Username { get; set; }
        public string? PasswordHash { get; set; }  // Store hashed password, not plain text
        public bool IsActive { get; set; } = true;

        // System-Wide ERP Admins
        public bool IsSystemAdmin { get; set; } = false;

        // A user can be part of multiple companies.
        public ICollection<CompanyUser> CompanyUsers { get; set; } = new List<CompanyUser>();

        // A system user can have system-wide roles.
        public ICollection<SystemUserRole> SystemUserRoles { get; set; } = new List<SystemUserRole>();

        // A user can have multiple active sessions.
        public ICollection<Session> Sessions { get; set; } = new List<Session>();
    }

    // Represents a many-to-many relationship between Users and Companies.
    public class CompanyUser : BaseEntity
    {
        public int UserId { get; set; }
        public User? User { get; set; }

        public int CompanyId { get; set; }
        public Company? Company { get; set; }

        // A user can have different roles in different companies.
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

        // A user may have different module/subdomain access per company.
        public ICollection<UserModuleAccess> UserModuleAccesses { get; set; } = new List<UserModuleAccess>();
    }

    // Represents a many-to-many relationship between Users and Roles within a Company.
    public class UserRole : BaseEntity
    {
        public int CompanyUserId { get; set; }
        public CompanyUser? CompanyUser { get; set; }

        public int RoleId { get; set; }
        public Role? Role { get; set; }
    }

    // Role: Defines a user role (e.g., Admin, Manager, User)
    public class Role : BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }

        // Each role can have multiple permissions.
        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();

        // Each role can be assigned to multiple users.
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }

    // Represents Permissions (e.g., View Reports, Manage Users)
    public class Permission : BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }

        // Many-to-many relationship with roles
        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }

    // Represents many-to-many relationship between Roles and Permissions.
    public class RolePermission : BaseEntity
    {
        public int RoleId { get; set; }
        public Role? Role { get; set; }

        public int PermissionId { get; set; }
        public Permission? Permission { get; set; }
    }

    // Represents ERP System-Wide User Roles (Not Company Specific).
    public class SystemRole : BaseEntity
    {
        public string? Name { get; set; }  // e.g., "Super Admin", "Billing Manager"
        public string? Description { get; set; }

        // ERP System-Wide Permissions
        public ICollection<SystemRolePermission> SystemRolePermissions { get; set; } = new List<SystemRolePermission>();

        // System Users assigned to this Role
        public ICollection<SystemUserRole> SystemUserRoles { get; set; } = new List<SystemUserRole>();
    }

    // Represents a many-to-many relationship between Users and System Roles.
    public class SystemUserRole : BaseEntity
    {
        public int UserId { get; set; }
        public User? User { get; set; }

        public int SystemRoleId { get; set; }
        public SystemRole? SystemRole { get; set; }
    }

    // Represents many-to-many relationship between System Roles and Permissions.
    public class SystemRolePermission : BaseEntity
    {
        public int SystemRoleId { get; set; }
        public SystemRole? SystemRole { get; set; }

        public int PermissionId { get; set; }
        public Permission? Permission { get; set; }
    }

    // Represents different modules/subdomains a company has access to.
    public class CompanyModule : BaseEntity
    {
        public int CompanyId { get; set; }
        public Company? Company { get; set; }

        public string? ModuleName { get; set; }  // e.g., "CRM", "HRMS", "Finance"
        public string? Subdomain { get; set; }  // e.g., "crm.company1.com", "hrms.company2.com"

        // Users of this company can have different access levels to this module.
        public ICollection<UserModuleAccess> UserModuleAccesses { get; set; } = new List<UserModuleAccess>();
    }

    // Represents a user's access level to a module in a specific company.
    public class UserModuleAccess : BaseEntity
    {
        public int CompanyUserId { get; set; }
        public CompanyUser? CompanyUser { get; set; }

        public int CompanyModuleId { get; set; }
        public CompanyModule? CompanyModule { get; set; }

        public ModuleAccessLevel AccessLevel { get; set; }  // Read, Write, Admin, etc.
    }

    public enum ModuleAccessLevel
    {
        None,
        ReadOnly,
        ReadWrite,
        Admin
    }

    // Represents user login sessions.
    public class Session : BaseEntity
    {
        public int UserId { get; set; }
        public User? User { get; set; }

        public int? CompanyId { get; set; } // Nullable because ShaunERP admins may not belong to a company.
        public Company? Company { get; set; }

        public string? Token { get; set; }  // Session token or JWT
        public DateTime ExpiryDate { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
