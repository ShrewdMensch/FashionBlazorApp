using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Extensions;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FashionAppBlazor.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AccessoriesController : BaseController
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<AccessoryDto>> Get(Guid id)
        {
            var accessory = await Repository.Get<Accessory>(id);

            return Ok(Mapper.Map<Accessory, AccessoryDto>(accessory));
        }

        [HttpPost]
        public async Task<ActionResult<AccessoryDto>> Upsert(AccessoryDto accessoryDto)
        {
            var accessory = Mapper.Map<AccessoryDto, Accessory>(accessoryDto);
            accessory.Name = accessory.Name.ToTitleCase();

            try
            {
                if (accessory.Id != default)
                {
                    Repository.Attach(accessory).State = EntityState.Modified;
                }

                else
                {
                    Repository.Add(accessory);
                }

                await Repository.SaveAll();

                return Ok(Mapper.Map<Accessory, AccessoryDto>(accessory));
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
            var accessoryToDelete = await Repository.Get<Accessory>(id);

            if (accessoryToDelete == null)
            {
                return BadRequest(new ErrorDto()
                {
                    ErrorMessage = "Accessory not found",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            try
            {
                Repository.Remove(accessoryToDelete);
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
                Repository.RemoveRange(await Repository.GetAll<Accessory>());
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
        public async Task<ActionResult<IEnumerable<AccessoryDto>>> All()
        {
            var accessories = await Repository.GetAll<Accessory>();

            return Ok(Mapper.Map<IEnumerable<Accessory>, IEnumerable<AccessoryDto>>(accessories));
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccessoryDto>>> AllForForm()
        {
            var accessories = await Repository.GetAll<Accessory>();

            return Ok(Mapper.Map<IEnumerable<Accessory>, IEnumerable<AccessoryWithQuantityDto>>(accessories));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Select2InputDto>>> GetAllForDropDown()
        {
            var accessories = await Repository.GetAll<Accessory>();

            return Ok(Mapper.Map<IEnumerable<Entity>, IEnumerable<Select2InputDto>>(accessories));
        }
    }
}