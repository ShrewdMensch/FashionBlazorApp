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
    public class MeasurementHeadersController : BaseController
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<EntityDto>> Get(Guid id)
        {
            var measurementHeader = await Repository.Get<MeasurementHeader>(id);

            return Ok(Mapper.Map<Entity, EntityDto>(measurementHeader));
        }

        [HttpPost]
        public async Task<ActionResult<EntityDto>> Upsert(EntityDto measurementHeaderDto)
        {
            var measurementHeader = Mapper.Map<EntityDto, MeasurementHeader>(measurementHeaderDto);
            measurementHeader.Name = measurementHeader.Name.ToTitleCase();

            try
            {
                if (measurementHeader.Id != default)
                {
                    Repository.Attach(measurementHeader).State = EntityState.Modified;
                }

                else
                {
                    Repository.Add(measurementHeader);
                }

                await Repository.SaveAll();

                return Ok(Mapper.Map<MeasurementHeader, EntityDto>(measurementHeader));
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
            var measurementHeaderToDelete = await Repository.Get<MeasurementHeader>(id);

            if (measurementHeaderToDelete == null)
            {
                return BadRequest(new ErrorDto()
                {
                    ErrorMessage = "Entity not found",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            try
            {
                Repository.Remove(measurementHeaderToDelete);
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
                Repository.RemoveRange(await Repository.GetAll<MeasurementHeader>());
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
            var measurementHeaders = await Repository.GetAll<MeasurementHeader>();

            return Ok(Mapper.Map<IEnumerable<Entity>, IEnumerable<EntityDto>>(measurementHeaders));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Select2InputDto>>> GetAllForDropDown()
        {
            var measurementHeaders = await Repository.GetAll<MeasurementHeader>();

            return Ok(Mapper.Map<IEnumerable<Entity>, IEnumerable<Select2InputDto>>(measurementHeaders));
        }
    }
}