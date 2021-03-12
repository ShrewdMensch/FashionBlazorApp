using System;
using System.Threading.Tasks;
using Application.DTOs;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Application.Utility.ApplicationConstants;

namespace FashionAppBlazor.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ReadyToWearPhotosController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult<ReadyToWearPhotoDto>> Create(ReadyToWearPhotoDto readyToWearPhoto)
        {
            var photo = Mapper.Map<ReadyToWearPhotoDto, ReadyToWearPhoto>(readyToWearPhoto);

            try
            {
                Repository.Add(photo);

                await Repository.SaveAll();

                return Ok(Mapper.Map<ReadyToWearPhoto, ReadyToWearPhotoDto>(photo));
            }

            catch (Exception ex)
            {
                return BadRequest(new ErrorDto()
                {
                    ErrorMessage = ex.ToString(),
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteById(Guid id)
        {
            var photoToDelete = await Repository.Get<ReadyToWearPhoto>(id);

            if (photoToDelete == null)
            {
                return BadRequest(new ErrorDto()
                {
                    ErrorMessage = "Entity not found",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            try
            {
                Repository.Remove(photoToDelete);
                await Repository.SaveAll();
                return Ok();
            }

            catch (Exception ex)
            {
                return BadRequest(new ErrorDto()
                {
                    ErrorMessage = ex.ToString(),
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

        }
        
        [HttpDelete("{photoUrl}")]
        public async Task<ActionResult> DeleteByUrl(string photoUrl)
        {
            var fullUrl = ReadyToWearImagesFolderName + photoUrl;

            var photoToDelete = await Repository.GetByPredicate<ReadyToWearPhoto>(p=>p.Url == fullUrl);

            if (photoToDelete == null)
            {
                return BadRequest(new ErrorDto()
                {
                    ErrorMessage = "Entity not found",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            try
            {
                Repository.Remove(photoToDelete);
                await Repository.SaveAll();
                return Ok();
            }

            catch (Exception ex)
            {
                return BadRequest(new ErrorDto()
                {
                    ErrorMessage = ex.ToString(),
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAll()
        {
            try
            {
                Repository.RemoveRange(await Repository.GetAll<ReadyToWearPhoto>());
                await Repository.SaveAll();
                return Ok();
            }

            catch (Exception ex)
            {
                return BadRequest(new ErrorDto()
                {
                    ErrorMessage = ex.ToString(),
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
        }
    }
}