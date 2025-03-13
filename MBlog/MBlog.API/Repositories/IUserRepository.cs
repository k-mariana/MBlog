﻿using MBlog.API.Models.Domain;

namespace MBlog.API.Repositories;

public interface IUserRepository
{
    Task<List<User>> GetAllAsync();
    Task<User?> GetByIdAsync(Guid id);
    Task<User> CreateAsync(User user);
    Task<User?> UpdateAsync(Guid id, User user);
    Task<User?> DeleteAsync(Guid id);
}
