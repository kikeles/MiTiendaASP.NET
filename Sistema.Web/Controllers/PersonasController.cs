using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;//Librería necesaria para la etiqueta [Authorize]
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Ventas;
using Sistema.Web.Models.Ventas.Persona;

namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public PersonasController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Personas/ListarClientes
        [Authorize(Roles = "Vendedor,Administrador")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<PersonaViewModel>> ListarClientes()
        {
            //recomendable usar ToListAsync() si es async await para liberar el hilo
            //Include() hace referencia a la clase Categoria relacionada con Articulo 
            var persona = await _context.Personas.Where(p => p.tipo_persona == "Cliente").ToListAsync();
            //aquí solo se muestra los campos que queremos mostrar y no
            //todos los campos mapeados desde la base de datos
            return persona.Select(p => new PersonaViewModel
            {
                idpersona = p.idpersona,
                tipo_persona = p.tipo_persona,
                nombre = p.nombre,
                tipo_documento = p.tipo_documento,
                num_documento = p.num_documento,
                direccion = p.direccion,
                telefono = p.telefono,
                email = p.email
            });
        }

        // GET: api/Personas/ListarProveedores
        [Authorize(Roles = "Almacenero,Administrador")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<PersonaViewModel>> ListarProveedores()
        {
            //recomendable usar ToListAsync() si es async await para liberar el hilo
            //Include() hace referencia a la clase Categoria relacionada con Articulo 
            var persona = await _context.Personas.Where(p => p.tipo_persona == "Proveedor").ToListAsync();
            //aquí solo se muestra los campos que queremos mostrar y no
            //todos los campos mapeados desde la base de datos
            return persona.Select(p => new PersonaViewModel
            {
                idpersona = p.idpersona,
                tipo_persona = p.tipo_persona,
                nombre = p.nombre,
                tipo_documento = p.tipo_documento,
                num_documento = p.num_documento,
                direccion = p.direccion,
                telefono = p.telefono,
                email = p.email
            });
        }

        // GET: api/Personas/SelectProveedores
        [Authorize(Roles = "Almacenero,Administrador")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> SelectProveedores()
        {
            //recomendable usar ToListAsync() si es async await para liberar el hilo
            var persona = await _context.Personas.Where(p => p.tipo_persona == "Proveedor").ToListAsync();
            //aquí solo se muestra los campos que queremos mostrar y no
            //todos los campos mapeados desde la base de datos
            return persona.Select(p => new SelectViewModel
            {
                idpersona = p.idpersona,
                nombre = p.nombre
            });
        }

        // GET: api/Personas/SelectClientes
        [Authorize(Roles = "Vendedor,Administrador")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> SelectClientes()
        {
            //recomendable usar ToListAsync() si es async await para liberar el hilo
            var persona = await _context.Personas.Where(p => p.tipo_persona == "Cliente").ToListAsync();
            //aquí solo se muestra los campos que queremos mostrar y no
            //todos los campos mapeados desde la base de datos
            return persona.Select(p => new SelectViewModel
            {
                idpersona = p.idpersona,
                nombre = p.nombre
            });
        }


        // POST: api/Personas/Crear
        [Authorize(Roles = "Almacenero,Administrador,Vendedor")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            //valida los data annotations
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var email = model.email.ToLower();

            if (await _context.Personas.AnyAsync(p => p.email == email))
            {
                return BadRequest("El email ya existe.");
            }
            
            Persona persona = new Persona
            {
                tipo_persona = model.tipo_persona,
                nombre = model.nombre,
                tipo_documento = model.tipo_documento,
                num_documento = model.num_documento,
                direccion = model.direccion,
                telefono = model.telefono,
                email = model.email.ToLower(),//converte a mayusculas
            };

            _context.Personas.Add(persona);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok();
        }

        // PUT: api/Personas/Actualizar
        [Authorize(Roles = "Almacenero,Administrador,Vendedor")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.idpersona <= 0)
            {
                return BadRequest();
            }

            var persona = await _context.Personas.FirstOrDefaultAsync(p => p.idpersona == model.idpersona);

            if (persona == null)
            {
                return NotFound();
            }

            persona.tipo_persona = model.tipo_persona;
            persona.nombre = model.nombre;
            persona.tipo_documento = model.tipo_documento;
            persona.num_documento = model.num_documento;
            persona.direccion = model.direccion;
            persona.telefono = model.telefono;
            persona.email = model.email;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //Guardar Excepcion
                return BadRequest();
            }

            return Ok();
        }


        private bool PersonaExists(int id)
        {
            return _context.Personas.Any(e => e.idpersona == id);
        }
    }
}