# Security Policy

## Supported Versions

| Version | Supported          |
| ------- | ------------------ |
| Current | ✅ Yes             |
| < Current | ❌ No            |

---

## Reporting a Vulnerability

If you discover a security vulnerability in GymPlatform, please report it **privately** — do not open a public issue.

### Reporting Process

1. **Email**: Send details to `security@gymplatform.app` (or project maintainer).
2. **Include in your report**:
   - Description of the vulnerability
   - Steps to reproduce
   - Potential impact assessment
   - Suggested fix (if available)

### Response Timeline

| Stage | Timeframe |
|-------|-----------|
| Initial acknowledgment | 48 hours |
| Vulnerability confirmed | 5 business days |
| Patch released | 30 days (critical), 90 days (high) |

---

## Security Standards

### Authentication & Authorization

- JWT tokens with short expiry (15 minutes access token, 7 days refresh)
- MFA enforced for Platform Admin and Gym Owner roles
- Role-based access control (RBAC) with granular permissions
- All endpoints require valid JWT or API key

### Data Protection

- AES-256 encryption for PII data at rest
- TLS 1.3 enforced in transit
- Tenant isolation via Row-Level Security (RLS) at database level
- EF Core Global Query Filter for defense-in-depth

### Input Validation

- All inputs validated at entry point (API layer)
- DTOs validated via Command Validators
- Domain-level business validation via Entity constructors
- Anti-forgery tokens for state-changing operations

### Vulnerability Disclosure

- Security issues are tracked privately until a patch is available
- CVE assigned for public vulnerabilities
- Credit given to reporters (unless anonymity requested)
- Patch released simultaneously with public disclosure

---

## Compliance

GymPlatform targets the following compliance standards:

| Standard | Target Phase | Notes |
|----------|-------------|-------|
| PCI-DSS Level 1 | Phase 3 (Payments) | Stripe tokenization; no card data touches our servers |
| SOC 2 Type I | Phase 4 (Pilot) | Access logs, change management, incident response |
| GDPR / CCPA | Phase 4 (Pilot) | Data export/deletion endpoints; consent management |
| HIPAA (if applicable) | Phase 5 | Separate encryption for health data; BAA with providers |

---

## Security Checklist for Contributors

Before submitting any PR:

- [ ] No secrets or credentials in code
- [ ] No SQL injection vectors (use parameterized queries)
- [ ] All user inputs are validated
- [ ] Authorization checked before all protected endpoints
- [ ] Error messages don't leak sensitive information
- [ ] Logging doesn't include PII
- [ ] Multi-tenant isolation verified (TenantId filter applied)
- [ ] New dependencies checked for known vulnerabilities

---

## Dependencies

- All dependencies managed via NuGet (backend) and npm (frontend)
- Regular dependency audits required
- Snyk and OWASP dependency-check recommended for CI pipeline

---

*Last updated: 2026-07-03*
