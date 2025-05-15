// <copyright file="CreateUserRequest.cs" company="DITOS SAS">
// Copyright (c) DITOS SAS. All rights reserved.
// </copyright>

namespace UserManager.Application.DTOS
{
    public class CreateUserRequest
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public int IdentificationTypeId { get; set; }
        public string IdentificationNumber { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
    }

}
