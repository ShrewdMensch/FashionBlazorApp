using System;
using System.Collections.Generic;
using System.Linq;
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
    public class TypeOfClothesController : BaseController
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<TypeOfClothDto>> Get(Guid id)
        {
            var typeOfCloth = await Repository.Get<TypeOfCloth>(id);

            return Ok(Mapper.Map<TypeOfCloth, TypeOfClothDto>(typeOfCloth));
        }

        [HttpPost]
        public async Task<ActionResult<TypeOfClothDto>> Upsert(TypeOfClothInputModel typeOfClothInput)
        {

            try
            {
                var typeOfCloth = await Repository.Get<TypeOfCloth>(typeOfClothInput.Id);

                if (typeOfCloth != null)
                {
                    typeOfCloth.Name = typeOfClothInput.Name;
                    typeOfCloth.ProductionDays = typeOfClothInput.ProductionDays;

                    UpdateTypeOfClothMeasurementHeaders(typeOfClothInput, typeOfCloth);
                    UpdateTypeOfClothAccessories(typeOfClothInput, typeOfCloth);
                    UpdateTypeOfClothIncurredExpenses(typeOfClothInput, typeOfCloth);
                }

                else
                {
                    typeOfCloth  = new TypeOfCloth
                    {
                        Name = typeOfClothInput.Name.ToTitleCase(),
                        ProductionDays = typeOfClothInput.ProductionDays
                    };

                    Repository.Add(typeOfCloth);

                    AddTypeOfClothMeasurementHeaders(typeOfClothInput, typeOfCloth);
                    AddTypeOfClothAccessories(typeOfClothInput, typeOfCloth);
                    AddTypeOfClothIncurredExpenses(typeOfClothInput, typeOfCloth);
                }

                await Repository.SaveAll();

                return Ok(Mapper.Map<TypeOfCloth, TypeOfClothDto>(typeOfCloth));
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
            var typeOfClothToDelete = await Repository.Get<TypeOfCloth>(id);

            if (typeOfClothToDelete == null)
            {
                return BadRequest(new ErrorDto()
                {
                    ErrorMessage = "Accessory not found",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            try
            {
                Repository.Remove(typeOfClothToDelete);
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
                Repository.RemoveRange(await Repository.GetAll<TypeOfCloth>());
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
        public async Task<ActionResult<IEnumerable<TypeOfClothDto>>> All()
        {
            var typeOfClothes = await Repository.GetAll<TypeOfCloth>();

            return Ok(Mapper.Map<IEnumerable<TypeOfCloth>, IEnumerable<TypeOfClothDto>>(typeOfClothes));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Select2InputDto>>> GetAllForDropDown()
        {
            var typeOfClothes = await Repository.GetAll<TypeOfCloth>();

            return Ok(Mapper.Map<IEnumerable<Entity>, IEnumerable<Select2InputDto>>(typeOfClothes));
        }

        private void UpdateTypeOfClothMeasurementHeaders(TypeOfClothInputModel typeOfClothInput, TypeOfCloth typeOfCloth)
        {
            // Remove existing TypeOfClothMeasurementHeaders before adding possibly updated ones
            //Case where Updating with no TypeOfClothMeasurementHeaders i.e remove all existing ones without adding new one(s)
            Repository.RemoveRange(typeOfCloth.MeasurementHeaders);

            if (typeOfClothInput.MeasurementHeaderId == null) return;

            for (var count = 0; count < typeOfClothInput.MeasurementHeaderId.Count(); count++)
            {
                var newTypeClothMeasurementHeader = new TypeOfClothMeasurementHeader
                {
                    TypeOfCloth = typeOfCloth,
                    MeasurementHeaderId = typeOfClothInput.MeasurementHeaderId.ElementAt(count)
                };

                Repository.Add(newTypeClothMeasurementHeader);
            }
        }

        private void UpdateTypeOfClothIncurredExpenses(TypeOfClothInputModel typeOfClothInput, TypeOfCloth typeOfCloth)
        {
            // Remove existing TypeOfClothIncurredExpenses before adding possibly updated ones
            //Case where Updating with no TypeOfClothIncurredExpenses i.e remove all existing ones without adding new one(s)
            Repository.RemoveRange(typeOfCloth.IncurredExpenses);

            if (typeOfClothInput.IncurredExpenseId == null) return;

            for (var count = 0; count < typeOfClothInput.IncurredExpenseId.Count(); count++)
            {
                var newTypeClothIncurredExpense = new TypeOfClothIncurredExpense
                {
                    TypeOfCloth = typeOfCloth,
                    IncurredExpenseId = typeOfClothInput.IncurredExpenseId.ElementAt(count)
                };

                Repository.Add(newTypeClothIncurredExpense);
            }
        }

        private void UpdateTypeOfClothAccessories(TypeOfClothInputModel typeOfClothInput, TypeOfCloth typeOfCloth)
        {
            // Remove existing TypeOfClothAccessory before adding possibly updated ones
            //Case where Updating with no TypeOfClothAccessory i.e remove all existing ones without adding new one(s)
            Repository.RemoveRange(typeOfCloth.Accessories);

            if (typeOfClothInput.Accessories == null) return;

            for (var count = 0; count < typeOfClothInput.Accessories.Count(); count++)
            {
                if (typeOfClothInput.Accessories.ElementAt(count).Quantity > 0)
                {
                    var newTypeClothAccessory = new TypeOfClothAccessory
                    {
                        TypeOfCloth = typeOfCloth,
                        AccessoryId = typeOfClothInput.Accessories.ElementAt(count).Id,
                        Quantity = typeOfClothInput.Accessories.ElementAt(count).Quantity
                    };

                    Repository.Add(newTypeClothAccessory);
                }
            }
        }

        private void AddTypeOfClothMeasurementHeaders(TypeOfClothInputModel typeOfClothInput, TypeOfCloth newTypeOfCloth)
        {
            if (typeOfClothInput.MeasurementHeaderId == null) return;

            for (var count = 0; count < typeOfClothInput.MeasurementHeaderId.Count(); count++)
            {
                var newTypeClothMeasurementHeader = new TypeOfClothMeasurementHeader
                {
                    TypeOfCloth = newTypeOfCloth,
                    MeasurementHeaderId = typeOfClothInput.MeasurementHeaderId.ElementAt(count)
                };

                Repository.Add(newTypeClothMeasurementHeader);
            }
        }

        private void AddTypeOfClothIncurredExpenses(TypeOfClothInputModel typeOfClothInput, TypeOfCloth newTypeOfCloth)
        {
            if (typeOfClothInput.IncurredExpenseId == null) return;

            for (var count = 0; count < typeOfClothInput.IncurredExpenseId.Count(); count++)
            {
                var newTypeClothIncurredExpense = new TypeOfClothIncurredExpense
                {
                    TypeOfCloth = newTypeOfCloth,
                    IncurredExpenseId = typeOfClothInput.IncurredExpenseId.ElementAt(count)
                };

                Repository.Add(newTypeClothIncurredExpense);
            }
        }

        private void AddTypeOfClothAccessories(TypeOfClothInputModel typeOfClothInput, TypeOfCloth newTypeOfCloth)
        {
            if (typeOfClothInput.Accessories == null) return;

            for (var count = 0; count < typeOfClothInput.Accessories.Count(); count++)
            {
                if (typeOfClothInput.Accessories.ElementAt(count).Quantity > 0)
                {
                    var newTypeClothAccessory = new TypeOfClothAccessory
                    {
                        TypeOfCloth = newTypeOfCloth,
                        AccessoryId = typeOfClothInput.Accessories.ElementAt(count).Id,
                        Quantity = typeOfClothInput.Accessories.ElementAt(count).Quantity
                    };

                    Repository.Add(newTypeClothAccessory);
                }
            }
        }
    }
}