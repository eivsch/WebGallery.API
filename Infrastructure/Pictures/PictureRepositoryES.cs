using DomainModel.Aggregates.Picture;
using DomainModel.Aggregates.Picture.Interfaces;
using Infrastructure.Pictures.DTO.ElasticSearch;
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
            
            return BuildAggregateFromDto(dto);
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

            return BuildAggregateFromDto(dto);
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
                        .LessThanOrEquals(offset + 48)
                    )
                )
                .Size(48)
                .Index("picture")
            );

            List<Picture> list = new List<Picture>();
            foreach(var dto in searchResponse.Documents)
            {
                var picture = BuildAggregateFromDto(dto);

                list.Add(picture);
            }

            return list;
        }

        public async Task<Picture> Save(Picture aggregate)
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
                GlobalSortOrder = aggregate.GlobalSortOrder,
            };

            var existing = _client.Get<PictureDTO>(new GetRequest<PictureDTO>("picture", aggregate.Id));

            WriteResponseBase response;
            if (!existing.Found)
            {
                var indexRequest = new IndexRequest<PictureDTO>(dto, "picture");
                response = await _client.IndexAsync(indexRequest);
            }
            else
            {
                response = await _client.UpdateAsync<PictureDTO>(dto.Id, u => u
                    .Doc(dto)
                    .Upsert(dto)
                    .Index("picture")
                );
            }

            if (!response.IsValid)
            {
                throw new Exception(response.DebugInformation);
            }

            return aggregate;
        }

        // TODO: Refactor to use GET?
        public async Task<Picture> FindById(string id)
        {
            var searchResponse = await _client.SearchAsync<PictureDTO>(s => s
                .Query(q => q
                    .Match(m => m
                        .Field(f => f.Id)
                        .Query(id)
                    )
                )
                .Size(1)
                .Index("picture")
            );

            var dto = searchResponse.Documents.Single();

            return BuildAggregateFromDto(dto);
        }

        private Picture BuildAggregateFromDto(PictureDTO dto)
        {
            Picture aggregate =  Picture.Create(
                id: dto.Id,
                name: dto.Name,
                appPath: dto.AppPath,
                originalPath: dto.OriginalPath,
                folderName: dto.FolderName,
                folderId: dto.FolderId,
                globalSortOrder: dto.GlobalSortOrder,
                folderSortOrder: dto.FolderSortOrder,
                size: dto.Size,
                created: dto.CreateTimestamp
            );

            return aggregate;
        }

        public Task<Picture> FindById(Guid id)
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

        public async Task<Picture> FindByAppPath(string appPath)
        {
            var searchResponse = await _client.SearchAsync<PictureDTO>(s => s
                .Query(q => q
                    .Match(m => m
                        .Field(f => f.AppPath.Suffix("keyword"))
                        .Query(appPath)
                    )
                )
                .Size(1)
                .Index("picture")
            );

            if (searchResponse.Documents.Count == 0)
                return null;

            var dto = searchResponse.Documents.Single();
            
            return BuildAggregateFromDto(dto);
        }
    }
}
