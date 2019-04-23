## 建立資料庫

### Table

```sql
USE [MARS]
GO

/****** Object:  Table [dbo].[Member]    Script Date: 4/7/2019 5:52:11 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Member](
	[MemberId] [bigint] IDENTITY(1,1) NOT NULL,
	[MemberName] [nvarchar](20) NOT NULL,
	[MemberAccount] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Member] PRIMARY KEY CLUSTERED 
(
	[MemberId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

```

## Code First

1. 準備 Entity
2. 建立 DBContext
3. Migration
   - 建立 Migration   
    ```shell
    > dotnet ef migrations add Migration_creta_member
    ```

   - 執行 Migration
    ```shell
    > dotnet ef database update
    ```

## 無中生有

[工具] –> [NuGet 套件管理員] –> [套件管理員主控台]

### 安裝 EntityFrameworkCore.Design

```shell
Install-Package Microsoft.EntityFrameworkCore.Design
```

### 安裝 DB Providers

```shell
Install-Package Microsoft.EntityFrameworkCore.SqlServer
```

更多請參考[資料庫提供者 - EF Core](https://docs.microsoft.com/zh-tw/ef/core/providers/index)

###

```shell
Scaffold-DbContext "Server=localhost;Database=MARS;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
```



## 參考
- [如何在 Entity Framework Core 使用 Migration ? (PostgreSQL)](https://oomusou.io/efcore/migration/)