# راهنمای توسعه‌دهنده Front-end GymPlatform

> راهنمای کامل برای توسعه‌دهندگان Front-end GymPlatform  
> آخرین به‌روزرسانی: ۳ ژوئیه ۲۰۲۶  
> وضعیت: Phase 1 — راهنمای توسعه، پیاده‌سازی در فاز بعدی

---

## فهرست مطالب

1. [مقدمه](#1-مقدمه)
2. [پشته فناوری](#2-پشته-فناوری)
3. [ساختار پروژه](#3-ساختار-پروژه)
4. [معماری Frontend](#4-معماری-frontend)
5. [نقشه‌راه پیاده‌سازی](#5-نقشه-راه-پیاده‌سازی)
6. [یکپارچگی API](#6-یکپارچگی-api)
7. [مدیریت State](#7-مدیریت-state)
8. [مسیریابی](#8-مسیریابی)
9. [طراحی واکنش‌گرا](#9-طراحی-واکنش‌گرا)
10. [سیستم کامپوننت](#10-سیستم-کامپوننت)
11. [استراتژی تست](#11-استراتژی-تست)
12. [استانداردهای کدنویسی](#12-استانداردهای-کدنویسی)
13. [مسیر یادگیری](#13-مسیر-یادگیری)
14. [وابستگی‌های Backend](#14-وابستگی‌های-backend)

---

## 1. مقدمه

GymPlatform در فاز فعلی فنی، لایه‌ی Frontend را پیاده‌سازی نکرده است. این راهنما برای توسعه‌دهندگانی طراحی شده که در فازهای بعدی (Phase 4+) می‌خواهند Frontend را پیاده‌سازی کنند.

این راهنما بر اساس `.ai/context/UI_UX_BLUEPRINT.md` و `.ai/context/IMPLEMENTATION_MASTER_PLAN.md` تهیه شده است.

**وضعیت فعلی**: Frontend هنوز شروع نشده — backtrack و scaffolding آماده است.

### پیش‌نیازهای خواندن این راهنما

- `docs/PROJECT_GUIDE_FA.md` — درک کلی پروژه
- `.ai/context/PRODUCT_BLUEPRINT.md` — مشخصات محصول
- `.ai/context/API_BLUEPRINT.md` — طراحی API
- `.ai/context/UI_UX_BLUEPRINT.md` — طراحی UI/UX
- `.ai/context/USER_ROLES.md` — نقش‌های کاربری

---

## 2. پشته فناوری

### تکنولوژی‌های اصلی

| لایه | فناوری | نسخه | توضیحات |
|------|--------|------|---------|
| فریم‌ورک | Next.js | 15+ | App Router، Server Components |
| کتابخانه UI | React | 19+ | TypeScript با strict mode |
| زبان | TypeScript | 5+ | Strict mode فعال |
| استایل‌دهی | Tailwind CSS | 3+ | Utility-first، dark mode پشتیبانی |
| مدیریت State | Zustand | Latest | State management سبک‌وزن |
| دریافت داده | TanStack Query (React Query) | 5+ | Server state management، caching |
| مسیریابی | Next.js App Router | داخلی | Route groups برای role-based access |
| فرم‌ها | React Hook Form | Latest | اعتبارسنجی با Zod |
| اعتبارسنجی | Zod | Latest | Schema validation هماهنگ با Backend |
| تست | Jest + React Testing Library | Latest | Unit و integration tests |
| E2E | Playwright |Latest | End-to-end testing |
| API Client | Custom Fetch | — | Built on Fetch API with TanStack Query |

### تکنولوژی‌های پشتیبانی

| یک Instrumentation | ابزار | هدف |
|---------|-------|------|
| Linting | ESLint | کیفیت کد |
| Formatting | Prettier | فرمت یکنواخت |
| Build | Next.js Build | Optimization و bundling |
| Hosting | Vercel (پیشنهادی) | یا Azure App Service |

---

## 3. ساختار پروژه

### ساختار پیشنهادی

```
gymplatform-web/                    # پروژه اصلی Next.js
├── src/
│   ├── app/                         # App Router (Next.js 13+)
│   │   ├── (auth)/                  # Route group: ورود/ثبت‌نام
│   │   │   ├── login/page.tsx
│   │   │   └── register/page.tsx
│   │   ├── (dashboard)/             # Route group: داشبوردهای کاربر
│   │   │   ├── owner/
│   │   │   ├── manager/
│   │   │   ├── trainer/
│   │   │   ├── member/
│   │   │   └── admin/
│   │   ├── layout.tsx               # Layout اصلی
│   │   ├── page.tsx                 # Landing page
│   │   └── globals.css
│   ├── components/
│   │   ├── ui/                      # کامپوننت‌های پایه (shadcn/ui)
│   │   ├── layouts/                 # Layoutهای مشترک
│   │   ├── features/                # کامپوننت‌های feature-specific
│   │   └── providers/               # Context Providers
│   ├── lib/
│   │   ├── api/                     # API client و hooks
│   │   ├── auth/                    # Authentication utilities
│   │   ├── utils/                   # Helper functions
│   │   └── validators/              # Zod schemas
│   ├── stores/                      # Zustand stores
│   ├── hooks/                       # Custom React hooks
│   ├── types/                       # TypeScript types
│   │   ├── api.ts                   # API response types
│   │   ├── auth.ts                  # Auth-related types
│   │   └── domain.ts                # Business types
│   └── middleware.ts                # Next.js middleware (auth guards)
├── public/
│   ├── images/
│   └── fonts/
├── tests/
│   ├── unit/
│   ├── integration/
│   └── e2e/
├── .env.local                       # Environment variables (local)
├── .env.example                     # Example environment file
├── next.config.js
├── tailwind.config.ts
├── tsconfig.json
└── package.json
```

### نگاشت نقش‌های کاربری به Routeها

```
/dashboard/owner/       → Gym Owner: مدیریت باشگاه
/dashboard/manager/     → Gym Manager: مدیریت روزمره
/dashboard/trainer/     → Fitness Trainer: برنامه‌های تمرین و اعضا
/dashboard/member/      → Gym Member: تمرین‌ها و رزروها
/dashboard/admin/       → Platform Admin: مدیریت پلتفرم
```

---

## 4. معماری Frontend

### لایه‌های معماری

```
┌──────────────────────────────────────┐
│  Pages (App Router)                   │  ← Route-level rendering
├──────────────────────────────────────┤
│  Feature Components                   │  ← بخش‌های مربوط به feature
├──────────────────────────────────────┤
│  Shared Components                    │  ← کامپوننت‌های مشترک
├──────────────────────────────────────┤
│  UI Component Library (shadcn/ui)     │  ← کامپوننت‌های پایه Design System
├──────────────────────────────────────┤
│  Hooks & State (Zustand, TSQ)        │  ← منطق state و副作用
├──────────────────────────────────────┤
│  API Client (Fetch + TSQ)            │  ← یکپارچگی Backend
├──────────────────────────────────────┤
│  Types & Validators (TypeScript, Zod)│  ← Type safety و اعتبارسنجی
└──────────────────────────────────────┘
```

### اصول معماری

**1. Server Components به عنوان پیش‌فرض**
- از Server Components برای fetch داده‌های اولیه استفاده کنید
- Client Components فقط برای تعاملات تعبیه شوند
- خط‌مشی: `"use client"` فقط وقتی لازم است

**2. Feature-based Organization**
- کامپوننت‌ها بر اساس feature سازماندهی شوند، نه نوع
- هر feature پوشه جداگانه در `components/features/` دارد

**3. Atomic Design به صورت سبک**
- Atoms: کامپوننت‌های پایه (Button, Input, Card)
- Molecules: ترکیب atoms (SearchBar, FormField)
- Organisms: بخش‌های بزرگ (MemberTable, SessionList)

**4. Type Safety در همه جا**
- همه API responses با TypeScript types تعریف شوند
- فرم‌ها با Zod schemas اعتبارسنجی شوند
- هیچ `any` استفاده نشود

---

## 5. نقشه‌راه پیاده‌سازی

### فازهای پیاده‌سازی Frontend

#### Phase 4.1: Foundation (هفته‌های ۲۳-۲۴)

| وظیفه | توضیحات |
|-------|---------|
| راه‌اندازی پروژه | Scaffolding Next.js 15 با TypeScript و Tailwind |
| Design System | پیاده‌سازی کامپوننت‌های پایه (Button, Input, Card, Modal) |
| API Client | پیاده‌سازی کلاینت API با TanStack Query |
| Authentication | پیاده‌سازی ورود/ثبت‌نام با JWT |
| Layoutها | پیاده‌سازی Sidebar،Header،Footer |
| Route Guards | پیاده‌سازی middleWare برای مسدودسازی دسترسی |

#### Phase 4.2: Dashboard (هفته‌های ۲۵-۲۶)

| نقش | صفحات |
|------|-------|
| Gym Owner | داشبورد، مدیریت اعضا، برنامه‌ها، گزارش مالی |
| Gym Manager | مدیریت روزمره، رزروها، برنامه‌ها |
| Trainer | برنامه‌های تمرین، اعضا، سشن‌ها |
| Member | تمرین‌های من، پیشرفت، رزرو جلسات |
| Admin | مدیریت سیستم، کاربران، تنظیمات |

#### Phase 4.3: Features (ماه‌های ۷-۹)

| ویژگی | توضیحات |
|-------|---------|
| جستجو و فیلتر | فیلتر پیشرفته تمرین‌ها و اعضا |
| تقویم | Calendar برای رزرو جلسات |
| آپلود مدیا | آپلود عکس و ویدیو |
| notification | نمایش notificationهای real-time |
| پرداخت | مدیریت روش‌های پرداخت |

### bunch‌ریزی اولویت‌ها

```
بالاترین اولویت (سروش):
├── Landing Page + Authentication
├── Member Dashboard (تمرین‌ها + پیشرفت)
├── Trainer Dashboard (برنامه‌های تربیتی)
└── Owner Dashboard (نمای کلی کسب‌وکار)

اولویت متوسط:
├── Booking & Calendar
├── Profile Management
├── Notifications
└── Settings

اولویت پایین (پست-MVP):
├── Advanced Analytics Charts
├── Gamification Elements
├── Social Features
└── Mobile-specific Optimizations
```

---

## 6. یکپارچگی API

### کلاینت API

```typescript
// src/lib/api/client.ts
const API_BASE = process.env.NEXT_PUBLIC_API_URL || 'http://localhost:5000';

async function apiFetch<T>(
  endpoint: string,
  options?: RequestInit
): Promise<T> {
  const token = getAccessToken(); // از storage或 Cookie

  const response = await fetch(`${API_BASE}${endpoint}`, {
    ...options,
    headers: {
      'Content-Type': 'application/json',
      'Authorization': token ? `Bearer ${token}` : undefined,
      ...options?.headers,
    },
  });

  if (!response.ok) {
    const error = await response.json();
    throw new ApiError(error.detail || 'API Error', response.status);
  }

  return response.json();
}
```

### Hooks با TanStack Query

```typescript
// src/hooks/useMembers.ts
import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';

export function useMembers(gymId: string) {
  return useQuery({
    queryKey: ['members', gymId],
    queryFn: () => apiFetch<Member[]>(`/v1/gyms/${gymId}/members`),
    enabled: !!gymId,
  });
}

export function useCreateMember() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (data: CreateMemberDto) =>
      apiFetch<Member>('/v1/members', {
        method: 'POST',
        body: JSON.stringify(data),
      }),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['members'] });
    },
  });
}
```

### استانداردهای پاسخ

```typescript
// پاسخ موفق
interface ApiSuccessResponse<T> {
  data: T;
  meta?: {
    page: number;
    totalPages: number;
    totalItems: number;
  };
}

// پاسخ خطا (RFC 7807 ProblemDetails)
interface ApiErrorResponse {
  type: string;
  title: string;
  status: number;
  detail: string;
  errors?: Record<string, string[]>;
}
```

### مدیریت خطاها

```tsx
// src/components/ErrorBoundary.tsx
'use client';

import { Component, ReactNode } from 'react';
import { AlertTriangle } from 'lucide-react';

interface Props {
  children: ReactNode;
  fallback?: ReactNode;
}

interface State {
  hasError: boolean;
  error: Error | null;
}

export class ApiErrorBoundary extends Component<Props, State> {
  constructor(props: Props) {
    super(props);
    this.state = { hasError: false, error: null };
  }

  static getDerivedStateFromError(error: Error): State {
    return { hasError: true, error };
  }

  render() {
    if (this.state.hasError) {
      return this.props.fallback || (
        <div className="flex flex-col items-center justify-center min-h-[400px] gap-4">
          <AlertTriangle className="h-12 w-12 text-red-500" />
          <h2 className="text-xl font-semibold">خطایی رخ داد</h2>
          <p className="text-muted-foreground">{this.state.error?.message}</p>
        </div>
      );
    }
    return this.props.children;
  }
}
```

---

## 7. مدیریت State

### Zustand Stores

```typescript
// src/stores/authStore.ts
import { create } from 'zustand';
import { persist } from 'zustand/middleware';

interface User {
  id: string;
  email: string;
  fullName: string;
  roles: string[];
  tenantId: string;
}

interface AuthState {
  user: User | null;
  token: string | null;
  isLoading: boolean;
  setAuth: (user: User, token: string) => void;
  logout: () => void;
}

export const useAuthStore = create<AuthState>()(
  persist(
    (set) => ({
      user: null,
      token: null,
      isLoading: true,
      setAuth: (user, token) => set({ user, token, isLoading: false }),
      logout: () => set({ user: null, token: null }),
    }),
    {
      name: 'gymplatform-auth',
      partialize: (state) => ({ token: state.token, user: state.user }),
    }
  )
);
```

### fixtures ممیزی

```typescript
// src/stores/gymStore.ts
interface GymState {
  currentGym: Gym | null;
  setCurrentGym: (gym: Gym) => void;
  updateGym: (updates: Partial<Gym>) => void;
}
```

### قوانین State Management

| انواع State | راه‌حل | مثال |
|-------------|--------|------|
| Server State | TanStack Query | لیست اعضا، برنامه‌های تمرین |
| Client State | Zustand | اطلاعات کاربر، UI state |
| Form State | React Hook Form | فرم‌های ثبت‌نام، ویرایش |
| URL State | Next.js Search Params | صفحه‌بندی، فیلترها |
| ephemeral State | useState | toggle، hover، acknowledge |

---

## 8. مسیریابی

### ساختار Route

```
src/app/
├── (public)/                    # Routes عمومی (نیاز به auth ندارد)
│   ├── page.tsx                 # Landing page
│   ├── login/
│   ├── register/
│   └── pricing/
├── (dashboard)/                 # Routes محافظت شده
│   ├── layout.tsx               # Dashboard layout با Sidebar
│   ├── owner/                   # Gym Owner routes
│   │   ├── page.tsx             # Overview
│   │   ├── members/
│   │   ├── programs/
│   │   ├── billing/
│   │   └── reports/
│   ├── manager/
│   ├── trainer/
│   │   ├── page.tsx
│   │   ├── clients/
│   │   ├── sessions/
│   │   └── programs/
│   ├── member/
│   │   ├── page.tsx
│   │   ├── workouts/
│   │   ├── progress/
│   │   └── bookings/
│   └── admin/
│       ├── page.tsx
│       ├── users/
│       ├── gyms/
│       └── settings/
└── layout.tsx                   # Root layout
```

### Route Guards

```typescript
// src/middleware.ts
import { NextResponse } from 'next/server';
import type { NextRequest } from 'next/server';

const protectedRoutes = ['/dashboard'];

export function middleware(request: NextRequest) {
  const token = request.cookies.get('access_token')?.value;
  const path = request.nextUrl.pathname;

  const isProtected = protectedRoutes.some(route =>
    path.startsWith(route)
  );

  if (isProtected && !token) {
    return NextResponse.redirect(new URL('/login', request.url));
  }

  return NextResponse.next();
}

export const config = {
  matcher: ['/((?!api|_next/static|_next/image|favicon.ico).*)'],
};
```

---

## 9. طراحی واکنش‌گرا

### Breakpoints (Tailwind)

```typescript
// tailwind.config.ts
const theme = {
  screens: {
    'sm': '640px',    // Mobile landscape
    'md': '768px',    // Tablet
    'lg': '1024px',   # Desktop
    'xl': '1280px',   # Large desktop
    '2xl': '1536px',  # Extra large
  }
};
```

### استراتژی Responsive

```tsx
// Responsive navigation
<nav className="
  flex flex-col gap-2
  md:flex-row md:items-center md:gap-4
  lg:gap-6
">
  <Link href="/" className="text-sm md:text-base">
    Home
  </Link>
</nav>

// Responsive data tables
<div className="overflow-x-auto">
  <table className="min-w-full">
    <thead>
      <tr className="hidden md:table-row">
        <th>Name</th>
        <th>Email</th>
        <th className="hidden lg:table-cell">Phone</th>
      </tr>
    </thead>
  </table>
</div>
```

---

## 10. سیستم کامپوننت

### کامپوننت‌های پایه (UI)

```typescript
// shadcn/ui Button با variants سفارشی
// src/components/ui/button.tsx

import { cva, type VariantProps } from 'class-variance-authority';
import { cn } from '@/lib/utils';

const buttonVariants = cva(
  'inline-flex items-center justify-center rounded-md text-sm font-medium transition-colors',
  {
    variants: {
      variant: {
        default: 'bg-blue-600 text-white hover:bg-blue-700',
        destructive: 'bg-red-600 text-white hover:bg-red-700',
        outline: 'border border-gray-300 hover:bg-gray-50',
        secondary: 'bg-gray-100 hover:bg-gray-200',
        ghost: 'hover:bg-gray-100',
        link: 'text-blue-600 underline-offset-4 hover:underline',
      },
      size: {
        default: 'h-10 px-4 py-2',
        sm: 'h-9 px-3 text-xs',
        lg: 'h-11 px-8 text-base',
        icon: 'h-10 w-10',
      },
    },
    defaultVariants: {
      variant: 'default',
      size: 'default',
    },
  }
);

interface ButtonProps
  extends React.ButtonHTMLAttributes<HTMLButtonElement>,
    VariantProps<typeof buttonVariants> {
  isLoading?: boolean;
}

export function Button({ isLoading, className, variant, size, ...props }: ButtonProps) {
  return (
    <button
      className={cn(buttonVariants({ variant, size, className }))}
      disabled={isLoading}
      {...props}
    >
      {isLoading && <Spinner className="mr-2 h-4 w-4" />}
      {props.children}
    </button>
  );
}
```

### کامپوننت‌های Feature

```
src/components/features/
├── members/
│   ├── MemberList.tsx        # لیست اعضا با جستجو و فیلتر
│   ├── MemberCard.tsx        # کارت اعضا برای نمایش grid
│   ├── MemberForm.tsx        # فرم ایجاد/ویرایش عضو
│   └── MemberDetail.tsx      # صفحه جزئیات عضو
├── workouts/
│   ├── WorkoutProgramList.tsx
│   ├── ExerciseCard.tsx
│   └── WorkoutPlayer.tsx     # پخش برنامه تمرین
├── sessions/
│   ├── SessionCalendar.tsx   # تقویم جلسات
│   ├── BookingForm.tsx       # فرم رزرو
│   └── SessionCard.tsx       # کارت جلسه
├── billing/
│   ├── InvoiceList.tsx
│   ├── PaymentMethodForm.tsx
│   └── ReceiptCard.tsx
└── shared/
    ├── LoadingState.tsx
    ├── EmptyState.tsx
    └── ErrorFallback.tsx
```

---

## 11. استراتژی تست

### Pyramid تست Frontend

| سطح | ابزار | هدف | پوشش |
|------|-------|------|------|
| Unit | Jest + RTL | کامپوننت‌های پایه | 80% |
| Integration | Jest + MSW | Hooks و API integration | 60% |
| E2E | Playwright | مسیرهای اصلی کاربر | 100% |

### مثال Unit Test

```typescript
// src/components/features/members/MemberCard.test.tsx
import { render, screen } from '@testing-library/react';
import { MemberCard } from './MemberCard';
import { mockMember } from '@/test/fixtures';

describe('MemberCard', () => {
  it('renders member name and email', () => {
    render(<MemberCard member={mockMember} />);
    expect(screen.getByText(mockMember.fullName)).toBeInTheDocument();
    expect(screen.getByText(mockMember.email)).toBeInTheDocument();
  });

  it('shows inactive badge for inactive members', () => {
    render(<MemberCard member={{ ...mockMember, isActive: false }} />);
    expect(screen.getByText('غیرفعال')).toBeInTheDocument();
  });
});
```

---

## 12. استانداردهای کدنویسی

### قوانین اصلی

1. **TypeScript Strict Mode**: تمام فایل‌ها در `strict` mode
2. **Functional Components فقط**: استفاده از Hooks، نه Class Components
3. **Named Exports برای کامپوننت‌ها**: `export function Button()` نه `export default Button`
4. **Custom Hooks با `use` پیشوند**: `useAuth()`, `useMembers()`
5. **No Inline Styles**: استفاده از Tailwind classes
6. **Server Components به عنوان پیش‌فرض**: `"use client"` فقط وقتی لازم
7. **Error Boundaries**: هر route group دارای Error Boundary باشد
8. **Loading States**: همه async operations دارای skeleton یا spinner باشند
9. **Accessibility**: همه کامپوننت‌ها دارای aria-label و keyboard support باشند
10. **Prop Validation**: استفاده از TypeScript interfaces، نه `any`

### نام‌گذاری

| نوع | Convention | مثال |
|-----|------------|------|
| کامپوننت | PascalCase | `MemberCard.tsx` |
| Hook | camelCase با `use` | `useMembers()` |
| Store | camelCase با `use` | `useAuthStore` |
| Utility | camelCase | `formatDate()` |
| Type/Interface | PascalCase | `MemberResponse` |
| Constant | UPPER_SNAKE_CASE | `API_BASE_URL` |
| CSS Class | Tailwind utilities | built-in |

---

## 13. مسیر یادگیری

### مرحله ۱: درک پروژه (۱ روز)

1. خواندن `docs/PROJECT_GUIDE_FA.md`
2. بررسی `.ai/context/PRODUCT_BLUEPRINT.md` — درک نیازمندهای ۶ نقش کاربری
3. بررسی `.ai/context/UI_UX_BLUEPRINT.md` — درک طراحی‌ها
4. بررسی `.ai/context/API_BLUEPRINT.md` — درک APIها

### مرحله ۲: Setup و Tooling (۱ روز)

1. نصب Node.js ۲۰+ و npm/pnpm
2. ایجاد پروژه Next.js با TypeScript و Tailwind
3. نصب shadcn/ui و تنظیم components
4. راه‌اندازی ESLint و Prettier

### مرحله ۳:凄凉‌سازی پایه (۲ روز)

1. پیاده‌سازی Layoutها (Sidebar، Header)
2. پیاده‌سازی landing page
3. پیاده‌سازی صفحات authentication (ورود/ثبت‌نام)
4. پیاده‌سازی Route Guards

### مرحله ۴: یکپارچگی API (۲ روز)

1. پیاده‌سازی API Client با TanStack Query
2. پیاده‌سازی Auth flow (ورود/refresh/outh)
3. پیاده‌سازی صفحات اولیه Dashboard

### مرحله ۵: Feature Implementation

1. انتخاب یک feature از backlog
2. پیاده‌سازی کامپوننت‌های مربوطه
3. اتصال به Backend API
4. تست‌نویسی

---

## 14. وابستگی‌های Backend

Frontend برای شروع پیاده‌سازی، موارد زیر از Backend نیاز دارد:

| نیازمندی | وضعیت Backend | توضیحات |
|----------|---------------|---------|
| API Endpoints Membership | ✅ موجود | 4 endpoints پیاده‌سازی شده |
| API Endpoints Training | ✅ موجود | 7 endpoints پیاده‌سازی شده |
| API Endpoints Communication | ✅ موجود | 6 endpoints پیاده‌سازی شده |
| Auth Endpoints | ❌ نیاز به پیاده‌سازی | Login، refresh، logout |
| Swagger/OpenAPI | ✅ موجود | در `/swagger` قابل دسترس |
| CORS | ❌ نیاز به پیکربندی | باید در Backend configure شود |

### نکات مهم

- Backend API در حال حاضر در `/api/` سرو می‌شود (نه `/v1/`).
- JWT token در header `Authorization: Bearer <token>` ارسال می‌شود.
- تمام درخواست‌های write نیازمند هدر `Idempotency-Key` هستند.
- پاسخ‌های خطا از فرمت RFC 7807 ProblemDetails پیروی می‌کنند.

---

## منابع اضافی

| سند | توضیحات |
|------|---------|
| `.ai/context/UI_UX_BLUEPRINT.md` | طراحی UI/UX |
| `.ai/context/API_BLUEPRINT.md` | مشخصات API |
| `.ai/context/USER_ROLES.md` | نقش‌های کاربری |
| `docs/PROJECT_GUIDE_FA.md` | راهنمای کلی پروژه |
| `.ai/context/IMPLEMENTATION_MASTER_PLAN.md` | نقشه راه اجرایی |
| `docs/frontend/FRONTEND_DEVELOPER_HANDBOOK_FA.md` | دفترچه راه Frontend |

---

*این راهنما بر اساس وضعیت پروژه به تاریخ ۳ ژوئیه ۲۰۲۶ تهیه شده است. برای آپدیت نسخه، مستندات `.ai/context/` را مراجعه کنید.*
