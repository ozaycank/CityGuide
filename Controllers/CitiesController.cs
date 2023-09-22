using AutoMapper;
using CityGuide.Data;
using CityGuide.Dtos;
using CityGuide.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityGuide.Controllers
{
    [Route("api/Cities")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private IAppRepository _appRepository;
        private IMapper _mapper;
        public CitiesController (IAppRepository appRepository, IMapper mapper ){ 
            _appRepository = appRepository;
            _mapper = mapper;
        }
        public ActionResult GetCities()
        {
            var cities = _appRepository.GetCities();
            var citiesToReturn = _mapper.Map<List<CityForListDto>>(cities);
            return Ok(citiesToReturn);
        }
        [HttpPost]
        [Route("add")]
        public ActionResult Add([FromBody]City city) {
            _appRepository.Add(city);
            _appRepository.SaveAll();
            return Ok();
            
        }
        [HttpGet]
        [Route("detail")]
        public ActionResult GetCityById(int  id ) {
            var city  = _appRepository.GetCity(id);
            var cityToReturn = _mapper.Map<CityForDetailDto>(city);
            return Ok(cityToReturn);
            
        }
        [HttpGet]
        [Route("Photos")]
        public ActionResult GetPhotosByCity(int CityId) {
            var photos = _appRepository.GetPhotosByCity(CityId);
            return Ok(photos);
        }
    }
}
