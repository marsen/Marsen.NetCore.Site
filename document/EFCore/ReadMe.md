#

## 建立資料庫

### Table

```sql
USE [MARS]
GO

/****** Object:  Table [dbo].[Member]    Script Date: 4/23/2019 11:19:15 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Member](
    [Member_Id] [bigint] IDENTITY(1,1) NOT NULL,
    [Member_Name] [nvarchar](20) NOT NULL,
    [Member_Account] [varchar](20) NOT NULL,
    CONSTRAINT [PK_Member] PRIMARY KEY CLUSTERED
(
    [Member_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

```

## Code First

1. 準備 Entity

    ```csharp
        public class Shop
        {
            public long Shop_Id { get; set; }
            public string Shop_Title { get; set; }
            public bool Shop_IsEnable { get; set; }

        }
    ```

2. 建立 DBContext

    記得要明確實作無參數的建構子

    ```csharp
        public class PhobosContext:DbContext
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="PhobosContext" /> class.
            /// </summary>
            public PhobosContext()
            { }

            public PhobosContext(DbContextOptions<PhobosContext> options)
                : base(options)
            { }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                if (!optionsBuilder.IsConfigured)
                {
                    optionsBuilder.UseSqlServer("Server=localhost;Database=Phobos;Trusted_Connection=True;");
                }
            }

            public DbSet<Shop> Shop { get; set; }
        }
    ```

3. Migration
   - 切換到 DA 專案底下

    ```shell
    > cd .\src\Marsen.NetCore.DA
    ```

   - 建立 Migration
    必要時，明確指定`Context`

    ```shell
    > dotnet ef migrations add Migration_creta_shop --context PhobosContext  
    ```

   - 執行 Migration
    必要時，明確指定`Context`

    ```shell
    > dotnet ef database update --context PhobosContext
    ```

## Database First

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

### Scaffold

```shell
Scaffold-DbContext "Server=localhost;Database=MARS;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
```

## 參考

- [如何在 Entity Framework Core 使用 Migration ? (PostgreSQL)](https://old-oomusou.goodjack.tw/efcore/migration/)

(fin)
