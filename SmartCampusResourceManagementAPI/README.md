# Smart Campus Resource Management API

Web API quan ly tai nguyen hoc tap cho truong dai hoc: CRUD + Repository Pattern + OData + Validation + JWT Authentication/Authorization + JavaScript Client.

## Cong nghe

- ASP.NET Core Web API (.NET 8)
- Entity Framework Core (In-Memory provider)
- Microsoft.AspNetCore.OData
- JWT Bearer Authentication
- Swagger (Swashbuckle)
- HTML + JavaScript (Fetch API va XMLHttpRequest)

## Cau truc project

```
Controllers/        REST controllers (Auth, ResourceCategories, LearningResources, Reports)
Controllers/OData/   OData controller (/odata/LearningResources)
Models/              Entities: ResourceCategory, LearningResource, UserAccount + enums
DTOs/                LoginRequest/Response, CategoryDTO, CreateResourceDTO, UpdateResourceDTO, validation attributes
Repositories/        EF Core repository implementations
Repositories/Interfaces/  Repository interfaces (Controller chi goi Repository, khong dung DbContext truc tiep)
Services/            JwtTokenService, PasswordHasher
Data/                AppDbContext, DbSeeder, OData EDM model builder
wwwroot/index.html   JavaScript client
```

## Chay project

```bash
cd SmartCampusResourceManagementAPI
dotnet run
```

Swagger UI: `https://localhost:<port>/swagger`
JavaScript client: `https://localhost:<port>/index.html`

Du lieu duoc seed tu dong vao In-Memory database khi ung dung khoi dong (xem `Data/DbSeeder.cs`).

## Tai khoan mau

| Email | Password | Role | Department |
|---|---|---|---|
| admin@campus.edu | 123456 | Admin | IT |
| staff@campus.edu | 123456 | Staff | Academic |
| itstaff@campus.edu | 123456 | Staff | IT |

## Authentication

`POST /api/auth/login` voi `{ "email": "...", "password": "..." }` tra ve JWT token chua claim Email, Role, Department. Token het han sau 30 phut (cau hinh trong `appsettings.json` muc `Jwt`).

Goi Protected API can gui header: `Authorization: Bearer <token>`

## Authorization

| Endpoint | Quyen |
|---|---|
| GET /api/resources/public | Public |
| GET /api/resources/manage | Can dang nhap |
| POST /api/resources | Admin |
| PUT /api/resources/{id} | Admin, Staff |
| DELETE /api/resources/{id} | Admin |
| GET /api/reports/it-only | Policy: Department = IT |

## CRUD API

- `ResourceCategory`: `GET/POST/PUT/DELETE /api/categories`, `GET /api/categories/{id}` (khong the xoa Category dang co LearningResource su dung).
- `LearningResource`: `GET /api/resources/public`, `GET /api/resources/manage`, `GET/POST/PUT/DELETE /api/resources/{id}`.

## Validation

- `LearningResource.Title`: bat buoc, 5-120 ky tu.
- `LearningResource.ContentUrl`: bat buoc, dung dinh dang URL.
- `LearningResource.PublishedDate`: khong duoc lon hon ngay hien tai.
- `LearningResource.Status`: chi nhan 0 hoac 1.
- `ResourceCategory.CategoryName`: bat buoc.
- `UserAccount.Email`: dung dinh dang email.

## OData

Endpoint: `/odata/LearningResources`

Ho tro: `$select`, `$filter`, `$orderby`, `$top`, `$skip`, `$count`, `$expand=Category`.

Vi du:

```
GET /odata/LearningResources?$filter=Status eq 1&$select=Title,PublishedDate&$orderby=PublishedDate desc
GET /odata/LearningResources?$expand=Category&$top=2&$count=true
```

## Test Cases

1. Login dung (`admin@campus.edu` / `123456`) tra ve token.
2. Login sai tra ve 401.
3. `GET /api/resources/public` khong can token.
4. `GET /api/resources/manage` khong co token tra 401.
5. Goi API sai Role (vi du Staff goi `POST /api/resources`) tra 403.
6. Admin tao Resource (`POST /api/resources`) thanh cong.
7. Staff cap nhat Resource (`PUT /api/resources/{id}`) thanh cong.
8. Admin xoa Resource (`DELETE /api/resources/{id}`) thanh cong.
9. Department IT (`admin@campus.edu`, `itstaff@campus.edu`) truy cap `GET /api/reports/it-only` thanh cong.
10. Department khac (`staff@campus.edu`, Academic) goi `GET /api/reports/it-only` bi 403.
11. OData voi `$filter`, `$select`, `$orderby` tra ve du lieu dung dinh dang.
12. Validation sai (Title qua ngan, ContentUrl khong dung dinh dang, Status khac 0/1, PublishedDate trong tuong lai) tra ve 400 Bad Request.
