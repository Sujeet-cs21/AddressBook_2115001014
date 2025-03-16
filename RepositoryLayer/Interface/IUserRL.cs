using RepositoryLayer.Entity;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        Task<UserEntity> GetUserByEmail(string email);
        Task CreateUser(UserEntity user);
        Task UpdatePassword(UserEntity user); 
    }
}
