using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Extensions;
using Application.InputModels;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FashionAppBlazor.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    public class IncurredExpensesController : BaseController
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<IncurredExpenseDto>> Get(Guid id)
        {
            var incurredExpense = await Repository.Get<IncurredExpense>(id);

            return Ok(Mapper.Map<Entity, EntityDto>(incurredExpense));
        }

        [HttpPost]
        public async Task<ActionResult<IncurredExpenseDto>> Upsert(IncurredExpenseDto incurredExpenseInput)
        {
            var incurredExpense = Mapper.Map<IncurredExpenseDto, IncurredExpense>(incurredExpenseInput);
            incurredExpense.Name = incurredExpense.Name.ToTitleCase();

            try
            {
                if (incurredExpense.Id != default)
                {
                    Repository.Attach(incurredExpense).State = EntityState.Modified;
                }

                else
                {
                    Repository.Add(incurredExpense);
                }

                await Repository.SaveAll();

                return Ok(Mapper.Map<IncurredExpense, IncurredExpenseDto>(incurredExpense));
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
            var incurredExpenseToDelete = await Repository.Get<IncurredExpense>(id);

            if (incurredExpenseToDelete == null)
            {
                return BadRequest(new ErrorDto()
                {
                    ErrorMessage = "Entity not found",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            try
            {
                Repository.Remove(incurredExpenseToDelete);
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
                Repository.RemoveRange(await Repository.GetAll<IncurredExpense>());
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
        public async Task<ActionResult<IEnumerable<IncurredExpenseDto>>> All()
        {
            var incurredExpenses = await Repository.GetAll<IncurredExpense>();

            return Ok(Mapper.Map<IEnumerable<IncurredExpense>, IEnumerable<IncurredExpenseDto>>(incurredExpenses));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Select2InputDto>>> GetAllForDropDown()
        {
            var incurredExpenses = await Repository.GetAll<IncurredExpense>();

            return Ok(Mapper.Map<IEnumerable<Entity>, IEnumerable<Select2InputDto>>(incurredExpenses));
        }
    }
}