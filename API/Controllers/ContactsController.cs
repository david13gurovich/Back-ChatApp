using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain;
using Services;
//using API.Data;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{   [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class contactsController : Controller
    {
        //{
        private readonly PomeloDB _context;
        private Service contactService;
        private string userLogIn = "string";
        public contactsController(PomeloDB context)
        {
            _context = context;
            contactService = new Service(context);
        }
        //get contacts
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            userLogIn = User.Claims.FirstOrDefault(claim => claim.Type == "UserId").Value;
            if (_context.Contact != null)
            {
                List<Contact> c = await contactService.GetContacts(userLogIn);
                if(c == null)
                {
                    return NotFound();
                }
                return Ok(c);

            }
            return BadRequest();

        }


        [HttpPost]
        [Route("/api/contacts")]
        public async Task<IActionResult> CreateContact([FromBody] Contact contact)
        {
            // JWT DO THIS IN EVERY START OF FUNCTION
            userLogIn = User.Claims.FirstOrDefault(claim => claim.Type == "UserId").Value;
            if (ModelState.IsValid)
            {
                if(_context.Contact == null || contact == null)
                { 
                    return null;
                }
                //using (var db = new PomeloDB())
                //{
                _context.Contact.Add(contact);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return Ok();
            }
            return BadRequest();
        }



        // GET: ContactsController/Details/5

        [HttpGet("{id}")]
        public async Task<ActionResult> Details(string id)
        {
            userLogIn = User.Claims.FirstOrDefault(claim => claim.Type == "UserId").Value;
            Contact c = await _context.Contact.FirstOrDefaultAsync(item => (item.ContactName == id) && item.UserName == userLogIn);
            return Json(c);
        }

        [HttpGet("{id}/messages")]
        public async Task<ActionResult> Messages(string id)
        {
            userLogIn = User.Claims.FirstOrDefault(claim => claim.Type == "UserId").Value;
            List<Message> m = await _context.Message.Where(item => (item.From == userLogIn) && (item.To == id)).ToListAsync();
            if (m == null)
            {
                return null;
            }

            //return Json(contactService.GetMessages(userLogIn, id));
            return Ok(m);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> EditContact(string id, [Bind("NickName,Server")] Contact contact)
        {
            userLogIn = User.Claims.FirstOrDefault(claim => claim.Type == "UserId").Value;
            if (ModelState.IsValid)
            {
                _context.Contact.Update(contact);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(string id)
        {
            userLogIn = User.Claims.FirstOrDefault(claim => claim.Type == "UserId").Value;
            if (ModelState.IsValid)
            {
                List<Contact> x = await _context.Contact.Where(item => (item.UserName == userLogIn) && (item.ContactName == id)).ToListAsync();
                _context.Contact.Remove(x[0]);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }


        [HttpGet("{id}/messages/{id2}")]
        public async Task<ActionResult> GetMeessage(string id, int id2)
        {
            userLogIn = User.Claims.FirstOrDefault(claim => claim.Type == "UserId").Value;
            return Json(await _context.Message.FirstOrDefaultAsync(item => item.Id == id2));
        }


        [HttpPost("{id}/messages")]
        public async Task<IActionResult> CreateMessage(string id, [FromBody] Message message)
        {
            userLogIn = User.Claims.FirstOrDefault(claim => claim.Type == "UserId").Value;
            //add from to (sent) id and login name 
            if (ModelState.IsValid)
            {
                message.From = userLogIn;
                message.To = id;
                //message.Created = DateTime.Now;
                Message m2 = new Message()
                {
                    Type = message.Type,
                    Content = message.Content,
                    Sent = !(message.Sent),
                    Created = message.Created,
                    From = id,
                    To = userLogIn
                };
                _context.Message.Add(message);
                _context.Message.Add(m2);
                //contactService.AddMessage(message);
                //contactService.AddMessage(m2);
                await _context.SaveChangesAsync();

                return Ok();
            }
            return BadRequest();
        }


        [HttpPut("{id}/messages/{id2}")]
        public async Task<IActionResult> EditMessage(int id2, [Bind("Id, Type, Content , Sent")] Message message)
        {
            userLogIn = User.Claims.FirstOrDefault(claim => claim.Type == "UserId").Value;
            if (ModelState.IsValid)
            {
                message.Id = id2;
                _context.Message.Update(message);
                await _context.SaveChangesAsync();
                //contactService.UpdateMessage(message);
                return Ok();
            }
            return BadRequest();
        }


        [HttpDelete("{id}/messages/{id2}")]
        public async Task<IActionResult> DeleteMessage(int id2)
        {
            userLogIn = User.Claims.FirstOrDefault(claim => claim.Type == "UserId").Value;
            if (ModelState.IsValid)
            {
                Message x = await _context.Message.FindAsync(id2);
                if (x != null)
                {
                    _context.Message.Remove(x);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    return NotFound();
                }
                //contactService.DeleteMessage(id2);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("[action]/time")]
        public async Task<ActionResult> GetTime()
        {
            return Json(DateTime.Now);
        }
    }
}



//// GET: Contacts1/Create
//public IActionResult Create()
//{
//    return View();
//}

//// POST: Contacts1/Create
//// To protect from overposting attacks, enable the specific properties you want to bind to.
//// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//[HttpPost]
//[ValidateAntiForgeryToken]
//public async Task<IActionResult> Create([Bind("Id,NickName,Server,Last,LastDate")] Contact contact)
//{
//    if (ModelState.IsValid)
//    {
//        contactService.AddContact(contact);
//        return RedirectToAction(nameof(Index));
//    }
//    return await Json(contact);
//}

//// GET: Contacts1/Edit/5
//public async Task<IActionResult> Edit(string id)
//{
//    if (id == null || _context.Contact == null)
//    {
//        return NotFound();
//    }

//    var contact = await _context.Contact.FindAsync(id);
//    if (contact == null)
//    {
//        return NotFound();
//    }
//    return View(contact);
//}

//// POST: Contacts1/Edit/5
//// To protect from overposting attacks, enable the specific properties you want to bind to.
//// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//[HttpPost]
//[ValidateAntiForgeryToken]
//public async Task<IActionResult> Edit(string id, [Bind("Id,NickName,Server,Last,LastDate")] Contact contact)
//{
//    if (id != contact.Id)
//    {
//        return NotFound();
//    }

//    if (ModelState.IsValid)
//    {
//        try
//        {
//            _context.Update(contact);
//            await _context.SaveChangesAsync();
//        }
//        catch (DbUpdateConcurrencyException)
//        {
//            if (!ContactExists(contact.Id))
//            {
//                return NotFound();
//            }
//            else
//            {
//                throw;
//            }
//        }
//        return RedirectToAction(nameof(Index));
//    }
//    return View(contact);
//}

//// GET: Contacts1/Delete/5
//public async Task<IActionResult> Delete(string id)
//{
//    if (id == null || _context.Contact == null)
//    {
//        return NotFound();
//    }

//    var contact = await _context.Contact
//        .FirstOrDefaultAsync(m => m.Id == id);
//    if (contact == null)
//    {
//        return NotFound();
//    }

//    return View(contact);
//}

//// POST: Contacts1/Delete/5
//[HttpPost, ActionName("Delete")]
//[ValidateAntiForgeryToken]
//public async Task<IActionResult> DeleteConfirmed(string id)
//{
//    if (_context.Contact == null)
//    {
//        return Problem("Entity set 'PomeloDB.Contact'  is null.");
//    }
//    var contact = await _context.Contact.FindAsync(id);
//    if (contact != null)
//    {
//        _context.Contact.Remove(contact);
//    }

//    await _context.SaveChangesAsync();
//    return RedirectToAction(nameof(Index));
//}

//private bool ContactExists(string id)
//{
//    return (_context.Contact?.Any(e => e.Id == id)).GetValueOrDefault();
//}

