using Microsoft.EntityFrameworkCore;
using ProyectoDeRecuperacion.Data;
using ProyectoDeRecuperacion.Models;
using System.Linq.Expressions;

public class EntradaService(IDbContextFactory<ApplicationDbContext> DbFactory)
{
    public async Task<bool> Existe(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.entradas.AnyAsync(e => e.EntradaId == id);
    }

    private async Task AfectarExistencia(EntradaDetalle[] detalles, TipoOperacion tipoOperacion)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        foreach (var item in detalles)
        {
            var producto = await contexto.productos.SingleAsync(p => p.ProductoId == item.ProductoId);

            if (tipoOperacion == TipoOperacion.Suma)
                producto.Existencia += item.Cantidad;
            else
                producto.Existencia -= item.Cantidad;

            await contexto.SaveChangesAsync();
        }
    }

    public async Task<bool> Insertar(Entrada entrada)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.entradas.Add(entrada);

        await AfectarExistencia(entrada.Detalles.ToArray(), TipoOperacion.Suma);

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(Entrada entrada)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var original = await contexto.entradas
            .Include(e => e.Detalles)
            .AsNoTracking()
            .SingleOrDefaultAsync(e => e.EntradaId == entrada.EntradaId);

        if (original == null) return false;

        await AfectarExistencia(original.Detalles.ToArray(), TipoOperacion.Resta);

        contexto.detalles.RemoveRange(original.Detalles);

        contexto.Update(entrada);

        await AfectarExistencia(entrada.Detalles.ToArray(), TipoOperacion.Suma);

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Entrada entrada)
    {
        if (!await Existe(entrada.EntradaId))
            return await Insertar(entrada);
        else
            return await Modificar(entrada);
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var entrada = await Buscar(id);

        if (entrada == null) return false;

        await AfectarExistencia(entrada.Detalles.ToArray(), TipoOperacion.Resta);

        contexto.detalles.RemoveRange(entrada.Detalles);
        contexto.entradas.Remove(entrada);

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<Entrada?> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.entradas
            .Include(e => e.Detalles)
            .FirstOrDefaultAsync(e => e.EntradaId == id);
    }

    public async Task<List<Entrada>> Listar(Expression<Func<Entrada, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.entradas
            .Include(e => e.Detalles)
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }
}

public enum TipoOperacion
{
    Suma = 1,
    Resta = 2
}