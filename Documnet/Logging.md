# Logging

## 概念

## 預設的行為
The default project template calls `CreateDefaultBuilder`, which adds the following logging providers:

- Console
- Debug
- EventSource (starting in ASP.NET Core 2.2)

```csharp
public static void Main(string[] args)
{
    CreateWebHostBuilder(args).Build().Run();
}

public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
    WebHost.CreateDefaultBuilder(args)
        .UseStartup<Startup>();
```

### Find the Logs

#### Console

範例說明:在建構子中注入 `ILogger` 實體，運行網站後連到 Home\Index 頁面，並觀察 Console

```csharp
public class HomeController : Controller
{
    private readonly ILogger _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="HomeController" /> class.
    /// </summary>
    public HomeController(ILogger<HomeController> logger)
    {
        this._logger = logger;
    }

    public IActionResult Index()
    {
        this._logger.Log(LogLevel.Information,"HomeController Information");
        this._logger.Log(LogLevel.Critical,"HomeController Critical");
        this._logger.Log(LogLevel.Debug,"HomeController Debug");
        this._logger.Log(LogLevel.Error,"HomeController Error");
        this._logger.Log(LogLevel.None,"HomeController None");
        this._logger.Log(LogLevel.Trace,"HomeController Trace");
        this._logger.Log(LogLevel.Warning,"HomeController Warning");
        return View();
    }
}
```

結果如下，可以發現 `LogLevel.None`、`LogLevel.Trace` 與 `LogLevel.Warning` 並未出現在 Console 資訊當中

```
info: Marsen.NetCore.Site.Controllers.HomeController[0]
      HomeController Information
crit: Marsen.NetCore.Site.Controllers.HomeController[0]
      HomeController Critical
dbug: Marsen.NetCore.Site.Controllers.HomeController[0]
      HomeController Debug
fail: Marsen.NetCore.Site.Controllers.HomeController[0]
      HomeController Error
```

[LogLevel](https://docs.microsoft.com/zh-tw/dotnet/api/microsoft.extensions.logging.loglevel?view=aspnetcore-2.2)說明了 `None` 的意義就是不記錄任何訊息，

| Enum | Level |  |
| -------- | -------- | -------- |
| Trace    | 0    |Logs that contain the most detailed messages. These messages may contain sensitive application data. These messages are disabled by default and should never be enabled in a production environment.   |
| Debug     | 1     | Logs that are used for interactive investigation during development. These logs should primarily contain information useful for debugging and have no long-term value.     |
| Information     | 2     | Logs that track the general flow of the application. These logs should have long-term value.    |
|Warning |3|Logs that highlight an abnormal or unexpected event in the application flow, but do not otherwise cause the application execution to stop.|
| Error     | 4     | Logs that highlight when the current flow of execution is stopped due to a failure. These should indicate a failure in the current activity, not an application-wide failure.     |
| Critical     | 5     | Logs that describe an unrecoverable application or system crash, or a catastrophic failure that requires immediate attention.     |
| None     | 6     | Not used for writing log messages. Specifies that a logging category should not write any messages.    |

Log 的作用範圍會受 `appsettings.json` 影響,另外要注意 appsettings.json 的載入順序.
```json
  "Logging": {
    "LogLevel": {
      "Default": "Trace",
      "System": "Information",
      "Microsoft": "Information"
    }
```    

#### Debug

如同 `Console` 的行為一般，可以在 Visual Studio 的輸出(Output)>偵錯(Debug)視窗中，查詢到記錄。

![](https://i.imgur.com/274witm.jpg)


#### EventSource

如同[官方文件](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-2.2#eventsource-provider)所說，我下載了 `PerfView` ,
如下圖作了設定， 
![](https://i.imgur.com/WHRwxUG.jpg)
不過我並沒有取得記錄，
![](https://i.imgur.com/dY9x4PG.jpg)
錯誤訊息為 `EventSource Microsoft-Extensions-Logging: Object reference not set to an instance of an object`
暫時不打算深追查，ETW 可以記錄的 Memory 、Disc IO 、CPU 等資訊其實與我想要的應用程式記錄有所差異，稍稍記錄一下以後也許用得到。
如果有人能留言給我一些方向，也是非常歡迎。

## 自訂 Filelog 與 EventLog

調整一下程式


## 參考
- [Logging in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-2.2#log-scopes)
- [[鐵人賽 Day16] ASP.NET Core 2 系列 - 多重環境組態管理 (Multiple Environments)](https://blog.johnwu.cc/article/ironman-day16-asp-net-core-multiple-environments.html)
- [ASP.NET Core EventLog provider](https://stackoverflow.com/questions/47773058/asp-net-core-eventlog-provider)
- [ASP.NET Core: The MVC Request Life Cycle](http://www.techbloginterview.com/asp-net-core-the-mvc-request-life-cycle/)
- [ASP.NET Core Logging](https://codingblast.com/asp-net-core-logging/)
- [Monitoring and Observability in the .NET Runtime](https://mattwarren.org/2018/08/21/Monitoring-and-Observability-in-the-.NET-Runtime/)

(fin)
