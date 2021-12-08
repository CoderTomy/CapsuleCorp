using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CapsuleCorp.Context;
using CapsuleCorp.Models;
using Microsoft.AspNetCore.Http;

namespace CapsuleCorp.Controllers
{
    public class TurnoController : Controller
    {
        private readonly CapsuleCorpDatabaseContext _context;

        public TurnoController(CapsuleCorpDatabaseContext context)
        {
            _context = context;
        }

        // GET: Turno
        public async Task<IActionResult> Index()
        {
            var capsuleCorpDatabaseContext = _context.Turnos.Include(t => t.paciente);
            return View(await capsuleCorpDatabaseContext.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Index(string busqueda)
        {
            var adminLog = HttpContext.Session.GetString("admin");

            if (String.IsNullOrEmpty(adminLog))
            {
                return RedirectToAction("Login", "Administrador");
            }

            ViewData["ObtenerPacientes"] = busqueda;

            var varPacientes = from p in _context.Turnos.Include(c => c.paciente) select p;

            if (!String.IsNullOrEmpty(busqueda))
            {
                varPacientes = varPacientes.Where(s => s.paciente.apellido.Contains(busqueda));
            }

            return View(await varPacientes.AsNoTracking().ToListAsync());
        }

        // GET: Turno/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turno = await _context.Turnos
                .Include(t => t.paciente)
                .FirstOrDefaultAsync(m => m.turnoID == id);
            if (turno == null)
            {
                return NotFound();
            }

            return View(turno);
        }

        // GET: Turno/Create
        public IActionResult Create()
        {
            // ViewData["pacienteID"] = new SelectList(_context.Pacientes, "pacienteID", "apellido");
            
            ViewData["pacienteID2"] =
                new SelectList((from a in _context.Pacientes
                                select new
                                {
                                    ID = a.pacienteID,
                                    nombreCompleto = a.nombre + " " + a.apellido
                                }),
                "ID",
                "nombreCompleto");

            ViewBag.SuccessMessage = "¡Se ha creado el turno correctamente!";
            return View();
        }

        // POST: Turno/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("turnoID,fecha,especialidad,pacienteID")] Turno turno)
        {
            var turnosFromDB = await _context.Turnos.FirstOrDefaultAsync(x => x.fecha.Year == turno.fecha.Year && 
            x.fecha.Month == turno.fecha.Month && x.fecha.Day == turno.fecha.Day &&
            x.fecha.Hour == turno.fecha.Hour && x.especialidad == turno.especialidad);

            if (turnosFromDB != null)
            {
                ViewBag.Error = "Este turno ya ha sido reservado.";
                return View();
            }

            if (ModelState.IsValid)
            {
                _context.Add(turno);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["pacienteID2"] =
                new SelectList((from a in _context.Pacientes
                                select new
                                {
                                    ID = a.pacienteID,
                                    nombreCompleto = a.nombre + " " + a.apellido
                                }),
                "ID",
                "nombreCompleto",
                turno.pacienteID);

            return View(turno);
        }

        // GET: Turno/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turno = await _context.Turnos.FindAsync(id);
            if (turno == null)
            {
                return NotFound();
            }

            ViewData["pacienteID"] = new SelectList(_context.Pacientes, "pacienteID", "apellido", turno.pacienteID);
            ViewBag.SuccessMessage = "¡Se ha actualizado el turno correctamente!";
            return View(turno);
        }

        // POST: Turno/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("turnoID,fecha,especialidad,pacienteID")] Turno turno)
        {
            var turnosFromDB = await _context.Turnos.FirstOrDefaultAsync(x => x.fecha.Year == turno.fecha.Year &&
            x.fecha.Month == turno.fecha.Month && x.fecha.Day == turno.fecha.Day &&
            x.fecha.Hour == turno.fecha.Hour && x.especialidad == turno.especialidad);

            if (turnosFromDB != null)
            {
                ViewBag.Error = "Este turno ya ha sido reservado.";
                return View();
            }

            if (id != turno.turnoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(turno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TurnoExists(turno.turnoID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["pacienteID"] = new SelectList(_context.Pacientes, "pacienteID", "apellido", turno.pacienteID);
            return View(turno);
        }

        // GET: Turno/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turno = await _context.Turnos
                .Include(t => t.paciente)
                .FirstOrDefaultAsync(m => m.turnoID == id);
            if (turno == null)
            {
                return NotFound();
            }

            ViewBag.SuccessMessage = "¡Se ha eliminado el turno correctamente!";
            return View(turno);
        }

        // POST: Turno/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var turno = await _context.Turnos.FindAsync(id);
            _context.Turnos.Remove(turno);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool TurnoExists(int id)
        {
            return _context.Turnos.Any(e => e.turnoID == id);
        }
    }
}