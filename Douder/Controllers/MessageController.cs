using Douder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Douder.Controllers
{
    public class MessageController : ApiController
    {
        
        DouderContext _context;

        public MessageController(DouderContext con)
        {
            _context = con;
            
        }

        public MessageController()
        { }


        public IEnumerable<Message> GetAllMessages()
        {
            return _context.Message.ToList();
        }


        public IHttpActionResult GetMessage(int id)
        {
            var mes = _context.Message.FirstOrDefault(m => m.ID == id);
            if (mes == null)
                return NotFound();
            return Ok(mes);

        }

    }
}
