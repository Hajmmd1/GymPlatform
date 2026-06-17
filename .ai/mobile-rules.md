# Mobile Rules

## Purpose
Defines mobile-specific architecture, implementation, and platform guidelines for the GymPlatform mobile application.

## Scope
React Native mobile app, native modules, offline capabilities, and mobile platform integration.

## Owner
Mobile Architecture Lead

## Status
Draft

## Last Updated
2026-06-17

---

## Table of Contents
1. [Technology Stack](#technology-stack)
2. [Architecture Patterns](#architecture-patterns)
3. [Offline-First Strategy](#offline-first-strategy)
4. [Platform Integration](#platform-integration)
5. [Performance Requirements](#performance-requirements)
6. [Security Requirements](#security-requirements)
7. [App Store Guidelines](#app-store-guidelines)
8. [Testing Strategy](#testing-strategy)

---

## Technology Stack

### Core Framework
- React Native 0.74+
- TypeScript 5+ strict mode
- Expo SDK 51+ (if using Expo)

### Navigation
- React Navigation v7 (native stack navigator)
- Deep linking support
- Type-safe navigation params

### State Management
- Redux Toolkit or Zustand
- MMKV for secure storage
- TanStack Query for server state

### UI Library
- NativeBase or React Native Paper
- Custom design system components
- Responsive and adaptive layouts

---

## Architecture Patterns

### Modular Architecture
```
/src
  /components     # Shared mobile components
  /screens        # Screen components
  /navigation     # Navigation configuration
  /hooks          # Custom hooks
  /services       # API and native service wrappers
  /store          # Redux/Zustand stores
  /utils          # Utility functions
/ios              # iOS native code
/android          # Android native code
```

### Platform-Specific Code
- Platform abstraction layer
- Native modules for platform-specific features
- Conditional rendering via `Platform.select`

### Code Sharing
- Shared business logic with backend via API
- Shared design tokens via style dictionary
- Common utilities abstracted

---

## Offline-First Strategy

### Data Synchronization
- Redux Persist or WatermelonDB
- Conflict resolution strategy
- Background sync when connectivity restored

### Local Storage
- SQLite for structured offline data
- Secure storage for tokens
- MMKV for key-value pairs

### Queue Pattern
- Offline actions queued
- Network status detection
- Automatic retry with exponential backoff

---

## Platform Integration

### Push Notifications
- Firebase Cloud Messaging (Android)
- Apple Push Notification Service (iOS)
- Notification handling in foreground/background

### Biometric Authentication
- Expo Local Authentication or react-native-keychain
- Face ID / Touch ID / Fingerprint
- Fallback to PIN/password

### Device Features
- Camera integration for profile photos
- GPS for location-based features
- Contacts for social features

---

## Performance Requirements

### App Size
- Initial bundle < 50MB
- Code splitting for feature modules
- Asset compression and optimization

### Rendering Performance
- 60fps target for animations
- Virtualized lists for long content
- Memoization for expensive renders

### Battery Optimization
- Background tasks minimized
- Location services optimized
- Network requests batched

---

## Security Requirements

### Secure Storage
- Keychain (iOS) or Keystore (Android)
- JWT tokens in secure storage
- Biometric protection for sensitive data

### Network Security
- Certificate pinning
- SSL enforcement
- Request signing

### Runtime Security
- Root/Jailbreak detection
- Code obfuscation
- Tamper detection

---

## App Store Guidelines

### iOS App Store
- App Store Review Guidelines compliance
- Privacy manifest for data collection
- App tracking transparency framework

### Google Play
- Play Store policy compliance
- Data safety section filled
- Target API level requirements

### Release Process
- Beta testing via TestFlight / Google Play Console
- Staged rollout strategy
- Release notes for each version

---

## Testing Strategy

### Unit Tests
- Jest for business logic
- React Native Testing Library
- 80% coverage target

### Integration Tests
- API integration tests
- Native module mocking
- Device event testing

### E2E Tests
- Detox for native E2E
- Critical user flows
- Device matrix testing

---

## Sections Prepared for Future Content

### Native Module Architecture
*To be defined*

### Cross-Platform Strategy
*To be defined*

### App Performance Monitoring
*To be defined*

### Crash Reporting and Analytics
*To be defined*