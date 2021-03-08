using System;
using System.Collections.Generic;
using System.IO;
using Application.PhotoAccessor;
using Application.Repository;
using Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.PhotoAccessor
{
    public class PhotoAccessor : IPhotoAccessor
    {
        public const string ImageFolder = @"images\product_images";
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IRepository _repository;
        private readonly string _fullImageFolderPath;

        public PhotoAccessor(IWebHostEnvironment hostingEnvironment, IRepository repository)
        {
            _hostingEnvironment = hostingEnvironment;
            _repository = repository;
            _fullImageFolderPath = Path.Combine(_hostingEnvironment.WebRootPath, ImageFolder);
        }
        public string AddPhoto(AppUser user, IFormFileCollection files)
        {
            var oldPhoto = user.Photo?.Url;

            if (files.Count != 0)
            {
                //Image was uploaded

                var extension_new = Path.GetExtension(files[0].FileName);

                var fileNameWithExtension = Path.Combine(_fullImageFolderPath, user.UserName +
                    extension_new);

                if (!String.IsNullOrWhiteSpace(oldPhoto))
                {

                    var extension_old = Path.GetExtension(oldPhoto);

                    if (File.Exists(Path.Combine(_fullImageFolderPath, Path.GetFileNameWithoutExtension(oldPhoto) +
                            extension_old)))
                    {
                        //There's existing Image in server
                        File.Delete(Path.Combine(_fullImageFolderPath, Path.GetFileNameWithoutExtension(oldPhoto) +
                            extension_old));

                        fileNameWithExtension = Path.Combine(_fullImageFolderPath, Path.GetFileNameWithoutExtension(oldPhoto) +
                            extension_new);
                    }
                }

                //Uplaod file to server
                using (var fileStream = new FileStream(fileNameWithExtension, FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }

                return String.Format("/{0}/{1}", ImageFolder, Path.GetFileName(fileNameWithExtension));
            }

            return oldPhoto; //No new file Uploaded, make the old value in DB constant

        }

        public void AddOrUpdatePhotos(ReadyToWear readyToWear, IFormFileCollection files)
        {
            var photosToReturn = new List<ReadyToWearPhoto>();
            DeletePhotosFromServer(readyToWear);

            if (files.Count != 0)
            {
                //Image was uploaded
                for (int count = 0; count < files.Count; count++)
                {
                    var extension_new = Path.GetExtension(files[count].FileName);

                    var fileNameWithExtension = Path.Combine(_fullImageFolderPath, $"{readyToWear.Id}_{count}{extension_new}");

                    //Uplaod file to server
                    using (var fileStream = new FileStream(fileNameWithExtension, FileMode.Create))
                    {
                        files[count].CopyTo(fileStream);
                    }

                    photosToReturn.Add(new ReadyToWearPhoto
                    {
                        Url = string.Format(@"\{0}\{1}", ImageFolder, Path.GetFileName(fileNameWithExtension))
                    });
                }
            }

            readyToWear.Photos = photosToReturn;
        }

        public void DeletePhotosFromServer(ReadyToWear readyToWear)
        {
            if (readyToWear.Photos != null)
            {
                //Delete All Existing Photo(s) from Server File System
                foreach (var oldPhoto in readyToWear.Photos)
                {
                    var extension_old = Path.GetExtension(oldPhoto.Url);

                    if (File.Exists(Path.Combine(_fullImageFolderPath, Path.GetFileNameWithoutExtension(oldPhoto.Url) +
                            extension_old)))
                    {
                        //There's existing Image in server
                        File.Delete(Path.Combine(_fullImageFolderPath, Path.GetFileNameWithoutExtension(oldPhoto.Url) +
                            extension_old));
                    }
                }

                //Persist Changes to Database as well
                _repository.RemoveRange(readyToWear.Photos);
            }
        }
        public void DeletePhotosFromServer(IEnumerable<ReadyToWear> readyToWears)
        {
            foreach (var readyToWear in readyToWears)
            {
                if (readyToWear.Photos != null)
                {
                    //Delete All Existing Photo(s) from Server File System
                    foreach (var oldPhoto in readyToWear.Photos)
                    {
                        var extension_old = Path.GetExtension(oldPhoto.Url);

                        if (File.Exists(Path.Combine(_fullImageFolderPath, Path.GetFileNameWithoutExtension(oldPhoto.Url) +
                                extension_old)))
                        {
                            //There's existing Image in server
                            File.Delete(Path.Combine(_fullImageFolderPath, Path.GetFileNameWithoutExtension(oldPhoto.Url) +
                                extension_old));
                        }
                    }

                    //Persist Changes to Database as well
                    _repository.RemoveRange(readyToWear.Photos);
                }
            }
        }
    }
}