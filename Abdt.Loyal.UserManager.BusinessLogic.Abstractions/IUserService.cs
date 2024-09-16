using Abdt.Loyal.UserManager.Domain;

namespace Abdt.Loyal.UserManager.BusinessLogic.Abstractions
{
    public interface IUserService<T> where T : class
    {
        Task<Result<T>> Add(T item);

        Task<Result<T>> Get(long id);

        Task<Result<T>> Update(T item);

        Task Delete(long id);
    }
}
