using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.Interfaces
{
    public interface IUserRepository
    {
        public Task<UserDTO> CreateUserAsync(UserDTO userDTO);
        public Task<UserDTO> GetUserAsync(int userId);
        public Task<UserDTO> UpdateUserAsync(UserDTO userDTO);
        public Task<int> DeleteUserAsync(int userId);

        //public Task<IEnumerable<UserDTO>> GetAllUsersAsync(CancellationToken cancellationToken);
        public Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        public Task<UserDTO> IsUserUniqueAsync(string name);

        

    }
}
