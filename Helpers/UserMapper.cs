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

    public static AdminUpdateViewModel ToAdminUpdateViewModel(this User user) {
        return new AdminUpdateViewModel {
            Id = user.UserId,
            Username = user.Username,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email
        };
    }

    public static User ToUser(this AdminUpdateViewModel adminUpdateViewModel, User user) {
        user.Username = adminUpdateViewModel.Username;
        user.FirstName = adminUpdateViewModel.FirstName;
        user.LastName = adminUpdateViewModel.LastName;
        user.Email = adminUpdateViewModel.Email;
        return user;
    }
}