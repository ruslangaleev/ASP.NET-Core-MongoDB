using Data;
using Model.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IFileService
    {
        Task<FileModel> GetFile(string id);

        Task AddFile(FileModel fileInfo);

        Task<DeleteResult> RemoveFile(string id);
    }

    public class FileService : IFileService
    {
        private readonly IFileRepository _fileRepository;

        // Controller
        public FileService(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public async Task AddFile(FileModel fileInfo)
        {
            await _fileRepository.AddFile(fileInfo);
        }

        public async Task<FileModel> GetFile(string id)
        {
            return await _fileRepository.GetFile(id) ?? new FileModel();
        }

        public async Task<DeleteResult> RemoveFile(string id)
        {
            return await _fileRepository.RemoveFile(id);
        }
    }
}
