# Rideshare.Uber.Sdk
C# Uber SDK for API v1.2

Accessing Uber API using a server token with `ServerAuthenticatedUberRiderService` has been updated to use API v1.2 and has all endpoints available with just server token authentication implemented. `UberAuthenticationClient` can be used to generate OAuth authorization urls to get client token.

---

Done:
- [x] Implement endpoints that only require server token authentication
- [x] Implement client for generating authentication urls

TODO:
- [ ] Implement endpoints that require OAuth authentication for Riders
- [ ] Implement Driver endpoints
- [ ] Implement Uber for Business endpoints
- [ ] Improve documentation and give samples
