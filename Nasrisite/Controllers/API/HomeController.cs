using Nasrisite.Models;
using Nasrisite.MyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Nasrisite.Controllers.API
{

    [RoutePrefix("api/Home")]
    public class HomeController : ApiController
    {
        SWENEntities DBentities = new SWENEntities();

        // //localhost:44366/api/Home/
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public Employee Get(int id)
        {
            return new Employee { ID = 1, Address = "Ramallah", ContactNumber = 1234, Name = "Nasri" };
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

        // localhost:44366/api/Home/login/Nasri
        //Login API recvie username and password
        [HttpPost]
        [Route("login/Nasri", Name = "Login")]
        public User Login([FromBody] LoginModel login)
        {
            //Employee emp;
            User user  = DBentities.Users.Where(emp => emp.UserPassword == login.Password && emp.UserName == login.UserName).FirstOrDefault();
            if (user == null)
            {
                return null;
            }
            //new Employee { ID = 1, Address = "Ramallah", ContactNumber = 1234, Name = "Nasri" };
            return user;
        }
       
    }
}