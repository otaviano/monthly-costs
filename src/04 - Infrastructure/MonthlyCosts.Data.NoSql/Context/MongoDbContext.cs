using MongoDB.Driver;
using MonthlyCosts.Domain.Settings;

namespace Stocks.Billing.Infra.Data.MongoDb.Context
{
    public class MongoDbContext<T>
    {
        private IMongoDatabase MongoDatabase { get; set; }
        public IMongoCollection<T> Collection { get; private set; }

        public MongoDbContext(MongoDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            MongoDatabase = client.GetDatabase(settings.Database);

            Collection = MongoDatabase.GetCollection<T>(typeof(T).Name);
        }
    }
}