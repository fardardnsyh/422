using Blogedium_api.Modals;

namespace Blogedium_api.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserModal> CreateUserAsync (UserModal userModal);
        Task<UserModal?> FindUserAsync (int id);
        Task<bool> DeleteUserAsync (int id);
        Task<IEnumerable<UserModal>> GetAllUsersAsync ();
        Task<UserModal?> FindUserByEmailAddressAsync (string emaildddress);
    }
}