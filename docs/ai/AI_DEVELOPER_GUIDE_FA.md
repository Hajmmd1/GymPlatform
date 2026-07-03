# راهنمای توسعه‌دهنده عامل هوش مصنوعی GymPlatform

> راهنمای رسمی برای عامل‌های هوش مصنوعی که بر روی GymPlatform کار می‌کنند  
> آخرین به‌روزرسانی: ۳ ژوئیه ۲۰۲۶  
> وضعیت: Active — حتمی برای تمام جلسات AI

---

## فهرست مطالب

1. [قوانین اجباری — اولین چیزی که می‌خوانید](#1-قوانین-اجباری--اولین-چیزی-که-می‌خوانید)
2. [لایه‌های Context هوش مصنوعی](#2-لایه‌های-context-هوش-مصنوعی)
3. [ساختار ریپازیتوری](#3-ساختار-ریپازیتوری)
4. [نحوه خواندن وضعیت پروژه](#4-نحوه-خواندن-وضعیت-پروژه)
5. [ocular痪‌ها و الگوهای اجرایی](#5-جریان‌ها-و-الگوهای-اجرایی)
6. [نحوه تعامل با Codebase](#6-نحوه-تعامل-با-codebase)
7. [رنامه‌های استاندارد](#7-الگوهای-استاندارد)
8. [Graph دانش](#8-graph-دانش)
9. [اسناد姣‌بینی AI](#9-ساختار-راهنمای-AI)
10. [مسیر یادگیری سریع](#10-مسیر-یادگیری-سریع)

---

## 1. قوانین اجباری — اولین چیزی که می‌خوانید

### ⚠️ هشدار حیاتی

**قبل از هر کاری**، این فایل را کامل مطالعه کنید: `.ai/agent-rules.md`

قوانین ذیل توسط `.ai/agent-rules.md` تعریف شده و **اجباری** هستند:

### 1.1 اصول اجرایی

- **همیشه قبل از شروع، پروژه context را بخوانید**. فایل‌های `.ai/context/WORKSPACE.md`، `.ai/context/PROJECT_STATE.md` و `.ai/context/IMPLEMENTATION_MASTER_PLAN.md` را **قبل از هر عمل اجرایی** بخوانید.
- **هرگز نیازمندی‌ها را حدس نزنید**. تمام تصمیمات فنی باید با نقشه‌های مستند شده در `.ai/context/` و `docs/` هماهنگ باشند. اگر تصمیم unclear است، **قبل از ادامه، توضیح بگیرید**.
- **هرگز کار کامل شده را بازنویسی نکنید**. پیاده‌سازی موجود که با نیازمندی‌های مستند شده مطابقت دارد بدون تغییر نیازمندی قانونی جایگزین نشود.
- **همیشه از اولین وظیفه ناتمام ادامه دهید**. When resuming work، identify the earliest incomplete work item در Sprint فعلی و به ترتیب ادامه دهید.
- **هرگز فایل‌های موجود را باز这一刻 نباشید**. Before creating new files، جستجوی کامل codebase را انجام دهید.
- **همیشه بعد از پیاده‌سازی، مستندات را آپدیت کنید**. هر وظیفه به پایان رسیده باید در `docs/IMPLEMENTATION_CHANGES.md` و `docs/PROJECT_HANDOFF.md` منعکس شود.
- **همیشه IMPLEMENTATION_CHANGES.md و PROJECT_HANDOFF.md را هماهنگ نگه دارید**. تغییرات مستندات باید در هر دو فایل mirror شوند.

### 1.2 تمیزگیری منابع (بسیار مهم)

**بعد از هر وظیفه سنگین، Sprint، پیاده‌سازی، تست یا اعتبارسنجی**، حتماً:

اجرای فرآیندهای پس‌زمینه‌ای ایجاد شده در طول کار را متوقف کنید:
- Node.js، npm، pnpm، yarn
- Playwright، Chromium، Browser Contexts، Browser Pages
- dotnet watch، ASP.NET Development Server، TestHost
- Background workers، Timers
- DbContextها و HttpClientهای دستیCreated

### 1.3 خط‌مشی Git

وقتی همه موارد زیر موفقیت‌آمیز باشند:
- Build
- Tests
- اعتبارسنجی
- مستندات

اجباریاً اجرا کنید:
```bash
git status
git add .
git commit -m "<Conventional Commit>"
git push origin <current-branch>
```

### 1.4 چک‌لیست پایان وظیفه

هر جلسه پیاده‌سازی باید با گزارش زیر پایان یابد:
- Phase
- Sprint
- وظایف به پایان رسیده
- وظایف باقی مانده
- وضعیت Build
- وضعیت Test
- وضعیت اعتبارسنجی
- وضعیت مستندات
- وضعیت تمیزگیری
- وضعیت Git
- Hash Commit
- وضعیت Push

---

## 2. لایه‌های Context هوش مصنوعی

### 2.1 منبع‌های حتمی

| فایل | هدف | الزام |
|------|------|-------|
| `.ai/agent-rules.md` | **اولین چیزی که می‌خوانید** | **ABSOLUTELY MANDATORY** |
| `.ai/context/WORKSPACE.md` | بافت پروژه و مرزهای سیستم | حتمی |
| `.ai/context/PROJECT_STATE.md` | وضعیت فعلی پیاده‌سازی | حتمی |
| `.ai/context/IMPLEMENTATION_MASTER_PLAN.md` | نقشه راه اجرایی کامل | حتمی |
| `.ai/context/PRODUCT_BLUEPRINT.md` | مشخصات کامل محصول | حتمی برای درک نیازمندی‌ها |
| `docs/PROJECT_HANDOFF.md` | فهرست کامل فایل‌های موجود | حتمی |
| `docs/IMPLEMENTATION_CHANGES.md` | تاریخچه تغییرات پیاده‌سازی | حتمی |

### 2.2 ساختار لایه Context

```
.ai/
├── agent-rules.md                   # ⚠️ FIRST THING — قوانین اجرایی اجباری
├── architecture-rules.md            # قوانین معماری
├── backend-rules.md                 # قوانین Backend
├── coding-standards.md              # استانداردهای کد
├── context/
│   ├── WORKSPACE.md                 # بافت پروژه
│   ├── PROJECT_STATE.md             # وضعیت فعلی
│   ├── AI_MEMORY.md                 # حافظه درون‌جلساتی
│   ├── IMPLEMENTATION_MASTER_PLAN.md # نقشه راه اجرایی
│   ├── PRODUCT_BLUEPRINT.md         # مشخصات محصول
│   ├── FUNCTIONAL_REQUIREMENTS.md   # نیازمندی‌های عملکردی
│   ├── NON_FUNCTIONAL_REQUIREMENTS.md # نیازمندی‌های غیرعملکردی
│   ├── DATABASE_BLUEPRINT.md        # Design پایگاه داده
│   ├── API_BLUEPRINT.md             # Design API
│   ├── UI_UX_BLUEPRINT.md           # Design UI/UX
│   ├── MASTER_ROADMAP.md            # نقشه راه توسعه
│   └── ARCHITECTURE_REVIEW.md       # بررسی معماری
├── database-rules.md
├── frontend-rules.md
├── mobile-rules.md
├── project-rules.md
├── security-rules.md
└── workflow.md
```

### 2.3 مرزهای مجاز و ممنوعه

#### مجاز (توسط .ai/context/)
- درک نیازمندی‌های محصول از `.ai/context/PRODUCT_BLUEPRINT.md`
- بررسی نقشه راه اجرایی از `.ai/context/IMPLEMENTATION_MASTER_PLAN.md`
- خواندن وضعیت فعلی پیاده‌سازی از `PROJECT_STATE.md`
- استفاده از `.ai/context/ARCHITECTURE_REVIEW.md` برای درک تصمیمات معماری

#### ممنوع (توسط .ai/agent-rules.md)
- ایجاد فایل‌های مستندات جدید بدون جستجوی موجود
- تغییر business logic موجود
- تغییر architecture بدون نیازمندی قانونی

---

## 3. ساختار ریپازیتوری

### فایل‌های موجود و وضعیت

```
GymPlatform/
├── README.md                        # ⚠️ نیاز به آپدیت (outdated tech stack)
├── CONTRIBUTING.md                  # ⚠️ نیاز به بخش onboarding اجباری
├── SECURITY.md                      # ❌ وجود ندارد — باید ایجاد شود
├── AGENTS.md                        # ✅ موجود (کوتاه)
├── .gitignore                       # ✅ موجود
│
├── .ai/                             # لایه هوش مصنوعی (context اصلی AI‌ها)
│   ├── agent-rules.md               # ⚠️ حتما بخوانید
│   └── context/                     # اسناد مورد نیاز AI‌ها
│       ├── WORKSPACE.md             # بافت پروژه
│       ├── PROJECT_STATE.md         # وضعیت فعلی
│       ├── IMPLEMENTATION_MASTER_PLAN.md # نقشه راه
│       └── [11 context files]       # اسناد تکمیلی
│
├── .github/
│   └── ISSUE_TEMPLATE/              # ⚠️ خالی — نیاز به PR template
│
├── docs/                            # اسناد عمومی پروژه
│   ├── PROJECT_GUIDE_FA.md          # ✅ راهنمای کامل فارسی
│   ├── PROJECT_HANDOFF.md           # ✅ تحویل پروژه
│   ├── IMPLEMENTATION_CHANGES.md    # ✅ تغییرات پیاده‌سازی
│   ├── CHANGELOG.md                 # ⚠️ نیاز به آپدیت
│   ├── VISION.md                    # ✅ چشم‌انداز محصول
│   ├── MASTER_PRD.md                # ⚠️ پیش‌نویس، نیاز به آپدیت
│   ├── BUSINESS_RULES.md            # ⚠️ پیش‌نویس، نیاز به آپدیت
│   ├── USER_ROLES.md                # ⚠️ پیش‌نویس، نیاز به آپدیت
│   ├── UI_UX.md                     # ⚠️ پیش‌نویس، نیاز به آپدیت
│   ├── ARCHITECTURE.md              # ⚠️ پیش‌نویس، نیاز به آپدیت
│   ├── API_DESIGN.md                # ⚠️ پیش‌نویس، نیاز به آپدیت
│   ├── DATABASE.md                  # ⚠️ پیش‌نویس، نیاز به آپدیت
│   ├── ROADMAP.md                   # ⚠️ پیش‌نویس، نیاز به آپدیت
│   ├── backend/BACKEND_GUIDE_FA.md  # ✅ راهنمای بک‌اند
│   ├── frontend/                    # ⚠️ خالی — نیاز به راهنما
│   └── ai/                          # 📁 دایرکتوری جدید (شامل AI_DEVELOPER_GUIDE_FA.md)
│
├── adr/                             # تصمیمات معماری
├── GymPlatform.sln                  # 🔑 Solution file
├── GymPlatform.SharedKernel/        # هسته مشترک
├── GymPlatform.Infrastructure/      # زیرساخت (EF Core + Repositories)
├── GymPlatform.Api/                 # Composition Root
├── GymPlatform.Modules.Membership/  # ماژول عضویت ✅ کامل
├── GymPlatform.Modules.Training/    # ماژول تمرینات ✅ کامل
├── GymPlatform.Modules.Financial/   # ماژول مالی (ساختار)
├── GymPlatform.Modules.Communication/ # ماژول ارتباطات 🔄 در حال توسعه
├── GymPlatform.Modules.Membership.Tests/    # تست‌های واحد Membership
└── GymPlatform.Modules.Communication.Tests/ # تست‌های واحد Communication
```

---

## 4. نحوه خواندن وضعیت پروژه

### 4.1 برنامه‌های اول (کمتر از ۱ ساعت)

1. **خواندن حتمی: `.ai/agent-rules.md`**
   - این فایل قوانین دائمی، تمیزگیری اجباری و خط‌مشی git را تعریف می‌کند.
   - **هرگز این فایل را نادیده نگیرید.**

2. **خواندن بافت: `PROJECT_STATE.md`**
   - وضعیت فعلی هر ماژول
   - چه چیز ساخته شده، چه چیزی نه
   - معیم‌های شناخته شده
   - مرحله بعدی الزامی

3. **بررسی Handoff: `PROJECT_HANDOFF.md`**
   - فهرست کامل فایل‌های موجود
   - بدهی فنی باقی مانده
   - وظیفه بعدی پیشنهادی

### 4.2 برنامه‌های متوسط (۱-۴ ساعت)

4. **خواندن نقشه راه: `IMPLEMENTATION_MASTER_PLAN.md`**
   - فاز فعلی و وظایف آن
   - نگاشت ماژول به پروژه
   - تصمیمات معماری مهم
   - معیارهای موفقیت

5. **خواندن bisogno محصول: `PRODUCT_BLUEPRINT.md`**
   - ۲۱ ماژول و شرح هر کدام
   - ۶ نقش کاربری
   - MVP definition
   - User journeys

### 4.3 برنامه‌های پیچیده (۴+ ساعت)

6. **مطالعه اسناد تخصصی:**
   - `.ai/context/FUNCTIONAL_REQUIREMENTS.md` — داستان‌های کاربری
   - `.ai/context/DATABASE_BLUEPRINT.md` — مدل داده
   - `.ai/context/API_BLUEPRINT.md` — طراحی API
   - `.ai/context/API_BLUEPRINT.md` — استانداردهای API
   - `.ai/context/NON_FUNCTIONAL_REQUIREMENTS.md` —targetهای کیفیت

7. **بررسی کد موجود:**
   - `GymPlatform.Modules.Membership/` — پیاده‌سازی کامل
   - `GymPlatform.Modules.Training/` — پیاده‌سازی کامل
   - `GymPlatform.Api/Program.cs` — composition root

---

## 5. جریان‌ها و الگوهای اجرایی

### 5.1 جریان پیاده‌سازی یک وظیفه

```
1. خواندن نیازمندی از ماژول مربوطه
   └─▶ .ai/context/IMPLEMENTATION_MASTER_PLAN.md (which phase?)
   └─▶ .ai/context/PRODUCT_BLUEPRINT.md (what does this module do?)

2. بررسی وضعیت فعلی
   └─▶ PROJECT_HANDOFF.md (has this file been created?)
   └─▶ IMPLEMENTATION_CHANGES.md (what's the pattern?)

3. پیاده‌سازی بر اساس Clean Architecture
   └─▶ Domain (Entity, VO, Events) ← هیچ وابستگی خارجی
   └─▶ Application (Command, Validator, Handler) ← فقط Domain
   └─▶ Infrastructure (Repository, DbContext) ← Domain + Application
   └─▶ Api (Endpoint registration) ← همه بالا

4. تست‌نویسی
   └─▶ Unit tests با xUnit + Moq
   └─▶ Target: >80% coverage

5. به‌روزرسانی مستندات
   └─▶ docs/IMPLEMENTATION_CHANGES.md
   └─▶ docs/PROJECT_HANDOFF.md

6. Commit و Push
   └─▶ Conventional Commits
```

### 5.2 الگوی پیاده‌سازی Command (CIQRS)

```csharp
// 1. Command (Record in Application)
internal sealed record CreateGymCommand(string Name, string? Phone) 
    : ICommand<GymResponse>;

// 2. Validator (Application)
internal sealed class CreateGymCommandValidator 
    : ICommandValidator<CreateGymCommand>
{
    public Result Validate(CreateGymCommand command)
    {
        // Format validation only — no business rules
        if (string.IsNullOrWhiteSpace(command.Name))
            return Result.Failure("نام الزامی است.");
        if (command.Name.Length > 200)
            return Result.Failure("نام حداکثر ۲۰۰ کاراکتر.");
        return Result.Success();
    }
}

// 3. Handler (Application)
internal sealed class CreateGymCommandHandler
    : ICommandHandler<CreateGymCommand, GymResponse>
{
    public async Task<Result<GymResponse>> HandleAsync(
        CreateGymCommand command, CancellationToken ct)
    {
        // Validate
        var validation = _validator.Validate(command);
        if (validation.IsFailure) return Result<GymResponse>.Failure(validation.Error);

        // Business logic in Domain
        var gym = Gym.Create(command.Name, command.Phone);
        
        // Persist
        await _gymRepository.AddAsync(gym, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        // Response
        return Result<GymResponse>.Success(new(...));
    }
}
```

### 5.3 الگوی اجازه‌نامه Multi-Tenant

```csharp
// هر Entity حتماً TenantId دارد
public sealed class Member : BaseEntity
{
    public Guid TenantId { get; private set; }
    
    // EF Core Global Query Filter
    // MembershipDbContext.OnModelCreating:
    // builder.Entity<Member>().HasQueryFilter(m => m.TenantId == _currentTenantId);
}

// Tenant از JWT استخراج می‌شود
public interface ICurrentUserService
{
    Guid? TenantId { get; }  // از claim «tenant» در JWT
}
```

---

## 6. نحوه تعامل با Codebase

### 6.1 قوانین جستجو

**قبل از ایجاد هر فایل جدید**، این grepها را اجرا کنید:

```bash
# آیا فایل از قبل وجود دارد؟
rg "ClassName" --type cs
rg "feature-name" --type cs

# آیا interface مربوطه تعریف شده؟
rg "IUserRepository" --type cs

# الگوی پیاده‌سازی مشابه چیست؟
rg "class .*Repository : I.*Repository" --type cs
```

### 6.2 جستجوی pattern

```bash
# پیدا کردن تمام Commandها
rg "ICommandHandler<" --type cs

# پیدا کردن تمام Entityهای ماژول
rg "class .* : BaseEntity" --type cs

# پیدا کردن تمام Domain Events
rg "IDomainEvent" --type cs

# پیدا کردن Migrationها
rg "Migration.*: Migration" --type cs
```

### 6.3 خواندن کد موجود

**Membership Module** (پیاده‌سازی کامل، الگو مرجع):
```
GymPlatform.Modules.Membership/
├── Domain/
│   ├── Entities/Gym.cs, Member.cs, Coach.cs
│   ├── ValueObjects/Email.cs, Phone.cs
│   ├── Events/*.cs
│   └── Repositories/IGymRepository.cs
├── Application/
│   ├── Commands/CreateGym/
│   │   ├── CreateGymCommand.cs
│   │   ├── CreateGymCommandValidator.cs
│   │   └── CreateGymCommandHandler.cs
│   └── DTOs/*.cs
```

**Training Module** (پیاده‌سازی کامل):
```
GymPlatform.Modules.Training/
├── Domain/
│   ├── Entities/ (Exercise, WorkoutProgram, WorkoutLog, ...)
│   ├── Enums/ (DifficultyLevel, ExerciseCategory, MeasurementType)
│   ├── Events/ (8 events)
│   └── Repositories/ (7 interfaces)
├── Application/
│   ├── Commands/CreateExercise/
│   ├── Commands/CreateWorkoutProgram/
│   └── DTOs/
```

---

## 7. الگوهای استاندارد

### 7.1 ساختار یک Module

```
GymPlatform.Modules.<ModuleName>/
├── <ModuleName>.csproj
├── Domain/
│   ├── Entities/
│   │   └── <Entity>.cs              # Aggregate Root
│   ├── ValueObjects/
│   │   └── <VO>.cs
│   ├── Enums/
│   │   └── <Enum>.cs
│   ├── Events/
│   │   └── <EventName>.cs
│   ├── Exceptions/
│   │   └── <Module>DomainException.cs
│   └── Repositories/
│       └── I<Entity>Repository.cs
├── Application/
│   ├── Interfaces/
│   │   ├── ICommand.cs
│   │   ├── ICommandHandler.cs
│   │   └── ICommandValidator.cs
│   ├── DTOs/
│   │   ├── <Entity>DTOs.cs
│   │   └── <Response>DTOs.cs
│   └── Commands/
│       └── <ActionName>/
│           ├── <Action>Command.cs
│           ├── <Action>CommandValidator.cs
│           └── <Action>CommandHandler.cs
```

### 7.2 Entity她已经ها

```csharp
// هر Entity:
// - sealed
// - از BaseEntity ارث می‌برد
// - Constructor خصوصی برای EF Core
// - منطق تجاری در constructor و public methods
// - DomainEvents را با AddDomainEvent() مدیریت می‌کند

public sealed class Gym : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string? Phone { get; private set; }
    public Guid TenantId { get; private set; }
    public bool IsActive { get; private set; }
    
    // EF Core constructor
    private Gym() { }
    
    // Domain constructor
    public Gym(string name, string? phone, Guid tenantId)
    {
        Name = name.Trim();
        Phone = phone?.Trim();
        TenantId = tenantId;
        IsActive = true;
        CreatedAt = DateTimeOffset.UtcNow;
        
        AddDomainEvent(new GymCreated(Id, Name));
    }
    
    public void Deactivate()
    {
        if (!IsActive) 
            throw new MembershipDomainException("باشگاه فعال نیست.");
        IsActive = false;
        AddDomainEvent(new GymDeactivated(Id));
    }
}
```

### 7.3 naming هcx

| الگو | Convention | مثال |
|------|-----------|------|
| Module | PascalCase | `GymPlatform.Modules.Membership` |
| Entity | PascalCase (singular) | `Member`, `Gym`, `Coach` |
| Repository Interface | I + Entity + Repository | `IMemberRepository` |
| Command | Action + Entity + Command | `RegisterMemberCommand` |
| Handler | Action + Entity + Handler | `RegisterMemberCommandHandler` |
| Validator | Action + Entity + Validator | `RegisterMemberCommandValidator` |
| DTO | Entity + Request/Response | `RegisterMemberRequest` |
| Domain Event | Past Tense | `MemberRegistered`, `GymCreated` |
| Value Object | Descriptive | `Email`, `Phone`, `Equipment` |
| Interface | I + Descriptive | `ICommand`, `ICommandHandler` |

---

## 8. Graph دانش

### ساختار گراف دانش پروژه

```
GymPlatform (پلتفرم)
    ├── CONTAINS Module Membership
    │       ├── HAS Entity Gym
    │       │   ├── HAS ValueObject Email
    │       │   ├── HAS DomainEvent GymCreated
    │       │   └── IMPLEMENTS INTERFACE IGymRepository
    │       ├── HAS Entity Member
    │       │   ├── HAS ValueObject Email
    │       │   └── HAS DomainEvent MemberRegistered
    │       └── HAS Commands (4)
    ├── CONTAINS Module Training
    │       ├── HAS Entity Exercise
    │       ├── HAS Entity WorkoutProgram
    │       └── HAS Commands (7)
    ├── CONTAINS Module Communication
    │       ├── HAS Entity Session
    │       ├── HAS Entity Booking
    │       └── HAS ICommunicationUnitOfWork
    └── HAS API Endpoints (17 total)
```

### نحوه به‌روزرسانی گراف دانش

```json
{
  "entities": [
    {
      "name": "GymPlatform.Modules.Membership.Domain.Entities.Member",
      "type": "Entity",
      "module": "Membership",
      "properties": ["Id", "FullName", "Email", "Phone", "GymId", "Status"],
      "events": ["MemberRegistered"],
      "repository": "IMemberRepository"
    },
    {
      "name": "MemberRegistered",
      "type": "DomainEvent",
      "module": "Membership",
      "data": ["MemberId", "GymId", "Email"]
    }
  ],
  "modules": ["Membership", "Training", "Communication", "Financial"],
  "api_endpoints": [
    { "method": "POST", "path": "/api/rooms", "module": "Communication" },
    { "method": "POST", "path": "/api/sessions", "module": "Communication" },
    { "method": "POST", "path": "/api/exercises", "module": "Training" }
  ]
}
```

---

## 9. ساختار راهنمای AI

### نحوه پاسخ به سوالات کاربر

```
1. ابتداتمام context فایل‌های مربوطه را بخوانید
2. از Graph دانش برای ارائه context استفاده کنید
3. پاسخ را بر اساس نیازمندی‌های مستند شده ارائه دهید
4. اگر نیازمندی ناقص است، به صراحت بگویید چه کmissing است
5. هرگز حدس نزنید، evererb
6. پیشنهاد دهید از哪裡 مستندات بیشتری بخوانند
```

### نحوه پیاده‌سازی

```
1. وضعیت فعلی را از PROJECT_HANDOFF.md بخوانید
۲. نیازمندی را از IMPLEMENTATION_MASTER_PLAN.md Warriors کنید
3. الگو را از ماژول‌های موجود (Membership, Training, Communication) پیدا کنید
4. پیاده‌سازی را طبق الگوی اجرایی انجام دهید
5. Happya و花果 را به‌روزرسانی کنید
6. تست‌ها را اجرا کنید، خروجی را grip کنید
7. Git commit و push
```

### اسناد姣‌بینی AI

| موقعیت | اقدام |
|--------|-------|
| نیازمندی ناقص | به کاربر بگویید چه فایلیが必要 است |
| تعارض در مستندات | IMPLEMENTATION_MASTER_PLAN.md به عنوان منبع قطعی |
| تصمیم فنی نیازمند | از کاربر confirmation بگیرید |
| پیاده‌سازی پیچیده | در `ai/AI_DEVELOPER_GUIDE_FA.md` ثبت کنید |

---

## 10. مسیر یادگیری سریع

برای فهم بهتر codebase، این ترتیب پیشنهاد می‌شود:

### هفته اول
1. **روز ۱**: خواندن `.ai/agent-rules.md` + `WORKSPACE.md` + `PROJECT_STATE.md`
2. **روز ۲**: بررسی `GymPlatform.Modules.Membership/Domain/` (همه Entities و ValueObjects)
3. **روز ۳**: بررسی `GymPlatform.Modules.Membership/Application/` (یک Command کامل از Interface تا Handler)
4. **روز ۴**: بررسی `GymPlatform.Infrastructure/` (DbContext، Repository implementations، DI)
5. **روز ۵**: بررسی `GymPlatform.Api/Program.cs` (Minimal API endpoints)

### هفته دوم
6. **روز ۶-۷**: بررسی `GymPlatform.Modules.Training/` (پارال∎ël با Membership)
7. **روز ۸-۹**: بررسی `GymPlatform.Modules.Communication/` (Calendar domain)
8. **روز ۱۰**: خواندن `.ai/context/IMPLEMENTATION_MASTER_PLAN.md` — نقشه راه کامل

### هفته سوم
9. **روز ۱۱-۱۲**: مطالعه `.ai/context/API_BLUEPRINT.md` و `.ai/context/DATABASE_BLUEPRINT.md`
10. **روز ۱۳-۱۴**: مطالعه `.ai/context/NON_FUNCTIONAL_REQUIREMENTS.md` و `.ai/context/FUNCTIONAL_REQUIREMENTS.md`
11. **روز ۱۵**: آماده‌سازی برای وظایف بعدی

---

## منابع اضافی

| سند | توضیحات |
|------|---------|
| `.ai/agent-rules.md` | قوانین اجرایی اجباری |
| `docs/PROJECT_GUIDE_FA.md` | راهنمای کامل پروژه به فارسی |
| `docs/backend/BACKEND_GUIDE_FA.md` | راهنمای توسعه Backend |
| `docs/PROJECT_HANDOFF.md` | وضعیت فعلی پروژه |
| `.ai/context/IMPLEMENTATION_MASTER_PLAN.md` | نقشه راه اجرایی |
| `AGENTS.md` | پیکربندی عامل |

---

*این راهنما بر اساس قوانین `.ai/agent-rules.md` و وضعیت پروژه به تاریخ ۳ ژوئیه ۲۰۲۶ تهیه شده است. این سند برای عوامل هوش مصنوعی الزامی است.*
