using ApiProjeKampi.WebApi.Context;
using ApiProjeKampi.WebApi.Dtos.AboutDtos;
using ApiProjeKampi.WebApi.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AboutsController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;

        public AboutsController(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AboutList()
        {
            var values = _context.Abouts.ToList();
            return Ok(values);

        }


        [HttpPost]
        public IActionResult CreateAbout(CreateAboutDto createAboutDto)
        {
            //_context.Abouts.Add(About);
            //_context.SaveChanges();
            var value = _mapper.Map<About>(createAboutDto);
            _context.Abouts.Add(value);
            _context.SaveChanges();
            return Ok("Hakkında ekleme işlemi başarılı");
        }

        [HttpDelete]
        public IActionResult DeleteAbout(int id)
        {
            var value = _context.Abouts.Find(id);
            _context.Abouts.Remove(value);
            _context.SaveChanges();
            return Ok("Hakkında Silme İşlemi Başarılı");

        }
        [HttpGet("GetAbout")]
        public IActionResult GetAbout(int id)
        {
            var value = _context.Abouts.Find(id);
            return Ok(value);
        }

        [HttpPut]
        public IActionResult UpdateAbout(UpdateAboutDto updateAboutDto)
        {
            var value = _mapper.Map<About>(updateAboutDto);
            _context.Abouts.Update(value);
            _context.SaveChanges();
            return Ok("Hakkında Güncelleme İşlemi Başarılı");

        }


    }
}
