using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ArticulosAPI.Data;
using ArticulosAPI.Modelos;
using ArticulosAPI.Dto;

public class ArticuloRepositorio : IArticuloRepositorio
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ArticuloRepositorio(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ArticuloDto>> ObtenerArticulos()
    {
        var articulos = await _context.Articulos.ToListAsync();
        var articulosDto = _mapper.Map<IEnumerable<ArticuloDto>>(articulos);

        return articulosDto;
    }

    public async Task<ArticuloDto> ObtenerArticuloPorId(int id)
    {
        var articulo = await _context.Articulos.FirstOrDefaultAsync(a => a.Id == id);

        if (articulo == null)
        {
            return null;
        }

        var articuloDto = _mapper.Map<ArticuloDto>(articulo);

        return articuloDto;
    }

    public async Task AgregarArticulo(ArticuloDto articuloDto)
    {
        var articulo = _mapper.Map<Articulo>(articuloDto);

        _context.Articulos.Add(articulo);
        await _context.SaveChangesAsync();
    }

    public async Task ActualizarArticulo(int id, ArticuloDto articuloDto)
    {
        var articulo = await _context.Articulos.FirstOrDefaultAsync(a => a.Id == id);

        if (articulo != null)
        {
            _mapper.Map(articuloDto, articulo);
            await _context.SaveChangesAsync();
        }
    }

    public async Task EliminarArticulo(int id)
    {
        var articulo = await _context.Articulos.FirstOrDefaultAsync(a => a.Id == id);

        if (articulo != null)
        {
            _context.Articulos.Remove(articulo);
            await _context.SaveChangesAsync();
        }
    }
}