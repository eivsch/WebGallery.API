using DomainModel.Aggregates.Picture;
using DomainModel.Aggregates.Picture.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Pictures
{
    public class PictureRepositoryMock : IPictureRepository
    {
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
            return Picture.Create(
                id: id,
                name: "name",
                appPath: "app\\path",
                originalPath: "orig\\path",
                folderName: "folderName",
                folderId: "folderAppPath",
                folderSortOrder: 24,
                globalSortOrder: 2465,
                size: 34221,
                created: DateTime.Now);
        }

        public Task<Picture> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Picture>> FindAll(string galleryId, int offset = 0)
        {
            return new List<Picture>
            {
                Picture.Create(
                    id: "1",
                    name: "name",
                    appPath: "app\\path",
                    originalPath: "orig\\path",
                    folderName: "folderName",
                    folderId: galleryId,
                    folderSortOrder: 24,
                    globalSortOrder: 2465,
                    size: 34221,
                    created: DateTime.Now
                ),
                Picture.Create(
                    id: "2",
                    name: "name",
                    appPath: "app\\path",
                    originalPath: "orig\\path",
                    folderName: "folderName",
                    folderId: galleryId,
                    folderSortOrder: 24,
                    globalSortOrder: 2465,
                    size: 34221,
                    created: DateTime.Now
                ),
            };
        }

        public void Remove(Picture aggregate)
        {
            throw new NotImplementedException();
        }

        public async Task<Picture> Save(Picture aggregate)
        {
            return aggregate;
        }

        public async Task<Picture> FindByIndex(int i)
        {
            string gallery;
            int galleryIndex;
            switch (i)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                    gallery = "gallery1";
                    galleryIndex = i;
                    break;
                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                    gallery = "subGal1";
                    galleryIndex = i - 7;
                    break;
                case 13:
                case 14:
                case 15:
                case 16:
                    gallery = "subGal2";
                    galleryIndex = i - 12;
                    break;
                default:
                    gallery = "subGal3";
                    galleryIndex = i - 16;
                    break;
            }

            return Helper(gallery, galleryIndex);
        }

        public async Task<Picture> FindByGalleryIndex(string galleryId, int index)
        {
            return Helper(galleryId, index);
        }

        private Picture Helper(string galleryId, int index)
        {
            switch (galleryId)
            {
                case "gallery1":
                    switch (index)
                    {
                        case 1:
                            return Create("name", "gallery1\\car.jpg", 1);
                        case 2:
                            return Create("name", "gallery1\\car3.jpg", 2);
                        case 3:
                            return Create("name", "gallery1\\dude.gif", 3);
                        case 4:
                            return Create("name", "gallery1\\future-cars-lead.jpg", 4);
                        case 5:
                            return Create("name", "gallery1\\Powerfly5EU_19_23179_A_Portrait.jpg", 5);
                        case 6:
                            return Create("name", "gallery1\\shuttle.png", 6);
                        case 7:
                            return Create("name", "gallery1\\small.mp4", 7);
                    };
                    break;
                case "subGal1":
                    switch (index)
                    {
                        case 1:
                            return Create("name", "gallery1\\subGal1\\car2.jpg", 8);
                        case 2:
                            return Create("name", "gallery1\\subGal1\\Rower-gorski-MTB-INDIANA-Fat-Bike-M26-Czarny-skos.jpg", 9);
                        case 3:
                            return Create("name", "gallery1\\subGal1\\rower-MTB-street-jump-dirt-bike-rad-26-Mafiabikes-Blackjack-D-Green-new-neu-2020-3.jpg", 10);
                        case 4:
                            return Create("name", "gallery1\\subGal1\\unnamed.gif", 11);
                        case 5:
                            return Create("name", "gallery1\\subGal1\\woodland_wanderer_dribbble.gif", 12);
                    };
                    break;
                case "subGal2":
                    switch (index)
                    {
                        case 1:
                            return Create("name", "gallery1\\subGal2\\sub2_1.jpg", 13);
                        case 2:
                            return Create("name", "gallery1\\subGal2\\sub2_2.jpg", 14);
                        case 3:
                            return Create("name", "gallery1\\subGal2\\sub2_3.jpg", 15);
                        case 4:
                            return Create("name", "gallery1\\subGal2\\sub2_4.jpg", 16);
                    };
                    break;
                case "subGal3":
                    switch (index)
                    {
                        case 1:
                            return Create("name", "gallery1\\subGal3\\sub3_1.jpg", 17);
                        case 2:
                            return Create("name", "gallery1\\subGal3\\sub3_2.gif", 18);
                        case 3:
                            return Create("name", "gallery1\\subGal3\\sub3_3.jpg", 19);
                        case 4:
                            return Create("name", "gallery1\\subGal3\\sub3_4.mp4", 20);
                        case 5:
                            return Create("name", "gallery1\\subGal3\\sub3_5.jpg", 21);
                    };
                    break;
            }

            return null;

            Picture Create(string name, string appPath, int globalSortOrder)
            {
                return Picture.Create(
                    id: "id_" + appPath,
                    name: name,
                    appPath: appPath,
                    originalPath: "orig\\path",
                    folderName: "folderName",
                    folderId: galleryId,
                    folderSortOrder: index,
                    globalSortOrder: globalSortOrder,
                    size: 34221,
                    created: DateTime.Now);
            }
        }

        public async Task<Picture> FindByAppPath(string appPath)
        {
            return Picture.Create(
                id: "123123",
                name: "name",
                appPath: appPath,
                originalPath: "orig\\path",
                folderName: "folderName",
                folderId: "hash1234",
                folderSortOrder: 24,
                globalSortOrder: 123,
                size: 34221,
                created: DateTime.Now);
        }
    }
}
