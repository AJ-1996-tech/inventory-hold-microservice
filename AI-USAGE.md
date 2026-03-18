# AI Usage Documentation

# AI Strategy
Used Copilot + Cursor for:
- Repository pattern generation
- Mongo queries
- React components

# Human Audit
Rejected:
- AI suggested non-atomic updates → replaced with FindOneAndUpdate

Accepted:
- DTO + controller scaffolding

# Verification
- Generated tests using AI
- Manually validated edge cases

Approach:
- Used AI to scaffold boilerplate code
- Provided structured prompts to enforce Clean Architecture
- Broke down features into smaller tasks (API, caching, messaging)

---

## Human Audit

### Accepted AI Suggestions
- DTO and controller scaffolding
- React component structure
- Basic repository pattern

### Rejected / Improved
- AI suggested non-atomic Mongo updates → replaced with atomic operations using FindOneAndUpdate
- Improved error handling with middleware
- Added null-safe Redis fallback
- Refined architecture to strictly follow DDD

---

## Verification

- Generated unit tests with AI assistance
- Manually validated:
  - Edge cases (expired holds)
  - Concurrency scenarios
  - API responses

- Tested full system:
  - Create hold
  - Release hold
  - Expiry worker
  - Frontend sync

---

## AI Limitations Observed

- AI sometimes ignored concurrency requirements
- Suggested overly simplified implementations
- Required manual corrections for production readiness

---

## Conclusion

AI significantly improved development speed, but human validation was essential to ensure correctness, performance, and architectural quality.
