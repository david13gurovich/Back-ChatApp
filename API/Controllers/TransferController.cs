using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
//using API.Data;
using Domain;
using API.Data;
using Microsoft.AspNetCore.SignalR;

namespace API.Controllers
{
    [ApiController]
    [Route("api/transfer")]
    public class TransferController : Controller
    {
        private readonly PomeloDB _context;
        private readonly IHubContext<MyHub> myHub;

        public TransferController(PomeloDB context, IHubContext<MyHub> myHub)
        {
            _context = context;
            this.myHub = myHub;
        }

        [HttpPost]
        [Route("/api/transfer")]
        public async Task<IActionResult> Transfer([Bind("From,To,Content")] TransferReq message)
        {
            if (ModelState.IsValid)
            {
                //add from to (sent) id and login name 
                if (ModelState.IsValid)
                {
                    message.From = message.From;
                    message.To = message.To;
                    //message.Created = DateTime.Now;
                    Message m2 = new Message()
                    {
                        Type = "text",
                        Content = message.Content,
                        Sent = false,
                        Created = DateTime.Now,
                        From = message.From,
                        To = message.To
                    };
                    _context.Message.Add(m2);
                    //contactService.AddMessage(message);
                    //contactService.AddMessage(m2);
                    await _context.SaveChangesAsync();

                    await myHub.Clients.All.SendAsync("NewContact", new TransferReq(message.From, message.To, message.Content));
                    return Ok();
                }
                return BadRequest();
            }
            return BadRequest();
        }

        //// GET: Transfer
        //public async Task<IActionResult> Index()
        //{
        //      return _context.Contact != null ? 
        //                  View(await _context.Contact.ToListAsync()) :
        //                  Problem("Entity set 'PomeloDB.Contact'  is null.");
        //}

        //// GET: Transfer/Details/5
        //public async Task<IActionResult> Details(string id)
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

        //// GET: Transfer/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Transfer/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,NickName,Server,Last,LastDate")] Contact contact)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(contact);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(contact);
        //}

        //// GET: Transfer/Edit/5
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

        //// POST: Transfer/Edit/5
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

        //// GET: Transfer/Delete/5
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

        //// POST: Transfer/Delete/5
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
        //  return (_context.Contact?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
