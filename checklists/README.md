# Quality Checklists

## Purpose
Provides systematic checklists for code quality, security, and operational reviews.

## Scope
Code review, security assessment, deployment verification, and operational procedures.

## Owner
Lead Software Architect

## Status
Draft

## Last Updated
2026-06-17

---

## Available Checklists

### Code Review Checklist
**File:** `code-review.md`

Comprehensive checklist for all code changes including:
- Architecture compliance
- Security considerations
- Performance impact
- Test coverage
- Documentation updates

### Security Checklist
**File:** `security.md`

Pre-deployment security verification:
- OWASP Top 10 coverage
- Authentication/authorization
- Data protection
- Input validation
- Secrets management

### API Checklist
**File:** `api.md`

API endpoint review before merge:
- RESTful design compliance
- Error handling consistency
- Rate limiting
- Documentation complete
- Versioning strategy

### Deployment Checklist
**File:** `deployment.md`

Production deployment verification:
- All tests passing
- Migration scripts reviewed
- Rollback plan ready
- Monitoring configured
- Health checks passing

### Database Checklist
**File:** `database.md`

Database schema and query review:
- Indexing strategy
- Query performance
- Data integrity
- Migration safety
- Backup verification

---

## Using Checklists

### During Development
1. Review relevant checklist before starting
2. Check off items as you implement
3. Ensure all items completed before PR

### During Review
1. Reviewer verifies checklist completion
2. Missing items must be addressed
3. Sign-off required on checklist

### Automation
Where possible, checklists are automated via:
- CI/CD pipeline checks
- SonarQube rules
- Security scanning tools

---

## Checklist Format

Each checklist follows this format:

```markdown
# {Checklist Name}

## Purpose
{What this checklist verifies}

## Scope
{When to use this checklist}

## Checklist Items
- [ ] Item 1
- [ ] Item 2
- [ ] Item 3
```

---

## Checklist Maintenance

- Review and update quarterly
- Add items based on incidents
- Remove obsolete checks
- Keep concise and actionable

---

## Sections Reserved for Future Content

### Performance Testing Checklist
*To be defined*

### Incident Response Checklist
*To be defined*

### Onboarding Checklist
*To be defined*

### User Acceptance Testing Checklist
*To be defined*