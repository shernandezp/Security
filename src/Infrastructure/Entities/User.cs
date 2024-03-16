// Copyright (c) 2024 Sergio Hernandez. All rights reserved.
//
//  Licensed under the Apache License, Version 2.0 (the "License").
//  You may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.
//

using Common.Infrastructure;

namespace Security.Infrastructure.Entities;

public sealed class User(string username,
    string password,
    string email,
    string firstName,
    string? secondName,
    string lastName,
    string? seconSurname,
    DateTime? dOB,
    Guid accountId) : BaseAuditableEntity
{
    public Guid UserId { get; private set; } = Guid.NewGuid();
    public string Username { get; set; } = username;
    public string Password { get; set; } = password;
    public string Email { get; set; } = email;
    public string FirstName { get; set; } = firstName;
    public string? SecondName { get; set; } = secondName;
    public string LastName { get; set; } = lastName;
    public string? SeconSurname { get; set; } = seconSurname;
    public DateTime? DOB { get; set; } = dOB;
    public DateTime? PasswordReset { get; set; }
    public DateTime? Verified { get; set; }
    public bool Active { get; set; } = false;
    public Guid AccountId { get; set; } = accountId;
    public Account? Account { get; set; }
    public IEnumerable<Role> Roles { get; } = new HashSet<Role>();
    public IEnumerable<Profile> Profiles { get; } = new HashSet<Profile>();
}
