using Buisness.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        ICarService _carservice;
        public CarsController(ICarService carService)
        {
            _carservice = carService;
        }
        [HttpGet("getall")]
        public IActionResult Get()
        {
            var result = _carservice.GetAll();
            if (result.Success)
            {
               return Ok(result);
            }
           return BadRequest(result);
        }
        [HttpPost("add")]
        public IActionResult Add(Car car)
        {
            var result = _carservice.Add(car);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _carservice.GetById(id);
            if (result.Success)
            {
                return Ok(result);

            }
            return BadRequest(result);
        }
        [HttpGet("getallwithcardetails")]
        public IActionResult GetAllDetails()
        {
            var result = _carservice.GetCarDetails();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getallbybrandid")]
        public IActionResult GetAllByBrand(int id)
        {
            var result = _carservice.GetAllByBrandId(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getallbybrandidwithdetails")]
        public IActionResult GetAllByBrandWithDetails(int id)
        {
           var result = _carservice.GetAllByBrandIdWithDetails(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getallbycoloridwithdetails")]
        public IActionResult GetAllByColorWithDetails(int id)
        {
            var result = _carservice.GetAllByColorIdWithDetails(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getbyidwithdetails")]
        public IActionResult GetByIdWithDetails(int id)
        {
            var result = _carservice.GetAllByIdWithDetails(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
