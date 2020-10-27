using Infrastructure.Pictures.DTO.ElasticSearch;
using System;
using System.Collections.Generic;

namespace Infrastructure.Pictures.Mock
{
    internal class MockData
    {
        public List<PictureDTO> GetAll() => new List<PictureDTO>
        {
            // gallery1
            new PictureDTO
            {
                Id = "1",
                GlobalSortOrder = 1,
                FolderSortOrder = 1,
                AppPath = "gallery1\\car.jpg",
                CreateTimestamp = DateTime.Now,
                FolderId = "gallery1",
                FolderName = "gallery1",
                Name = "car.jpg",
                OriginalPath = "",
                Size = 34221
            },
            new PictureDTO
            {
                Id = "2",
                GlobalSortOrder = 2,
                FolderSortOrder = 2,
                AppPath = "gallery1\\car3.jpg",
                CreateTimestamp = DateTime.Now,
                FolderId = "gallery1",
                FolderName = "gallery1",
                Name = "car3.jpg",
                OriginalPath = "",
                Size = 34221
            },
            new PictureDTO
            {
                Id = "3",
                GlobalSortOrder = 3,
                FolderSortOrder = 3,
                AppPath = "gallery1\\dude.gif",
                CreateTimestamp = DateTime.Now,
                FolderId = "gallery1",
                FolderName = "gallery1",
                Name = "dude.gif",
                OriginalPath = "",
                Size = 34221
            },
            new PictureDTO
            {
                Id = "4",
                GlobalSortOrder = 4,
                FolderSortOrder = 4,
                AppPath = "gallery1\\future-cars-lead.jpg",
                CreateTimestamp = DateTime.Now,
                FolderId = "gallery1",
                FolderName = "gallery1",
                Name = "future-cars-lead.jpg",
                OriginalPath = "",
                Size = 34221
            },
            new PictureDTO
            {
                Id = "5",
                GlobalSortOrder = 5,
                FolderSortOrder = 5,
                AppPath = "gallery1\\Powerfly5EU_19_23179_A_Portrait.jpg",
                CreateTimestamp = DateTime.Now,
                FolderId = "gallery1",
                FolderName = "gallery1",
                Name = "Powerfly5EU_19_23179_A_Portrait.jpg",
                OriginalPath = "",
                Size = 34221
            },
            new PictureDTO
            {
                Id = "6",
                GlobalSortOrder = 6,
                FolderSortOrder = 6,
                AppPath = "gallery1\\shuttle.png",
                CreateTimestamp = DateTime.Now,
                FolderId = "gallery1",
                FolderName = "gallery1",
                Name = "shuttle.png",
                OriginalPath = "",
                Size = 34221
            },
            new PictureDTO
            {
                Id = "7",
                GlobalSortOrder = 7,
                FolderSortOrder = 7,
                AppPath = "gallery1\\small.mp4",
                CreateTimestamp = DateTime.Now,
                FolderId = "gallery1",
                FolderName = "gallery1",
                Name = "small.mp4",
                OriginalPath = "",
                Size = 34221
            },
            // subGal1
            new PictureDTO
            {
                Id = "8",
                GlobalSortOrder = 8,
                FolderSortOrder = 1,
                AppPath = "gallery1\\subGal1\\car2.jpg",
                CreateTimestamp = DateTime.Now,
                FolderId = "subGal1",
                FolderName = "subGal1",
                Name = "car2.jpg",
                OriginalPath = "",
                Size = 34221
            },
            new PictureDTO
            {
                Id = "9",
                GlobalSortOrder = 9,
                FolderSortOrder = 2,
                AppPath = "gallery1\\subGal1\\Rower-gorski-MTB-INDIANA-Fat-Bike-M26-Czarny-skos.jpg",
                CreateTimestamp = DateTime.Now,
                FolderId = "subGal1",
                FolderName = "subGal1",
                Name = "Rower-gorski-MTB-INDIANA-Fat-Bike-M26-Czarny-skos.jpg",
                OriginalPath = "",
                Size = 34221
            },
            new PictureDTO
            {
                Id = "10",
                GlobalSortOrder = 10,
                FolderSortOrder = 3,
                AppPath = "gallery1\\subGal1\\rower-MTB-street-jump-dirt-bike-rad-26-Mafiabikes-Blackjack-D-Green-new-neu-2020-3.jpg",
                CreateTimestamp = DateTime.Now,
                FolderId = "subGal1",
                FolderName = "subGal1",
                Name = "rower-MTB-street-jump-dirt-bike-rad-26-Mafiabikes-Blackjack-D-Green-new-neu-2020-3.jpg",
                OriginalPath = "",
                Size = 34221
            },
            new PictureDTO
            {
                Id = "11",
                GlobalSortOrder = 11,
                FolderSortOrder = 4,
                AppPath = "gallery1\\subGal1\\unnamed.gif",
                CreateTimestamp = DateTime.Now,
                FolderId = "subGal1",
                FolderName = "subGal1",
                Name = "unnamed.gif",
                OriginalPath = "",
                Size = 34221
            },
            new PictureDTO
            {
                Id = "12",
                GlobalSortOrder = 12,
                FolderSortOrder = 5,
                AppPath = "gallery1\\subGal1\\woodland_wanderer_dribbble.gif",
                CreateTimestamp = DateTime.Now,
                FolderId = "subGal1",
                FolderName = "subGal1",
                Name = "woodland_wanderer_dribbble.gif",
                OriginalPath = "",
                Size = 34221
            },
            // subGal2
            new PictureDTO
            {
                Id = "13",
                GlobalSortOrder = 13,
                FolderSortOrder = 1,
                AppPath = "gallery1\\subGal2\\sub2_1.jpg",
                CreateTimestamp = DateTime.Now,
                FolderId = "subGal2",
                FolderName = "subGal2",
                Name = "sub2_1.jpg",
                OriginalPath = "",
                Size = 34221
            },
            new PictureDTO
            {
                Id = "14",
                GlobalSortOrder = 14,
                FolderSortOrder = 2,
                AppPath = "gallery1\\subGal2\\sub2_2.jpg",
                CreateTimestamp = DateTime.Now,
                FolderId = "subGal2",
                FolderName = "subGal2",
                Name = "sub2_2.jpg",
                OriginalPath = "",
                Size = 34221
            },
            new PictureDTO
            {
                Id = "15",
                GlobalSortOrder = 15,
                FolderSortOrder = 3,
                AppPath = "gallery1\\subGal2\\sub2_3.jpg",
                CreateTimestamp = DateTime.Now,
                FolderId = "subGal2",
                FolderName = "subGal2",
                Name = "sub2_3.jpg",
                OriginalPath = "",
                Size = 34221
            },
            new PictureDTO
            {
                Id = "16",
                GlobalSortOrder = 16,
                FolderSortOrder = 4,
                AppPath = "gallery1\\subGal2\\sub2_4.jpg",
                CreateTimestamp = DateTime.Now,
                FolderId = "subGal2",
                FolderName = "subGal2",
                Name = "sub2_4.jpg",
                OriginalPath = "",
                Size = 34221
            },
            // subGal3
            new PictureDTO
            {
                Id = "17",
                GlobalSortOrder = 17,
                FolderSortOrder = 1,
                AppPath = "gallery1\\subGal3\\sub3_1.jpg",
                CreateTimestamp = DateTime.Now,
                FolderId = "subGal3",
                FolderName = "subGal3",
                Name = "sub3_1.jpg",
                OriginalPath = "",
                Size = 34221
            },
            new PictureDTO
            {
                Id = "18",
                GlobalSortOrder = 18,
                FolderSortOrder = 2,
                AppPath = "gallery1\\subGal3\\sub3_2.gif",
                CreateTimestamp = DateTime.Now,
                FolderId = "subGal3",
                FolderName = "subGal3",
                Name = "sub3_2.gif",
                OriginalPath = "",
                Size = 34221
            },
            new PictureDTO
            {
                Id = "19",
                GlobalSortOrder = 19,
                FolderSortOrder = 3,
                AppPath = "gallery1\\subGal3\\sub3_3.jpg",
                CreateTimestamp = DateTime.Now,
                FolderId = "subGal3",
                FolderName = "subGal3",
                Name = "sub3_3.jpg",
                OriginalPath = "",
                Size = 34221
            },
            new PictureDTO
            {
                Id = "20",
                GlobalSortOrder = 20,
                FolderSortOrder = 4,
                AppPath = "gallery1\\subGal3\\sub3_4.mp4",
                CreateTimestamp = DateTime.Now,
                FolderId = "subGal3",
                FolderName = "subGal3",
                Name = "sub3_4.mp4",
                OriginalPath = "",
                Size = 34221
            },
            new PictureDTO
            {
                Id = "21",
                GlobalSortOrder = 21,
                FolderSortOrder = 5,
                AppPath = "gallery1\\subGal3\\sub3_5.jpg",
                CreateTimestamp = DateTime.Now,
                FolderId = "subGal3",
                FolderName = "subGal3",
                Name = "sub3_5.jpg",
                OriginalPath = "",
                Size = 34221
            },
        };
    }
}
