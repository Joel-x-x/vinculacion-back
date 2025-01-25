using AnimalProtecction.Domain.Entities;
using AnimalProtecction.Generated.Repositories.Interface;
using AnimalProtecction.GenericRepository;
using AnimalProtection.Domain.Result;
using Microsoft.EntityFrameworkCore;

namespace AnimalProtecction.Generated.Repositories.Repository;

public class UsuarioRepository : GenericRepository<Usuario>,IUsuarioRepository
{
    public UsuarioRepository(AnimalprotectionContext context) : base(context)
    {
    }

    
    public async Task<ResultResponse<Usuario>> GetByEmail(string email)
    {
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(d => d.Email ==email);
        return usuario == null ? 
            ResultResponse<Usuario>.Failure($"No existe usuario asociado al email: {email}") : 
            ResultResponse<Usuario>.Success(usuario);
    }
}