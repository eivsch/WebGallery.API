﻿using Dapper;
using DomainModel.Aggregates.Picture;
using DomainModel.Aggregates.Picture.Interfaces;
using DomainModel.Exceptions;
using Infrastructure.Common;
using Infrastructure.Pictures.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Pictures
{
    public class PictureRepository : IPictureRepository
    {
        private readonly IDataContext _webGalleryDb;

        public PictureRepository(IWebGalleryDb webGalleryDb)
        {
            _webGalleryDb = webGalleryDb ?? throw new InfrastructureLayerException($"The parameter '{nameof(webGalleryDb)}' is required to initialise the {nameof(PictureRepository)} repository.", new ArgumentNullException(nameof(IWebGalleryDb)));
        }

        public Task<Picture> Find(Picture aggregate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Picture>> FindAll(Picture aggregate)
        {
            throw new NotImplementedException();
        }

        public Task<Picture> FindById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Picture> FindById(string id)
        {
            var pictureDto = await GetPictureFromPersistence(id);

            return Picture.Create(pictureDto.Id, pictureDto.Path);
        }

        private async Task<PictureDTO> GetPictureFromPersistence(string id)
        {
            var sql = $@"SELECT  [Id]
                                ,[Path]
                         FROM [WebGallery].[dbo].[Picture]
                         WHERE Id = @pictureId;";

            using (var connection = _webGalleryDb.Connection)
            {
                connection.Open();

                var results = await connection.QueryAsync<PictureDTO>(
                    sql, 
                    new { pictureId = id }
                );


                return results.FirstOrDefault();
            }
        }

        public Task<Picture> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Picture aggregate)
        {
            throw new NotImplementedException();
        }

        public Task<Picture> Save(Picture aggregate)
        {
            throw new NotImplementedException();
        }
    }
}
