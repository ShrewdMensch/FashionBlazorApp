using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Extensions;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Application.Utility.ApplicationConstants;

namespace FashionAppBlazor.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ReadyToWearsController : BaseController
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadyToWearDto>> Get(Guid id)
        {
            var readyToWearCloth = await Repository.Get<ReadyToWear>(id);

            return Ok(Mapper.Map<ReadyToWear, ReadyToWearDto>(readyToWearCloth));
        }

        [HttpPost]
        public async Task<ActionResult<ReadyToWearDto>> Upsert(ReadyToWearDto readyToWearClothDto)
        {
            try
            {
                var readyToWearCloth = await Repository.Get<ReadyToWear>(readyToWearClothDto.Id);

                if (readyToWearCloth != null)
                {
                    readyToWearCloth.Name = readyToWearClothDto.Name.ToTitleCase();
                    readyToWearCloth.TypeOfClothId = readyToWearClothDto.TypeOfClothId;
                    readyToWearCloth.NumberInStock = readyToWearClothDto.NumberInStock;
                }

                else
                {
                    readyToWearCloth = new ReadyToWear()
                    {
                        Name = readyToWearClothDto.Name,
                        TypeOfClothId = readyToWearClothDto.TypeOfClothId,
                        NumberInStock = readyToWearClothDto.NumberInStock
                    };

                    Repository.Add(readyToWearCloth);
                }

                await Repository.SaveAll();

                return Ok(Mapper.Map<ReadyToWear, ReadyToWearDto>(readyToWearCloth));
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
        public async Task<ActionResult> Delete(Guid id)
        {
            var readyToWearClothToDelete = await Repository.Get<ReadyToWear>(id);

            if (readyToWearClothToDelete == null)
            {
                return BadRequest(new ErrorDto()
                {
                    ErrorMessage = "ReadyToWear not found",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            try
            {
                foreach (var photo in readyToWearClothToDelete.Photos)
                {
                    FileUpload.DeleteFile(photo.Url.Replace(ReadyToWearImagesFolderName, ""));
                }

                Repository.Remove(readyToWearClothToDelete);
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
                var allPhotos = await Repository.GetAll<ReadyToWearPhoto>();

                foreach (var photo in allPhotos)
                {
                    FileUpload.DeleteFile(photo.Url.Replace(ReadyToWearImagesFolderName, ""));
                }

                Repository.RemoveRange(await Repository.GetAll<ReadyToWear>());
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadyToWearDto>>> All()
        {
            var readyToWearCloths = await Repository.GetAll<ReadyToWear>();

            return Ok(Mapper.Map<IEnumerable<ReadyToWear>, IEnumerable<ReadyToWearDto>>(readyToWearCloths));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Select2InputDto>>> GetAllForDropDown()
        {
            var readyToWearCloths = await Repository.GetAll<ReadyToWear>();

            return Ok(Mapper.Map<IEnumerable<Entity>, IEnumerable<Select2InputDto>>(readyToWearCloths));
        }
    }
}