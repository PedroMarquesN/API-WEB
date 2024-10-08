﻿using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Dto.Autor;
using WebAPI.Models;

namespace WebAPI.Services.Autor;

public class AutorService : IAutorInterface
{


    private readonly AppDbContext _context;

    public AutorService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<ResponseModel<AutorModel>> BuscarAutorPorId(int idAutor)
    {
        ResponseModel<AutorModel> response = new ResponseModel<AutorModel>();
        try
        {
            var autor = await _context.Autores.FirstOrDefaultAsync(x => x.Id == idAutor);

            if (autor == null)
            {

                response.Mensagem = "Autor não encontrado!";
                response.Status = false;
                return response;
            }

            response.Dados = autor;
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

    public async Task<ResponseModel<AutorModel>> BuscarAutorPorIdLivro(int idLivro)
    {
        ResponseModel<AutorModel> response = new ResponseModel<AutorModel>();

        try
        {
            var livro = await _context.Livros.Include(x => x.Autor).FirstOrDefaultAsync(x => x.Id == idLivro);

            if (livro == null)
            {
                response.Mensagem = "Autor não encontrado!";
                response.Status = false;
                return response;
            }
            response.Dados = livro.Autor;
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

    public async Task<ResponseModel<List<AutorModel>>> CriarAutor(CreateAutorDto autorCriacaoDto)
    {
        ResponseModel<List<AutorModel>> response = new ResponseModel<List<AutorModel>>();

        try
        {
            var autor = new AutorModel
            {
                Name = autorCriacaoDto.Name,
                Sobrenome = autorCriacaoDto.Sobrenome
            };

            _context.Add(autor);
            await _context.SaveChangesAsync();

            response.Dados = await _context.Autores.ToListAsync();
            response.Mensagem = "Autor criado com sucesso!";
            return response;
        }
        catch (Exception ex)
        {
            response.Mensagem = "Erro: " + ex.Message;
            response.Status = false;
            return response;
        }

    }

    public async Task<ResponseModel<List<AutorModel>>> DeletarAutor(int idAutor)
    {
        ResponseModel<List<AutorModel>> response = new ResponseModel<List<AutorModel>>();
        try
        {
            var autor = await _context.Autores.FirstOrDefaultAsync(x => x.Id == idAutor);

            if (autor == null)
            {
                response.Mensagem = "Autor não encontrado!";
                response.Status = false;
                return response;
            }

            _context.Remove(autor);
            await _context.SaveChangesAsync();

            response.Dados = await _context.Autores.ToListAsync();
            response.Mensagem = "Autor deletado com sucesso!";
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

    public async Task<ResponseModel<List<AutorModel>>> EditarAutor(EditAutorDto autorEdicaoDto)
    {
        ResponseModel<List<AutorModel>> response = new ResponseModel<List<AutorModel>>();
        try
        {
            var autor = await _context.Autores.FirstOrDefaultAsync(x => x.Id == autorEdicaoDto.Id);

            if (autor == null)
            {
                response.Mensagem = "Autor não encontrado!";
                response.Status = false;
                return response;
            }

            autor.Name = autorEdicaoDto.Name;
            autor.Sobrenome = autorEdicaoDto.Sobrenome;

            _context.Update(autor);
            _context.SaveChanges();

            response.Dados = await _context.Autores.ToListAsync();
            response.Mensagem = "Autor editado com sucesso!";
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

    public async Task<ResponseModel<List<AutorModel>>> ListarAutores()
    {
        ResponseModel<List<AutorModel>> response = new ResponseModel<List<AutorModel>>();
        try
        {
            var autores = await _context.Autores.ToListAsync();

            response.Dados = autores;
            response.Mensagem = "Autores listados com sucesso!";
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
