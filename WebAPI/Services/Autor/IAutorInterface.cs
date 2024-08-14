using WebAPI.Dto.Autor;
using WebAPI.Models;

namespace WebAPI.Services.Autor;

public interface IAutorInterface
{
    Task<ResponseModel<List<AutorModel>>> ListarAutores();
    Task<ResponseModel<AutorModel>> BuscarAutorPorId(int idAutor);

    Task<ResponseModel<AutorModel>> BuscarAutorPorIdLivro(int idLivro);

    Task<ResponseModel<List<AutorModel>>> CriarAutor(CreateAutorDto autorCriacaoDto);


    Task<ResponseModel<List<AutorModel>>> EditarAutor(EditAutorDto autorEdicaoDto);

    Task<ResponseModel<List<AutorModel>>> DeletarAutor(int idAutor);
}
