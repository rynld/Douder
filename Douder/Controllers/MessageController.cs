using Douder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;

namespace Douder.Controllers
{
    
   
    public class MessageController : ApiController
    {
        
        DouderContext _context;
        ApplicationUserManager _userManager;

        public MessageController(DouderContext con, ApplicationUserManager user_man)
        {
            _context = con;
            _userManager = user_man;            
        }


        [Route("api/Messages/isnull")]
        public IHttpActionResult GetNull()
        {
            if (_userManager == null)
                return BadRequest();
            return Ok(UserManager.Users.ToList());
            
        }
        public MessageController()
        { }

        
        public IEnumerable<Message> GetAllMessages()
        {
            return _context.Message.ToList();
        }
 
        [ResponseType(typeof(Message))]
        public IHttpActionResult GetMessage(int id)
        {
            var mes = _context.Message.FirstOrDefault(m => m.ID == id);
            if (mes == null)
                return NotFound();
            return Ok(mes);
            
        }

        [Route("api/Messages/fromuser")]
        [ResponseType(typeof(IEnumerable<Message>))]
        public IHttpActionResult GetMessageFromUser(string user_id)
        {
            var mes = _context.Users.FirstOrDefault(u => u.Id == user_id);
            if (mes != null)
                return Ok(mes.Messages);
            return NotFound();
        }

        [Route("api/Messages/{id:int}/coord")]
        [ResponseType(typeof(Coordinate))]
        public IHttpActionResult GetMessageCoord(int id)
        {
            var mes = _context.Message.Where(m => m.ID == id);
            if (mes != null)
                return Ok(mes.Select(AsCoord).FirstOrDefault());
            return NotFound();
        }

        [HttpPost]
        public IHttpActionResult PostMessages([FromBody]Message mes)
        {
            mes.User = UserManager.FindByName(User.Identity.Name);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            _context.Message.Add(mes);
            _context.SaveChanges();

            return Ok();
        }

        [Route("api/Messages/currentuser")]
        public IHttpActionResult GetCurrentUser()
        {
            return Ok(User.Identity.Name);
        }

        private static readonly Expression<Func<Message, Coordinate>> AsCoord =
            x => new Coordinate() {
                CoordX = x.CoordX,
                CoordY = x.CoordY
            };
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
    }
}
