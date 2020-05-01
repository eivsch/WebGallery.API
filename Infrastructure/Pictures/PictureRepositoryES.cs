using DomainModel.Aggregates.Picture;
using DomainModel.Aggregates.Picture.Interfaces;
using Infrastructure.Pictures.DtoEs;
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

        private readonly ElasticClient _client;

        public PictureRepositoryES(string esEndpoint, string root)
        {
            _root = root;

            var settings = new ConnectionSettings(new Uri(esEndpoint))
                .DefaultIndex("picture");
            _client = new ElasticClient(settings);
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
            var searchResponse = await _client.SearchAsync<EsPictureDto>(s => s
                .Query(q => q
                    .Match(m => m
                        .Field(f => f.GlobalSortOrder)
                        .Query(i.ToString())
                    )
                )
                .Size(1)
            );

            var pic = searchResponse.Documents.Single();

            if(Path.DirectorySeparatorChar == '/')
            {
                pic.AppPath = pic.AppPath.Replace('\\', '/');
            }

            var currentPath = Path.Combine(_root, pic.AppPath);

            return currentPath;
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
