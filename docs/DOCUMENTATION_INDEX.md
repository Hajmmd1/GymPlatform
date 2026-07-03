# GymPlatform Documentation Index / شاخص مستندات GymPlatform

**Purpose | هدف**: Provides a clear reading path to every documentation file in GymPlatform.  
**Purpose | هدف**: مسیر خواندن واضحی برای تمام فایل‌های مستندات GymPlatform ارائه می‌دهد.  

---

## Recommended Reading Order / سفارش پیشنهادی مطالعه

### Backend Developer / توسعه‌دهنده Back-end

| Order | Document | Reason |
|-------|----------|--------|
| 1 | `AGENTS.md` | Project-level agent configuration and commands |
| 2 | `README.md` | High-level project overview and tech stack |
| 3 | `.ai/agent-rules.md` | **MANDATORY** mandatory AI execution and cleanup rules |
| 4 | `docs/PROJECT_GUIDE_FA.md` | Complete Persian project blueprint |
| 5 | `docs/backend/BACKEND_GUIDE_FA.md` | Detailed backend architecture and patterns |
| 6 | `.ai/context/IMPLEMENTATION_MASTER_PLAN.md` | Execution roadmap and module mapping |
| 7 | `.ai/context/API_BLUEPRINT.md` | API design standards |
| 8 | `.ai/context/DATABASE_BLUEPRINT.md` | Database design and entity model |
| 9 | `docs/PROJECT_HANDOFF.md` | Current implementation state |
| 10 | `docs/IMPLEMENTATION_CHANGES.md` | Change history |

### Frontend Developer / توسعه‌دهنده Front-end

| Order | Document | Reason |
|-------|----------|--------|
| 1 | `AGENTS.md` | Project-level agent configuration |
| 2 | `README.md` | High-level project overview |
| 3 | `.ai/agent-rules.md` | **MANDATORY** mandatory rules |
| 4 | `docs/PROJECT_GUIDE_FA.md` | Complete project understanding |
| 5 | `docs/frontend/FRONTEND_DEVELOPER_HANDBOOK_FA.md` | Frontend architecture and patterns |
| 6 | `.ai/context/UI_UX_BLUEPRINT.md` | UI design specifications |
| 7 | `docs/UI_UX.md` | Design system and component standards |
| 8 | `docs/CHANGELOG.md` | Version history |

### AI Agent / عامل هوش مصنوعی

| Order | Document | Reason |
|-------|----------|--------|
| 1 | `.ai/agent-rules.md` | **MANDATORY FIRST** — permanent execution rules |
| 2 | `.ai/context/WORKSPACE.md` | Workspace context and boundaries |
| 3 | `.ai/context/PROJECT_STATE.md` | Current implementation state |
| 4 | `.ai/context/IMPLEMENTATION_MASTER_PLAN.md` | Execution roadmap |
| 5 | `docs/PROJECT_HANDOFF.md` | Current code and file status |
| 6 | `docs/IMPLEMENTATION_CHANGES.md` | Recent implementation changes |
| 7 | `.ai/context/PRODUCT_BLUEPRINT.md` | Product vision for feature understanding |
| 8 | `docs/AI_MEMORY.md` | AI session memory |

### Technical Architect / معمار فنی

| Order | Document | Reason |
|-------|----------|--------|
| 1 | `.ai/agent-rules.md` | **MANDATORY** execution rules |
| 2 | `docs/PROJECT_GUIDE_FA.md` | Complete project blueprint |
| 3 | `.ai/context/IMPLEMENTATION_MASTER_PLAN.md` | Full implementation roadmap with decisions |
| 4 | `.ai/context/ARCHITECTURE_REVIEW.md` | Architecture decisions and rationale |
| 5 | `docs/ARCHITECTURE.md` | System architecture overview |
| 6 | `.ai/context/DATABASE_BLUEPRINT.md` | Database architecture |
| 7 | `.ai/context/API_BLUEPRINT.md` | API architecture |
| 8 | `.ai/context/NON_FUNCTIONAL_REQUIREMENTS.md` | Performance, security, scalability |
| 9 | `.ai/context/FUNCTIONAL_REQUIREMENTS.md` | Functional requirements |
| 10 | `.ai/context/MASTER_ROADMAP.md` | Long-term roadmap |

### Project Manager / مدیر پروژه

| Order | Document | Reason |
|-------|----------|--------|
| 1 | `docs/PROJECT_GUIDE_FA.md` | Business overview and goals |
| 2 | `docs/VISION.md` | Product vision and long-term goals |
| 3 | `.ai/context/PRODUCT_BLUEPRINT.md` | Complete product specification |
| 4 | `.ai/context/MASTER_ROADMAP.md` | Phased development roadmap |
| 5 | `.ai/context/IMPLEMENTATION_MASTER_PLAN.md` | Sprint breakdown and success criteria |
| 6 | `docs/PROJECT_HANDOFF.md` | Current state and remaining work |
| 7 | `docs/CHANGELOG.md` | Completed milestones |
| 8 | `.ai/context/NON_FUNCTIONAL_REQUIREMENTS.md` | Quality standards and SLAs |

---

## Document Catalog / فهرست اسناد

---

### AGENTS.md

**English Title**: Agent Configuration  
**فارسی**: پیکربندی عوامل  

**Purpose**: Defines lint, typecheck, and test commands for Kilo agents working on this project.  
**هدف**: دستورات بررسی، بررسی تایپ و تست را برای عامل‌های Kilo تعریف می‌کند.  

**Audience**: AI Agents  
**مخاطب**: عوامل هوش مصنوعی  

**Reading Time**: 2 minutes  
**زمان مطالعه**: ۲ دقیقه  

**Required**: YES  
**الزامی**: بله  

---

### README.md

**English Title**: Project Introduction  
**فارسی**: معرفی پروژه  

**Purpose**: High-level introduction to GymPlatform, including purpose, tech stack, documentation links, and contributing guide.  
**هدف**: معرفی سطح بالا GymPlatform، شامل هدف، پشته فناوری، پیوندهای مستندات و راهنمای مشارکت.  

**Audience**: Everyone (Developers, Stakeholders, Users)  
**مخاطب**: همه (توسعه‌دهندگان، ذینفعان، کاربران)  

**Reading Time**: 5 minutes  
**زمان مطالعه**: ۵ دقیقه  

**Required**: YES  
**الزامی**: بله  

---

### CONTRIBUTING.md

**English Title**: Contributing Guide  
**فارسی**: راهنمای مشارکت  

**Purpose**: Onboarding instructions for new contributors including setup, PR process, code standards, and mandatory documentation reading list.  
**هدف**: راهنمای ورود برای مشارکت‌کنندگان جدید شامل تنظیمات، فرآیند PR، استانداردهای کد و لیست اجباری مطالعه مستندات.  

**Audience**: All Contributors  
**مخاطب**: تمام مشارکت‌کنندگان  

**Reading Time**: 10 minutes  
**زمان مطالعه**: ۱۰ دقیقه  

**Required**: YES  
**الزامی**: بله  

---

### SECURITY.md

**English Title**: Security Policy  
**فارسی**: سیاست امنیتی  

**Purpose**: Security policy including vulnerability reporting, supported versions, and compliance standards.  
**هدف**: سیاست امنیتی شامل گزارش آسیب‌پذیری، نسخه‌های پشتیبانی و استانداردهای انطباق.  

**Audience**: All Contributors, Security Researchers  
**مخاطب**: تمام مشارکت‌کنندگان، محققان امنیتی  

**Reading Time**: 5 minutes  
**زمان مطالعه**: ۵ دقیقه  

**Required**: YES  
**الزامی**: بله  

---

### CHANGELOG.md

**English Title**: Version History  
**فارسی**: تاریخچه نسخه‌ها  

**Purpose**: Record of all significant changes, releases, features, and bug fixes by version.  
**هدف**: ثبت تمام تغییرات مهم، انتشارها، ویژگی‌ها و رفع اشکالات به تفکیک نسخه.  

**Audience**: Everyone  
**مخاطب**: همه  

**Reading Time**: 10 minutes  
**زمان مطالعه**: ۱۰ دقیقه  

**Required**: Optional (for historical reference)  
**الزامی**: اختیاری (برای مرجع تاریخی)  

---

### .github/PULL_REQUEST_TEMPLATE.md

**English Title**: Pull Request Template  
**فارسی**: قالب Pull Request  

**Purpose**: Standardized format for submitting pull requests including description, checklist, and testing requirements.  
**هدف**: فرمت استاندارد برای ارسال pull request شامل توضیحات، چک‌لیست و نیازمندی‌های تست.  

**Audience**: All Contributors  
**مخاطب**: تمام مشارکت‌کنندگان  

**Reading Time**: 2 minutes  
**زمان مطالعه**: ۲ دقیقه  

**Required**: YES (before submitting PRs)  
**الزامی**: بله (قبل از ارسال PR)  

---

### docs/PROJECT_GUIDE_FA.md

**English Title**: Project Guide (Persian)  
**فارسی**: راهنمای پروژه  

**Purpose**: Complete project understanding in Persian — covers architecture, modules, technology stack, repository structure, getting started, glossary, and project philosophy. The primary Persian reference for all roles.  
**هدف**: درک کامل پروژه به فارسی — شامل معماری، ماژول‌ها، پشته فناوری، ساختار ریپازیتوری، شروع کار، واژه‌نامه و فلسفه پروژه. مرجع فارسی اصلی برای تمام نقش‌ها.  

**Audience**: Backend Developers, Frontend Developers, AI Agents, Technical Architects, Project Managers  
**مخاطب**: توسعه‌دهندگان Backend، توسعه‌دهندگان Frontend، عوامل هوش مصنوعی، معماران فنی مدیران پروژه  

**Reading Time**: 45 minutes  
**زمان مطالعه**: ۴۵ دقیقه  

**Required**: YES  
**الزامی**: بله  

---

### docs/backend/BACKEND_GUIDE_FA.md

**English Title**: Backend Developer Guide (Persian)  
**فارسی**: راهنمای توسعه‌دهنده Back-end  

**Purpose**: Comprehensive backend development guide in Persian covering Clean Architecture, CQRS, Repository pattern, Domain Events, Multi-Tenant, Validation, DI, testing, and common mistakes. Includes step-by-step code patterns for implementing commands end-to-end.  
**هدف**: راهنمای جامع توسعه Backend به فارسی شامل معماری خالص، CQRS، الگوی Repository، رویدادهای دامنه، چندمستاجری، اعتبارسنجی، DI، تست و اشتباهات متداول. شامل الگوهای کد گام‌به‌گام برای پیاده‌سازی کامل Commandها.  

**Audience**: Backend Developers  
**مخاطب**: توسعه‌دهندگان Backend  

**Reading Time**: 60 minutes  
**زمان مطالعه**: ۶۰ دقیقه  

**Required**: YES (for Backend Developers)  
**الزامی**: بله (برای توسعه‌دهندگان Backend)  

---

### docs/frontend/FRONTEND_DEVELOPER_HANDBOOK_FA.md

**English Title**: Frontend Developer Handbook (Persian)  
**فارسی**: راهنمای توسعه‌دهنده Front-end  

**Purpose**: Frontend development handbook in Persian covering planned tech stack (Next.js 15, React 19, TypeScript 5, Tailwind CSS 3), project structure, component organization, state management, API integration, routing, responsive design, and testing strategy. Note: Frontend is not yet implemented.  
**هدف**: راهنمای توسعه Frontend به فارسی شامل پشته فناوری برنامه‌ریزی شده (Next.js ۱۵، React ۱۹، TypeScript ۵، Tailwind CSS ۳)، ساختار پروژه، سازماندهی کامپوننت‌ها، مدیریت state، یکپارچگی API، مسیریابی، طراحی واکنش‌گرا و استراتژی تست. توجه: Frontend هنوز پیاده‌سازی نشده است.  

**Audience**: Frontend Developers  
**مخاثب**: توسعه‌دهندگان Frontend  

**Reading Time**: 30 minutes  
**زمان مطالعه**: ۳۰ دقیقه  

**Required**: YES (when frontend implementation begins)  
**الزامی**: بله (هنگام شروع پیاده‌سازی Frontend)  

---

### docs/ai/AI_DEVELOPER_GUIDE_FA.md

**English Title**: AI Agent Developer Guide (Persian)  
**فارسی**: راهنمای توسعه‌دهنده عامل هوش مصنوعی  

**Purpose**: Developer guide for AI agents working on GymPlatform in Persian. Covers AI execution rules, mandatory cleanup, git policies, context layer navigation, how to read implementation state, how to use the knowledge graph, and how to spawn sub-tasks.  
**هدف**: راهنمای توسعه‌دهنده برای عوامل هوش مصنوعی که در GymPlatform کار می‌کنند. شامل قوانین اجرای AI، تمیزگیری اجباری، خط‌مشی‌های git، ناوبری لایه context، نحوه خواندن وضعیت پیاده‌سازی، نحوه استفاده از گراف دانش و نحوه ایجاد زیروظایف.  

**Audience**: AI Agents, Technical Architects  
**مخاطب**: عوامل هوش مصنوعی، معماران فنی  

**Reading Time**: 20 minutes  
**زمان مطالعه**: ۲۰ دقیقه  

**Required**: YES (for all AI sessions)  
**الزامی**: بله (برای تمام جلسات AI)  

---

### .ai/context/IMPLEMENTATION_MASTER_PLAN.md

**English Title**: Implementation Master Plan  
**فارسی**: نقشه راه اجرایی اصلی  

**Purpose**: Definitive implementation roadmap translating product vision into actionable execution plan. Covers architectural decisions, module-to-project mapping, 6 implementation phases, sprint breakdown, database strategy, API strategy, testing strategy, security, rollout, and success criteria.  
**هدف**: نقشه راه اجرایی قطعی که دیدگاه محصول را به برنامه اجرایی قابل اجرا تبدیل می‌کند. شامل تصمیمات معماری، نگاشت ماژول به پروژه، ۶ فاز پیاده‌سازی، تفکیک Sprint، استراتژی پایگاه داده، استراتژی API، استراتژی تست، امنیت، rollout و معیارهای موفقیت.  

**Audience**: Technical Architects, Backend Developers, AI Agents, Project Managers  
**مخاطب**: معماران فنی، توسعه‌دهندگان Backend، عوامل هوش مصنوعی، مدیران پروژه  

**Reading Time**: 60 minutes  
**زمان مطالعه**: ۶۰ دقیقه  

**Required**: YES  
**الزامی**: بله  

---

### .ai/context/PRODUCT_BLUEPRINT.md

**English Title**: Product Blueprint  
**فارسی**: نقشه محصول  

**Purpose**: Complete product specification including vision, mission, problems solved, user personas, business model, 21 product modules, feature inventory, MVP definition, user journeys, UX principles, and 5-year goals.  
**هدف**: مشخصات کامل محصول شامل چشم‌انداز، ماموریت، مشکلات حل‌شده، شخصاهای کاربری، مدل تجاری، ۲۱ ماژول محصول، فهرست ویژگی‌ها، تعریف MVP، سفرهای کاربر، اصول UX و اهداف ۵ ساله.  

**Audience**: Everyone, especially Product Managers and Technical Architects  
**مخاطب**: همه، به ویژه مدیران محصول و معماران فنی  

**Reading Time**: 45 minutes  
**زمان مطالعه**: ۴۵ دقیقه  

**Required**: YES  
**الزامی**: بله  

---

### .ai/context/FUNCTIONAL_REQUIREMENTS.md

**English Title**: Functional Requirements  
**فارسی**: نیازمندی‌های عملکردی  

**Purpose**: Detailed functional requirements for all 21 modules including user stories, business rules, permission matrix, notification triggers, and edge cases.  
**هدف**: نیازمندی‌های عملکردی تفصیلی برای همه ۲۱ ماژول شامل داستان‌های کاربری، قوانین تجاری، ماتریس مجوزها، triggerهای notification و موارد مرزی.  

**Audience**: Backend Developers, Technical Architects, Frontend Developers  
**مخاطب**: توسعه‌دهندگان Backend، معماران فنی، توسعه‌دهندگان Frontend  

**Reading Time**: 45 minutes  
**زمان مطالعه**: ۴۵ دقیقه  

**Required**: YES  
**الزامی**: بله  

---

### .ai/context/NON_FUNCTIONAL_REQUIREMENTS.md

**English Title**: Non-Functional Requirements  
**فارسی**: نیازمندی‌های غیرعملکردی  

**Purpose**: Quality attributes including performance targets, scalability requirements, security standards, compliance roadmap (PCI-DSS, SOC 2, GDPR, HIPAA), CI/CD requirements, SLA, and testing standards.  
**هدف**: ویژگی‌های کیفیت شامل هدف‌های عملکردی، نیازمندی‌های مقیاس‌پذیری، استانداردهای امنیتی، نقشه راه انطباق (PCI-DSS، SOC 2، GDPR، HIPAA)، نیازمندی‌های CI/CD، SLA و استانداردهای تست.  

**Audience**: Technical Architects, Backend Developers, DevOps  
**مخاطب**: معماران فنی، توسعه‌دهندگان Backend، DevOps  

**Reading Time**: 30 minutes  
**زمان مطالعه**: ۳۰ دقیقه  

**Required**: YES  
**الزامی**: بله  

---

### .ai/context/DATABASE_BLUEPRINT.md

**English Title**: Database Blueprint  
**فارسی**: نقشه پایگاه داده  

**Purpose**: Conceptual database architecture including module schemas, entity relationships, aggregate boundaries, multi-tenant strategy, index recommendations, naming conventions, and ~100+ entity definitions.  
**هدف**: معماری مفهومی پایگاه داده شامل schemaهای ماژول، روابط Entity، مرزهای Aggregate، استراتژی چندمستاجری، توصیه‌های index، قراردادهای نام‌گذاری و تعریف حدود ۱۰۰+ Entity.  

**Audience**: Backend Developers, Technical Architects  
**مخاطب**: توسعه‌دهندگان Backend، معماران فنی  

**Reading Time**: 40 minutes  
**زمان مطالعه**: ۴۰ دقیقه  

**Required**: YES  
**الزامی**: بله  

---

### .ai/context/API_BLUEPRINT.md

**English Title**: API Blueprint  
**فارسی**: نقشه API  

**Purpose**: Complete API architecture philosophy including versioning strategy (URL-based), JWT authentication flow, permission model, request/response standards, error format (RFC 7807 ProblemDetails), rate limiting, pagination, filtering, and endpoint catalog.  
**هدف**: فلسفه کامل معماری API شامل استراتژی نسخه‌بندی، جریان احراز هویت JWT، مدل مجوز، استانداردهای درخواست/پاسخ، فرمت خطا، محدودیت نرخ، صفحه‌بندی، فیلتر و کataloگ endpoint.  

**Audience**: Backend Developers, Frontend Developers  
**مخاطب**: توسعه‌دهندگان Backend، توسعه‌دهندگان Frontend  

**Reading Time**: 40 minutes  
**زمان مطالعه**: ۴۰ دقیقه  

**Required**: YES  
**الزامی**: بله  

---

### .ai/context/UI_UX_BLUEPRINT.md

**English Title**: UI/UX Blueprint  
**فارسی**: نقشه UI/UX  

**Purpose**: Complete UI/UX design specification including screen specifications for all 6 user roles, component library, responsive breakpoints, navigation hierarchy, design tokens, accessibility requirements, and mobile considerations.  
**هدف**: مشخصات کامل طراحی UI/UX شامل مشخصات صفحه برای همه ۶ نقش کاربری، کتابخانه کامپوننت، breakpoints واکنش‌گرا، سلسله مراتب ناوبری، توکن‌های طراحی، نیازمندی‌های دسترس‌پذیری و ملاحظات موبایل.  

**Audience**: Frontend Developers, UX Designers  
**مخاطب**: توسعه‌دهندگان Frontend، طراحان UX  

**Reading Time**: 40 minutes  
**زمان مطالعه**: ۴۰ دقیقه  

**Required**: YES  
**الزامی**: بله  

---

### .ai/context/MASTER_ROADMAP.md

**English Title**: Master Roadmap  
**فارسی**: نقشه راه اصلی  

**Purpose**: 12-month development roadmap with 5 phases, sprint planning, milestone definitions, success metrics, resource planning, and risk mitigation strategies.  
**هدف**: نقشه راه توسعه ۱۲ ماهه با ۵ فاز، برنامه‌ریزی Sprint، تعریف milestone، معیارهای موفقیت، برنامه‌ریزی منابع و استراتژی‌های کاهش ریسک.  

**Audience**: Technical Architects, Project Managers, Backend Developers  
**مخاطب**: معماران فنی، مدیران پروژه، توسعه‌دهندگان Backend  

**Reading Time**: 30 minutes  
**زمان مطالعه**: ۳۰ دقیقه  

**Required**: Optional  
**الزامی**: اختیاری  

---

### .ai/context/WORKSPACE.md

**English Title**: Workspace Context  
**فارسی**: بافت فضای کاری  

**Purpose**: AI agent workspace context including system boundaries, included/excluded scope, repository constraints, and recommended next steps.  
**هدف**: بافت فضای کاری عامل هوش مصنوعی شامل مرزهای سیستم، محدوده شامل/استثنای شده، محدودیت‌های ریپازیتوری و مراحل بعدی پیشنهادی.  

**Audience**: AI Agents  
**مخاطب**: عوامل هوش مصنوعی  

**Reading Time**: 10 minutes  
**زمان مطالعه**: ۱۰ دقیقه  

**Required**: YES  
**الزامی**: بله  

---

### .ai/context/PROJECT_STATE.md

**English Title**: Project State  
**فارسی**: وضعیت پروژه  

**Purpose**: Current implementation state including completed phases, what exists, what is NOT built yet, known risks, and next required steps.  
**هدف**: وضعیت فعلی پیاده‌سازی شامل فازهای کامل شده، آنچه وجود دارد، آنچه هنوز ساخته نشده، ریسک‌های شناخته شده و مراحل بعدی الزامی.  

**Audience**: AI Agents, All Developers, Technical Architects  
**مخاطب**: عوامل هوش مصنوعی، تمام توسعه‌دهندگان، معماران فنی  

**Reading Time**: 15 minutes  
**زمان مطالعه**: ۱۵ دقیقه  

**Required**: YES  
**الزامی**: بله  

---

### .ai/context/ARCHITECTURE_REVIEW.md

**English Title**: Architecture Review  
**فارسی**: بررسی معماری  

**Purpose**: Architecture review findings including identified gaps, decisions, and resolutions for the GymPlatform codebase.  
**هدف**: یافته‌های بررسی معماری شامل شکاف‌های شناسایی شده، تصمیمات و راه‌حل‌ها برای کد پایه GymPlatform.  

**Audience**: Technical Architects, AI Agents  
**مخاطب**: معماران فنی، عوامل هوش مصنوعی  

**Reading Time**: 20 minutes  
**زمان مطالعه**: ۲۰ دقیقه  

**Required**: YES (for Architects and AI Agents)  
**الزامی**: بله (برای معماران و عوامل هوش مصنوعی)  

---

### .ai/agent-rules.md

**English Title**: AI Execution Rules  
**فارسی**: قوانین اجرایی هوش مصنوعی  

**Purpose**: **ABSOLUTELY MANDATORY** permanent rules for all AI agents including execution principles, mandatory resource cleanup policy, mandatory git policy, and end-of-task checklist.  
**هدف**: **کاملاً الزامی** قوانین دائمی برای تمام عوامل هوش مصنوعی شامل اصول اجرایی، سیاست تمیزگیری منابع اجباری، خط‌مشی git اجباری و چک‌لیست پایان وظیفه.  

**Audience**: AI Agents  
**مخاطب**: عوامل هوش مصنوعی  

**Reading Time**: 10 minutes (READ EVERY WORD)  
**زمان مطالعه**: ۱۰ دقیقه (خواندن هر کلمه)  

**Required**: ABSOLUTELY MANDATORY — FIRST THING READ IN EVERY SESSION  
**الزامی**: کاملاً الزامی — اولین چیزی در هر جلسه  

---

### .ai/context/MASTER_PLAN.md (placeholder in some references)

**English Title**: Master Plan Reference  
**فارسی**: مرجع نقشه اصلی  

**Purpose**: See `.ai/context/IMPLEMENTATION_MASTER_PLAN.md` — the definitive implementation roadmap.  
**هدف**: برای اطلاعات بیشتر به `.ai/context/IMPLEMENTATION_MASTER_PLAN.md` مراجعه کنید — نقشه راه اجرایی قطعی.  

**Audience**: Technical Architects  
**مخاطب**: معماران فنی  

**Reading Time**: See IMPLEMENTATION_MASTER_PLAN.md  
**زمان مطالعه**: به IMPLEMENTATION_MASTER_PLAN.md مراجعه کنید  

**Required**: YES  
**الزامی**: بله  

---

### docs/PROJECT_HANDOFF.md

**English Title**: Project Handoff  
**فارسی**: تحویل پروژه  

**Purpose**: Live project handoff document recording current implementation phase, architecture summary, complete file listing by module, known technical debt, remaining roadmap, and next recommended task. Updated after each sprint.  
**هدف**: سند تحویل پروژه ثبت وضعیت فعلی پیاده‌سازی، خلاصه معماری، فهرست کامل فایل‌ها به تفکیک ماژول، بدهی فنی شناخته شده، نقشه راه باقی مانده و وظیفه بعدی پیشنهادی. به‌روزرسانی بعد از هر Sprint.  

**Audience**: All Developers, AI Agents, Technical Architects  
**مخاطب**: تمام توسعه‌دهندگان، عوامل هوش مصنوعی، معماران فنی  

**Reading Time**: 20 minutes  
**زمان مطالعه**: ۲۰ دقیقه  

**Required**: YES  
**الزامی**: بله  

---

### docs/IMPLEMENTATION_CHANGES.md

**English Title**: Implementation Changes Log  
**فارسی**: ثبت تغییرات پیاده‌سازی  

**Purpose**: Chronological log of every file created or modified during implementation, tracking what was built in each sprint, what technical debt was resolved, and what remains.  
**هدف**: ثبت زمانی از هر فایل ایجاد یا تغییر داده شده در طول پیاده‌سازی، ردیابی آنچه در هر Sprint ساخته شد، بدهی فنی رفع شده و آنچه باقی مانده.  

**Audience**: Developers, Technical Architects, AI Agents  
**مخاطب**: توسعه‌دهندگان، معماران فنی، عوامل هوش مصنوعی  

**Reading Time**: 15 minutes  
**زمان مطالعه**: ۱۵ دقیقه  

**Required**: YES  
**الزامی**: بله  

---

### .ai/context/AI_MEMORY.md

**English Title**: AI Memory  
**فارسی**: حافظه هوش مصنوعی  

**Purpose**: Persistent AI session memory storing knowledge graph entities and relationships discovered during development sessions.  
**هدف**: حافظه دائمی جلسات AI ذخیره‌سازی موجودیت‌های گراف دانش و روابط کشف شده در طول جلسات توسعه.  

**Audience**: AI Agents  
**مخاطب**: عوامل هوش مصنوعی  

**Reading Time**: Variable  
**زمان مطالعه**: متغیر  

**Required**: YES (for AI continuity)  
**الزامی**: بله (برای تداوم AI)  

---

### docs/VISION.md

**English Title**: Product Vision  
**فارسی**: چشم‌انداز محصول  

**Purpose**: Business vision statement including who GymPlatform serves, the core problem it solves, the high-level value proposition, business goals, and system boundaries.  
**هدف**: بیانیه چشم‌انداز کسب‌وکار شامل مخاطبان GymPlatform، مشکل اصلی حل‌شده، پیشنهاد ارزش سطح بالا، اهداف تجاری و مرزهای سیستم.  

**Audience**: Product Managers, Technical Architects, Stakeholders  
**مخاطب**: مدیران محصول، معماران فنی، ذینفعان  

**Reading Time**: 10 minutes  
**زمان مطالعه**: ۱۰ دقیقه  

**Required**: YES  
**الزامی**: بله  

---

### docs/MASTER_PRD.md

**English Title**: Master Product Requirements Document  
**فارسی**: سند اصلی نیازمندی‌های محصول  

**Purpose**: High-level product requirements including vision statement, target audience, market analysis, product goals, feature requirements (MVP and Phase 2/3), NFRs, success metrics, and constraints.  
**هدف**: نیازمندی‌های سطح بالای محصول شامل بیانیه چشم‌انداز، مخاطبان هدف، تحلیل بازار، اهداف محصول، نیازمندی‌های ویژگی، NFRها، معیارهای موفقیت و محدودیت‌ها.  

**Audience**: Product Managers, Technical Architects  
**مخاطب**: مدیران محصول، معماران فنی  

**Reading Time**: 20 minutes  
**زمان مطالعه**: ۲۰ دقیقه  

**Required**: YES  
**الزامی**: بله  

---

### docs/BUSINESS_RULES.md

**English Title**: Business Rules  
**فارسی**: قوانین تجاری  

**Purpose**: All business rules, policies, and operational constraints for the platform covering membership, billing, scheduling, trainers, check-in, notifications, and data retention.  
**هدف**: تمام قوانین تجاری، خط‌مشی‌ها و محدودیت‌های عملیاتی پلتفرم شامل عضویت، صورت‌حساب، برنامه‌ریزی، مربیان، ورود، notification و نگهداری داده.  

**Audience**: Backend Developers, Technical Architects, Product Managers  
**مخاطب**: توسعه‌دهندگان Backend، معماران فنی، مدیران محصول  

**Reading Time**: 25 minutes  
**زمان مطالعه**: ۲۵ دقیقه  

**Required**: YES  
**الزامی**: بله  

---

### docs/USER_ROLES.md

**English Title**: User Roles and Permissions  
**فارسی**: نقش‌های کاربری و مجوزها  

**Purpose**: Complete permission model including all 6 user roles (Administrator, Gym Owner, Gym Manager, Fitness Trainer, Front Desk Staff, Member), permission matrix, multi-tenant access rules, and role management procedures.  
**هدف**: مدل کامل مجوزها شامل همه ۶ نقش کاربری، ماتریس مجوز، قوانین دسترسی چندمستاجر و رویه‌های مدیریت نقش.  

**Audience**: Backend Developers, Technical Architects, AI Agents  
**مخاطب**: توسعه‌دهندگان Backend، معماران فنی، عوامل هوش مصنوعی  

**Reading Time**: 20 minutes  
**زمان مطالعه**: ۲۰ دقیقه  

**Required**: YES  
**الزامی**: بله  

---

### docs/ARCHITECTURE.md

**English Title**: System Architecture  
**فارسی**: معماری سیستم  

**Purpose**: System architecture overview covering technology stack, system components, data architecture, API architecture, security architecture, deployment architecture, and monitoring strategy.  
**هدف**: مرور معماری سیستم شامل پشته فناوری، کامپوننت‌های سیستم، معماری داده، معماری API، معماری امنیتی، معماری استقرار و استراتژی پایش.  

**Audience**: Technical Architects, Backend Developers, AI Agents  
**مخاطب**: معماران فنی، توسعه‌دهندگان Backend، عوامل هوش مصنوعی  

**Reading Time**: 25 minutes  
**زمان مطالعه**: ۲۵ دقیقه  

**Required**: YES  
**الزامی**: بله  

**Note | توجه**: This file is a preliminary document. For current implementation details, refer to `.ai/context/IMPLEMENTATION_MASTER_PLAN.md`.  
**نکته**: این فایل یک سند اولیه است. برای جزئیات پیاده‌سازی فعلی به `.ai/context/IMPLEMENTATION_MASTER_PLAN.md` مراجعه کنید.  

---

### docs/API_DESIGN.md

**English Title**: API Design  
**فارسی**: طراحی API  

**Purpose**: REST API design principles, conventions, HTTP method usage, error responses, rate limiting, versioning, pagination, and filtering specifications.  
**هدف**: اصول طراحی REST API، قراردادها، استفاده از متدهای HTTP، پاسخ‌های خطا، محدودیت نرخ، نسخه‌بندی، صفحه‌بندی و مشخصات فیلتر.  

**Audience**: Backend Developers, Frontend Developers  
**مخاطب**: توسعه‌دهندگان Backend، توسعه‌دهندگان Frontend  

**Reading Time**: 20 minutes  
**زمان مطالعه**: ۲۰ دقیقه  

**Required**: YES  
**الزامی**: بله  

**Note | توجه**: This file is a preliminary document. For the authoritative API specifications, refer to `.ai/context/API_BLUEPRINT.md`.  
**نکته**: این فایل یک سند اولیه است. برای مشخصات قطعی API به `.ai/context/API_BLUEPRINT.md` مراجعه کنید.  

---

### docs/DATABASE.md

**English Title**: Database Design  
**فارسی**: طراحی پایگاه داده  

**Purpose**: Database design overview including schema organization, entity relationships, core tables, indexing strategy, migration approach, performance optimization, and backup/recovery procedures.  
**هدف**: مرور طراحی پایگاه داده شامل سازماندهی schema، روابط Entity، جداول اصلی، استراتژی index، رویکرد migration، بهینه‌سازی عملکرد و رویه‌های پشتیبان‌گیری/بازیابی.  

**Audience**: Backend Developers, Technical Architects  
**مخاطب**: توسعه‌دهندگان Backend، معماران فنی  

**Reading Time**: 20 minutes  
**زمان مطالعه**: ۲۰ دقیقه  

**Required**: YES  
**الزامی**: بله  

**Note | توجه**: This file is a preliminary document. For the authoritative database specifications, refer to `.ai/context/DATABASE_BLUEPRINT.md`.  
**نکته**: این فایل یک سند اولیه است. برای مشخصات قطعی پایگاه داده به `.ai/context/DATABASE_BLUEPRINT.md` مراجعه کنید.  

---

### docs/UI_UX.md

**English Title**: UI/UX Design  
**فارسی**: طراحی UI/UX  

**Purpose**: Design system specifications including color palette, typography scale, component library, navigation patterns, user flows, accessibility (WCAG 2.1 AA), mobile breakpoints, and admin portal design guidelines.  
**هدف**: مشخصات سیستم طراحی شامل پالت رنگی، مقیاس تایپوگرافی، کتابخانه کامپوننت، الگوهای ناوبری، جریان‌های کاربر، دسترس‌پذیری (WCAG 2.1 AA)، breakpoints موبایل و راهنمای طراحی پورتال ادمین.  

**Audience**: Frontend Developers, UX Designers  
**مخاطب**: توسعه‌دهندگان Frontend، طراحان UX  

**Reading Time**: 25 minutes  
**زمان مطالعه**: ۲۵ دقیقه  

**Required**: YES  
**الزامی**: بله  

**Note | توجه**: This file is a preliminary document. For current UI specifications, refer to `.ai/context/UI_UX_BLUEPRINT.md`.  
**نکته**: این فایل یک سند اولیه است. برای مشخصات فعلی UI به `.ai/context/UI_UX_BLUEPRINT.md` مراجعه کنید.  

---

### docs/ROADMAP.md

**English Title**: Product Roadmap  
**فارسی**: نقشه راه محصول  

**Purpose**: Strategic timeline with quarterly milestones (Q4 2026 through Q4 2027), feature priorities, success metrics by quarter, and resource allocation targets.  
**هدف**: خط زمانی استراتژیک با milestoneهای فصلی، اولویت‌های ویژگی، معیارهای موفقیت به تفکیک فصل و اهداف تخصیص منابع.  

**Audience**: Product Managers, Technical Architects, Stakeholders  
**مخاطب**: مدیران محصول، معماران فنی، ذینفعان  

**Reading Time**: 15 minutes  
**زمان مطالعه**: ۱۵ دقیقه  

**Required**: YES  
**الزامی**: بله  

**Note | توجه**: This file is a preliminary document. For the authoritative roadmap, refer to `.ai/context/MASTER_ROADMAP.md`.  
**نکته**: این فایل یک سند اولیه است. برای نقشه راه قطعی به `.ai/context/MASTER_ROADMAP.md` مراجعه کنید.  

---

### docs/backend/BACKEND_GUIDE_FA.md

**English Title**: Backend Developer Guide (Persian)  
**فارسی**: راهنمای توسعه‌دهنده Back-end  

**Purpose**: Comprehensive Persian guide for backend development covering Clean Architecture enforcement, CQRS patterns, SharedKernel types, Repository implementation, Domain Events, Multi-Tenant isolation, validation layers, DI registration rules, testing strategy, and 15+ common mistakes with solutions. Includes full end-to-end command implementation example.  
**هدف**: راهنمای فارسی جامع توسعه Backend شامل اجرای Clean Architecture، الگوهای CQRS، انواع SharedKernel، پیاده‌سازی Repository، رویدادهای دامنه، انزوای چندمستاجر، لایه‌های اعتبارسنجی، قوانین ثبت DI، استراتژی تست و بیش از ۱۵ اشتباه متداول با راه‌حل. شامل مثال کامل پیاده‌سازی Command از ابتدا تا انتها.  

**Audience**: Backend Developers  
**مخاطب**: توسعه‌دهندگان Backend  

**Reading Time**: 60 minutes  
**زمان مطالعه**: ۶۰ دقیقه  

**Required**: YES (for Backend Developers)  
**الزامی**: بله (برای توسعه‌دهندگان Backend)  

---

### docs/frontend/FRONTEND_DEVELOPER_HANDBOOK_FA.md

**English Title**: Frontend Developer Handbook (Persian)  
**فارسی**: دفترچه راه توسعه‌دهنده Front-end  

**Purpose**: Persian handbook for frontend development covering planned architecture, project structure, technologies (Next.js, React, TypeScript, Tailwind, TanStack Query), state management, API integration patterns, routing, responsive design, and testing strategy. Also covers migration path from initial scaffold to full implementation.  
**هدف**: دفترچه راه فارسی توسعه Frontend شامل معماری برنامه‌ریزی شده، ساختار پروژه، فناوری‌ها، مدیریت state، الگوهای یکپارچگی API، مسیریابی، طراحی واکنش‌گرا و استراتژی تست. همچنین مسیر مهاجرت از skelet اولیه به پیاده‌سازی کامل را پوشش می‌دهد.  

**Audience**: Frontend Developers  
**مخاطب**: توسعه‌دهندگان Frontend  

**Reading Time**: 30 minutes  
**زمان مطالعه**: ۳۰ دقیقه  

**Required**: YES (when frontend implementation begins)  
**الزامی**: بله (هنگام شروع پیاده‌سازی Frontend)  

---

### .ai/context/PRODUCT_BLUEPRINT.md

**English Title**: Product Blueprint  
**فارسی**: نقشه محصول  

**Purpose**: Authoritative product specification covering 21 modules, business model, complete feature inventory, 6 user journeys, UX principles, product goals across 5-year horizon.  
**هدف**: مشخصات قطعی محصول شامل ۲۱ ماژول، مدل تجاری، فهرست کامل ویژگی‌ها، ۶ سفر کاربر، اصول UX و اهداف محصول در افق ۵ ساله.  

**Audience**: Technical Architects, Product Managers, AI Agents  
**مخاطب**: معماران فنی، مدیران محصول، عوامل هوش مصنوعی  

**Reading Time**: 45 minutes  
**زمان مطالعه**: ۴۵ دقیقه  

**Required**: YES  
**الزامی**: بله  

---

### .ai/context/ARCHITECTURE_REVIEW.md

**English Title**: Architecture Review  
**فارسی**: بررسی معماری  

**Purpose**: Formal architecture review findings, decisions, and resolutions for the GymPlatform implementation. Documents architectural decisions, conflicts resolved, and guidelines for future development.  
**هدف**: یافته‌های رسمی بررسی معماری، تصمیمات و راه‌حل‌ها برای پیاده‌سازی GymPlatform. تصمیمات معماری، تعارض‌های حل شده و راهنمای توسعه آینده را ثبت می‌کند.  

**Audience**: Technical Architects, AI Agents  
**مخاطب**: معماران فنی، عوامل هوش مصنوعی  

**Reading Time**: 20 minutes  
**زمان مطالعه**: ۲۰ دقیقه  

**Required**: YES  
**الزامی**: بله  

---

## Documentation Maintenance Policy / خط‌مشی نگهداری مستندات

Documentation must be updated as part of every implementation task.  
مستندات باید به عنوان بخشی از هر وظیفه پیاده‌سازی به‌روزرسانی شود.

- Every sprint must update `docs/PROJECT_HANDOFF.md` and `docs/IMPLEMENTATION_CHANGES.md`.  
  هر Sprint باید `docs/PROJECT_HANDOFF.md` و `docs/IMPLEMENTATION_CHANGES.md` را به‌روز کند.
- New features require corresponding updates to user guides.  
  ویژگی‌های جدید نیاز به به‌روزرسانی متناظر راهنمای کاربران دارند.
- Outdated docs in `docs/` (marked Draft) should be updated or archived.  
  مستندات منسوخ شده در `docs/` (علامت‌دار Draft) باید به‌روز یا بایگانی شوند.
- The `.ai/context/` directory is the authoritative source for AI agent context.  
  دایرکتوری `.ai/context/` منبع قطعی بافت عامل هوش مصنوعی است.
- CHANGELOG.md must be updated for every version release.  
  CHANGELOG.md باید برای هر نسخه منتشر شده به‌روز شود.

---

*Generated by Documentation Audit — 2026-07-03*  
*تولید شده توسط بررسی مستندات — ۲۰۲۶-۰۷-۰۳*
