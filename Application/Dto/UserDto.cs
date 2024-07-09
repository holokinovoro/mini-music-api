﻿using Domain.Models;

namespace Application.Dto
{
    public class UserDto
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public ICollection<RoleEntity> Roles { get; set; }
    }
}
