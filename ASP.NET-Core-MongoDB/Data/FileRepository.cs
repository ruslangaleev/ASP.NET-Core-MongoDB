using Microsoft.Extensions.Options;
using Model.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface IFileRepository
    {
        Task<FileModel> GetFile(string id);

        Task AddFile(FileModel fileInfo);

        Task<DeleteResult> RemoveFile(string id);
    }

    public class FileRepository : IFileRepository
    {
        private readonly DBContextMongo _context = null;

        // Constructor
        public FileRepository(IOptions<Settings> settings)
        {
            _context = new DBContextMongo(settings);
        }

        public async Task AddFile(FileModel file)
        {
            var id = await _context.Sources.UploadFromBytesAsync(file.FileName, file.Source);

            file.IdSource = id.ToString();

            await _context.FileInfoes.InsertOneAsync(file);
        }

        public async Task<FileModel> GetFile(string id)
        {
            var filter = Builders<FileModel>.Filter.Eq("Id", id);
            var fileInfo = await _context.FileInfoes
                            .Find(filter)
                            .FirstOrDefaultAsync();

            var source = await _context.Sources.DownloadAsBytesByNameAsync(id);

            fileInfo.Source = source;

            return fileInfo;
        }

        public async Task<DeleteResult> RemoveFile(string id)
        {
            FileModel fileInfo = await GetFile(id);

            await _context.Sources.DeleteAsync(ObjectId.Parse(fileInfo.IdSource));

            return await _context.FileInfoes.DeleteOneAsync(
                Builders<FileModel>.Filter.Eq("Id", id));
        }
    }
}
