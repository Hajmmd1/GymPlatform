# User Roles and Permissions

## Purpose
Defines all user roles, their permissions, and access control policies for GymPlatform.

## Scope
Authentication, authorization, and role-based access across all platform features.

## Owner
Security Architecture Lead

## Status
Draft

## Last Updated
2026-06-17

---

## Table of Contents
1. [Role Overview](#role-overview)
2. [Permission Model](#permission-model)
3. [Role Definitions](#role-definitions)
4. [Permission Matrix](#permission-matrix)
5. [Multi-Tenant Access](#multi-tenant-access)
6. [Role Management](#role-management)

---

## Role Overview

### Role Types
- **System Roles** - Built-in roles with predefined permissions
- **Custom Roles** - Organization-specific roles
- **Temporary Roles** - Time-bound role assignments

### Access Levels
- **Read** - View only access
- **Write** - Create and modify access
- **Admin** - Full access including delete
- **Owner** - System owner with super permissions

---

## Permission Model

### Permission Structure
```
{resource}.{action}.{scope}
```

Examples:
- `members.read.any` - Read any member
- `members.read.own` - Read own member data
- `classes.write.location` - Create/edit classes in assigned location

### Permission Inheritance
- Higher roles inherit lower role permissions
- Custom permissions additive only
- Explicit deny takes precedence

### Permission Categories
- Member Management
- Class Management
- Financial Management
- Reporting and Analytics
- System Administration
- Location Management

---

## Role Definitions

### Administrator
**Scope:** Entire organization
**Permissions:** All permissions granted
**Description:** Full system access, can manage all aspects of the platform.

### Gym Owner
**Scope:** All locations in organization
**Permissions:**
- `members.*` - Full member management
- `classes.*` - Full class management
- `billing.read.write` - Financial viewing
- `reports.*` - All reporting access
- `settings.*` - Organization settings

### Gym Manager
**Scope:** Assigned locations
**Permissions:**
- `members.*` within locations
- `classes.*` within locations
- `billing.read` within locations
- `reports.read` within locations

### Fitness Trainer
**Scope:** Own classes and clients
**Permissions:**
- `classes.read.assigned` - View assigned classes
- `classes.write.assigned` - Modify assigned classes
- `members.read.clients` - View own clients
- `attendance.write.assigned` - Mark attendance

### Gym Member
**Scope:** Own data and bookings
**Permissions:**
- `profile.read.write.own` - Own profile
- `classes.read.all` - Browse all classes
- `classes.book.own` - Book own classes
- `billing.read.own` - View own billing

### Front Desk Staff
**Scope:** All members in location
**Permissions:**
- `members.read.all` - View all members
- `members.checkin.all` - Check in any member
- `classes.book.any` - Book on behalf of members
- `payments.process` - Process manual payments

---

## Permission Matrix

| Resource | Administrator | Gym Owner | Gym Manager | Trainer | Front Desk | Member |
|----------|---------------|-----------|-------------|---------|------------|--------|
| Members - View All | ✓ | ✓ | ✓ | Clients Only | ✓ | Own Only |
| Members - Edit | ✓ | ✓ | ✓ | Own Clients | ✓ | Own Only |
| Members - Delete | ✓ | ✓ | ✓ | ✗ | ✗ | ✗ |
| Classes - Create/Edit | ✓ | ✓ | ✓ | Assigned Only | ✗ | ✗ |
| Classes - Book | ✓ | ✓ | ✓ | ✓ | ✓ | Own Only |
| Billing - View | ✓ | ✓ | Limited | ✗ | Limited | Own Only |
| Billing - Process | ✓ | ✓ | ✓ | ✗ | ✓ | ✗ |
| Reports - View All | ✓ | ✓ | Location | ✗ | ✗ | ✗ |
| Settings - Edit | ✓ | ✓ | ✗ | ✗ | ✗ | ✗ |

---

## Multi-Tenant Access

### Organization Isolation
- Users belong to exactly one organization
- Data isolation enforced at query level
- Cross-organization access requires explicit delegation

### Role Scoping
- Roles inherit organization context
- Location-level scoping for managers
- Trainer scoping to assigned classes

### Super Admin Access
- System administrators with cross-org access
- Full audit logging required
- Time-bound access tokens
- Dual approval for critical operations

---

## Role Management

### Role Assignment
- Admin assigns roles via UI
- Bulk import via CSV for onboarding
- Time-bound assignments supported
- Assignment requires audit trail

### Role Review
- Quarterly role review process
- Access certification by managers
- Revocation of unused roles
- Permission creep detection

### Custom Roles
- Organization owners can create custom roles
- Permission sets can be combined
- Naming convention: `custom.{org}.{role}`
- Admin approval for sensitive permissions

---

## Sections Prepared for Future Content

### API Token Permissions
*To be defined*

### SSO Integration
*To be defined*

### Guest User Roles
*To be defined*

### Partner Access Roles
*To be defined*