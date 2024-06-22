using ApiValidacionUsuarios.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class TraerModulosController : ControllerBase
{
    private readonly TraerModulosService _traerModulosService;

    public TraerModulosController(TraerModulosService traerModulosService)
    {
        _traerModulosService = traerModulosService;
    }

    [HttpGet]
    public async Task<IActionResult> TraerModulos()
    {
        try
        {
            var modulos = await _traerModulosService.TraerModulosAsync("Prueba24");
            return Ok(modulos);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
