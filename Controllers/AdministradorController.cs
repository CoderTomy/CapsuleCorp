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
    public class AdministradorController : Controller
    {
        private readonly CapsuleCorpDatabaseContext _context;
        private Administrador adminContext;

        public AdministradorController(CapsuleCorpDatabaseContext context)
        {
            _context = context;
        }

        // GET: Administrador
        public async Task<IActionResult> Index()
        {
            return View(await _context.Administradores.ToListAsync());
        }

        // GET: Administrador/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var administrador = await _context.Administradores
                .FirstOrDefaultAsync(m => m.ID == id);
            if (administrador == null)
            {
                return NotFound();
            }

            return View(administrador);
        }

        // GET: Administrador/Create
        public IActionResult Create()
        {
            ViewBag.SuccessMessage = "¡Se ha creado el administrador correctamente!";
            return View();
        }

        // POST: Administrador/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,mail,contrasenia")] Administrador administrador)
        {
            if (ModelState.IsValid)
            {
                _context.Add(administrador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(administrador);
        }

        // GET: Administrador/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var administrador = await _context.Administradores.FindAsync(id);
            if (administrador == null)
            {
                return NotFound();
            }

            ViewBag.SuccessMessage = "¡Se ha actualizado el administrador correctamente!";
            return View(administrador);
        }

        // POST: Administrador/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,mail,contrasenia")] Administrador administrador)
        {
            if (id != administrador.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(administrador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdministradorExists(administrador.ID))
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

            return View(administrador);
        }

        // GET: Administrador/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var administrador = await _context.Administradores
                .FirstOrDefaultAsync(m => m.ID == id);
            if (administrador == null)
            {
                return NotFound();
            }

            ViewBag.SuccessMessage = "¡Se ha eliminado el administrador correctamente!";
            return View(administrador);
        }

        // POST: Administrador/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var administrador = await _context.Administradores.FindAsync(id);
            _context.Administradores.Remove(administrador);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool AdministradorExists(int id)
        {
            return _context.Administradores.Any(e => e.ID == id);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(String mail, String contrasenia)
        {
            if (ModelState.IsValid)
            {
                var adminsFromDB = await _context.Administradores.FirstOrDefaultAsync(adm => adm.mail == mail && adm.contrasenia == contrasenia);

                if (adminsFromDB == null)
                {
                    ViewBag.Error = "Datos incorrectos. Por favor, intente nuevamente.";
                    return View();
                }

                adminContext = adminsFromDB;
                HttpContext.Session.SetString("admin", adminsFromDB.mail);

                return RedirectToAction("Index", "Paciente");
            }

            return View(null);
        }

        public RedirectToActionResult Logout()
        {
            HttpContext.Session.SetString("admin", string.Empty);
            Console.WriteLine(HttpContext.Session.GetString("admin"));

            return RedirectToAction("Index", "Home");
        }
    }
}