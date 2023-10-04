using FashionShop.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FashionShop.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictsController : ControllerBase
    {
        private readonly IDistrictRepository _districtRepository; 
        public DistrictsController(IDistrictRepository districtRepository)
        {
            _districtRepository = districtRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _districtRepository.loadData();
            return Ok();
        }
    }
}
