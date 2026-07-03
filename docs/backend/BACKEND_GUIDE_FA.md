# Backend Developer Guide — GymPlatform  
# راهنمای توسعه‌دهنده Back-end GymPlatform

> **English (Primary Reference)**: This document is the authoritative backend development guide for GymPlatform. It covers Clean Architecture, CQRS pattern, Repository pattern, Domain Events, Multi-Tenant isolation, Validation layers, DI registration rules, testing strategy, and 15+ common mistakes with solutions. Includes a full end-to-end command implementation example.  
> **فارسی**: این سند راهنمای قطعی توسعه Backend GymPlatform است. شامل معماری خالص، الگوی CQRS، الگوی Repository، رویدادهای دامنه، انزوای چندمستاجر، لایه‌های اعتبارسنجی، قوانین ثبت DI، استراتژی تست و بیش از ۱۵ اشتباه متداول با راه‌حل. شامل مثال کامل پیاده‌سازی Command از ابتدا تا انتها.

---

## English Table of Contents

1. [About This Guide](#1-درباره-این-راهنما)
2. [Backend Architecture Overview](#2-معماری-کلی-بک‌اند)
3. [Clean Architecture](#3-معماری-خالص-clean-architecture)
4. [CQRS Pattern](#4-الگوی-cqrs)
5. [SharedKernel](#5-sharedkernel)
6. [Repository Pattern](#6-الگوی-repository)
7. [Domain Events](#7-رویدادهای-دامنه-domain-events)
8. [Multi-Tenant](#8-چندمستاجری-multi-tenant)
9. [Validation](#9-اعتبارسنجی-validation)
10. [Dependency Injection](#10-تزریق-وابستگی-dependency-injection)
11. [Development Rules](#11-قوانین-توسعه)
12. [Best Practices](#12-بهترین-شیوه‌ها)
13. [Patterns and Common Code](#13-الگوها-و-کدهای-رایج)
14. [Common Mistakes](#14-اشتباهات-متداول)
15. [Learning Path for New Developers](#15-مسیر-یادگیری-توسعه‌دهنده-جدید)

---

# راهنمای توسعه‌دهنده Back-end GymPlatform

> راهنمای کامل برای توسعه‌دهندگانی که نیاز به درک کامل معماری، الگوها و قراردادهای پروژه دارند.  
> آخرین به‌روزرسانی: ۲۸ ژوئن ۲۰۲۶  

---

## فهرست مطالب (فارسی)

1. [درباره این راهنما](#1-درباره-این-راهنما)
2. [معماری کلی بک‌اند](#2-معماری-کلی-بک‌اند)
3. [معماری خالص (Clean Architecture)](#3-معماری-خالص-clean-architecture)
4. [الگوی CQRS](#4-الگوی-cqrs)
5. [SharedKernel](#5-sharedkernel)
6. [الگوی Repository](#6-الگوی-repository)
7. [رویدادهای دامنه (Domain Events)](#7-رویدادهای-دامنه-domain-events)
8. [چندمستاجری (Multi-Tenant)](#8-چندمستاجری-multi-tenant)
9. [اعتبارسنجی (Validation)](#9-اعتبارسنجی-validation)
10. [تزریق وابستگی (Dependency Injection)](#10-تزریق-وابستگی-dependency-injection)
11. [قوانین توسعه](#11-قوانین-توسعه)
12. [بهترین شیوه‌ها](#12-بهترین-شیوه‌ها)
13. [الگوها و کدهای رایج](#13-الگوها-و-کدهای-رایج)
14. [اشتباهات متداول](#14-اشتباهات-متداول)
15. [مسیر یادگیری توسعه‌دهنده جدید](#15-مسیر-یادگیری-توسعه‌دهنده-جدید)

---

## 1. درباره این راهنما

این سند برای توسعه‌دهندگانی است که می‌خواهند کد بک‌اند GymPlatform را بنویسند، بررسی کنند یا گسترش دهند. خواندن پیش‌نیازهای زیر قبل از خواندن این راهنما الزامی است:

- `docs/PROJECT_GUIDE_FA.md` — مقدمه کلی پروژه
- `docs/ARCHITECTURE.md` — معماری سیستم
- `.ai/context/IMPLEMENTATION_MASTER_PLAN.md` — نقشه راه اجرایی

**توجه**: این راهنما توضیح می‌دهد که چگونه باید پیاده‌سازی کرد، نه چه ویژگی‌هایی را در حال حاضر پیاده‌سازی کنید. برای وضعیت فعلی هر ماژول، `/PROJECT_STATE.md` را بررسی کنید.

---

## 2. معماری کلی بک‌اند

### ساختار حلقه‌ای

بک‌اند GymPlatform از ترکیب **Modular Monolith** و **Clean Architecture** پیروی می‌کند:

```
┌──────────────────────────────────────────────────────────┐
│   GymPlatform.Api (Composition Root)                       │
│   ┌────────────────────────────────────────────────────┐ │
│   │ Middleware (Exception, Auth, Tenant Resolution)     │ │
│   │ Minimal API Endpoints (MapPost, MapGet, MapPatch)   │ │
│   │ [DI Registration: Handlers, Validators, Abstractions]│ │
│   └──────────────────────┬─────────────────────────────┘ │
├──────────────────────────┼───────────────────────────────┤
│   Application Layer       │                              │
│   ┌────────────────────┐  │   ┌────────────────────────┐ │
│   │ Commands           │  │   │ Queries                │ │
│   │ Validators         │──┼──▶│ Handlers               │ │
│   │ DTOs               │  │   │ Result<T>              │ │
│   └────────────────────┘  │   └────────────────────────┘ │
├──────────────────────────────────────────────────────────┤
│   Domain Layer                                             │
│   ┌────────────────────────────────────────────────────┐ │
│   │ Entities          ← کلید تجاری                      │ │
│   │ ValueObjects      ← اشیاء بدون Identity               │ │
│   │ Enums             ← انواع شمارشی                     │ │
│   │ DomainEvents      ← رویدادهای داخلی                   │ │
│   │ Repository接口  ← قراردادهای دسترسی داده               │ │
│   │ DomainExceptions  ← خطاهای اختصاصی دامنه              │ │
│   └────────────────────────────────────────────────────┘ │
│        ^          ^          ^           ^                │
│        │          │          │           │                │
│   Application    App       App          App              │
│   Depends On ──────── DEPENDS ON ONLY (no external deps) │
├──────────────────────────────────────────────────────────┤
│   Infrastructure Layer                                      │
│   ┌────────────────────────────────────────────────────┐ │
│   │ DbContexts         ← EF Core، Migrationها           │ │
│   │ RepositoryImpl     ← پیاده‌سازی Repositoryها        │ │
│   │ EF Config          ← Fluent API                       │ │
│   │ External Services  ← Email، SMS، Storage             │ │
│   [depends on Domain + Application abstractions]         │ │
│   └────────────────────────────────────────────────────┘ │
└──────────────────────────────────────────────────────────┘
```

### تعاریف لایه‌ها

| لایه | مسئولیت | وابستگی‌ها |
|------|---------|------------|
| **SharedKernel** | انواع مشترک بین همه ماژول‌ها | هیچ |
| **Domain** | منطق تجاری اصلی | فقط SharedKernel |
| **Application** | orchestration، Use Cases | Domain فقط |
| **Infrastructure** | پیاده‌سازی واقعی | Domain + Application |
| **Api** | نقطه ورود، orchestration نهایی | همه بالا |

### چرخه حیات یک درخواست

```
HTTP درخواست
    └─▶ GlobalExceptionMiddleware (error handling)
         └─▶ Minimal API Endpoint
              └─▶ Validator.Validate(command)  ← Application
                   └─ [اگر ناموفق] → ۴۰۰ ProblemDetails
                   └─ [اگر موفق] ↓
              └─▶ Handler.HandleAsync(command)  ← Application
                   └─▶ Entity.Create(...)        ← Domain
                   └─▶ Repository.Add(entity)    ← Infrastructure
                   └─▶ UnitOfWork.SaveChanges()   ← Infrastructure
                   └─▶ DomainEvents raised
              └─▶ Result<UserResponse> returned
HTTP پاسخ
```

---

## 3. معماری خالص (Clean Architecture)

### اصل Dependency Inversion در GymPlatform

```
                    Depends On
                    ↓
Application ──────────────────▶ Domain
                    ↑
Infrastructure ────────────────▶ Domain
                    ↑
Api (Composition Root) ──────────▶ Application + Infrastructure
```

**قانون غیرقابل نقض**: هیچ کد در لایه Domain به هیچ چیزی خارج از Domain وابسته نباید باشد.

### مثال درک لایه‌ها

#### ✅ درست: Business Logic در Domain

```csharp
// Domain/Entities/Member.cs
public sealed class Member : BaseEntity
{
    public Member(Guid gymId, string fullName, Email email, bool gymIsActive)
    {
        EnsureGymId(gymId);
        EnsureGymIsActive(gymIsActive);
        // ...validation in constructor
        AddDomainEvent(new MemberRegistered(Id, GymId, Email.Value));
    }
    // ...
}
```

❌ نادرست: Business Logic در Application

```csharp
// Application/Handlers/... - باید منطق تجاری در اینجا نباشد
// ✅ Handler فقط orchestrate می‌کند:
var member = new Member(gymId, fullName, email, gymIsActive);
await _memberRepository.AddAsync(member, ct);
```

### تعیین مرز ماژول از طریق Namespace

```csharp
// Membership
using GymPlatform.Modules.Membership.Domain;
using GymPlatform.Modules.Membership.Application;

// Training
using GymPlatform.Modules.Training.Domain;
using GymPlatform.Modules.Training.Application;
```

---

## 4. الگوی CQRS

### تفاوت Command و Query

| جنبه | Command | Query |
|------|---------|-------|
| هدف | تغییر وضعیت سیستم | خواندن داده |
| خروجی | `Result<T>` (موفقیت/شکست) | داده مطلق |
| تست‌پذیری | تست حالت موفق و ناموفق | تست داده بازگشتی |
| مثال | RegisterMemberCommand | GetGymByIdQuery |

### ساختار استاندارد Command

```csharp
// Application/Commands/RegisterMember/RegisterMemberCommand.cs
internal sealed record RegisterMemberCommand(
    Guid GymId,
    string FullName,
    string Email,
    string? Phone
) : ICommand<MemberResponse>;

// Application/Commands/RegisterMember/RegisterMemberCommandValidator.cs
internal sealed class RegisterMemberCommandValidator
    : ICommandValidator<RegisterMemberCommand>
{
    public Result Validate(RegisterMemberCommand command)
    {
        // ✅ فقط اعتبارسنجی فرمت (string.IsNullOrEmpty, regex, etc.)
        // ❌ اعتبارسنجی تجاری (business rules) - این در Domain انجام می‌شود
    }
}

// Application/Commands/RegisterMember/RegisterMemberCommandHandler.cs
internal sealed class RegisterMemberCommandHandler
    : ICommandHandler<RegisterMemberCommand, MemberResponse>
{
    public async Task<Result<MemberResponse>> HandleAsync(
        RegisterMemberCommand command, CancellationToken ct)
    {
        // 1. اعتبارسنجی اولیه
        var validationResult = _validator.Validate(command);
        if (validationResult.IsFailure)
            return Result<MemberResponse>.Failure(validationResult.Error);

        // 2. بارگذاری تزاک (context from repositories)
        var gym = await _gymRepository.GetByIdAsync(command.GymId, ct);
        if (gym is null)
            return Result<MemberResponse>.Failure("باشگاه یافت نشد.");

        // 3. ایجاد Entity ( منطق تجاری در Domain)
        var emailResult = Email.Create(command.Email);
        if (emailResult.IsFailure)
            return Result<MemberResponse>.Failure(emailResult.Error);

        var member = new Member(command.GymId, command.FullName, emailResult.Value, gym.IsActive);

        // 4. ذخیره
        await _memberRepository.AddAsync(member, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        // 5. بازگشت پاسخ
        return Result<MemberResponse>.Success(
            new MemberResponse(member.Id, member.FullName, member.Email.Value, ...));
    }
}
```

### مرتب‌سازی فایل‌های Command

```
Application/Commands/CreateGym/
├── CreateGymCommand.cs           ← Command object (record)
├── CreateGymCommandValidator.cs  ← Validator
├── CreateGymCommandHandler.cs    ← Handler (orchestrator)
└── CreateGymCommandHandlerTests.cs ← Unit test (در پروژه .Tests)
```

---

## 5. SharedKernel

### نقش SharedKernel

SharedKernel جایی است که انواع مشترک بین تمام ماژول‌ها قرار می‌گیرند. **تغییر در SharedKernel نیازمند آپدیت تمام ماژول‌های مصرف‌کننده در یک PR است.**

### ساختار SharedKernel

```
GymPlatform.SharedKernel/
├── BaseEntity.cs                   ← پایه همه Entities، شامل ID و DomainEvents
├── Result.cs                       ← Result<T> type برای موفقیت/شکست
├── DomainEventBase.cs              ← پیاده‌سازی پیش‌فرض IDomainEvent
├── IDomainEvent.cs                 ← رابط رویدادهای دامنه
├── IUnitOfWork.cs                  ← رابط واحد کار
├── Exceptions/
│   └── DomainException.cs          ← Exception پایه دامنه
└── Abstractions/
    ├── IDateTimeProvider.cs        ← ارائه‌دهنده زمان (تست‌پذیر)
    ├── ICurrentUserService.cs      ← اطلاعات کاربر و Tenant فعلی
    ├── IAuditService.cs            ← اطلاعات ایجاد/تغییر
    └── Pagination.cs               ← انواع صفحه‌بندی
```

### Result<T> — سیستم بازگشت

```csharp
// استفاده
var emailResult = Email.Create("invalid");

if (emailResult.IsFailure)
    return Result<MemberResponse>.Failure(emailResult.Error);

return Result<MemberResponse>.Success(response);
```

**قانون**: هیچ Exception در لایه Application برای کنترل جریانpherds سود را پرتاب نکنید. از `Result<T>` استفاده کنید.

### IUnitOfWork — مدیریت تراکنش

```csharp
// تعریف
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

// استفاده - هر Module有其专属的UnitOfWork接口
public interface ICommunicationUnitOfWork : IUnitOfWork { }

// در InfrastructureServiceCollectionExtensions:
services.AddScoped<IUnitOfWork>(sp =>
    sp.GetRequiredService<MembershipDbContext>());
services.AddScoped<ICommunicationUnitOfWork>(sp =>
    sp.GetRequiredService<CommunicationDbContext>());
```

---

## 6. الگوی Repository

### رابطه Repository و Domain

```
Application Layer                     Infrastructure Layer
     │                                       │
     │  uses IMemberRepository               │  implements MemberRepository
     │  (abstraction in Domain)              │  (concrete in Infrastructure)
     │                                       │
     ├── IMemberRepository ────────────────▶ MemberRepository
     │      ↓ (contract)                     │      ↓ (concrete)
     │                                       │    → EF Core DbContext
     ├── ExerciseRepository ───────────────▶ ExerciseRepository
     ├── CoachProfileRepository ───────────▶ CoachProfileRepository
     └── ...                                └── ...
```

### تعریف Repository interface (در Domain)

```csharp
// Domain/Repositories/IMemberRepository.cs
namespace GymPlatform.Modules.Membership.Domain.Repositories;

public interface IMemberRepository
{
    Task<Member?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<Member?> GetByEmailAsync(string email, CancellationToken ct = default);
    Task<IReadOnlyList<Member>> GetByGymIdAsync(Guid gymId, CancellationToken ct = default);
    Task AddAsync(Member member, CancellationToken ct = default);
    void Update(Member member);
    void Delete(Member member);
}
```

**قوانین تعریف Repository**:
1. Repository فقط برای Aggregate Root تعریف می‌شود (جایگاه سطح Member نه MemberStatus)
2. فقط عملیات مورد نیاز در Application در Repository قرار می‌گیرد (حتی GET ALL)
3. نام Repository: `<EntityName>Repository`
4. قرارداد نام روش‌ها: `GetByIdAsync`، `GetBy<Filter>Async`، `AddAsync`، `GetListAsync`

### پیاده‌سازی Repository (در Infrastructure)

```csharp
// Infrastructure/Persistence/Repositories/MemberRepository.cs
internal sealed class MemberRepository : IMemberRepository
{
    private readonly MembershipDbContext _context;

    public MemberRepository(MembershipDbContext context) => _context = context;

    public async Task<Member?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await _context.Members.FirstOrDefaultAsync(m => m.Id == id, ct);

    public async Task<IReadOnlyList<Member>> GetByGymIdAsync(Guid gymId, CancellationToken ct = default)
        => await _context.Members
            .Where(m => m.GymId == gymId)
            .ToListAsync(ct);

    public void Add(Member member) => _context.Members.Add(member);
    // ...
}
```

### ماژول‌های دارای Repository

| ماژول | Repositoryهای فعال |
|--------|-------------------|
| Membership | GymRepository، MemberRepository، CoachRepository |
| Training | ExerciseRepository، WorkoutProgramRepository، WorkoutLogRepository، ExerciseVideoRepository، BodyMeasurementRepository، ProgressPhotoRepository، CoachProfileRepository |
| Communication | RoomRepository، SessionRepository، BookingRepository، CoachAvailabilityRepository |

---

## 7. رویدادهای دامنه (Domain Events)

###なぜ Domain Events؟

برای هماهنگی بین بخش‌های مختلف سیستم بدون ایجاد وابستگی مستقیم. به عنوان مثال، وقتی عضوی ثبت می‌شود، رویداد `MemberRegistered` صادر می‌شود.

### ساختار رویداد دامنه

```csharp
// Domain/Events/MemberRegistered.cs
internal sealed record MemberRegistered(Guid MemberId, Guid GymId, string Email)
    : IDomainEvent;

// پخش رویداد (در Entity)
public sealed class Member : BaseEntity
{
    public Member(...)
    {
        // ...
        AddDomainEvent(new MemberRegistered(Id, GymId, Email.Value));
    }
}
```

### IDomainEvent Interface

```csharp
// SharedKernel/IDomainEvent.cs
public interface IDomainEvent;

// BaseEntity مدیریت رویدادها را ارائه می‌دهد:
public abstract class BaseEntity
{
    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
    public void ClearDomainEvents() => _domainEvents.Clear();
}
```

### رویدادهای تعریف شده در هر ماژول

**Membership**: `GymCreated`، `GymActivated`، `GymDeactivated`، `MemberRegistered`، `CoachAssigned`

**Training**: `ExerciseCreated`، `WorkoutProgramCreated`، `WorkoutLogCreated`، `ExerciseVideoUploaded`، `BodyMeasurementRecorded`، `ProgressPhotoUploaded`، `CoachProfileUpdated`

**Communication**: `SessionCreated`， `SessionCancelled`， `BookingCreated`， `BookingCancelled`

### نحوه Publish Events در فاز بعدی

```csharp
// Phase 2 onwards - domain event distributor
public interface IDomainEventDispatcher
{
    Task DispatchAsync(IEnumerable<IDomainEvent> events, CancellationToken ct = default);
}

// Handlerهای داخلی (مثلاً Notification):
internal sealed class MemberRegisteredHandler : IDomainEventHandler<MemberRegistered>
{
    public async Task HandleAsync(MemberRegistered @event, CancellationToken ct)
    {
        // ارسال ایمیل خوش‌آمدگویی
    }
}
```

---

## 8. چندمستاجری (Multi-Tenant)

### استراتژی انزوای Tenant

از **Row-Level Security (RLS)** به عنوان مکانیزم اصلی و **Global Query Filter** به عنوان دفاع در عمق استفاده می‌شود:

#### قاعدة داده

```sql
CREATE SECURITY POLICY TenantFilterPolicy
ADD FILTER PREDICATE dbo.fn_TenantId() = TenantId ON dbo.Members
WITH (STATE = ON);
```

#### EF Core Global Query Filter

```csharp
// MembershipDbContext.OnModelCreating
modelBuilder.Entity<Gym>().HasQueryFilter(g => g.TenantId == _currentTenantService.TenantId);
modelBuilder.Entity<Member>().HasQueryFilter(m => m.TenantId == _currentTenantService.TenantId);
modelBuilder.Entity<Coach>().HasQueryFilter(c => c.TenantId == _currentTenantService.TenantId);
```

**نتیجه**: تمام کوئری‌های EF Core به طور خودکار با TenantId فیلتر می‌شوند. نوشتن کوئری بدون TenantFilter امکان‌پذیر نیست.

#### TenantId از جی‌دبلیو تی استخراج می‌شود

```csharp
// API layer: CurrentUserService extracts tenant from JWT
public interface ICurrentUserService
{
    Guid? UserId { get; }
    Guid? TenantId { get; }  // ← از claim «tenant» در JWT
    bool IsAuthenticated { get; }
    IEnumerable<string> Roles { get; }
    IEnumerable<string> Permissions { get; }
}
```

### قوانین الزامی Multi-Tenant

**برای هر Entity جدید**:
- ✅ حتماً فیلد `TenantId` از نوع `Guid` (NOT NULL) دارد
- ✅ در EF Core `HasQueryFilter()` تعریف می‌شود
- ✅ در کلید خارجی (FK) مربوطه، `TenantId` هم وجود دارد
- ❌ هرگز `TenantId` را به صورت hard-coded قرار ندهید
- ❌ هرگز کوئری cross-tenant بنویسید

---

## 9. اعتبارسنجی (Validation)

### لایه‌های اعتبارسنجی

```
Input Validation (Application/Validators)
        ↓
Format Validation (ValueObjects)
        ↓
Business Rules (Domain Entities)
```

| لایه | مسئولیت | مثال |
|------|---------|------|
| Validator (Application) | Format, required, ranges | `command.Email.IsNotNullOrEmpty()` |
| Value Object (Domain) | Concentrated validation | `Email.Create("invalid") → failure` |
| Entity (Domain) | Business rules | `gym.EnsureActive()` |

### ساختار Validator

```csharp
// Application/Commands/CreateGym/CreateGymCommandValidator.cs
internal sealed class CreateGymCommandValidator : ICommandValidator<CreateGymCommand>
{
    public Result Validate(CreateGymCommand command)
    {
        // 1. Required checks
        if (command.Name is null || command.Name.Length == 0)
            return Result.Failure("نام باشگاه الزامی است.");

        if (command.Name.Length > 200)
            return Result.Failure("نام باشگاه حداکثر ۲۰۰ کاراکتر است.");

        // 2. Format checks (application-level)
        // ❌ Email validation - این در ValueObject انجام می‌شود

        // 3. No business logic!
        // ❌ Don't check if gym already exists here
        return Result.Success();
    }
}
```

### Concentrated Validation در ValueObject

```csharp
// Domain/ValueObjects/Email.cs
public sealed class Email
{
    public string Value { get; }

    private Email(string value) => Value = value;

    public static Result<Email> Create(string? email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return Result<Email>.Failure("ایمیل الزامی است.");

        if (email.Length > 320)
            return Result<Email>.Failure("ایمیل حداکثر ۳۲۰ کاراکتر است.");

        // RFC 5322 format (simplified)
        if (!Regex.IsMatch(email, @"^[a-zA-Z0-9._%+\-]+@[a-zA-Z0-9.\-]+\.[a-zA-Z]{2,}$"))
            return Result<Email>.Failure("فرمت ایمیل نامعتبر است.");

        // Normalization
        var normalized = email.Trim().ToLowerInvariant();
        return Result<Email>.Success(new Email(normalized));
    }
}
```

---

## 10. تزریق وابستگی (Dependency Injection)

### DI Container سازت (Composition Root)

تمام ثبت‌های DI در `GymPlatform.Api/Program.cs` و `InfrastructureServiceCollectionExtensions.cs` انجام می‌شود:

```csharp
// GymPlatform.Api/Program.cs
var builder = WebApplication.CreateBuilder(args);

// 1. Infrastructure (DbContext, Repositories, External Services)
builder.Services.AddInfrastructure(builder.Configuration);

// 2. Platform services
builder.Services.AddHttpContextAccessor();
builder.Services.AddHealthChecks();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 3. Abstractions (concrete implementations)
builder.Services.AddSingleton<DateTimeProvider>();
builder.Services.AddScoped<CurrentUserService>();

// 4. Command Handlers (per module)
builder.Services.AddScoped<ICommandHandler<CreateGymCommand, GymResponse>, CreateGymCommandHandler>();
builder.Services.AddScoped<ICommandHandler<RegisterMemberCommand, MemberResponse>, RegisterMemberCommandHandler>();
// ... all handlers

var app = builder.Build();
```

### Scope ها

| Scope | کاربرد |
|-------|--------|
| `Singleton` | سرویس‌های stateless بدون Estado (`DateTimeProvider`، configuration) |
| `Scoped` | سرویس‌های tied به request (`CurrentUserService`، Handlers، Validators، DbContext) |
| `Transient` | استفاده‌های زیاد، constructor تازه در هر زمان |

### Dependency Rule

```
Api (Composition Root)
    ↓ knows about
Application (ICommandHandler<,>)
    ↓ knows about
Domain (IMemberRepository, IUnitOfWork)
        ↑ implements interface
Infrastructure (MemberRepository, UnitOfWork)
```

---

## 11. قوانین توسعه

### ۱. لایه Domain هیچ وابستگی خارجی ندارد

**الماس**: هیچ `using Microsoft.Extensions.DependencyInjection` یا `using System.Data` در ماژول Domain.

```csharp
❌ اشتباه
using Microsoft.Extensions.DependencyInjection;
namespace GymPlatform.Modules.Membership.Domain;

✅ درست
namespace GymPlatform.Modules.Membership.Domain;
// فقط SharedKernel را import کنید
```

### ۲. منطق تجاری فقط در Domain قرار می‌گیرد

```csharp
❌ اشتباه - Business logic در Application Handler
var email = command.Email.ToLower();
var member = new Member { Email = email, ... };

✅ درست - Business logic در Entity/ValueObject
var emailResult = Email.Create(command.Email); // validates & normalizes
var member = new Member(gymId, fullName, emailResult.Value, gymIsActive);
```

### ۳. Handlerها فقط orchestrate می‌کنند

```csharp
❌ اشتباه - Business logic در Handler
if (member.Age < 18) return Result.Failure("سن کمتر از ۱۸ است.");

✅ درست - Business logic در Entity
// Handler:
var member = new Member(...);
// Member constructor/Entity Self-Validation handles this
```

### ۴. Repositoryها فقط عملیات CRUD انجام می‌دهند

```csharp
❌ اشتباه - Business logic در Repository
if (member.Status == MemberStatus.Active) { ... }

✅ درست - Repository فقط data access
var member = await _memberRepository.GetByIdAsync(id, ct);
if (member is null) return Result<MemberResponse>.Failure("یافت نشد.");
// Any business decision goes in application handler
```

### ۵. Exception Domain مخصوص بدیم

```csharp
// Domain/Exceptions/MembershipDomainException.cs
public sealed class GymNotFoundException : MembershipDomainException
{
    public GymNotFoundException(Guid gymId)
        : base($"باشگاه با شناسه {gymId} یافت نشد.") { }
}

public sealed class MemberAlreadyExistsException : MembershipDomainException
{
    public MemberAlreadyExistsException(string email)
        : base($"عضویی با ایمیل {email} قبلاً ثبت شده است.") { }
}
```

### ۶. Entityها Sealed باشند

```csharp
public sealed class Gym : BaseEntity  // ✅ sealed
```
به جز اینکه برای تست‌پذیری (Mocking) نیازی به extensibility نداشته باشید.

### ۷. Value Objects Immutable باشند

```csharp
public sealed class Email
{
    public string Value { get; }  // ✅ get-only

    private Email(string value) => Value = value;
}
```

### ۸. Constructor-only DI

```csharp
internal sealed class RegisterMemberCommandHandler
    : ICommandHandler<RegisterMemberCommand, MemberResponse>
{
    // ✅ Constructor Injection
    private readonly IMemberRepository _memberRepository;
    private readonly IValidator<RegisterMemberCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterMemberCommandHandler(
        IMemberRepository memberRepository,
        IValidator<RegisterMemberCommand> validator,
        IUnitOfWork unitOfWork)
    {
        _memberRepository = memberRepository;
        _validator = validator;
        _unitOfWork = unitOfWork;
    }
}
```

---

## 12. بهترین شیوه‌ها

### ✅ بدون استفاده از async/await به جزzone I/O

```csharp
public async Task<Result<MemberResponse>> HandleAsync(RegisterMemberCommand command, CancellationToken ct)
{
    // ✅ عملیات async: IO-bound
    var gym = await _gymRepository.GetByIdAsync(command.GymId, ct);
    
    // ✅ CancellationToken همواره منتقل می‌شود
    await _memberRepository.AddAsync(member, ct);
    await _unitOfWork.SaveChangesAsync(ct);

    return Result<MemberResponse>.Success(response);
}
```

### ✅ استفاده از Record برای Commandها و DTOها

```csharp
internal sealed record RegisterMemberCommand(Guid GymId, string FullName, string Email) : ICommand<MemberResponse>;
```

### ✅ CancellationToken در تمام async methods

```csharp
// ✅ Application
public async Task<Result<T>> HandleAsync(TCommand command, CancellationToken ct = default)

// ✅ Infrastructure
public async Task<Member?> GetByIdAsync(Guid id, CancellationToken ct = default)
```

### ✅ استفاده از Result<T> به جای try-catch برای business errors

```csharp
// ✅ Expected business errors
if (gym is null)
    return Result<MemberResponse>.Failure("باشگاه یافت نشد.");

// ❌ Don't throw exceptions for expected business failures
// throw new NotFoundException("Gym not found");
```

### ✅ استفادهاز Factory Method برای Value Object

```csharp
// ✅ Static factory method that returns Result
public static Result<Email> Create(string? email) { ... }

// Usage:
var emailResult = Email.Create(command.Email);
if (emailResult.IsFailure) return Result.Failure(emailResult.Error);
var email = emailResult.Value;
```

### ✅ استفاده از Endpoint Filters برایکشتر的逻辑 مشترک

```csharp
// ✅ در Program.cs - استفاده از TypeFilter یا FilterFactory
app.MapPost("/api/rooms", async (...) => { ... })
   .AddEndpointFilter<ValidationFilter<CreateRoomCommand>>();
```

---

## 13. الگوها و کدهای رایج

### الگوی پیاده‌سازی کامل یک Command

```csharp
// 1. Entity (Domain)
namespace GymPlatform.Modules.Communication.Domain.Entities;

public sealed class Room : BaseEntity
{
    public Room(string name, int capacity, Guid tenantId)
    {
        SetName(name);
        SetCapacity(capacity);
        TenantId = tenantId;
        IsActive = true;
        AddDomainEvent(new RoomCreated(Id, name));
    }

    private Room() { } // EF Core requires parameterless constructor

    public string Name { get; private set; } = string.Empty;
    public int Capacity { get; private set; }
    public Guid TenantId { get; private set; }
    public bool IsActive { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.UtcNow;

    public void Activate()
    {
        EnsureNotActive();
        IsActive = true;
    }

    public void Deactivate()
    {
        EnsureActive();
        IsActive = false;
    }

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > 100)
            throw new DomainException("نام اتاق الزامی و حداکثر ۱۰۰ کاراکتر است.");
        Name = name.Trim();
    }

    private void SetCapacity(int capacity)
    {
        if (capacity < 1)
            throw new DomainException("ظرفیت باید حداقل ۱ باشد.");
        Capacity = capacity;
    }

    private void EnsureActive() { if (!IsActive) throw new DomainException("اتاق فعال نیست."); }
    private void EnsureNotActive() { if (IsActive) throw new DomainException("اتاق فعال است."); }
}
```

```csharp
// 2. Command (Application)
namespace GymPlatform.Modules.Communication.Application.Commands.CreateRoom;

internal sealed record CreateRoomCommand(string Name, int Capacity) 
    : ICommand<RoomResponse>;

internal sealed class CreateRoomCommandValidator : ICommandValidator<CreateRoomCommand>
{
    public Result Validate(CreateRoomCommand command)
    {
        if (string.IsNullOrWhiteSpace(command.Name))
            return Result.Failure("نام اتاق الزامی است.");

        if (command.Name.Length > 100)
            return Result.Failure("نام اتاق حداکثر ۱۰۰ کاراکتر است.");

        if (command.Capacity < 1)
            return Result.Failure("ظرفیت باید حداقل ۱ باشد.");

        return Result.Success();
    }
}

internal sealed class CreateRoomCommandHandler
    : ICommandHandler<CreateRoomCommand, RoomResponse>
{
    private readonly IRoomRepository _roomRepository;
    private readonly IValidator<CreateRoomCommand> _validator;
    private readonly ICommunicationUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUser;

    public CreateRoomCommandHandler(IRoomRepository roomRepository, ...) { ... }

    public async Task<Result<RoomResponse>> HandleAsync(CreateRoomCommand command, CancellationToken ct)
    {
        // 1. Validate
        var validationResult = _validator.Validate(command);
        if (validationResult.IsFailure) return Result<RoomResponse>.Failure(validationResult.Error);

        // 2. Domain Business Logic
        var currentTenantId = _currentUser.TenantId
            ?? throw new UnauthorizedAccessException("Tenant ID is required.");

        var room = new Room(command.Name, command.Capacity, currentTenantId);

        // 3. Persist
        await _roomRepository.AddAsync(room, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        // 4. Response
        return Result<RoomResponse>.Success(new(
            room.Id, room.Name, room.Capacity, room.IsActive, room.CreatedAt));
    }
}
```

```csharp
// 3. EF Configuration (Infrastructure)
public class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.ToTable("Rooms", "Communication");
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Name).HasMaxLength(100).IsRequired();
        builder.Property(r => r.Capacity).IsRequired();
        builder.Property(r => r.TenantId).IsRequired();
        builder.Property(r => r.IsActive).IsRequired();
        builder.Property(r => r.CreatedAt).IsRequired();
        builder.HasIndex(r => r.TenantId);
        builder.HasQueryFilter(r => r.TenantId == GetTenantId());
    }
}
```

```csharp
// 4. API Endpoint (GymPlatform.Api/Program.cs)
app.MapPost("/api/rooms", async (
    CreateRoomCommand command,
    IValidator<CreateRoomCommand> validator,
    ICommandHandler<CreateRoomCommand, RoomResponse> handler,
    CancellationToken ct) =>
{
    var validationResult = validator.Validate(command);
    if (validationResult.IsFailure)
        return Results.Problem(statusCode: 400, detail: validationResult.Error);

    var result = await handler.HandleAsync(command, ct);
    return result.IsSuccess
        ? Results.Ok(result.Value)
        : Results.Problem(statusCode: 400, detail: result.Error);
})
.RequireAuthorization()
.WithName("CreateRoom")
.WithTags("Rooms");
```

```csharp
// 5. Unit Test (.in Tests project)
public sealed class CreateRoomCommandHandlerTests
{
    [Fact]
    public async Task HandleAsync_WithValidCommand_ShouldCreateRoom()
    {
        // Arrange
        var mockRepo = new Mock<IRoomRepository>();
        var mockUow = new Mock<ICommunicationUnitOfWork>();
        var mockValidator = new Mock<ICommandValidator<CreateRoomCommand>>();
        var mockCurrentUser = new Mock<ICurrentUserService>();

        mockValidator.Setup(v => v.Validate(It.IsAny<CreateRoomCommand>()))
            .Returns(Result.Success());
        mockCurrentUser.Setup(c => c.TenantId).Returns(Guid.NewGuid());

        var handler = new CreateRoomCommandHandler(
            mockRepo.Object, mockValidator.Object, mockUow.Object, mockCurrentUser.Object);

        var command = new CreateRoomCommand("Room A", 20);

        // Act
        var result = await handler.HandleAsync(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        mockRepo.Verify(r => r.AddAsync(It.IsAny<Room>(), It.IsAny<CancellationToken>()), Times.Once);
        mockUow.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
```

---

## 14. اشتباهات متداول

### ❌ ۱. وابستگی از Domain به Infrastructure

```csharp
❌ using GymPlatform.Infrastructure;
   namespace GymPlatform.Modules.Membership.Domain;

✅他只引用 SharedKernel, 他自己这个 Module 的东西
```

### ❌ ۲. منطق تجاری در Validator

```csharp
❌ if (_memberRepository.GetByEmailAsync(email).Result is not null)
       return Result.Failure("ایمیل تکراری است.");

✅ این منطق در Handler یا Entity قرار می‌گیرد:
   var existing = await _memberRepository.GetByEmailAsync(email, ct);
   if (existing is not null)
       return Result.Failure("ایمیل تکراری است.");
```

### ❌ ۳. منطق تجاری در Repository

```csharp
❌ public void Update(Member member)
   {
       if (member.Status != MemberStatus.Active)
           throw new InvalidOperationException("فقط اعضای فعال قابل به‌روزرسانی هستند.");
       _context.Members.Update(member);
   }

✅ Repository فقط از هدایت و callbacks مسئول است.
   منطق业务流程在 Entity 的方法中:
   member.Activate();
   _unitOfWork.SaveChangesAsync(ct);
```

### ❌ ۴. استفاده از Singletons برای سرویس‌های Stateful

```csharp
❌ services.AddScoped<ICurrentUserService, CurrentUserService>();
// CurrentUserService روی HttpContext وابسته است → Scoped!

✅ services.AddScoped<ICurrentUserService, CurrentUserService>();
// DateTimeProvider stateless است → Singleton
services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
```

### ❌ ۵. فراموشی CancellationToken

```csharp
❌ public async Task<Member?> GetByIdAsync(Guid id)
   {
       return await _context.Members.FirstOrDefaultAsync(m => m.Id == id);
   }

✅ public async Task<Member?> GetByIdAsync(Guid id, CancellationToken ct = default)
   {
       return await _context.Members.FirstOrDefaultAsync(m => m.Id == id, ct);
   }
```

### ❌ ۶. استفاده از try-catch برای خطاهای تجاری

```csharp
❌ try
   {
       member.Activate();
   }
   catch (DomainException ex)
   {
       return Result.Failure(ex.Message);
   }

✅ Result<T> برای خطاهای تجاری:
   if (!member.CanActivate())
       return Result.Failure("عضویت فعال نیست.");
```

### ❌ ۷. هاردکورد کردن TenantId

```csharp
❌ var tenantId = Guid.Parse("...");

✅ var tenantId = _currentUserService.TenantId
    ?? throw new UnauthorizedAccessException();
```

---

## 15. مسیر یادگیری توسعه‌دهنده جدید

### مرحله ۱: درک Domain (۳-۵ روز)

1. خواندن `docs/PROJECT_GUIDE_FA.md`
2. بررسی `GymPlatform.Modules.Membership/Domain/`:
   - `Entities/Gym.cs`, `Member.cs`, `Coach.cs`
   - `ValueObjects/Email.cs`, `Phone.cs`
   - `Events/` — همه رویدادهای تعریف شده
3. بررسی `GymPlatform.SharedKernel/`:
   - `BaseEntity.cs`
   - `Result.cs`
   - `IDomainEvent.cs`

### مرحله ۲: درک Application Layer (۳-۵ روز)

1. بررسی `RegisterMemberCommand` از Interface تا Handler
2. بررسی ساختار Validator
3. بررسی `Result<T>` و جریان موفقیت/شکست

### مرحله ۳: درک Infrastructure (۲-۳ روز)

1. بررسی `InfrastructureServiceCollectionExtensions.cs` — ثبت DI
2. بررسی یک Repository مثال: `MemberRepository`
3. بررسی یک DbContext: `MembershipDbContext`
4. بررسی Migration اولیه

### مرحله ۴: درک API Layer (۲ روز)

1. بررسی `Program.cs` — Minimal API endpoints
2. بررسی `GlobalExceptionMiddleware.cs`
3. بررسی `CurrentUserService.cs`

### مرحله ۵: تست (۲ روز)

1. بررسی `GymPlatform.Modules.Membership.Tests/`
2. اجرای `dotnet test` به صورت local
3. نوشتن یک تست ساده برای یک Command Handler

### مرحله ۶: اولین Pull Request

1. انتخاب یک Task کوچک از Phase جاری
2. پیاده‌سازی با رعایت تمام قوانین این راهنما
3. بررسی خود (self-review) قبل از提交 PR
4. تست‌های واحد برای کد جدید
5. `dotnet build` بدون خطا
6. PR submission

---

## منابع اضافی

| سند | توضیحات |
|------|---------|
| `docs/ARCHITECTURE.md` | معماری سیستم |
| `docs/PROJECT_HUIDE_FA.md` | راهنمای کلی پروژه |
| `.ai/context/IMPLEMENTATION_MASTER_PLAN.md` | نقشه راه اجرایی |
| `.ai/context/API_BLUEPRINT.md` | مشخصات API |
| `.ai/context/DATABASE_BLUEPRINT.md` | طراحی پایگاه داده |
| `docs/PROJECT_HANDOFF.md` | وضعیت فعلی پروژه |
| `AGENTS.md` | قوانین AI Agent |
