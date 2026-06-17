# AI Memory

- Last Updated: 2026-06-17T22:39:56+03:30
- Role: Persistent project brain for GymPlatform AI sessions
- Context Source of Truth: `.ai/context/*`

## Stable Technical Stack

No stable technical stack is confirmed by the project context layer. Existing repository files contain documented technology references, but archived ADRs explicitly state that architecture, database, and stack decisions are unapproved. Treat those references as information only, not as approved implementation decisions.

## Documented Technology References

- `README.md` documents .NET 8, ASP.NET Core, PostgreSQL, Redis, RabbitMQ, Next.js 15, React 19, TypeScript, Tailwind CSS, React Native, and Expo SDK as the current documented stack.
- Archived ADRs reference .NET 8, Next.js 15, React Native, PostgreSQL, and Clean Architecture, but they are marked as unapproved and archived for reference only.
- Draft `.ai/*` rule files contain additional technology references, but they are not treated as approved decisions by this context layer.

## Allowed Technologies

- Markdown documentation is allowed for context and project documentation work.
- Existing `.ai/*` guidance may be read and referenced for rules and standards.
- No backend framework, frontend framework, mobile framework, database, cloud provider, authentication provider, payment provider, messaging provider, or external service is approved for implementation from this context alone.
- Any future implementation must wait for explicit user confirmation of the required technical decision.

## Rules for AI Behavior in This Repo

- Read `.ai/context/*` before planning or changing project files.
- Treat `.ai/context/*` as the single source of truth for project context.
- Treat `docs/VISION.md` as the product vision source.
- Do not assume architecture, database, APIs, UI, integrations, providers, or implementation details.
- Do not create architecture documents, ADRs, database design, or application code unless explicitly requested.
- If critical information is missing, ask instead of guessing.
- Preserve security, privacy, and enterprise SaaS expectations in all guidance.
- Keep changes minimal and precise.

## AI Must NEVER Assume

- That the documented technology stack is approved.
- That archived ADRs are active decisions.
- That PostgreSQL, .NET, Next.js, React Native, Redis, RabbitMQ, Azure, Stripe, PayPal, or any other provider is selected.
- That a database schema, API contract, UI flow, authentication design, payment flow, or integration exists.
- That product requirements are complete beyond what is documented in verified context files.

## Confirmed Non-Functional Expectations

- Enterprise SaaS orientation
- Security by design
- Production-ready quality expectations
- Documentation required for significant decisions
- No placeholders in project context files
