using GamjaTest.Data;
using GamjaTest.Dtos;
using GamjaTest.Models;
using Microsoft.EntityFrameworkCore;

namespace GamjaTest.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;

        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        //회원가입
        public async Task<UserResponseDto> RegisterUserAsync(UserRequestDto request)
        {
            if (await _context.Users.AnyAsync(u => u.Email == request.Email))
            {
                throw new Exception("이미 등록된 이메일입니다.");
            }

            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                Password = request.Password,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
            };
        }

        // 전체 조회
        public async Task<List<UserResponseDto>> GetAllUsersAsync()
        {
            return await _context
                .Users.Select(u => new UserResponseDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                })
                .ToListAsync();
        }

        //단건 조회
        public async Task<UserResponseDto?> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return null;

            return new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
            };
        }

        //정보 수정
        public async Task<UserResponseDto?> updatedUserAsync(int id, UserRequestDto request)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return null;

            user.Name = request.Name;
            user.Email = request.Email;
            user.Password = request.Password;
            user.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
            };
        }

        // 삭제
        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
