using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Text;
using WebApi.Handlers;
using WebApi.Models;


namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicController : ControllerBase
    {


        [HttpPost]
        [Route("Update")]
        [Authorize(Policy = "UpdateClinic")]
        public IActionResult CreateOrUpdate([FromHeader]string username)
        {
            try
            {
                string body = RequestBodyHandler.GetRawBodyString(HttpContext, new UTF8Encoding());
                Clinic newClinic = JsonConvert.DeserializeObject<Clinic>(body);
                ObjectsRepository.clinics[ObjectsRepository.clinics.FindIndex(x => x.Id.Equals(newClinic.Id))] = newClinic;
                return Ok(ObjectsRepository.clinics.Find(x => x.Id.Equals(newClinic.Id)));
                
            }catch(Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetClinic")]
        [AllowAnonymous]
        public IActionResult GetClinic(int id)
        {
            Clinic c = ObjectsRepository.clinics.Find(x => x.Id.Equals(id));
            return Ok(c);
        }

        
    }
    
}