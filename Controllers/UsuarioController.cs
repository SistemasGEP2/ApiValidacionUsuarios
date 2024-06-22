using ApiValidacionUsuarios.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class UsuariosController : ControllerBase
{
    private readonly UsuarioService _usuarioService;

    public UsuariosController(UsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }


    [HttpGet]
    public async Task<IActionResult> TraerUsuarios()
    {
        try
        {
            var usuario = await _usuarioService.TraerUsuariosAsync("Prueba24");
            return Ok(usuario);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
