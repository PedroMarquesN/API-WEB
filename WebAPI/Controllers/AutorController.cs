using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dto.Autor;
using WebAPI.Models;
using WebAPI.Services.Autor;

namespace WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AutorController : ControllerBase
{
    private readonly IAutorInterface _autorInterface;

    public AutorController(IAutorInterface autorInterface)
    {
        _autorInterface = autorInterface;
    }


    [HttpGet("ListarAutores")]
    public async Task<ActionResult<ResponseModel<List<AutorModel>>>> ListarAutores()
    {
        var autores = await _autorInterface.ListarAutores();

        if (autores.Status)
        {
            return Ok(autores);
        }
        else
        {
            return StatusCode(StatusCodes.Status500InternalServerError, autores);
        }
    }



    [HttpGet("BuscarAutorPorId/{idAutor}")]

    public async Task<ActionResult<ResponseModel<AutorModel>>> BuscarAutorPorId(int idAutor)
    {
        var autor = await _autorInterface.BuscarAutorPorId(idAutor);

        if (autor.Status)
        {
            return Ok(autor);
        }
        else
        {
            return StatusCode(StatusCodes.Status500InternalServerError, autor);
        }
    }


    [HttpGet("BuscarAutorPorIdLivro/{idLivro}")]
    public async Task<ActionResult<ResponseModel<AutorModel>>> BuscarAutorPorIdLivro(int idLivro)
    {
        var autor = await _autorInterface.BuscarAutorPorIdLivro(idLivro);

        if (autor.Status)
        {
            return Ok(autor);
        }
        else
        {
            return StatusCode(StatusCodes.Status500InternalServerError, autor);
        }
    }


    [HttpPost("CriarAutor")]
    public async Task<ActionResult<ResponseModel<List<AutorModel>>>> CriarAutor(CreateAutorDto autorCriacaoDto)
    {
        var autores = await _autorInterface.CriarAutor(autorCriacaoDto);
        return Ok(autores);


    }

    [HttpPut("EditarAutor")]
    public async Task<ActionResult<ResponseModel<List<AutorModel>>>> EditarAutor(EditAutorDto autorEdicaoDto)
    {
        var autores = await _autorInterface.EditarAutor(autorEdicaoDto);
        return Ok(autores);
    }

    [HttpDelete("DeletarAutor/{idAutor}")]
    public async Task<ActionResult<ResponseModel<List<AutorModel>>>> DeletarAutor(int idAutor)
    {
        var autores = await _autorInterface.DeletarAutor(idAutor);
        return Ok(autores);
    }
}
