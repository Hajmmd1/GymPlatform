# Contributing to GymPlatform

## Development Setup

### Prerequisites
- .NET 8 SDK
- Node.js 20+
- Docker Desktop
- Git

### Installation
```bash
# Clone repository
git clone <repository-url>
cd GymPlatform

# Backend
dotnet restore
dotnet build

# Frontend
cd web-app
npm install
npm run dev
```

## Pull Request Process

1. Fork and create feature branch
2. Implement changes following coding standards
3. Write/update tests
4. Update documentation
5. Submit PR with clear description

## Code Standards

- Follow [coding standards](.ai/coding-standards.md)
- All tests must pass (80% coverage minimum)
- Security review required
- Architecture review for major changes

## Questions?

Contact the Lead Software Architect or open a discussion.