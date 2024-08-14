using WebAPI.Models;
using WebAPI.Dto.Livro;
namespace WebAPI.Services.Livro;

public interface ILivroInterface
{
    Task<ResponseModel<List<LivroModel>>> ListarLivros();
    Task<ResponseModel<LivroModel>> BuscarLivroPorId(int idLivro);
    Task<ResponseModel<List<LivroModel>>> BuscarLivroPorIdAutor(int idAutor);
    Task<ResponseModel<List<LivroModel>>> CriarLivro(CreateLivroDto livroCriacaoDto);
    Task<ResponseModel<List<LivroModel>>> EditarLivro(EditLivroDto livroEdicaoDto);
    Task<ResponseModel<List<LivroModel>>> DeletarLivro(int idLivro);
}
