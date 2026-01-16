using Microsoft.EntityFrameworkCore;
using ProyectoDeRecuperacion.Data;
using ProyectoDeRecuperacion.Models;
using System.Linq.Expressions;

public class ProductosService(IDbContextFactory<ApplicationDbContext> DbFactory)
{
    public async Task<bool> Guardar(Productos producto)
    {
        if (!await Existe(producto.ProductoId))
        {
            return await Insertar(producto);
        }
        else
        {
            return await Modificar(producto);
        }
    }

    public async Task<bool> Existe(int productoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.productos.AnyAsync(p => p.ProductoId == productoId);
    }

    private async Task<bool> Insertar(Productos producto)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.productos.Add(producto);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Productos producto)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(producto);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<Productos?> Buscar(int productoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.productos
            .FirstOrDefaultAsync(p => p.ProductoId == productoId);
    }

    public async Task<bool> Eliminar(int productoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.productos
            .AsNoTracking()
            .Where(p => p.ProductoId == productoId)
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<List<Productos>> Listar(Expression<Func<Productos, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.productos
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }
}
