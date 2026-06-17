# Business Rules

## Purpose
Documents all business rules, policies, and operational constraints for GymPlatform.

## Scope
Membership logic, billing rules, scheduling constraints, and operational policies.

## Owner
Product Manager / Domain Expert

## Status
Draft

## Last Updated
2026-06-17

---

## Table of Contents
1. [Membership Rules](#membership-rules)
2. [Billing Rules](#billing-rules)
3. [Class Scheduling Rules](#class-scheduling-rules)
4. [Trainer Rules](#trainer-rules)
5. [Check-in Rules](#check-in-rules)
6. [Notification Rules](#notification-rules)
7. [Data Retention Rules](#data-retention-rules)

---

## Membership Rules

### Registration Requirements
- Email or phone required for registration
- Age verification (must be 18+ or guardian consent)
- Emergency contact required
- Waiver signature required before first check-in

### Membership Plans
- Plans have start and end dates
- Plans may be recurring or one-time
- Failed payments result in suspension after 3 days
- Suspended memberships allow 7-day grace period

### Membership Tiers
- Basic: Limited class access
- Standard: Full class access
- Premium: All access + personal training credits

### Status Transitions
```
Inactive → Active → Suspended → Cancelled
                    ↘ Expired
```

---

## Billing Rules

### Payment Processing
- Payments processed via Stripe
- Failed payments trigger retry sequence (3 attempts)
- Manual retry available after 7 days
- Auto-cancellation after 30 days of failed payments

### Pricing Rules
- Per-member pricing tiers
- Location-based pricing multipliers
- Discount codes with expiration and usage limits
- Proration for mid-cycle changes

### Refund Policy
- Full refund within 7 days of purchase
- Pro-rated refund for unused services
- No refunds after 30 days
- Admin approval required for all refunds

### Invoice Generation
- Automatic monthly invoice generation
- Invoice delivery via email
- Payment reminder 3 days before due
- Late fee applied after 7 days overdue

---

## Class Scheduling Rules

### Class Creation
- Classes must have start/end times
- Maximum 4 hours per class session
- Minimum 2 hours advance booking required
- Maximum 90 days advance booking

### Capacity Management
- Class capacity cannot exceed room capacity
- Waitlist available when capacity reached
- Automatic waitlist promotion on cancellation
- 2-hour cancellation policy for classes

### Booking Rules
- Members can book up to 30 days in advance
- Maximum 2 active bookings per member
- No-show tracking (3 strikes policy)
- Booking allowed only for active memberships

---

## Trainer Rules

### Trainer Profiles
- Certification documents required
- Insurance verification required
- Profile approval by admin
- Rating system from member feedback

### Scheduling
- Trainers can set availability
- Minimum 4-hour advance scheduling
- Maximum 8-hour daily teaching limit
- Vacation and holiday scheduling

### Payments
- Commission-based payment model
- Payments processed monthly
- Performance bonuses for high ratings
- Automatic payouts via preferred method

---

## Check-in Rules

### Member Check-in
- QR code or RFID scan
- 24-hour check-in window per visit
- Check-in restricted to active membership hours
- Guest passes limited to 2 per month

### Location Restrictions
- Members restricted to home location
- Multi-location access via premium tier
- Guest passes valid for any location
- Check-in audit log maintained

### Anti-fraud
- Duplicate check-in prevention (1 hour window)
- Suspicious activity flagging
- Manual override requires admin approval
- Check-in dispute process

---

## Notification Rules

### Automated Notifications
- Welcome email on registration
- Payment confirmation
- Class booking confirmation
- Class reminder (24 hours before)
- Membership renewal reminder (7 days before)

### Communication Limits
- Maximum 2 promotional emails per week
- Opt-out available for non-critical notifications
- SMS reserved for critical alerts only
- Unsubscribe honored within 24 hours

### Notification Types
- Marketing: Promotional content
- Transactional: Account-related actions
- Critical: Security and payment alerts
- System: Maintenance and updates

---

## Data Retention Rules

### Active Data
- Membership data retained while active
- Payment history retained indefinitely
- Class attendance retained 7 years

### Inactive Data
- Cancelled memberships retained 3 years
- Inactive accounts archived after 2 years
- Audit logs retained 7 years

### Deletion Requests
- GDPR right to erasure honored
- 30-day processing time
- Exception for legal/compliance requirements
- Deletion confirmation provided

---

## Sections Prepared for Future Content

### Integration Rules
*To be defined*

### Reporting Rules
*To be defined*

### API Rate Limiting
*To be defined*

### Custom Pricing Models
*To be defined*