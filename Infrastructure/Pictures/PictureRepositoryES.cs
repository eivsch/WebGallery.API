using DomainModel.Aggregates.Gallery;
using DomainModel.Aggregates.Picture;
using DomainModel.Aggregates.Picture.Interfaces;
using Infrastructure.Pictures.DTO.ElasticSearch;
using Microsoft.Extensions.Configuration;
using Nest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Pictures
{
    public class PictureRepositoryES : IPictureRepository
    {
        private readonly string _root;

        private readonly IElasticClient _client;
        private readonly IConfiguration _configuration;

        public PictureRepositoryES(IElasticClient elasticClient, IConfiguration configuration)
        {
            _client = elasticClient ?? throw new ArgumentNullException(nameof(elasticClient));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

            _root = _configuration.GetValue($"RootFolder", "");
        }

        public Task<Picture> Find(Picture aggregate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Picture>> FindAll(Picture aggregate)
        {
            throw new NotImplementedException();
        }

        public async Task<string> FindByGalleryIndex(string galleryId, int imageIndex)
        {
            var searchResponse = await _client.SearchAsync<PictureDTO>(s => s
                .Query(q => q
                    .Match(m => m
                        .Field(f => f.FolderId)
                        .Query(galleryId)
                    ) && q
                    .Match(m => m
                        .Field(f => f.FolderSortOrder)
                        .Query(imageIndex.ToString())
                    )
                )
                .Size(1)
                .Index("picture")
            );

            var pic = searchResponse.Documents.Single();

            if (Path.DirectorySeparatorChar == '/')
            {
                pic.AppPath = pic.AppPath.Replace('\\', '/');
            }

            var currentPath = Path.Combine(_root, pic.AppPath);

            return currentPath;
        }

        public Task<Picture> FindById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Picture> FindById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Picture> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<string> FindByIndex(int i)
        {
            var searchResponse = await _client.SearchAsync<PictureDTO>(s => s
                .Query(q => q
                    .Match(m => m
                        .Field(f => f.GlobalSortOrder)
                        .Query(i.ToString())
                    )
                )
                .Size(1)
                .Index("picture")
            );

            var pic = searchResponse.Documents.Single();

            if(Path.DirectorySeparatorChar == '/')
            {
                pic.AppPath = pic.AppPath.Replace('\\', '/');
            }

            var currentPath = Path.Combine(_root, pic.AppPath);

            return currentPath;
        }

        public async Task<IEnumerable<Picture>> FindAll(string galleryId)
        {
            var searchResponse = await _client.SearchAsync<PictureDTO>(s => s
                .Query(q => q
                    .Match(m => m
                        .Field(f => f.FolderId)
                        .Query(galleryId)
                    )
                )
                .Size(24)
                .Index("picture")
            );

            List<Picture> list = new List<Picture>();
            foreach(var pic in searchResponse.Documents)
            {
                var picture = Picture.Create(
                        id: pic.Id,
                        name: pic.Name,
                        globalSortOrder: pic.GlobalSortOrder
                );

                list.Add(picture);
            }

            return list;
        }

        public void Remove(Picture aggregate)
        {
            throw new NotImplementedException();
        }

        public async Task<Picture> Save(Picture aggregate)
        {
            var existing = _client.Get<PictureDTO>(new GetRequest<PictureDTO>("picture", aggregate.Id));

            if (!existing.Found)
            {
                var dto = new PictureDTO
                {
                    Id = aggregate.Id,
                    AppPath = aggregate.AppPath,
                    Name = aggregate.Name,
                    FolderName = aggregate.FolderName,
                    FolderSortOrder = aggregate.FolderSortOrder,
                    FolderId = aggregate.FolderId,
                    OriginalPath = aggregate.OriginalPath,
                    Size = aggregate.Size,
                    CreateTimestamp = aggregate.CreateTimestamp,
                    GlobalSortOrder = aggregate.GlobalSortOrder
                };

                var indexRequest = new IndexRequest<PictureDTO>(dto, "picture");
                var response = _client.Index(indexRequest);
                if (!response.IsValid)
                {
                    throw new Exception(response.DebugInformation);
                }
            }

            return aggregate;
        }
    }
}
