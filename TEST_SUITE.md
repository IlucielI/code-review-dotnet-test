# ASP.NET Core (.NET 8) Benchmark Test Suite Documentation

Dokumentasi suite pengujian kerentanan keamanan dan performa pada ASP.NET Core (.NET 8).

## Daftar Test Case

| File | Kategori | Deskripsi Masalah | Tingkat Risiko |
| :--- | :--- | :--- | :--- |
| `UserRepository.cs` | Security | Raw SQL injection via EF Core `FromSqlRaw` | High |
| `UserController.cs` | Security | Overposting / Mass Assignment on unbinded model | High |
| `SessionSerializer.cs` | Security | Insecure deserialization via `BinaryFormatter` (RCE) | High |
| `SystemService.cs` | Security | Command injection via `Process.Start` with `sh -c` | High |
| `WebProxyService.cs` | Security | Server-Side Request Forgery via unvalidated `HttpClient` | Medium |
| `AuthService.cs` | Security | Hardcoded JWT secret key & credential console logging | High |
| `AccountController.cs` | Security | Open redirect via unvalidated `Redirect()` | Medium |
| `OrderController.cs` | Security | IDOR endpoint penghapusan order tanpa validasi otentikasi/ownership | High |
| `OrderService.cs` | Performance | EF Core query evaluation N+1 loop pattern | Medium |
| `FormValidator.cs` | Performance | Catastrophic backtracking ReDoS regex pattern | Medium |

## False-Positive Guard Files

| File | Pola Pengujian Guard | Ekspektasi Reviewer |
| :--- | :--- | :--- |
| `SafeGuardController.cs` | `FromSqlInterpolated`, `Url.IsLocalUrl`, `[Bind]` attribute overposting guard, `AsNoTracking` | **0 False Positives** |
