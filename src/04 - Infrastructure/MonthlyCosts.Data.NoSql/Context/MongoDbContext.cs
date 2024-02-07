using MongoDB.Driver;
using Stocks.Billing.Domain.Settings;

namespace Stocks.Billing.Infra.Data.NoSql.Context
{
    public class MongoDbContext<T>
    {
        private readonly IMongoDatabase _mongoDatabase;
        public IMongoCollection<T> Collection { get; private set; }

        public MongoDbContext(MongoDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            _mongoDatabase = client.GetDatabase(settings.Database);

            Collection = _mongoDatabase.GetCollection<T>(typeof(T).Name);
        }
    }
}