﻿using Api.Database.Entities.People;
using Api.Repository.Interface;

namespace Api.Repositories.Interface.People
{
    public interface IUserRepository : IBaseRepository<User>
    {
        User? Find(string? username, string? email);
    }
}
