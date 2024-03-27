C# 實作 WebAssembly，需在 VisualStudio 2022 for Mac 的 IDE 中建立兩個 .NET 7 的專案，分別是 Class Library (Net7.Wasm) 與 ASP.NET Core WebApp (Net7.Web.Test)。

重點如下:
* Class Library 要能順利建立 WASM 模組，得調整 .csproj 內容。具體可參考 Zack Yang 的[技術文章](https://yangzhongke8.medium.com/without-blazor-webassembly-develop-a-web-site-that-compiles-and-runs-c-code-on-browser-c381873f6d03)進行修正。 
* ASP.NET Core WebApp 專案最重要的關鍵之一是在 .cshtml 頁面上引用 WASM 的 JavaScript ，以及調用 WASM Methods。
```
<script src="~/_framework/blazor.webassembly.js" autostart="false" asp-append-version="true"></script>
<script>
   window.onload = async function () {
      await Blazor.start();
      const r = await DotNet.invokeMethodAsync("Net7.Wasm", "Add", 666, 333);
   };
</script>
```
按照 Zack Yang 的文章，仍是無法在 VS2022 for Mac IDE 執行，因為還缺少關鍵性第二個動作。在參考 Elringus 在 Stack Overflow [技術文章](https://stackoverflow.com/questions/69743254/how-to-use-c-sharp-webassemly-from-javascript-without-blazor-web-components) 得解，原來要在 Program 上面加入 UseBlazorFrameworkFiles() 才能順利啟動，而且該 Method 必須放在 UseStaticFiles() 之前。

按照前述動作，我終於也進入了 WASM 領域!!! 但是前端 MVVM 的框架 AngularJS/ReactJS/Vue.js 目前未推出 WASM 版本，只能在其 Methods 中調用 WASM 以支援各式應用，至少也得到端點保護效果。
