using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Business.Repository.Interfaces;
using DataAccess.Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;


        public UserRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<UserDTO> CreateUserAsync(UserDTO userDTO)
        {
            User user = _mapper.Map<UserDTO, User>(userDTO);
            var addedUser = await _dbContext.Users.AddAsync(user);

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<User, UserDTO>(addedUser.Entity);
        }

        public async Task<int> DeleteUserAsync(int userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            if (user != null)
            {
                // Don't forget to delete friends, points/tokens/user inventory - if we wanna do that kind of thing..?  AKA - Housekeeping is good/bad?
                // var theStuffs = await _dbContext.SomeDataTable.Where(x => x.UserId == userId).ToListAsync();

                //_dbContext.SomeDataTable.RemoveRange(theStuffs);
                _dbContext.Users.Remove(user);
                return await _dbContext.SaveChangesAsync();
            }

            return 0;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            try
            {
                IEnumerable<UserDTO> userDTOs = _mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(_dbContext.Users);

                return userDTOs;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<UserDTO> GetUserAsync(int userId)
        {
            try
            {
                UserDTO user = _mapper.Map<User, UserDTO>(await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId));
                return user;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<UserDTO> IsUserUniqueAsync(string name)
        {
            try
            {
                UserDTO user = _mapper.Map<User, UserDTO>(await _dbContext.Users.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower()));
                return user;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<UserDTO> UpdateUserAsync(UserDTO userDTO)
        {
            try
            {
                User userDetails = await _dbContext.Users.FindAsync(userDTO.Id);
                User user = _mapper.Map(userDTO, userDetails);

                var updateUser = _dbContext.Users.Update(user);
                await _dbContext.SaveChangesAsync();
                return _mapper.Map<User, UserDTO>(updateUser.Entity);
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}
