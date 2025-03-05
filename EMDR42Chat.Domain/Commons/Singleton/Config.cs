
namespace EMDR42Chat.Domain.Commons.Singleton;

public class Config
{

    //Database
    public string DbHost => Environment.GetEnvironmentVariable("DB_HOST");
    public string DbUser => Environment.GetEnvironmentVariable("DB_USER");
    public string DbPassword => Environment.GetEnvironmentVariable("DB_PASSWORD");
    public string DbName => Environment.GetEnvironmentVariable("DB_CHAT_NAME");
    public string DbPort => Environment.GetEnvironmentVariable("DB_PORT");
}
