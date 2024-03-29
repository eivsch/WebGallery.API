﻿using DomainModel.Aggregates.Gallery;
using DomainModel.Aggregates.Gallery.Interfaces;
using Infrastructure.Galleries.DTO;
using Microsoft.AspNetCore.Http;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Galleries
{
    public class GalleryRepository : IGalleryRepository
    {
        private readonly IElasticClient _client;
        private string _indexName;

        public GalleryRepository(IElasticClient elasticClient, IHttpContextAccessor httpContextAccessor)
        {
            _client = elasticClient ?? throw new ArgumentNullException(nameof(elasticClient));

            ResolveIndexName();

            void ResolveIndexName()
            {
                var httpRequestHeaders = httpContextAccessor.HttpContext.Request.Headers;
                var userId = httpRequestHeaders["Gallery-User"];
                if (!string.IsNullOrWhiteSpace(userId))
                    _indexName = $"{userId}_picture";
                else
                    _indexName = "picture";
            }
        }

        #region Not Implemented

        public Task<Gallery> Find(Gallery aggregate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Gallery>> FindAll(Gallery aggregate)
        {
            throw new NotImplementedException();
        }

        public Task<Gallery> FindById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Gallery> FindById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Gallery> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Gallery> Save(Gallery aggregate)
        {
            throw new NotImplementedException();
        }

        public Task Remove(Gallery aggregate)
        {
            throw new NotImplementedException();
        }

        #endregion Not Implemented

        public async Task<Gallery> FillEmptyGalleryWithItems(Gallery gallery)
        {
            if (gallery.GalleryItems.Count != 0)
                throw new ArgumentException("The gallery must be empty, meaning it has no items.");

            var searchResponse = await _client.SearchAsync<GalleryPictureDTO>(s => s
                .Query(q => q
                    .Match(m => m
                        .Field(f => f.FolderId)
                        .Query(gallery.Id)
                    ) && q
                    .Range(r => r
                        .Field(f => f.FolderSortOrder)
                        .GreaterThanOrEquals(gallery.GalleryItemIndexStart)
                        .LessThan(gallery.GalleryItemIndexStart + gallery.NumberOfItems)
                    )
                )
                .Size(gallery.NumberOfItems)
                .Index(_indexName)
            );

            string galleryname = searchResponse.Documents?.FirstOrDefault()?.FolderName ?? "Unknown";
            gallery.SetGalleryName(galleryname);

            foreach (var dto in searchResponse.Documents)
            {
                gallery.AddGalleryItem(
                    galleryItemId: dto.Id,
                    indexGlobal: dto.GlobalSortOrder,
                    name: dto.Name,
                    appPath: dto.AppPath
                );
            }

            return gallery;
        }

        public async Task<List<Gallery>> GetAll()
        {
            try
            {
                var aggregationResponse = await _client.SearchAsync<GalleryDTO>(s => s
                    .Aggregations(a => a
                        .Terms("my_agg", st => st
                            .Field(f => f.FolderId.Suffix("keyword"))   // "keyword" is an ElasticSearch data-type: https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/multi-fields.html
                            .Size(800)
                        )
                    )
                    .Index(_indexName)
                );

                var list = new List<Gallery>();
                foreach (var bucket in aggregationResponse.Aggregations.Terms("my_agg").Buckets)
                {
                    var singleGallerySearchResponse = await _client.SearchAsync<GalleryDTO>(s => s
                        .Query(q => q
                            .Match(m => m
                                .Field(f => f.FolderId.Suffix("keyword"))
                                .Query(bucket.Key)
                            )
                        )
                        .Size(1)
                        .Index(_indexName)
                    );

                    var gallery = singleGallerySearchResponse.Documents.FirstOrDefault();

                    list.Add(
                        Gallery.Create(
                            bucket.Key, 
                            Convert.ToInt32(bucket.DocCount), 
                            galleryName: gallery.FolderName)
                    );
                }

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Gallery> GetRandom(int itemsInGallery)
        {
            Gallery gallery;
            var searchResponse = await _client.SearchAsync<GalleryPictureDTO>(s => s
                .Query(q => q
                    .FunctionScore(f => f
                        .Functions(fx => fx
                            .RandomScore(rng => rng.Seed(DateTime.Now.Millisecond))
                        )
                    )
                )
                .Size(itemsInGallery)
                .Index(_indexName)
            );

            gallery = Gallery.Create($"random-{Guid.NewGuid()}".Substring(0, 15).ToLower(), itemsInGallery);
            foreach (var pic in searchResponse.Documents)
            {
                gallery.AddGalleryItem(
                    galleryItemId: pic.Id, 
                    indexGlobal: pic.GlobalSortOrder, 
                    name: pic.Name,
                    appPath: pic.AppPath
                );
            }

            return gallery;
        }
    }
}
