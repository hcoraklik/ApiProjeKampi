using ApiProjeKampi.WebApi.Context;
using ApiProjeKampi.WebApi.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class YummyEventsController : ControllerBase
    {
        private readonly ApiContext _context;
        public YummyEventsController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult YummyEventList()
        {
            var values = _context.YummyEvents.ToList();
            return Ok(values);

        }


        [HttpPost]
        public IActionResult CreateYummyEvent(YummyEvent YummyEvent)
        {
            _context.YummyEvents.Add(YummyEvent);
            _context.SaveChanges();
            return Ok("Etkinlik ekleme işlemi başarılı");
        }

        [HttpDelete]
        public IActionResult DeleteYummyEvent(int id)
        {
            var value = _context.YummyEvents.Find(id);
            _context.YummyEvents.Remove(value);
            _context.SaveChanges();
            return Ok("Etkinlik Silme İşlemi Başarılı");

        }
        [HttpGet("GetYummyEvent")]
        public IActionResult GetYummyEvent(int id)
        {
            var value = _context.YummyEvents.Find(id);
            return Ok(value);
        }

        [HttpPut]
        public IActionResult UpdateYummyEvent(YummyEvent YummyEvent)
        {
            _context.YummyEvents.Update(YummyEvent);
            _context.SaveChanges();
            return Ok("Etkinlik Güncelleme İşlemi Başarılı");

        }
    }
}
