using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Dto.Livro;
using WebAPI.Models;

namespace WebAPI.Services.Livro;

public class LivroService : ILivroInterface
{
    private readonly AppDbContext _context;

    public LivroService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<ResponseModel<LivroModel>> BuscarLivroPorId(int idLivro)
    {
       ResponseModel<LivroModel> response = new ResponseModel<LivroModel>();
        try
        {
            var livro = await _context.Livros.FirstOrDefaultAsync(x => x.Id == idLivro);

            if (livro == null)
            {

                response.Mensagem = "Livro não encontrado!";
                response.Status = false;
                return response;
            }

            response.Dados = livro;
            response.Mensagem = "Livro encontrado com sucesso!";
            response.Status = true;
            return response;

        }
        catch (Exception ex)
        {
            response.Mensagem = "Erro: " + ex.Message;
            response.Status = false;
            return response;
        }
    }

    public async Task<ResponseModel<List<LivroModel>>> BuscarLivroPorIdAutor(int idAutor)
    {
        ResponseModel<List<LivroModel>> response = new ResponseModel<List<LivroModel>>();

        try
        {
            var autor = await _context.Autores.Include(x => x.Livros).FirstOrDefaultAsync(x => x.Id == idAutor);

            if (autor == null)
            {
                response.Mensagem = "Autor não encontrado!";
                response.Status = false;
                return response;
            }

            response.Dados =  autor.Livros.ToList();
            response.Mensagem = "Autor encontrado com sucesso!";
            response.Status = true;
            return response;
        }
        catch (Exception ex)
        {
            response.Mensagem = "Erro: " + ex.Message;
            response.Status = false;
            return response;
        }
    }

    public async Task<ResponseModel<List<LivroModel>>> CriarLivro(CreateLivroDto livroCriacaoDto)
    {
        ResponseModel<List<LivroModel>> response = new ResponseModel<List<LivroModel>>();
        try
        {
            
            var autor = await _context.Autores.FindAsync(livroCriacaoDto.AutorId);
            if (autor == null)
            {
                response.Mensagem = "Autor não encontrado!";
                response.Status = false;
                return response;
            }

            
            var livro = new LivroModel
            {
                Titulo = livroCriacaoDto.Titulo,
                AutorId = livroCriacaoDto.AutorId,
                Autor = autor
            };

            _context.Livros.Add(livro);
            await _context.SaveChangesAsync();

            response.Dados = await _context.Livros.Include(l => l.Autor).ToListAsync();
            response.Mensagem = "Livro criado com sucesso!";
            response.Status = true;
            return response;
        }
        catch (Exception ex)
        {
            response.Mensagem = "Erro: " + ex.Message;
            response.Status = false;
            return response;
        }
    }

    public async Task<ResponseModel<List<LivroModel>>> DeletarLivro(int idLivro)
    {
        ResponseModel<List<LivroModel>> response = new ResponseModel<List<LivroModel>>();

        try
        {
            var livro = await _context.Livros.FirstOrDefaultAsync(x => x.Id == idLivro);

            if (livro == null)
            {
                response.Mensagem = "Livro não encontrado!";
                response.Status = false;
                return response;
            }

            _context.Livros.Remove(livro);
            await _context.SaveChangesAsync();

            response.Dados = await _context.Livros.ToListAsync();
            response.Mensagem = "Livro deletado com sucesso!";
            response.Status = true;
            return response;
        }
        catch (Exception ex)
        {
            response.Mensagem = "Erro: " + ex.Message;
            response.Status = false;
            return response;
        }
    }

    public async Task<ResponseModel<List<LivroModel>>> EditarLivro(EditLivroDto livroEdicaoDto)
    {
        ResponseModel<List<LivroModel>> response = new ResponseModel<List<LivroModel>>();
        try
        {
            var livro = await _context.Livros.FirstOrDefaultAsync(x => x.Id == livroEdicaoDto.Id);

            if (livro == null)
            {
                response.Mensagem = "Livro não encontrado!";
                response.Status = false;
                return response;
            }

            var autor = await _context.Autores.FindAsync(livroEdicaoDto.AutorId);
            if (autor == null)
            {
                response.Mensagem = "Autor não encontrado!";
                response.Status = false;
                return response;
            }

            livro.Titulo = livroEdicaoDto.Titulo;
            livro.AutorId = livroEdicaoDto.AutorId;
            livro.Autor = autor;

            await _context.SaveChangesAsync();

            response.Dados = await _context.Livros.Include(l => l.Autor).ToListAsync();
            response.Mensagem = "Livro editado com sucesso!";
            response.Status = true;
            return response;
        }
        catch (Exception ex)
        {
            response.Mensagem = "Erro: " + ex.Message;
            response.Status = false;
            return response;
        }
    }

    public async Task<ResponseModel<List<LivroModel>>> ListarLivros()
    {
        ResponseModel<List<LivroModel>> response = new ResponseModel<List<LivroModel>>();
        try
        {
           var livros = await _context.Livros.ToListAsync();

            if (livros.Count == 0)
            {
                response.Mensagem = "Nenhum livro encontrado!";
                response.Status = false;
                return response;
            }

            response.Dados = livros;
            response.Mensagem = "Livros encontrados com sucesso!";
            response.Status = true;
            return response;
        }
        catch (Exception ex)
        {
            response.Mensagem = "Erro: " + ex.Message;
            response.Status = false;
            return response;
        }
    }
}
