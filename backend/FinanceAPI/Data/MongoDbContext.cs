using FinanceAPI.Models;
using FinanceAPI.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FinanceAPI.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IOptions<MongoDbSettings> settings, IMongoClient mongoClient)
        {
            var mongoSettings = settings.Value;
            _database = mongoClient.GetDatabase(mongoSettings.DatabaseName);
        }

        public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
        public IMongoCollection<Transaction> Transactions => _database.GetCollection<Transaction>("Transactions");
        public IMongoCollection<BudgetCategory> BudgetCategories => _database.GetCollection<BudgetCategory>("BudgetCategories");
        public IMongoCollection<Alert> Alerts => _database.GetCollection<Alert>("Alerts");
    }
}
