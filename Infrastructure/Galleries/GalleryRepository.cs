﻿using DomainModel.Aggregates.Gallery;
using DomainModel.Aggregates.Gallery.Interfaces;
using Infrastructure.Galleries.DTO;
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

        public GalleryRepository(IElasticClient elasticClient)
        {
            _client = elasticClient ?? throw new ArgumentNullException(nameof(elasticClient));
        }

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
                .Index("picture")
            );

            foreach (var dto in searchResponse.Documents)
            {
                gallery.AddGalleryItem(
                    galleryItemId: dto.Id,
                    indexGlobal: dto.GlobalSortOrder,
                    name: dto.Name
                );
            }

            return gallery;
        }

        public async Task<List<Gallery>> GetAll()
        {
            try
            {
                var result = await _client.SearchAsync<GalleryDTO>(s => s
                    .Aggregations(a => a
                            .Terms("my_agg", st => st
                                .Field(f => f.FolderId.Suffix("keyword"))   // "keyword" is an ElasticSearch data-type: https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/multi-fields.html
                                .Size(200)
                            )
                        )
                    .Index("picture")
                );

                var list = new List<Gallery>();
                foreach (var bucket in result.Aggregations.Terms("my_agg").Buckets)
                {
                    list.Add(
                        Gallery.Create(bucket.Key, Convert.ToInt32(bucket.DocCount))
                    );
                }

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Remove(Gallery aggregate)
        {
            throw new NotImplementedException();
        }

        public async Task<Gallery> Save(Gallery aggregate)
        {
            var dto = new GalleryDTO
            {
                Id = aggregate.Id,
                GalleryPictures = aggregate.GalleryItems.ToList().Select(i => Map(i))
            };

            var indexRequest = new IndexRequest<GalleryDTO>(dto, "gallery");
            var response = await _client.IndexAsync(indexRequest);
            if (!response.IsValid)
            {
                throw new Exception(response.DebugInformation);
            }

            return aggregate;
        }
        


        private GalleryPictureDTO Map(GalleryItem galleryItem)
        {
            return new GalleryPictureDTO
            {
                Id = galleryItem.Id,
                GlobalSortOrder = galleryItem.IndexGlobal
            };
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
                .Index("picture")
            );

            gallery = Gallery.Create($"random-{Guid.NewGuid()}".Substring(0, 15).ToLower(), itemsInGallery);
            foreach (var pic in searchResponse.Documents)
            {
                gallery.AddGalleryItem(
                    galleryItemId: pic.Id, 
                    indexGlobal: pic.GlobalSortOrder, 
                    name: pic.Name
                );
            }

            return gallery;
        }
    }
}
