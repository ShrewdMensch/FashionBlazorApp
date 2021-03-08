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
    public class StandardSizesController : BaseController
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<EntityDto>> Get(Guid id)
        {
            var standardSize = await Repository.Get<StandardSize>(id);

            return Ok(Mapper.Map<Entity, EntityDto>(standardSize));
        }

        [HttpPost]
        public async Task<ActionResult<EntityDto>> Upsert(EntityDto entityDto)
        {
            var standardSize = Mapper.Map<EntityDto, StandardSize>(entityDto);
            standardSize.Name = standardSize.Name.ToTitleCase();

            try
            {
                if (standardSize.Id != default)
                {
                    Repository.Attach(standardSize).State = EntityState.Modified;
                }

                else
                {
                    Repository.Add(standardSize);
                }

                await Repository.SaveAll();

                return Ok(Mapper.Map<StandardSize, EntityDto>(standardSize));
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
            var standardSizeToDelete = await Repository.Get<StandardSize>(id);

            if (standardSizeToDelete == null)
            {
                return BadRequest(new ErrorDto()
                {
                    ErrorMessage = "Entity not found",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            try
            {
                Repository.Remove(standardSizeToDelete);
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
                Repository.RemoveRange(await Repository.GetAll<StandardSize>());
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
        public async Task<ActionResult<IEnumerable<EntityDto>>> All()
        {
            var standardSizes = await Repository.GetAll<StandardSize>();

            return Ok(Mapper.Map<IEnumerable<Entity>, IEnumerable<EntityDto>>(standardSizes));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Select2InputDto>>> GetAllForDropDown()
        {
            var standardSizes = await Repository.GetAll<StandardSize>();

            return Ok(Mapper.Map<IEnumerable<Entity>, IEnumerable<Select2InputDto>>(standardSizes));
        }
    }
}