using Microsoft.Extensions.Options;
using Model.Models;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class DBContextMongo
    {
        private readonly IMongoDatabase _database = null;
        private readonly IGridFSBucket _gridFSBucket = null;

        public DBContextMongo(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
            {
                _database = client.GetDatabase(settings.Value.Database);
                _gridFSBucket = new GridFSBucket(_database);
            }
        }

        public IMongoCollection<FileModel> FileInfoes
        {
            get
            {
                return _database.GetCollection<FileModel>("Files");
            }
        }

        public IGridFSBucket Sources
        {
            get
            {
                return _gridFSBucket;
            }
        }
    }
}
