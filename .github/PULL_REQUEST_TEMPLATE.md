# Pull Request

## Description

<!-- Provide a brief description of the changes in this PR. -->

## Type of Change

- [ ] 🐛 Bug fix (non-breaking change that fixes an issue)
- [ ] ✨ New feature (non-breaking change that adds functionality)
- [ ] 💥 Breaking change (fix or feature that would cause existing functionality to not work as expected)
- [ ] 📝 Documentation update
- [ ] ♻️ Refactoring (no functional changes)
- [ ] ✅ Test addition or update
- [ ] 🏗️ Build or CI change

## Module Affected

- [ ] SharedKernel
- [ ] Membership
- [ ] Training
- [ ] Communication
- [ ] Financial
- [ ] API Layer
- [ ] Tests
- [ ] Documentation

## Checklist

### Mandatory Reading (before submitting PR)

- [ ] I have read `.ai/agent-rules.md`
- [ ] I have read `docs/DOCUMENTATION_INDEX.md`
- [ ] I have read `docs/PROJECT_GUIDE_FA.md`
- [ ] I have read `docs/backend/BACKEND_GUIDE_FA.md`
- [ ] I have reviewed `docs/PROJECT_HANDOFF.md`

### Code Quality

- [ ] `dotnet build` passes with zero errors
- [ ] `dotnet test` passes with >80% coverage for new code
- [ ] `dotnet format` has been run
- [ ] Code follows Clean Architecture (Domain has no external deps)
- [ ] No business logic in Validators or Repositories
- [ ] All entities inherit from `BaseEntity`
- [ ] Multi-tenant isolation verified (TenantId filter applied)

### Documentation

- [ ] `docs/IMPLEMENTATION_CHANGES.md` updated with new files
- [ ] `docs/PROJECT_HANDOFF.md` updated (if applicable)
- [ ] Code comments added for complex logic

### Testing

- [ ] Unit tests added for new handlers
- [ ] Unit tests added for new validators
- [ ] All existing tests pass

## Related Issues

<!-- Link related issues using "Closes #123" or "Relates to #123" -->

Closes #

## Additional Notes

<!-- Any additional information for reviewers -->
