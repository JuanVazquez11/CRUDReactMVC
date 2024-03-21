using CRUDReactMVC.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace CRUDReactMVC.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactoController : Controller
    {
        private readonly CrudreactMvcContext _context;
        public ContactoController(CrudreactMvcContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> lista()
        {
            var lista = await _context.Contactos.OrderByDescending(c => c.IdContacto).ToListAsync();
            return StatusCode(StatusCodes.Status200OK,lista);
        }

        [HttpPost]
        [Route("guardar")]
        public async Task<IActionResult> Guardar([FromBody] Contacto request)
        {
            await _context.Contactos.AddAsync(request);
            await _context.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut]
        [Route("editar")]
        public async Task<IActionResult> Editar([FromBody] Contacto request)
        {
            _context.Contactos.Update(request);
            await _context.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpDelete]
        [Route("elimninar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var contacto = _context.Contactos.FirstOrDefault(c => c.IdContacto == id);

            if(contacto != null)
            {
                _context.Remove(contacto);
                await _context.SaveChangesAsync();
                return StatusCode(StatusCodes.Status200OK);
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }
    }
}
