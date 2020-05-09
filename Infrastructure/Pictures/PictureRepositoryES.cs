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
        private readonly IElasticClient _client;

        public PictureRepositoryES(IElasticClient elasticClient)
        {
            _client = elasticClient ?? throw new ArgumentNullException(nameof(elasticClient));
        }

        public async Task<Picture> FindByGalleryIndex(string galleryId, int imageIndex)
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

            var dto = searchResponse.Documents.Single();
            
            Picture picture = Picture.Create(
                id: dto.Id,
                name: dto.Name,
                globalSortOrder: dto.GlobalSortOrder,
                folderSortOrder: dto.FolderSortOrder,
                appPath: dto.AppPath
            );

            return picture;
        }

        public async Task<Picture> FindByIndex(int i)
        {
            PictureDTO dto;

            if(i > 0)
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

                dto = searchResponse.Documents.Single();
            }
            else 
            { 
                dto = await FindRandom();
            }

            Picture picture = Picture.Create(
                id: dto.Id,
                name: dto.Name,
                globalSortOrder: dto.GlobalSortOrder,
                folderSortOrder: dto.FolderSortOrder,
                appPath: dto.AppPath
            );

            return picture;

            //if(Path.DirectorySeparatorChar == '/')
            //{
            //    dto.AppPath = dto.AppPath.Replace('\\', '/');
            //}

            //var currentPath = Path.Combine(_root, dto.AppPath);

            //return currentPath;
        }

        private async Task<PictureDTO> FindRandom()
        {
            var searchResponse = await _client.SearchAsync<PictureDTO>(s => s
                .Query(q => q
                    .FunctionScore(f => f
                        .Functions(fx => fx
                            .RandomScore(rng => rng.Seed(DateTime.Now.Millisecond))
                        )
                    )
                )
                .Size(1)
                .Index("picture")
            );

            return searchResponse.Documents.Single();
        }

        public async Task<IEnumerable<Picture>> FindAll(string galleryId, int offset = 0)
        {
            var searchResponse = await _client.SearchAsync<PictureDTO>(s => s
                .Query(q => q
                    .Match(m => m
                        .Field(f => f.FolderId)
                        .Query(galleryId)
                    ) && q
                    .Range(r => r
                        .Field( f => f.FolderSortOrder)
                        .GreaterThan(offset)
                    )
                )
                .Size(48)
                .Index("picture")
            );

            List<Picture> list = new List<Picture>();
            foreach(var pic in searchResponse.Documents)
            {
                var picture = Picture.Create(
                        id: pic.Id,
                        name: pic.Name,
                        globalSortOrder: pic.GlobalSortOrder,
                        folderSortOrder: pic.FolderSortOrder
                );

                list.Add(picture);
            }

            return list;
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

        public void Remove(Picture aggregate)
        {
            throw new NotImplementedException();
        }

        public Task<Picture> Find(Picture aggregate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Picture>> FindAll(Picture aggregate)
        {
            throw new NotImplementedException();
        }
    }
}
