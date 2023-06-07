using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CrudNet7MVC.Models;
using CrudNet7MVC.Data;
using Microsoft.EntityFrameworkCore;

namespace CrudNet7MVC.Controllers
{
    public class InicioController : Controller
    {
        private readonly ApplicationDbContext _contexto;

        public InicioController(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Contactos()
        {
            return View(await _contexto.Contacto.ToListAsync());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<JsonResult> GetById(int? id)
        {
            if (id == null)
            {
                return Json(new { success = false, message = "Error al cargar el contacto" });
            }

            var contacto = await _contexto.Contacto.FindAsync(id);
            if (contacto == null)
            {
                return Json(new { success = false, message = "Error al cargar el contacto" });
            }
            return Json(new { success = true, message = "Contacto cargado correctamente", data = contacto });
        }

        [HttpGet]
        public async Task<JsonResult> GetAll()
        {
            var contactos = await _contexto.Contacto.Select(x => new { x.Id, x.Nombre, x.Telefono, x.Celular, x.Email, FechaCreacion = x.FechaCreacion.ToString("dd/MM/yyyy HH:mm:ss") }).ToListAsync();
            return Json(new { data = contactos });
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int? id)
        {
            if (id == null)
            {
                return Json(new { success = false, message = "Error al eliminar el contacto" });
            }

            var contacto = await _contexto.Contacto.FindAsync(id);
            if (contacto == null)
            {
                return Json(new { success = false, message = "Error al eliminar el contacto" });
            }

            _contexto.Contacto.Remove(contacto);
            await _contexto.SaveChangesAsync();
            return Json(new { success = true, message = "Contacto eliminado correctamente" });
        }

        [HttpPost]
        public async Task<JsonResult> Create(Contacto contacto)
        {
            if (ModelState.IsValid)
            {
                _contexto.Add(contacto);
                await _contexto.SaveChangesAsync();
                return Json(new { success = true, message = "Contacto creado correctamente" });
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, message = "Error al crear el contacto", errors });
            }
        }

        [HttpPost]
        public async Task<JsonResult> Update(Contacto contacto)
        {
            if (ModelState.IsValid)
            {
                _contexto.Update(contacto);
                await _contexto.SaveChangesAsync();
                return Json(new { success = true, message = "Contacto actualizado correctamente" });
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, message = "Error al actualizar el contacto", errors });
            }
        }
    }
}