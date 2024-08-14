using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dto.Livro;
using WebAPI.Models;
using WebAPI.Services.Livro;
namespace WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class LivroController : ControllerBase
{

    private readonly ILivroInterface _livroService;

    public LivroController(ILivroInterface livroService)
    {
        _livroService = livroService;
    }

    [HttpGet("ListarLivros")]
    public async Task<ActionResult<ResponseModel<List<LivroModel>>>> ListarLivros()
    {
        var livros = await _livroService.ListarLivros();

        if (livros.Status)
        {
            return Ok(livros);
        }
        else
        {
            return StatusCode(StatusCodes.Status500InternalServerError, livros);
        }
    }

    [HttpGet("BuscarLivroPorId/{idLivro}")]
    public async Task<ActionResult<ResponseModel<LivroModel>>> BuscarLivroPorId(int idLivro)
    {
        var livro = await _livroService.BuscarLivroPorId(idLivro);

        if (livro.Status)
        {
            return Ok(livro);
        }
        else
        {
            return StatusCode(StatusCodes.Status500InternalServerError, livro);
        }
    }

    [HttpGet("BuscarLivroPorIdAutor/{idAutor}")]
    public async Task<ActionResult<ResponseModel<List<LivroModel>>>> BuscarLivroPorIdAutor(int idAutor)
    {
        var livros = await _livroService.BuscarLivroPorIdAutor(idAutor);

        if (livros.Status)
        {
            return Ok(livros);
        }
        else
        {
            return StatusCode(StatusCodes.Status500InternalServerError, livros);
        }
    }

    [HttpPost("CriarLivro")]
    public async Task<ActionResult<ResponseModel<List<LivroModel>>>> CriarLivro(CreateLivroDto livroCriacaoDto)
    {
        var livros = await _livroService.CriarLivro(livroCriacaoDto);

        if (livros.Status)
        {
            return Ok(livros);
        }
        else
        {
            return StatusCode(StatusCodes.Status500InternalServerError, livros);
        }
    }

    [HttpPut("EditarLivro")]
    public async Task<ActionResult<ResponseModel<List<LivroModel>>>> EditarLivro(EditLivroDto livroEdicaoDto)
    {
        var livros = await _livroService.EditarLivro(livroEdicaoDto);

        if (livros.Status)
        {
            return Ok(livros);
        }
        else
        {
            return StatusCode(StatusCodes.Status500InternalServerError, livros);
        }
    }

    [HttpDelete("DeletarLivro/{idLivro}")]
    public async Task<ActionResult<ResponseModel<List<LivroModel>>>> DeletarLivro(int idLivro)
    {
        var livros = await _livroService.DeletarLivro(idLivro);

        if (livros.Status)
        {
            return Ok(livros);
        }
        else
        {
            return StatusCode(StatusCodes.Status500InternalServerError, livros);
        }
    }
}
