using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Handlers
{
    public class UpdateClinicPolicyRequirement : IAuthorizationRequirement
    {
        IHttpContextAccessor _contextAccessor;

        public async Task<bool> Pass(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            Clinic newClinic, originialClinic;
            try
            {

                string body = RequestBodyHandler.GetRawBodyString(_contextAccessor.HttpContext, new UTF8Encoding());
                newClinic = JsonConvert.DeserializeObject<Clinic>(body);
                _contextAccessor.HttpContext.Request.Headers.TryGetValue("username", out var username);
                User user = ObjectsRepository.users.Find(x => x.Username.Equals(username.ToString()));
                originialClinic = ObjectsRepository.clinics.Find(x => x.Id.Equals(newClinic.Id));
                if (user.Username.Equals(newClinic.CreatedBy.Username) || user.Roles.Contains("admin"))
                {
                    return await Task.FromResult(true);
                }
                else if (user.Roles.Contains("employee"))
                {
                    Clinic c = new Clinic { Id = newClinic.Id, IsActive = newClinic.IsActive, City = newClinic.City, Name = newClinic.Name, Description = newClinic.Description, Members = newClinic.Members, ZipCode = newClinic.ZipCode};
                    string json = JsonConvert.SerializeObject(c);
                    _contextAccessor.HttpContext.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(json));
                    return await Task.FromResult(true);
                }
                else if (user.Roles.Contains("partner") && originialClinic.Members != null)
                {
                    if (originialClinic.Members.Find(x => x.Username.Equals(user.Username)) != null)
                    {
                        Clinic c = new Clinic { Id = newClinic.Id, City = newClinic.City, Name = newClinic.Name, Description = newClinic.Description, Members = newClinic.Members, ZipCode = newClinic.ZipCode };
                        string json = JsonConvert.SerializeObject(c);
                        _contextAccessor.HttpContext.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(json));
                        return await Task.FromResult(true);
                    }
                }
                return await Task.FromResult(false);
            }
            catch (Exception e)
            {
                return await Task.FromResult(false);
            }

        }
    }
}
