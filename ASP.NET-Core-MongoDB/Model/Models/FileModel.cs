using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
    public class FileModel
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string IdInfo { get; set; }

        [BsonIgnore]
        public string FileName { get; set; }

        public DateTime UpdatedOn { get; set; } = DateTime.Now;

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        // Содержимое FSGrid:

        [BsonRepresentation(BsonType.ObjectId)]
        public string IdSource { get; set; }

        [BsonIgnore]
        public byte[] Source { get; set; }
    }
}
