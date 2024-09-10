namespace Abdt.Loyal.UserManager.BusinessLogic.Abstractions
{
    public interface IAccountManager<T> where T : class
    {
        Task<T> Register(T item);

        Task<string?> Login(string login, string password);

        Task<T> Update(T item);

        Task Delete(long id);
    }
}
