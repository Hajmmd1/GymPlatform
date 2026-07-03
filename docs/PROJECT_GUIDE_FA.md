# راهنمای پروژه GymPlatform

> مستندات رسمی برای توسعه‌دهندگان， DevOps و ذینفعان  
> آخرین به‌روزرسانی: ۲۸ ژوئن ۲۰۲۶

---

## فهرست مطالب

1. [مقدمه](#1-مقدمه)
2. [چشم‌انداز پروژه](#2-چشمانداز-پروژه)
3. [اهداف تجاری](#3-اهداف-تجاری)
4. [معماری SaaS](#4-معماری-saas)
5. [ماژول‌های سیستم](#5-ماژول‌های-سیستم)
6. [نقشه‌راه توسعه](#6-نقشه‌راه-توسعه)
7. [جریان کاری توسعه](#7-جریان-کاری-توسعه)
8. [فلسفه پروژه](#8-فلسفه-پروژه)
9. [فناوری‌های اصلی](#9-فناوری‌های-اصلی)
10. [ساختار ریپازیتوری](#10-ساختار-ریپازیتوری)
11. [نحوه شروع کار](#11-نحوه-شروع-کار)
12. [واژه‌نامه](#12-واژه‌نامه)

---

## 1. مقدمه

**GymPlatform** یک پلتفرم SaaS سازمانی برای مدیریت کامل کسب‌وکارهای ورزشی است.

### هدف اصلی

کاهش اصطکاک‌های عملیاتی و افزایش رضایت مشتریان باشگاه‌ها با تمرکز بر سه گروه اصلی کاربر:

| گروه کاربر | هدف اصلی |
|-----------|---------|
| مالک باشگاه | مدیریت مالی، اعضا، مربیان و گزارش‌گیری |
| مربی | برنامه‌سازی تمرین، پیگیری پیشرفت اعضا، درآمدزایی |
| عضو | مشاهده تمرین‌ها، رزرو جلسات، پیگیری پیشرفت |

### محدوده محصول

GymPlatform فرآیندهای زیر را یکپارچه می‌کند:

- عضویت و مدیریت کاربران باشگاه
- برنامه‌سازی تمرینات و کتابخانه تمرینات
- رزرو جلسات و مدیریت تقویم
- پرداخت‌ها، اشتراک‌ها و بازار برنامه تمرینی
- ارسال notification، چت و پیام‌رسانی
- گزارش‌گیری و تحلیل عملکرد باشگاه

### وضعیت فعلی پروژه

| بخش | وضعیت |
|-----|-------|
| مستندات محصول | ✅ کامل (۱۵ فاز مستندات‌سازی) |
| Membersip (عضویت) | ✅ پیاده‌سازی شده: Domain + Application + Infrastructure + API |
| Training (تمرینات) | ✅ پیاده‌سازی شده: Domain + Application + Infrastructure + API |
| Communication (ارتباطات) | 🔄 در حال توسعه: تقویم و رزرو جلسات کامل |
| Financial (مالی) | 🟡 ساختار آماده، پیاده‌سازی نشده |
| Frontend | 🟡 آشنایی، پیاده‌سازی نشده |
| تست‌ها | 🔄 Membership و Communication تست‌های واحد دارند |

---

## 2. چشم‌انداز پروژه

### ماموریت

ارائه بستر نرم‌افزاری یکپارچه که به باشگاه‌های ورزشی کمک کند تا به جای نگرانی در مورد مشکلات عملیاتی، بر رشد تجاری و تجربه بهتر مشتری تمرکز کنند.

### چشم‌انداز بلندمدت

| بازه زمانی | هدف |
|----------|-----|
| ۶ ماه اول | راه‌اندازی پایلوت با ۱۰ باشگاه + پایداری فنی |
| ۱۲ ماه اول | ۱۰۰ باشگاه پرداخت‌کننده + ۵۰،۰۰۰ عضو فعال + $۱۰۰K MRR |
| ۲۴ ماه | ۵۰۰ باشگاه + ۵۰،۰۰۰ عضو فعال + $۲M ARR |

### اصول بنیادی چشم‌انداز

- **تمرکز بر محلی**: اولویت با نیازهای باشگاه‌های ایرانی و منطقه‌ای
- **سادگی در استفاده**: رابط کاربری ساده و بدون نیاز به آموزش طولانی
- **یکپارچگی**: همه فرآیندهای باشگاه در یک پلتفرم
- **توسعت‌پذیری**: معماری برای رشد از ۱۰ باشگاه تا ۱۰۰۰ باشگاه

---

## 3. اهداف تجاری

### مدل درآمدی

| منبع درآمد | توضیحات |
|----------|---------|
| اشتراک باشگاه‌ها | پلان‌های ماهیانه/سالانه بر اساس تعداد اعضا |
| اشتراک مربیان | پلان‌های تمرینات آنلاین برای مربیان مستقل |
| مارکت‌پلیس | کمیسیون از فروش برنامه‌های تمرینی |
| اشتراک سازمانی | پلان‌های سازمانی برای زنجیره‌های باشگاه |

### هدف‌های کلیدی

- کاهش ۷۰٪ زمان صرف‌شده بر روی امور اداری باشگاه‌ها
- افزایش ۴۰٪ نرخ تمدید اشتراک اعضا
- کاهش ۶۰٪ هزینه عملیاتی باشگاه‌ها
- دستیابی به NPS بالاتر از ۵۰

---

## 4. معماری SaaS

### ساختار کلی پلتفرم

```
┌─────────────────────────────────────────────────────┐
│                    لایه کلاینت                        │
│         (وب، موبایل، PWA، ادمین پنل)                   │
├─────────────────────────────────────────────────────┤
│              GymPlatform.Api (Composition Root)       │
│           authentication ─ middleware                 │
│           minimal API endpoints                        │
├─────────────────────────────────────────────────────┤
│  Application  ←  (Use Cases / Commands / Queries)     │
│  Domain       ←  (Entities / Value Objects / Events)  │
│  Infrastructure← (EF Core / Repositories / External)  │
├─────────────────────────────────────────────────────┤
│           SQL Server (Multi-tenant Schema)             │
└─────────────────────────────────────────────────────┘
```

### معماری ماژولار

GymPlatform از الگوی **Modular Monolith** پیروی می‌کند:

```
GymPlatform.sln
├── GymPlatform.Api                    ← نقطه ورود و API
├── GymPlatform.SharedKernel           ← انواع مشترک بین ماژول‌ها
├── GymPlatform.Infrastructure         ← EF Core، Repositoryها، سرویس‌های خارجی
├── GymPlatform.Modules.Membership/    ← ماژول عضویت
├── GymPlatform.Modules.Training/      ← ماژول تمرینات
├── GymPlatform.Modules.Financial/     ← ماژول مالی
└── GymPlatform.Modules.Communication/ ← ماژول ارتباطات
```

### معماری خالص (Clean Architecture)

هر ماژول از ساختار Clean Architecture پیروی می‌کند:

```
GymPlatform.Modules.<Module>/
├── Application/                  ← Use Cases، Commandها، Queryها، Validators
│      Domain只依存
│   ├── Commands/
│   ├── DTOs/
│   ├── Interfaces/
│   ├── Validators/
│   └── Queries/
├── Domain/                       ← Entities، Value Objects، Domain Events
    nothing依存 (pure domain)
│   ├── Entities/
│   ├── Enums/
│   ├── Events/
│   ├── Exceptions/
│   ├── Repositories/
│   └── ValueObjects/
└── Infrastructure/               ← EF Core، Repository پیاده‌سازی‌ها
    Domain + Application に依存
```

**قانون کلیدی**: لایه Domain هیچ وابستگی خارجی ندارد. لایه Application فقط به Domain متصل است. لایه Infrastructure هر دو را پیاده‌سازی می‌کند.

---

## 5. ماژول‌های سیستم

### جدول کلی ماژول‌ها

| # | ماژول | توضیحات | پروژه هدف |
|---|-------|---------|----------|
| ۱ | عضویت | ایجاد باشگاه، ثبت‌نام اعضا، مدیریت مربیان | Membership ✅ |
| ۲ | مدیریت تمرینات | CRUD برنامه‌های تمرینی | Training ✅ |
| ۳ | ویرایشگر تمرین | سازمان‌دهی تمرینات با Drag & Drop | Training 🔲 |
| ۴ | کتابخانه تمرینات | کتابخانه تمرینات معتبر | Training ✅ |
| ۵ | ویدیوی تمرینات | آپلود و مدیریت ویدیوهای آموزشی | Training ✅ |
| ۶ | اندازه‌گیری‌های بدن | ثبت و پیگیری اندازه‌های فیزیولوژیک | Training 🔲 |
| ۷ | عکس‌های پیشرفت | آپلود عکس برای پیگیری پیشرفت | Training 🔲 |
| ۸ | پیگیری پیشرفت | نمودارهای پیشرفت با مقایسه | Training 🔲 |
| ۹ | پروفایل مربی | مشخصات عمومی مربیان، گواهینامه‌ها | Training 🔲 |
| ۱۰ | مربیگری آنلاین | جلسات و برنامه‌های آنلاین مربیگری | Training 🔲 |
| ۱۱ | پرداخت | مدیریت روش‌های پرداخت و اشتراک‌ها | Financial 🔲 |
| ۱۲ | مدیریت مالی | تراکنش‌ها، خزانه‌داری، گزارش مالی | Financial 🔲 |
| ۱۳ | بازار | لیست کردن و خرید برنامه‌های تمرینی | Financial 🔲 |
| ۱۴ | امتیازها و نقدها | امتیازدهی مربیان و برنامه‌ها | Financial 🔲 |
| ۱۵ | چت و پیام‌رسانی | ارتباط بلادرنگ مربی و عضو | Communication 🔲 |
| ۱۶ | اعلان‌ها | ایمیل، SMS، اعلان موبایل | Communication 🔲 |
| ۱۷ | تقویم | جلسات، رزروها، در دسترس بودن مربی | Communication ✅ |
| ۱۸ | تنظیمات | تنظیمات باشگاه و پلان‌ها | Communication 🔲 |
| ۱۹ | پنل مدیریت | مدیریت پلتفرم توسط ادمین | Communication 🔲 |
| ۲۰ | مدیریت رسانه | مدیریت فایل‌های چندرسانه‌ای | Communication 🔲 |
| ۲۱ | گزارش و تحلیل | داشبوردهای تحلیلی | Communication 🔲 |

### وضعیت پیاده‌سازی

| پروژه | وضعیت | توضیحات |
|--------|-------|---------|
| Membership | ✅ کامل | Domain، Application، Infrastructure، API، تست‌ها |
| Training | ✅ Domain + Application + API | Infrastructure + تست‌های کامل باقی مانده |
| Communication | 🔄 در حال توسعه | تقویم کامل، چت و پیام‌رسانی باقی مانده |
| Financial | 🟡 آماده | ساختار پروژه، پیاده‌سازی نشده |

---

## 6. نقشه‌راه توسعه

### فازهای کل پروژه

| فاز | بازه زمانی | هدف | نتیجه کلیدی |
|------|-----------|------|--------------|
| ۰ | هفته‌های ۱-۳ | پایدارسازی Membership | یکپارچگی تست و API |
| ۱ | هفته‌های ۴-۱۰ | پیاده‌سازی Training | تمرینات کامل + API |
| ۲ | هفته‌های ۱۱-۱۶ | ارتباطات و عملیات | رزرو جلسات + چت + اعلان |
| ۳ | هفته‌های ۱۷-۲۲ | مالی و مارکت‌پلیس | پرداخت + اشتراک + معامله |
| ۴ | هفته‌های ۲۳-۲۶ | یکپارچگی و پایلوت | هدایت کامل + پیلوت |
| ۵ | ماه‌های ۷-۹ | مقیاس‌دهی | ۱۰۰+ باشگاه پرداخت‌کننده |
| ۶ | ماه‌های ۱۰-۱۲ | هوشمندسازی | AI + بین‌المللی |

### معیارهای موفقیت کل

| مایل‌استون | هدف |
|-----------|------|
| Week ۲۶ | $۲۵K+ MRR، ۱۰ باشگاه پایلوت، production-ready |
| Month ۹ | ۱۰۰+ باشگاه پرداخت‌کننده، $۱۰۰K+ MRR |
| Month ۱۲ | ۵۰۰ باشگاه، ۵۰،۰۰۰ عضو، $۲M ARR |
| NPS | بالاتر از ۵۰ |
| آپتایم | ۹۹.۹٪ سرویس |

### Sprint جاری

| Sprint | هفته ها | موضوع | وضعیت |
|--------|---------|-------|-------|
| Sprint ۱۰ | ۱۰ | Training Integration | ✅ کامل |
| Sprint ۱۱ | ۱۱ | Communication Calendar Domain | ✅ کامل |
| Sprint ۱۲ | ۱۱-۱۲ | Communication Application + Infrastructure | 🔄 در حال توسعه |
| Sprint ۱۳ | ۱۳-۱۴ | چت و پیام‌رسانی | 🔲 برنامه‌ریزی شده |

---

## 7. جریان کاری توسعه

### Git Workflow

```
main (نسخه production)
    ↑
develop (توسعه فعال)
    ↑
feature/xxx (ویژگی جدید)
    ↑
hotfix/xxx (رفع باگ فوری)
```

### روند کار (Pull Request)

```
1. شاخه feature از develop شاخه‌برداری می‌شود
2. پیاده‌سازی + تست‌های واحد
3.Ran dotnet test
4.送出 Pull Request به develop
5. حداقل ۱ بررسی‌کننده (review)
6. پیاده‌سازی با موفقیت ادغام می‌شود
```

### قوانین کامیت

از Rag_of الگوی [Conventional Commits] پیروی کنید:

| پیشوند | نوع کامیت |
|---------|----------|
| `feat:` | ویژگی جدید |
| `fix:` | رفع باگ |
| `docs:` | تغییرات مستندات |
| `refactor:` | بازنویسی کد |
| `test:` | افزودن/اصلاح تست |
| `chore:` | کارهای نگه‌داری |

### استاندارد تست

| سطح | ابزار | هدف |
|------|-------|------|
| واحد | xUnit + Moq | پوشش >۸۰٪ لایه Application |
| یکپارچگی | TestContainers | همه جریان‌های handler و Repository |
| قراردادی | OpenAPI | تمام endpointهای عمومی |
| End-to-End | Playwright | سفرهای اصلی کاربر (در فاز پایلوت) |

### فرآیند Build

```bash
# بازسازی کامل
dotnet build

# تست‌های واحد
dotnet test

# بررسی ساختار (AST)
dotnet format
```

---

## 8. فلسفه پروژه

### اصول فنی

**۱. Clean Architecture سخت‌گیرانه**
- لایه Domain هیچ وابستگی خارجی ندارد
- تمام منطق تجاری در Domain نه Application قرار می‌گیرد
- Repositoryها فقط در Infrastructure پیاده‌سازی می‌شوند

**۲. CQRS برای عملیات بزرگ**
- Write Model (Commands) برای تغییر وضعیت
- Read Model (Queries) برای خواندن
- جدا بودن خواندن از نوشتن به سادگی نگهداری کمک می‌کند

**۳. Event-Driven از هسته outward**
- Domain Events برای رویدادهای تجاری مهم
- Integration Events (در فازهای بعدی) برای ارتباط بین ماژول‌ها
- Eventual consistency برای داده‌های بین-ماژولی

**۴. Multi-Tenant را اول بگیر**
- هر Entity شامل TenantId است
- EF Core Global Query Filter به طور خودکار اعمال می‌شود
- Application مثل Single Tenant عمل می‌کند

**۵. YAGNI + KISS**
- پیاده‌سازی رو به آینده—فقط نیاز الآن
- پیچیدگی اضافی پرهیز شود
- نقشه راه ماژول‌ها به Phaseهای بعدی موکول شده

### اخلاق حرفه‌ای

- **Open Communication**: مشکلات سریع مطرح شوند، نه پنهان
- **Collective Ownership**: هیچ بخشی دارای مالک انفرادی نیست
- **Sustainable Pace**: pacesend سمی معتبر برای کدهای جدید
- **Fearless Refactoring**: با اطمینان از پوشش تست، بازنویسی کنید
- **Documentation as Code**: مستندات با کد آپدیت و مرتبط است

---

## 9. فناوری‌های اصلی

### Backend

| لایه | فناوری | نسخه |
|------|--------|------|
| فریم‌ورک | ASP.NET Core | ۱۰ |
| زبان | C# | ۱۲ |
| ORM | Entity Framework Core | ۱۰.۰.9 |
| پایگاه داده | SQL Server / LocalDB | - |
| احراز هویت | JWT Bearer (سفارشی) | - |
| مستندات API | Swagger / OpenAPI | Swashbuckle ۸.۱.۰ |
| تست‌نویسی | xUnit + Moq + FluentAssertions | - |

### Shared Kernel

| کامپوننت | توضیحات |
|---------|---------|
| `BaseEntity` | پایه همه Entityها، شامل ID و DomainEvents |
| `Result<T>` | نوع بازگشت عملیات با موفقیت/شکست |
| `IDomainEvent` | رابط رویدادهای دامنه |
| `IUnitOfWork` | الگوی واحد کار برای ذخیره‌سازی |
| `ICurrentUserService` | دسترسی به کاربر و Tenant فعلی |
| `IDateTimeProvider` | ارائه‌دهنده زمان (برای تست‌پذیری) |
| `Pagination` | انواع صفحه‌بندی |

### Infrastructure

| کامپوننت | توضیح |
|---------|--------|
| MembershipDbContext | EF Core DbContext برای ماژول عضویت |
| TrainingDbContext | EF Core DbContext برای ماژول تمرینات |
| CommunicationDbContext | EF Core DbContext برای ماژول ارتباطات |
| Repositoryها | پیاده‌سازی Repository Pattern از هر ماژول |
| Global Filters | فیلترهای خودکار EF برای TenantId |

### CI/CD (آینده)

| کامپوننت | فناوری |
|---------|--------|
| Pipeline | GitHub Actions (مرحله طراحی) |
| امنیت | OWASP ZAP، Snyk |
| کیفیت کد | SonarQube |
| نشر | Docker + Kubernetes (آینده) |

---

## 10. ساختار ریپازیتوری

```
GymPlatform/
├── .ai/                              ← AI Agent قوانین و بافت پروژه
│   ├── agent-rules.md                ← قوانین اجرایی اجباری AI
│   ├── context/
│   │   ├── WORKSPACE.md              ← بافت کیف پروژه
│   │   ├── PROJECT_STATE.md          ← وضعیت پروژه
│   │   ├── IMPLEMENTATION_MASTER_PLAN.md  ← نقشه راه اجرایی
│   │   ├── PRODUCT_BLUEPRINT.md
│   │   ├── FUNCTIONAL_REQUIREMENTS.md
│   │   ├── NON_FUNCTIONAL_REQUIREMENTS.md
│   │   ├── DATABASE_BLUEPRINT.md
│   │   ├── API_BLUEPRINT.md
│   │   ├── UI_UX_BLUEPRINT.md
│   │   └── MASTER_ROADMAP.md
├── adr/                              ← تصمیمات معماری (ADR)
├── docs/                             ← مستندات رسمی پروژه
│   ├── PROJECT_GUIDE_FA.md           ← این فایل
│   ├── backend/BACKEND_GUIDE_FA.md   ← راهنمای بک‌اند
│   ├── frontend/FRONTEND_DEVELOPER_HANDBOOK_FA.md
│   ├── PROJECT_HANDOFF.md
│   ├── IMPLEMENTATION_CHANGES.md
│   ├── CHANGELOG.md
│   ├── VISION.md
│   └── ...
├── GymPlatform.Api/                  ← لایه API
│   ├── Program.cs
│   ├── GlobalExceptionMiddleware.cs
│   ├── CurrentUserService.cs
│   └── appsettings.json
├── GymPlatform.SharedKernel/         ← هسته مشترک
│   ├── BaseEntity.cs
│   ├── Result.cs
│   ├── IDomainEvent.cs
│   ├── DomainEventBase.cs
│   ├── IUnitOfWork.cs
│   └── Abstractions/
├── GymPlatform.Infrastructure/       ← زیرساخت
│   ├── InfrastructureServiceCollectionExtensions.cs
│   ├── Persistence/
│   │   ├── DbContexts/
│   │   ├── Configurations/
│   │   ├── Repositories/
│   │   └── Migrations/
│   └── ...
├── GymPlatform.Modules.Membership/   ← ماژول عضویت
├── GymPlatform.Modules.Training/     ← ماژول تمرینات
├── GymPlatform.Modules.Financial/    ← ماژول مالی (اسکلت)
├── GymPlatform.Modules.Communication/← ماژول ارتباطات
├── GymPlatform.Modules.Membership.Tests/    ← تست‌های Membership
├── GymPlatform.Modules.Communication.Tests/ ← تست‌های Communication
├── GymPlatform.sln                   ← Solution file
├── README.md
├── CONTRIBUTING.md
├── SECURITY.md
├── AGENTS.md
├── CHANGELOG.md
└── .github/                          ← GitHub Actions + Templates
```

### ماژول Membership - ساختار کامل

```
GymPlatform.Modules.Membership/
└── (به صورت Flat در سطح پروژه)
    ├── Domain/
    │   ├── Entities/
    │   │   ├── Gym.cs              ←aggregate root
    │   │   ├── Member.cs           ←aggregate root
    │   │   └── Coach.cs            ←aggregate root
    │   ├── ValueObjects/
    │   │   ├── Email.cs            ← concentrated value object
    │   │   └── Phone.cs
    │   ├── Enums/
    │   │   ├── MemberStatus.cs
    │   ├── Events/
    │   │   ├── GymCreated.cs
    │   │   ├── GymActivated.cs
    │   │   ├── GymDeactivated.cs
    │   │   ├── MemberRegistered.cs
    │   │   └── CoachAssigned.cs
    │   ├── Exceptions/
    │   │   └── MembershipDomainException.cs
    │   └── Repositories/
    │       ├── IGymRepository.cs
    │       ├── IMemberRepository.cs
    │       └── ICoachRepository.cs
    └── Application/
        ├── Interfaces/
        │   ├── ICommand.cs
        │   ├── ICommandHandler.cs
        │   └── ICommandValidator.cs
        ├── DTOs/
        │   ├── CreateGymRequest.cs
        │   ├── GymResponse.cs
        │   ├── RegisterMemberRequest.cs
        │   ├── MemberResponse.cs
        │   └── ...
        └── Commands/
            ├── CreateGym/
            ├── RegisterMember/
            ├── AssignMemberToCoach/
            └── DeactivateGym/
```

### ماژول Training - ساختار

```
GymPlatform.Modules.Training/
├── Domain/
│   ├── Entities/        (Exercise, WorkoutProgram, WorkoutLog, ExerciseVideo, BodyMeasurement, ProgressPhoto, CoachProfile, ProgramExercise, Certification)
│   ├── Enums/           (DifficultyLevel, ExerciseCategory, MeasurementType)
│   ├── Events/
│   ├── Exceptions/
│   └── Repositories/
└── Application/
    ├── Interfaces/
    ├── DTOs/
    └── Commands/
        ├── CreateExercise/
        ├── CreateWorkoutProgram/
        ├── LogWorkout/
        ├── UploadExerciseVideo/
        ├── RecordBodyMeasurement/
        ├── UploadProgressPhoto/
        └── UpdateCoachProfile/
```

### ماژول Communication - ساختار

```
GymPlatform.Modules.Communication/
├── Domain/
│   ├── Entities/        (Session, Booking, Room, CoachAvailability)
│   ├── Enums/           (SessionType, BookingStatus)
│   ├── Events/          (SessionCreated, SessionCancelled, BookingCreated, BookingCancelled)
│   ├── Exceptions/
│   └── Repositories/
└── Application/
    ├── Interfaces/
    ├── DTOs/
    └── Commands/
        ├── CreateRoom/
        ├── CreateSession/
        ├── BookSession/
        ├── CancelBooking/
        ├── CancelSession/
        └── SetCoachAvailability/
```

---

## 11. نحوه شروع کار

### پیش‌نیازها

| ابزار | نسخه الزامی |
|------|-----------|
| .NET SDK | ۱۰.۰.۳۰۱+ |
| SQL Server / LocalDB | هر نسخه پشتیبانی شده |
| Git | ۲.۳۴+ |
| IDE | Visual Studio ۲۰۲۲ یا Rider |

### کلون کردن ریپازیتوری

```bash
git clone https://github.com/your-org/GymPlatform.git
cd GymPlatform
```

### بازیابی بسته‌ها

```bash
dotnet restore
```

### ساخت پروژه

```bash
dotnet build
```

### اجرای تست‌ها

```bash
dotnet test
```

### اجرای API

```bash
cd GymPlatform.Api
dotnet run
```

سپس به آدرس زیر مراجعه کنید:
- Swagger UI: `https://localhost:<port>/swagger`
- Health Check: `https://localhost:<port>/health`

### تنظیمات Local

فایل `GymPlatform.Api/appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "Default": "Server=(localdb)\\mssqllocaldb;Database=GymPlatform;Trusted_Connection=true;MultipleActiveResultSets=true"
  },
  "Jwt": {
    "Key": "dev-secret-key-change-in-production",
    "Issuer": "GymPlatform",
    "Audience": "GymPlatformClients",
    "ExpiryMinutes": 60
  }
}
```

### ساخت دیتابیس

```bash
dotnet ef migrations add <MigrationName> --project GymPlatform.Modules.Membership
dotnet ef database update --project GymPlatform.Modules.Membership
```

### رفع ابزارها

```bash
dotnet tool install --global dotnet-ef
```

---

## 12. واژه‌نامه

| واژه | معادل فارسی | توضیحات |
|-----|------------|---------|
| Aggregate / Aggregate Root | توده / ریشه توده | مجموعه Entities و Value Objects با یک Identity در DDD |
| Modular Monolith | مونولیت ماژولار | معماری واحد با ماژول‌های مشخص و تفکیک شده |
| Clean Architecture | معماری خالص | جداسازی لایه‌های Concern با وابستگی‌های یکطرفه |
| CQRS | CQRS | Command Query Responsibility Segregation |
| Domain Event | رویداد دامنه | رویدادی که در سطح domain اتفاق می‌افتد و به صورت notification منتشر می‌شود |
| Value Object | شیء مقدار | Entity بدون Identity، بر اساس Attributes قابل شناسایی می‌شود |
| Tenant | مستاجر | هر باشگاه به عنوان یک Tenant در سیستم |
| Multi-Tenant | چندمستاجر | معماری که چند سازمان را در یک نصب از سیستم پشتیبانی می‌کند |
| Global Query Filter | فیلتر کوئری جهانی | اعمال خودکار شرط (مثلاً TenantId) روی تمام کوئری‌های EF Core |
| Unit of Work | واحد کار | الگوی مدیریت تراکنش‌ها در Repository Pattern |
| Dependency Injection | تزریق وابستگی |何方ای به صورت خودکار توسط فریم‌ورک مدیریت می‌شود |
| DTO | Data Transfer Object | شیء انتقال داده بین لایه‌ها |
| Idempotency Key | کلید یکپارچگی | هدر برای جلوگیری از پردازش تکراری درخواست‌های write |
| SaaS | نرم‌افزار به عنوان سرویس | مدل عرضه نرم‌افزار به صورت subscription |

---

*این سند بر اساس وضعیت پروژه به تاریخ ۲۸ ژوئن ۲۰۲۶ تهیه شده است. برای آپدیت نسخه، مستندات `.ai/context/` را مراجعه کنید.*
