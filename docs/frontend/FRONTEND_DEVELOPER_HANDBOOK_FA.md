# دفترچه راه توسعه‌دهنده Front-end GymPlatform

> راهنمای جامع برای توسعه‌دهندگان Front-end  
> آخرین به‌روزرسانی: ۳ ژوئیه ۲۰۲۶  
> وضعیت: Phase 1 — راهنمای توسعه، پیاده‌سازی در فاز بعدی

---

## فهرست مطالب

1. [مقدمه](#1-مقدمه)
2. [ساختار پروژه](#2-ساختار-پروژه)
3. [فناوری‌ها](#3-فناوری‌ها)
4. [سیستم کامپوننت‌ها](#4-سیستم-کامپوننت‌ها)
5. [مدیریت State](#5-مدیریت-state)
6. [یکپارچگی با Backend](#6-یکپارچگی-با-backend)
7. [مسیریابی و دسترسی](#7-مسیریابی-و-دسترسی)
8. [طراحی واکنش‌گر](#8-طراحی-واکنش‌گرا)
9. [تست‌نویسی](#9-تست‌نویسی)
10. [بهینه‌سازی و Performance](#10-بهینه‌سازی-و-performance)
11. [استانداردهای کد](#11-استانداردهای-کد)
12. [Deploy](#12-deploy)

---

## 1. مقدمه

این دفترچه راه برای توسعه‌دهندگانی است که در پیاده‌سازی لایه Frontend GymPlatform مشارکت خواهند کرد.

**وضعیت فعلی**: پروژه Frontend در این مرحله ایجاد نشده است. این راهنما بر اساس تصمیمات `.ai/context/IMPLEMENTATION_MASTER_PLAN.md` و `.ai/context/UI_UX_BLUEPRINT.md` تهیه شده است.

**پیش‌نیازها**:
- Node.js 20+ و npm/pnpm
- آشنایی با Next.js ۱۵ و React ۱۹
- آشنایی با TypeScript ۵
- درک REST API و HTTP

---

## 2. ساختار پروژه

### ساختار پیشنهادی

```
gymplatform-web/
├── src/
│   ├── app/                          # Next.js App Router
│   │   ├── (auth)/
│   │   │   ├── login/page.tsx
│   │   │   ├── register/page.tsx
│   │   │   └── forgot-password/page.tsx
│   │   ├── (dashboard)/             # Route group محافظت شده
│   │   │   ├── layout.tsx
│   │   │   ├── owner/
│   │   │   ├── manager/
│   │   │   ├── trainer/
│   │   │   ├── member/
│   │   │   └── admin/
│   │   ├── layout.tsx
│   │   └── page.tsx                 # Landing page
│   ├── components/
│   │   ├── ui/                      # shadcn/ui componentهای پایه
│   │   ├── features/                # کامپوننت‌های feature-specific
│   │   ├── layouts/                 # Header، Sidebar، Footer
│   │   └── providers/               # QueryClientProvider، ThemeProvider
│   ├── lib/
│   │   ├── api/                     # API client
│   │   ├── auth/                    # Auth utilities
│   │   └── utils/                   # Helper functions
│   ├── stores/                      # Zustand stores
│   ├── hooks/                       # Custom hooks
│   └── types/                       # TypeScript types
├── public/
├── tests/
│   ├── unit/
│   ├── integration/
│   └── e2e/
├── .env.local
├── .env.example
└── package.json
```

---

## 3. فناوری‌ها

### Dependencies اصلی

```json
{
  "dependencies": {
    "next": "^15.0.0",
    "react": "^19.0.0",
    "react-dom": "^19.0.0",
    "@tanstack/react-query": "^5.0.0",
    "zustand": "^4.0.0",
    "zod": "^3.0.0",
    "@hookform/resolvers": "^3.0.0",
    "react-hook-form": "^7.0.0",
    "class-variance-authority": "^0.7.0",
    "clsx": "^2.0.0",
    "tailwind-merge": "^2.0.0",
    "lucide-react": "^0.300.0",
    "date-fns": "^3.0.0",
    "recharts": "^2.0.0"
  },
  "devDependencies": {
    "@types/node": "^20.0.0",
    "@types/react": "^19.0.0",
    "typescript": "^5.0.0",
    "tailwindcss": "^3.0.0",
    "eslint": "^8.0.0",
    "eslint-config-next": "^15.0.0",
    "@testing-library/react": "^14.0.0",
    "jest": "^29.0.0",
    "@playwright/test": "^1.40.0"
  }
}
```

---

## 4. سیستم کامپوننت‌ها

### اماچ کامپوننت‌های پایه (shadcn/ui)

```bash
# نصب shadcn/ui
npx shadcn-ui@latest init

# کامپوننت‌های مورد نیاز
npx shadcn-ui@latest add button
npx shadcn-ui@latest add card
npx shadcn-ui@latest add form
npx shadcn-ui@latest add input
npx shadcn-ui@latest add table
npx shadcn-ui@latest add dialog
npx shadcn-ui@latest add toast
npx shadcn-ui@latest add dropdown-menu
npx shadcn-ui@latest add tabs
npx shadcn-ui@latest add sheet
npx shadcn-ui@latest add calendar
npx shadcn-ui@latest add avatar
npx shadcn-ui@latest add badge
```

### کامپوننت‌های Custom

```
src/components/
├── ui/                    # shadcn/ui components
│   ├── button.tsx
│   ├── card.tsx
│   ├── input.tsx
│   └── ...
├── features/             # Feature-specific components
│   ├── members/
│   │   ├── MemberList.tsx
│   │   ├── MemberForm.tsx
│   │   └── MemberCard.tsx
│   ├── workouts/
│   │   ├── WorkoutList.tsx
│   │   ├── ProgramPlayer.tsx
│   │   └── ExerciseDetail.tsx
│   ├── sessions/
│   │   ├── SessionCalendar.tsx
│   │   ├── BookingCard.tsx
│   │   └── RoomList.tsx
│   └── billing/
│       ├── InvoiceList.tsx
│       ├── PaymentMethodCard.tsx
│       └── SubscriptionCard.tsx
└── layouts/
    ├── AppSidebar.tsx
    ├── AppHeader.tsx
    └── DashboardShell.tsx
```

###同名Composition Pattern

```tsx
// src/components/features/members/MemberList.tsx
import { Button } from '@/components/ui/button';
import { MemberCard } from './MemberCard';
import { MemberFilters } from './MemberFilters';
import { useMembers } from '@/hooks/useMembers';

interface MemberListProps {
  gymId: string;
}

export function MemberList({ gymId }: MemberListProps) {
  const { data: members, isLoading } = useMembers(gymId);

  if (isLoading) return <MemberListSkeleton />;
  if (!members?.length) return <EmptyMembers />;

  return (
    <div className="space-y-4">
      <MemberFilters />
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
        {members.map(member => (
          <MemberCard key={member.id} member={member} />
        ))}
      </div>
    </div>
  );
}
```

---

## 5. مدیریت State

### Zustand Store Pattern

```typescript
// src/stores/useAuthStore.ts
import { create } from 'zustand';
import { persist } from 'zustand/middleware';

interface AuthState {
  user: User | null;
  token: string | null;
  login: (email: string, password: string) => Promise<void>;
  logout: () => void;
}

export const useAuthStore = create<AuthState>()(
  persist(
    (set) => ({
      user: null,
      token: null,
      login: async (email, password) => {
        const response = await apiFetch<LoginResponse>('/auth/login', {
          method: 'POST',
          body: JSON.stringify({ email, password }),
        });
        set({ user: response.user, token: response.token });
      },
      logout: () => set({ user: null, token: null }),
    }),
    { name: 'auth-storage' }
  )
);
```

---

## 6. یکپارچگی با Backend

### API Client

```typescript
// src/lib/api/client.ts
const API_BASE = process.env.NEXT_PUBLIC_API_URL!;

export async function apiFetch<T>(
  endpoint: string,
  options?: RequestInit
): Promise<T> {
  const response = await fetch(`${API_BASE}${endpoint}`, {
    ...options,
    headers: {
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${getToken()}`,
      ...options?.headers,
    },
  });

  if (!response.ok) {
    const error = await response.json();
    throw new ApiError(error.detail, response.status);
  }

  return response.json();
}
```

### Custom Hooks

```typescript
// src/hooks/useWorkouts.ts
export function useWorkouts(memberId: string) {
  return useQuery({
    queryKey: ['workouts', memberId],
    queryFn: () => apiFetch<Workout[]>(`/v1/members/${memberId}/workouts`),
    enabled: !!memberId,
  });
}

export function useCreateWorkout() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (data: CreateWorkoutDto) =>
      apiFetch<Workout>('/v1/workouts', {
        method: 'POST',
        body: JSON.stringify(data),
      }),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['workouts'] });
    },
  });
}
```

---

## 7. مسیریابی و دسترسی

### Route Groups بر اساس نقش

```
src/app/
├── (auth)/                       # بدون نیاز به ورود
│   ├── login/page.tsx
│   └── register/page.tsx
├── (owner)/                      # Gym Owner
│   ├── dashboard/page.tsx
│   ├── members/page.tsx
│   └── billing/page.tsx
├── (trainer)/                    # Trainer
│   ├── dashboard/page.tsx
│   ├── clients/page.tsx
│   └── programs/page.tsx
├── (member)/                     # Member
│   ├── dashboard/page.tsx
│   ├── workouts/page.tsx
│   └── progress/page.tsx
└── (admin)/                      # Platform Admin
    ├── dashboard/page.tsx
    ├── gyms/page.tsx
    └── settings/page.tsx
```

### Middleware برای Role Guard

```typescript
// src/middleware.ts
export function middleware(request: NextRequest) {
  const token = request.cookies.get('access_token')?.value;
  const userRoles = getUserRolesFromToken(token);

  if (request.nextUrl.pathname.startsWith('/dashboard/owner')
      && !userRoles.includes('GymOwner')) {
    return NextResponse.redirect(new URL('/unauthorized', request.url));
  }

  return NextResponse.next();
}
```

---

## 8. طراحی واکنش‌گرا

### Breakpoints

```typescript
// Mobile-first with Tailwind
className="
  flex flex-col           # mobile: vertical
  md:flex-row            # tablet: horizontal
  lg:gap-8               # desktop: wider gap
"
```

### رزولوشن‌های هدف

| دستگاه | عرض | ویژگی‌های اصلی |
|----------|------|----------------|
| Mobile   | < 640px | Bottom nav، کارت‌های تک ستونه |
| Tablet   | 640-1024px | Sidebar collapsed، grid ۲ ستونه |
| Desktop  | > 1024px | Full sidebar، grid کامل |

---

## 9. تست‌نویسی

### Unit Test Example

```typescript
import { render, screen } from '@testing-library/react';
import { MemberCard } from './MemberCard';

describe('MemberCard', () => {
  it('renders member information', () => {
    const member = { id: '1', fullName: 'علی', email: 'ali@example.com', isActive: true };
    render(<MemberCard member={member} />);
    expect(screen.getByText('علی')).toBeInTheDocument();
  });
});
```

### Playwright E2E

```typescript
// tests/e2e/auth.spec.ts
test('user can login', async ({ page }) => {
  await page.goto('/login');
  await page.fill('[name="email"]', 'test@gym.test');
  await page.fill('[name="password"]', 'password');
  await page.click('button[type="submit"]');
  await expect(page).toHaveURL('/dashboard/member');
});
```

---

## 10. بهینه‌سازی و Performance

### استراتژی‌های بهینه‌سازی

| تکنیک | هدف | پیاده‌سازی |
|--------|------|---------|
| Image Optimization | LCP بهبود | `next/image` با automatic sizing |
| Font Optimization | FOIT جلوگیری | `next/font` باself-hosted fonts |
| Code Splitting | Bundle size کاهش | Dynamic imports برای heavy components |
| Route Prefetching | Navigation بهبود | Next.js automatic prefetching |
| TanStack Query Caching | API calls کاهش | Stale-while-revalidate |
| Memoization | Re-render جلوگیری | `useMemo`, `useCallback` برای expensive computations |
| Virtual Lists | Listهای بزرگ | `@tanstack/react-virtual` |

---

## 11. استانداردهای کد

### قوانین اصلی

```bash
# Lint
npm run lint

# Type Check
npm run typecheck

# Format
npm run format
```

### Checklist Code Review

- [ ] TypeScript errors صفر
- [ ] ESLint errors صفر
- [ ] کامپوننت‌های جدید دارای تست unit هستند
- [ ] از `any` استفاده نشده
- [ ] All async operations دارای loading state هستند
- [ ] Error states handle شده
- [ ] Responsive در همه breakpoints
- [ ] Accessibility (keyboard navigation، aria-labels)

---

## 12. Deploy

### Vercel (پیشنهادی)

```bash
# Deploy
vercel --prod

# یا با GitHub Actions
# .github/workflows/frontend.yml
name: Deploy Frontend
on:
  push:
    branches: [main]
    paths: ['gymplatform-web/**']
jobs:
  deploy:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4
      - uses: actions/setup-node@v4
        with: { node-version: 20 }
      - run: cd gymplatform-web && npm ci && npm run build
      - uses: amondnet/vercel-action@v20
        with:
          vercel-token: ${{ secrets.VERCEL_TOKEN }}
          vercel-org-id: ${{ secrets.VERCEL_ORG_ID }}
          vercel-project-id: ${{ secrets.VERCEL_PROJECT_ID }}
```

### محیط‌های توسعه

| محیط | آدرس | داده‌ها |
|-------|------|---------|
| Local | localhost:3000 | Local Backend |
| Staging | staging.gymplatform.app | Staging Backend |
| Production | gymplatform.app | Production Backend |

---

## منابع اضافی

| سند | توضیحات |
|------|---------|
| `docs/frontend/FRONTEND_DEVELOPER_GUIDE_FA.md` | راهنمای فارسی Frontend |
| `.ai/context/UI_UX_BLUEPRINT.md` | طراحی UI/UX |
| `.ai/context/API_BLUEPRINT.md` | مشخصات API |
| `docs/UI_UX.md` | Design system fundamentals |
| `.ai/context/IMPLEMENTATION_MASTER_PLAN.md` | نقشه راه اجرایی |

---

*این دفترچه راه بر اساس وضعیت پروژه به تاریخ ۳ ژوئیه ۲۰۲۶ تهیه شده است.*
