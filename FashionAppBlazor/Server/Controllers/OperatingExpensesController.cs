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
    public class OperatingExpensesController : BaseController
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<OperatingExpenseDto>> Get(Guid id)
        {
            var operatingExpense = await Repository.Get<OperatingExpense>(id);

            return Ok(Mapper.Map<Entity, EntityDto>(operatingExpense));
        }

        [HttpPost]
        public async Task<ActionResult<OperatingExpenseDto>> Upsert(OperatingExpenseInputModel operatingExpenseInput)
        {
            var operatingExpense = Mapper.Map<OperatingExpenseInputModel, OperatingExpense>(operatingExpenseInput);
            operatingExpense.Name = operatingExpense.Name.ToTitleCase();

            try
            {
                if (operatingExpense.Id != default)
                {
                    Repository.Attach(operatingExpense).State = EntityState.Modified;
                }

                else
                {
                    Repository.Add(operatingExpense);
                }

                await Repository.SaveAll();

                return Ok(Mapper.Map<OperatingExpense, OperatingExpenseDto>(operatingExpense));
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
            var operatingExpenseToDelete = await Repository.Get<OperatingExpense>(id);

            if (operatingExpenseToDelete == null)
            {
                return BadRequest(new ErrorDto()
                {
                    ErrorMessage = "Entity not found",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            try
            {
                Repository.Remove(operatingExpenseToDelete);
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
                Repository.RemoveRange(await Repository.GetAll<OperatingExpense>());
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
        public async Task<ActionResult<IEnumerable<OperatingExpenseDto>>> All()
        {
            var operatingExpenses = await Repository.GetAll<OperatingExpense>();

            return Ok(Mapper.Map<IEnumerable<OperatingExpense>, IEnumerable<OperatingExpenseDto>>(operatingExpenses));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Select2InputDto>>> GetAllForDropDown()
        {
            var operatingExpenses = await Repository.GetAll<OperatingExpense>();

            return Ok(Mapper.Map<IEnumerable<Entity>, IEnumerable<Select2InputDto>>(operatingExpenses));
        }
    }
}