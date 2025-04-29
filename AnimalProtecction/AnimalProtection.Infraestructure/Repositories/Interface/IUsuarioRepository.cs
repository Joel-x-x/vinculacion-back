using AnimalProtecction.GenericRepository;
using AnimalProtection.Domain.Entities;
using AnimalProtection.Domain.Result;

namespace AnimalProtecction.Generated.Repositories.Interface;

public interface IUsuarioRepository : IGenericRepository<Usuario>
{
    /// <summary>
    /// Metodo para devolver usuario por medio del email
    /// </summary>
    /// <param name="email">Email del usuario</param>
    /// <returns>Retorno un usuario</returns>
    Task<ResultResponse<Usuario>> GetByEmail(string email);
}