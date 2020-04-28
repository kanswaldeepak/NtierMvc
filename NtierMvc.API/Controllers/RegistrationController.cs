using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json;
using System.Collections;
using NtierMvc.BusinessLogic.Interface;
using NtierMvc.Model.Account;
using NtierMvc.BusinessLogic.Worker;

namespace NtierMvc.API.Controllers.Application
{
    public class RegistrationController : ApiController
    {
        IAccountWorker _repository = new AccountWorker();

        
        [HttpPost]
        [ResponseType(typeof(Dictionary<int, string>))]
        public IHttpActionResult Post(ArrayList paramList)
        {
            UserEntity objUser = JsonConvert.DeserializeObject<UserEntity>(paramList[0].ToString());
            return Ok(_repository.SaveApplicationRegistraionDetails(objUser));
        }
        
    }
}
