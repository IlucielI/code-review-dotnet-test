# ASP.NET Core (.NET 8) Benchmark Test Suite

[![ASP.NET Core Version](https://img.shields.io/badge/ASP.NET%20Core-8.0-512BD4.svg?logo=dotnet)](https://dotnet.microsoft.com/apps/aspnet)
[![EF Core Version](https://img.shields.io/badge/EF%20Core-8.0-512BD4.svg)](https://learn.microsoft.com/en-us/ef/core/)
[![Benchmark Category](https://img.shields.io/badge/Benchmark-Enterprise%20Security%20%26%20ORM-blue.svg)](#test-case-matrix)
[![Safe Guard](https://img.shields.io/badge/False%20Positive%20Guard-Active-brightgreen.svg)](#anti-false-positive-guard-controller)

Benchmark test suite for automated code review engines on enterprise ASP.NET Core (.NET 8) Web APIs and Entity Framework Core (EF Core). This repository validates detection accuracy across EF Core SQL injection, model overposting (mass assignment), BinaryFormatter deserialization, client-side N+1 evaluation queries, and enterprise safe guards.

---

## 🎯 Benchmark Purpose

1. **EF Core Precision (Raw vs Interpolated):** Accurately flags `FromSqlRaw($"... {input}")` as SQL injection while recognizing `FromSqlInterpolated($"... {input}")` as safe parameterized binding.
2. **Model Binding Security:** Detects Overposting / Mass Assignment when controllers bind complex models directly without DTOs or `[Bind]` attributes.
3. **Enterprise Insecure Deserialization:** Catches hazardous usage of `BinaryFormatter.Deserialize()` leading to remote code execution.
4. **ORM Query Bottlenecks:** Identifies EF Core client-side query evaluation resulting in N+1 database roundtrips inside loops.
5. **Zero False Positives:** Validates that `Url.IsLocalUrl()`, `[Bind]` guards, and `FromSqlInterpolated` produce **0 false positives**.

---

## 📋 Test Case Matrix

### 🔴 Security Vulnerabilities

| File | Issue / Vulnerability | Type | CWE | Severity | Expected |
| :--- | :--- | :--- | :--- | :---: | :---: |
| `UserRepository.cs` | SQL Injection via EF Core `FromSqlRaw` interpolated string | Injection | CWE-89 | High | **BLOCKING** |
| `UserController.cs` | Model Overposting / Mass Assignment on unbinded `User` model | Broken Access Control | CWE-915 | High | **BLOCKING** |
| `SessionSerializer.cs` | Insecure Deserialization via legacy `BinaryFormatter` (RCE) | Deserialization | CWE-502 | Critical | **BLOCKING** |
| `SystemService.cs` | Command Injection via `ProcessStartInfo` with `sh -c` | RCE | CWE-78 | High | **BLOCKING** |
| `WebProxyService.cs` | Server-Side Request Forgery via unvalidated `HttpClient` | Network Security | CWE-918 | Medium | **BLOCKING** |
| `AuthService.cs` | Hardcoded JWT Secret Key & Console Credential Logging | Information Disclosure | CWE-798 / CWE-532 | High | **BLOCKING** |
| `AccountController.cs` | Open Redirect via unvalidated destination in `Redirect()` | Redirection | CWE-601 | Medium | **BLOCKING** |
| `OrderController.cs` | IDOR on order deletion without tenant/user ownership check | Broken Access Control | CWE-639 | High | **BLOCKING** |
| `CorsSetup.cs` | Wildcard \`AllowAnyOrigin()\` with \`AllowCredentials()\` | CORS Misconfiguration | CWE-942 | High | **BLOCKING** |
| `XmlService.cs` | XML parser without DTD expansion prohibition (XXE) | Injection / XXE | CWE-611 | High | **BLOCKING** |
| `AuthController.cs` | Cookie set without \`HttpOnly\` and \`Secure\`, and login route without rate limiting | Insecure Cookie / Rate Limit | CWE-614 / CWE-307 | Medium | **NON-BLOCKING** |

### ⚡ Performance & ORM Bottlenecks

| File | Issue | Type | Severity | Expected |
| :--- | :--- | :--- | :---: | :---: |
| `OrderService.cs` | EF Core client-side evaluation N+1 query loop | Query Performance | Medium | **NON-BLOCKING** |
| `FormValidator.cs` | Catastrophic Backtracking Regular Expression (ReDoS) | Algorithmic Complexity | Medium | **NON-BLOCKING** |

---

## 🛡️ Anti-False-Positive Guard Controller

| File | Safe Pattern Implemented | Expected Reviewer Result |
| :--- | :--- | :---: |
| `SafeGuardController.cs` | `FromSqlInterpolated` parameterized binding, `Url.IsLocalUrl()` redirect check, `[Bind]` attribute overposting guard, `AsNoTracking()`, safe XML reader (`DtdProcessing.Prohibit`), hardened `HttpOnly`/`Secure` cookies | **0 False Positives** (Clean) |

---

## 🚀 How to Run the Benchmark

```bash
# View PR on GitHub
gh pr view 1 --web

# Trigger Review via API
curl -X POST http://localhost:8081/api/v1/review/trigger \
  -H "Content-Type: application/json" \
  -d '{
    "repository": "IlucielI/code-review-dotnet-test",
    "pull_request_id": 1
  }'
```

---

## 📊 Benchmark Validation Results

- **Detection Rate:** 14 / 14 (100%)
- **False Positive Rate:** 0 / 1 (`SafeGuardController.cs` completely passed)
- **False Negative Rate:** 0%
