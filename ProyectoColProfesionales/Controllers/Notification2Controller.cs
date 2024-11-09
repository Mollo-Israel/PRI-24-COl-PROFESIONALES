using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoColProfesionales.Models.DB1;

namespace ProyectoColProfesionales.Controllers
{
    public class Notification2Controller : Controller
    {
        private readonly DBColProfessionalContext _context;

        public Notification2Controller(DBColProfessionalContext context)
        {
            _context = context;
        }

        // GET: Notification2
        public async Task<IActionResult> Index()
        {
            var notifications = _context.Notifications2
                .Include(n => n.Person)
                .ToListAsync();
            return View(await notifications);
        }

        // GET: Notification2/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var notification = await _context.Notifications2
                .Include(n => n.Person)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (notification == null)
                return NotFound();

            return View(notification);
        }

        // GET: Notification2/Create
        public IActionResult Create()
        {
            ViewData["IdPerson"] = new SelectList(_context.People, "IdPerson", "Names");
            return View();
        }
        // POST: Notification2/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Message,Date1,Date2,Date3,Status,IdPerson")] Notification2 notification2)
        {
            // Validar que la fecha seleccionada no sea en el pasado
            if (notification2.Date1 < DateTime.Now || notification2.Date2 < DateTime.Now || notification2.Date3 < DateTime.Now)
            {
                ModelState.AddModelError("Date1", "La fechas de actividad debe ser posterior o igual a la fecha actual.");
            }


            // Validar que al menos una fecha esté presente
            if (!notification2.Date1.HasValue && !notification2.Date2.HasValue && !notification2.Date3.HasValue)
            {
                ModelState.AddModelError("", "Debe ingresar al menos una fecha válida.");
            }

            if (ModelState.IsValid)
            {
                // Lógica para asignar DateTime.MinValue a las fechas no ingresadas
                if (notification2.Date1 == null)
                    notification2.Date1 = DateTime.MinValue;
                if (notification2.Date2 == null)
                    notification2.Date2 = DateTime.MinValue;
                if (notification2.Date3 == null)
                    notification2.Date3 = DateTime.MinValue;

                _context.Add(notification2);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["IdPerson"] = new SelectList(_context.People, "IdPerson", "Names", notification2.IdPerson);
            return View(notification2);
        }


        // GET: Notification2/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var notification = await _context.Notifications2.FindAsync(id);
            if (notification == null)
                return NotFound();

            ViewData["IdPerson"] = new SelectList(_context.People, "IdPerson", "Names", notification.IdPerson);
            return View(notification);
        }

        // Similar lógica para el método Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Message,Date1,Date2,Date3,Status,IdPerson")] Notification2 notification2)
        {
            if (id != notification2.Id)
                return NotFound();

            // Validar que al menos una fecha esté presente
            if (!notification2.Date1.HasValue && !notification2.Date2.HasValue && !notification2.Date3.HasValue)
            {
                ModelState.AddModelError("", "Debe ingresar al menos una fecha válida.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Lógica para asignar DateTime.MinValue a las fechas no ingresadas
                    if (notification2.Date1 == null)
                        notification2.Date1 = DateTime.MinValue;
                    if (notification2.Date2 == null)
                        notification2.Date2 = DateTime.MinValue;
                    if (notification2.Date3 == null)
                        notification2.Date3 = DateTime.MinValue;

                    _context.Update(notification2);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Notification2Exists(notification2.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["IdPerson"] = new SelectList(_context.People, "IdPerson", "Names", notification2.IdPerson);
            return View(notification2);
        }


        // GET: Notification2/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var notification = await _context.Notifications2
                .Include(n => n.Person)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (notification == null)
                return NotFound();

            return View(notification);
        }

        // POST: Notification2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var notification = await _context.Notifications2.FindAsync(id);
            _context.Notifications2.Remove(notification);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Notification2Exists(int id)
        {
            return _context.Notifications2.Any(e => e.Id == id);
        }
    }
}
