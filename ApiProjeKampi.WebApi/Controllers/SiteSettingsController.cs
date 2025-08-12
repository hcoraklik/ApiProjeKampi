using ApiProjeKampi.WebApi.Context;
using ApiProjeKampi.WebApi.Dtos.ContactDtos;
using ApiProjeKampi.WebApi.Dtos.SiteSettingDtos;
using ApiProjeKampi.WebApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class SiteSettingsController : ControllerBase
    {
        private readonly ApiContext _context;

        public SiteSettingsController(ApiContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult SiteSettingList()
        {
            var values = _context.SiteSettings.FirstOrDefault();
            return Ok(values);
        }
        [HttpPost]
        public IActionResult CreateSiteSettings(CreateSiteSettingDto createSiteSettingDto)
        {
            SiteSetting siteSetting = new SiteSetting();
            siteSetting.Email = createSiteSettingDto.Email;
            siteSetting.Address = createSiteSettingDto.Address;
            siteSetting.Phone = createSiteSettingDto.Phone;
            _context.SiteSettings.Add(siteSetting);
            _context.SaveChanges();
            return Ok("SiteSetting ekleme İşlemi Başarılı");
        }
        [HttpPut]

        public IActionResult UpdateSiteSetting(UpdateSiteSettingDto updateSiteSettingDto)
        {
            SiteSetting siteSetting = new SiteSetting();
            siteSetting.Email = updateSiteSettingDto.Email;
            siteSetting.Address = updateSiteSettingDto.Address;
            siteSetting.Phone = updateSiteSettingDto.Phone;
            siteSetting.SiteSettingId = updateSiteSettingDto.SiteSettingId;
            _context.SiteSettings.Update(siteSetting);
            _context.SaveChanges();
            return Ok("SiteSetting Güncelleme İşlemi Başarılı");
        }






    }

    
} 
