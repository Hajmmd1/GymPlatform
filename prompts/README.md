# Development Prompts

## Purpose
Provides curated prompts and guidance for AI-assisted development of GymPlatform.

## Scope
AI development workflows, prompt templates, and development assistance patterns.

## Owner
Lead Software Architect

## Status
Draft

## Last Updated
2026-06-17

---

## What are Development Prompts?

Development prompts are pre-defined instructions and templates for AI assistants working on GymPlatform. They ensure:

- Consistent architectural decisions
- Proper security implementation
- Compliance with coding standards
- Efficient feature development

---

## Prompt Categories

### Feature Development
Templates for building new features following platform patterns.

### Refactoring
Guidance for code improvement while maintaining architecture.

### Testing
Prompt templates for test generation and quality assurance.

### Documentation
Templates for documenting features and changes.

### Security Review
Checklists for security-focused code review.

---

## Using Prompts

### For Feature Development
1. Copy the relevant feature template
2. Fill in the feature specifics
3. Provide to AI assistant with context
4. Review output against standards

### Best Practices
- Always include architecture context
- Specify performance requirements
- Include security considerations
- Define success criteria

---

## Available Prompt Templates

### Feature Template
```
Implement [FEATURE] following Clean Architecture:
- Domain entity in Domain layer
- Use case in Application layer
- Controller in API layer
- Follow coding standards in .ai/coding-standards.md
- Include unit tests
- Document in BUSINESS_RULES.md
```

### Security Review Template
```
Perform security review on [FEATURE]:
- Input validation
- Authorization checks
- Data protection
- OWASP Top 10 coverage
- Document findings
```

### API Design Template
```
Design API endpoint for [RESOURCE]:
- Follow RESTful conventions
- Include request/response schemas
- Define error responses
- Consider rate limiting
- Document in API_DESIGN.md
```

---

## Contributing Prompts

When adding new prompts:
1. Create a new `.md` file in this directory
2. Include context, requirements, and constraints
3. Update this README with the prompt reference
4. Test prompt with sample feature

---

## Sections Reserved for Future Content

### Architecture Review Prompts
*To be defined*

### Performance Optimization Prompts
*To be defined*

### Database Design Prompts
*To be defined*

### UI Component Prompts
*To be defined*