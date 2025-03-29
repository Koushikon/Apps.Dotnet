using Microsoft.Extensions.Configuration;
using ReadJsonArray.Models;

namespace ReadJsonArray.Services;

public static class AppSettingsService
{
    public static IEnumerable<User> GetUsersV1(IConfiguration configuration)
    {
        IConfiguration userSection = configuration.GetSection("AppSettings:Users");
        IEnumerable<IConfigurationSection> usersArray = userSection.GetChildren();

        return usersArray.Select(config => new User
        (
            Id: int.Parse(config["Id"]!.ToString()),
            Name: config["Name"]!.ToString(),
            Role: config["Role"]!.ToString()
        ));
    }

    public static List<User>? GetUsersV2(IConfiguration configuration)
    {
        return configuration.GetSection("AppSettings:Users").Get<List<User>>();
    }

    public static IEnumerable<Member>? GetGroupMembers(IConfiguration configuration)
    {
        if (configuration.GetSection("AppSettings").Exists())
        {
            AppSettings? appSettings = configuration.GetSection("AppSettings")?.Get<AppSettings>();

            return appSettings?.Groups?.SelectMany(x => x.Members!);
        }
        else
        {
            throw new KeyNotFoundException();
        }
    }
}