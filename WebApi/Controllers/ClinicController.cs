using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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
        [EnableBodyRewind]
        public IActionResult CreateOrUpdate([FromHeader]string username)
        {
            try
            {
                string body = ObjectsRepository.GetRawBodyString(HttpContext, new UTF8Encoding());
                Clinic newClinic = JsonConvert.DeserializeObject<Clinic>(body);
                ObjectsRepository.clinics[ObjectsRepository.clinics.FindIndex(x => x.Id.Equals(newClinic.Id))] = newClinic;
                return Ok(ObjectsRepository.clinics.Find(x => x.Id.Equals(newClinic.Id)));
                
            }catch(Exception e)
            {
                return BadRequest();
            }
            //if (success)
            //{
            //    return Request.CreateResponse(HttpStatusCode.OK);
            //}
            //else
            //{
            //    return Request.CreateResponse(HttpStatusCode.BadRequest);
            //}
        }

        [HttpGet]
        [Route("GetClinic")]
        [AllowAnonymous]
        [EnableBodyRewind]
        public IActionResult GetClinic(int id)
        {
            Clinic c = ObjectsRepository.clinics.Find(x => x.Id.Equals(id));
            return Ok(c);
        }

        
    }

    public class EnableBodyRewind : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var bodyStr = "";
            var req = context.HttpContext.Request;

            // Allows using several time the stream in ASP.Net Core
            req.EnableRewind();

            // Arguments: Stream, Encoding, detect encoding, buffer size 
            // AND, the most important: keep stream opened
            using (StreamReader reader
                      = new StreamReader(req.Body, Encoding.UTF8, true, 1024, true))
            {
                bodyStr = reader.ReadToEnd();
            }

            // Rewind, so the core is not lost when it looks the body for the request
            req.Body.Position = 0;

            // Do whatever work with bodyStr here

        }
    }

}