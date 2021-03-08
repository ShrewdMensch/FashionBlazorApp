using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs;
using Application.InputModels;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace FashionAppBlazor.Server.Controllers
{
    public class ReadyToWearClothesController : BaseController
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadyToWearDto>> Get(Guid id)
        {
            var standardSize = await Repository.Get<ReadyToWear>(id);

            return Ok(Mapper.Map<ReadyToWear, ReadyToWearDto>(standardSize));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadyToWear>>> GetAll()
        {
            var readyTowear = await Repository.GetAll<ReadyToWear>();

            return Ok(Mapper.Map<IEnumerable<ReadyToWear>, IEnumerable<ReadyToWearDto>>(readyTowear));
        }

        [HttpGet("ForSelect2")]
        public async Task<ActionResult<IEnumerable<Select2InputDto>>> GetAllForDropDown()
        {
            var readyToWears = await Repository.GetAll<ReadyToWear>();

            return Ok(Mapper.Map<IEnumerable<Entity>, IEnumerable<Select2InputDto>>(readyToWears));
        }
        
       /* [HttpPost]
        public async Task<ActionResult> Post(ReadyToWearInputModel input)
        {
            var readyToWears = await Repository.GetAll<ReadyToWear>();

            var readyToWear = await Repository.Get<ReadyToWear>(input.Id);

            readyToWear.Name = input.Name;
            readyToWear.NumberInStock = input.NumberInStock;
            readyToWear.TypeOfClothId = input.TypeOfClothId;
            *//*_photoAccessor.AddOrUpdatePhotos(readyToWear, input.Files);*//*
            return Ok();
        }*/
    }
}