using Abdt.Loyal.UserManager.Domain;

namespace Abdt.Loyal.UserManager.BusinessLogic.Abstractions
{
    public interface IAccountManager<T> where T : class
    {
        Task<Result<T>> Register(T item);

        Task<Result<string>> Login(string login, string password);

        Task<Result<T>> Update(T item);

        Task Delete(long id);
    }
}
