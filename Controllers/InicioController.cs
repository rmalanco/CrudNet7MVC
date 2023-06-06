using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CrudNet7MVC.Models;
using CrudNet7MVC.Data;
using Microsoft.EntityFrameworkCore;

namespace CrudNet7MVC.Controllers;

public class InicioController : Controller
{
    private readonly ApplicationDbContext _contexto;

    public InicioController(ApplicationDbContext contexto)
    {
        _contexto = contexto;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return View(await _contexto.Contacto.ToListAsync());
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    public async Task<IActionResult> Create(Contacto contacto)
    {
        if (ModelState.IsValid)
        {
            contacto.FechaCreacion = DateTime.Now;
            _contexto.Add(contacto);
            await _contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(contacto);
    }

    [HttpGet]
    public async Task<JsonResult> Details(int? id)
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

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Contacto contacto)
    {
        if (id != contacto.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var contactoEditar = await _contexto.Contacto.FindAsync(id);
                contactoEditar.Nombre = contacto.Nombre;
                contactoEditar.Telefono = contacto.Telefono;
                contactoEditar.Celular = contacto.Celular;
                contactoEditar.Email = contacto.Email;
                await _contexto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactoExists(contacto.Id))
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
        return View(contacto);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var contacto = await _contexto.Contacto.FindAsync(id);
        _contexto.Contacto.Remove(contacto);
        await _contexto.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // metodos inicio
    private bool ContactoExists(int id)
    {
        return _contexto.Contacto.Any(e => e.Id == id);
    }
}
