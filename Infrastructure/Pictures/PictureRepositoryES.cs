using DomainModel.Aggregates.Picture;
using DomainModel.Aggregates.Picture.Interfaces;
using Infrastructure.Pictures.DtoEs;
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
