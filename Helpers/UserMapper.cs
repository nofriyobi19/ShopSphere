using Isopoh.Cryptography.Argon2;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopSphere.Data.Models;
using ShopSphere.Models;
using ShopSphere.Models.Users;

namespace ShopSphere.Helpers;

public static class UserMapper {
    public static UserViewModel ToUserViewModel(this User user) {
        return new UserViewModel {
            Id = user.UserId,
            Username = user.Username,
            FullName = $"{user.FirstName} {user.LastName}".Trim(),
            Email = user.Email,
            Role = user.Role
        };
    }

    public static GridViewModel<UserViewModel> ToUserViewModel(this GridViewModel<User> userGrid) {
        var userViewModels = userGrid.Content.Select(e => e.ToUserViewModel()).ToList();
        return new GridViewModel<UserViewModel>(userViewModels, userGrid.Pagination);
    }

    public static User ToUser(this AdminRegisterViewModel adminRegisterViewModel) {
        return new User {
            Username = adminRegisterViewModel.Username,
            Password = Argon2.Hash(adminRegisterViewModel.Password),
            FirstName = adminRegisterViewModel.FirstName,
            LastName = adminRegisterViewModel.LastName,
            Email = adminRegisterViewModel.Email,
            Role = "Admin"
        };
    }

    public static UserUpdateViewModel ToUserUpdateViewModel(this User user) {
        return new UserUpdateViewModel {
            Id = user.UserId,
            Username = user.Username,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email
        };
    }

    public static User ToUser(this UserUpdateViewModel userUpdateViewModel, User user) {
        user.Username = userUpdateViewModel.Username;
        user.FirstName = userUpdateViewModel.FirstName;
        user.LastName = userUpdateViewModel.LastName;
        user.Email = userUpdateViewModel.Email;
        return user;
    }

    public static User ToUser(this UserRegisterViewModel userRegisterViewModel) {
        return new User {
            Username = userRegisterViewModel.Username,
            Password = Argon2.Hash(userRegisterViewModel.Password),
            FirstName = userRegisterViewModel.FirstName,
            LastName = userRegisterViewModel.LastName,
            Email = userRegisterViewModel.Email,
            Role = "User"
        };
    }
}