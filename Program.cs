using DeepSeekDemo.Models;
using DeepSeekDemo.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var serviceProvider = new ServiceCollection()
    .AddSingleton<IConfiguration>(configuration)
    .AddSingleton<DeepSeekService>()
    .BuildServiceProvider();

var deepSeekService = serviceProvider.GetRequiredService<DeepSeekService>();

var response = await deepSeekService.SendMessageAsync("");
ConsoleHelper.PrintBotResponse(response);
do
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write("You: ");
    Console.ResetColor();

    var userInput = Console.ReadLine();

    if (userInput?.ToLower() == "exit")
    {
        Console.WriteLine("Goodbye! It was a pleasure to debate with you.");
        break;
    }

    if (!string.IsNullOrEmpty(userInput))
    {
        response = await deepSeekService.SendMessageAsync(userInput);
        ConsoleHelper.PrintBotResponse(response);
    }

} while (true);