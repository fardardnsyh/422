using Blogedium_api.Modals;
using Blogedium_api.Interfaces.Services;
using Blogedium_api.Interfaces.Repository;
using System.Security.Cryptography;
using System.Text;

namespace Blogedium_api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
    
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserModal> CreateUserAsync (UserModal userModal)
        {
            var existinguser = await _userRepository.FindUserByEmailAddress(userModal.EmailAddress); // null / not null
            if (existinguser == null){
                return await _userRepository.CreateUser(userModal);
            }
            throw new InvalidOperationException("User Already Exist, Please signin to continue");
        }

        public async Task<UserModal?> FindUserAsync (int id)
        {
            return await _userRepository.FindUser(id);
        }

        public async Task<bool> DeleteUserAsync (int id)
        {
            var user = await _userRepository.DeleteUser(id);
            return user != null;
        }

        public async Task<IEnumerable<UserModal>> GetAllUsersAsync ()
        {
            return await _userRepository.GetAllUsers();
        }

        public async Task<UserModal?> FindUserByEmailAddressAsync (string emaildddress)
        {
            return await _userRepository.FindUserByEmailAddress(emaildddress);
        }
    }
}