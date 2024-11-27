using Microsoft.AspNetCore.Mvc;
using SCEE.backend.Data;
using SCEE.Shared.Entities;
namespace SCEE.backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly DataContext _datacontext;
        public UsuariosController(DataContext context)
        {
            _datacontext = context;
        }
        [HttpPost]
        public async Task<IActionResult> addUsuario(Usuario usuario)
        {
            _datacontext.Add(usuario);
            await _datacontext.SaveChangesAsync();
            return Ok();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> LeerUsuario(int id)
        {
            try
            {
                var usuario = await _datacontext.Usuarios.FindAsync(id);
                if (usuario == null)
                {
                    return NotFound("Usuario no encontrado");
                }

                return Ok(usuario);
            }
            catch (Exception ex) // sirve para que no termine abruptamente el programa 
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarUsuario(int id, Usuario usuarioActualizado)
        {
            if (usuarioActualizado == null || id != usuarioActualizado.Id)
            {
                return BadRequest("Datos inválidos para actualizar");
            }

            try
            {
                var usuarioExistente = await _datacontext.Usuarios.FindAsync(id);
                if (usuarioExistente == null)
                {
                    return NotFound("Usuario no encontrado");
                }

                // Actualizar las propiedades necesarias
                usuarioExistente.Name = usuarioActualizado.Name;
                
                // Otras propiedades aquí...

                _datacontext.Usuarios.Update(usuarioExistente);
                await _datacontext.SaveChangesAsync();

                return Ok("Usuario actualizado exitosamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            try
            {
                // Busca el usuario por su ID
                var usuario = await _datacontext.Usuarios.FindAsync(id);

                // Verifica si el usuario existe
                if (usuario == null)
                {
                    return NotFound("Usuario no encontrado");
                }

                // Elimina el usuario
                _datacontext.Usuarios.Remove(usuario);
                await _datacontext.SaveChangesAsync();

                return Ok("Usuario eliminado exitosamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }




    }
}
