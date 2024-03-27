using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using Net7.Wasm.Models;

namespace Net7.Wasm;

public class Program
{
    private static IJSRuntime? js;

    private static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        var host = builder.Build();
        js = host.Services.GetRequiredService<IJSRuntime>();
        await host.RunAsync();
    }

    [JSInvokable]
    public static int Add(int i, int j)
    {
        return i + j;
    }

    [JSInvokable]
    public static string GetMessage(string name)
    {
        return $"Hello {name}!";
    }

    [JSInvokable]
    public static async Task<string> CheckBookingAsync(BookingModel booking)
    {
        var time = await GetTimeViaJS();
        return $@"Current time is {time.ToString("yyyy-MM-dd HH:mm:ss")}
{booking.prod_name}, {booking.qty} * {booking.price} = {booking.amount}";
    }

    public static async Task<DateTime> GetTimeViaJS()
    {
        return await js.InvokeAsync<DateTime>("getTime");
    }
}