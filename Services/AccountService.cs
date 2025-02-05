using Isopoh.Cryptography.Argon2;
using ShopSphere.Data.Repositories.Interfaces;
using ShopSphere.Helpers;
using ShopSphere.Models;
using ShopSphere.Models.Users;

namespace ShopSphere.Services;

public class AccountService(IUserRepository userRepository) {
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<UserGridViewModel> GetAllUserAsync(int pageNumber, int pageSize, string sortBy, string sort, string username, string name, string email, string role) {
        sortBy = sortBy is null || sortBy == "" || sortBy.Equals("entry", StringComparison.CurrentCultureIgnoreCase) ? "UserId" : sortBy;
        PaginationViewModel pagination = new(pageNumber, pageSize, sortBy, sort);
        var users = await _userRepository.FindAllAsync(username, name, email, role, pagination);
        return new UserGridViewModel {
            Payload = users.ToUserViewModel(),
            Username = username,
            Name = name,
            Email = email,
            Role = role
        };
    }

    public async Task<UserUpdateViewModel> GetUserUpdateAsync(long id) {
        var user = await _userRepository.FindByIdAsync(id);
        return user.ToUserUpdateViewModel();
    }

    public async Task<UserViewModel> SaveAdminAsync(AdminRegisterViewModel adminRegisterViewModel) {
        var user = await _userRepository.SaveAsync(adminRegisterViewModel.ToUser());
        return user.ToUserViewModel();
    }

    public async Task<UserViewModel> SaveUserAsync(UserRegisterViewModel userRegisterViewModel) {
        var user = await _userRepository.SaveAsync(userRegisterViewModel.ToUser());
        return user.ToUserViewModel();
    }

    public async Task<UserViewModel> SaveUserAsync(UserUpdateViewModel userUpdateViewModel) {
        var userModel = await _userRepository.FindByIdAsync(userUpdateViewModel.Id);
        var user = await _userRepository.SaveAsync(userUpdateViewModel.ToUser(userModel));
        return user.ToUserViewModel();
    }

    public async Task<UserViewModel> DeleteUserByIdAsync(long id) {
        var user = await _userRepository.FindByIdAsync(id);
        await _userRepository.DeleteAsync(user);
        return user.ToUserViewModel();
    }

    public async Task<UserViewModel> VerifyUser(LoginViewModel loginViewModel) {
        var user = await _userRepository.FindByUsernameAsync(loginViewModel.Username) ?? throw new KeyNotFoundException("invalid username or password");
        if (!Argon2.Verify(user.Password, loginViewModel.Password)) throw new KeyNotFoundException("invalid username or password");
        return user.ToUserViewModel();
    }
}