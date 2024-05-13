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

using Security.Domain.Interfaces;
using Security.Infrastructure.Interfaces;

namespace Security.Infrastructure.Readers;
public sealed class UserReader(IApplicationDbContext context) : IUserReader
{

    public async Task<string> GetUserNameAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Users
            .Where(u => u.UserId.Equals(id))
            .Select(u => u.Username)
            .FirstAsync(cancellationToken);
    }

    public async Task<UserVm> GetUserAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Users
            .Where(u => u.UserId.Equals(id))
            .Include(role => role.Roles)
            .Include(profile => profile.Profiles)
            .Select(u => new UserVm(
                u.UserId,
                u.Username,
                u.Password,
                u.Email,
                u.FirstName,
                u.SecondName,
                u.LastName,
                u.SeconSurname,
                u.DOB,
                u.Roles.Select(r => new RoleVm(r.Name)).ToList(),
                u.Profiles.Select(p => new ProfileVm(p.Name)).ToList()))
            .FirstAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<UserVm>> GetUserByAccountAsync(Guid accountId, CancellationToken cancellationToken)
    {
        return await context.Users
            .Where(u => u.AccountId.Equals(accountId))
            .Select(u => new UserVm(
                u.UserId,
                u.Username,
                string.Empty,
                u.Email,
                u.FirstName,
                u.SecondName,
                u.LastName,
                u.SeconSurname,
                u.DOB,
                new List<RoleVm>(),
                new List<ProfileVm>()))
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> IsInRoleAsync(Guid id, string name, CancellationToken cancellationToken = default)
    {
        var user = await context.Users
            .Where(u => u.UserId.Equals(id))
            .Include(
                role => role.Roles
                    .Where(r => r.Name.Equals(name)))
            .FirstAsync(cancellationToken);

        return user.Roles.Any();
    }

    public async Task<bool> AuthorizeAsync(Guid id, string name, CancellationToken cancellationToken = default)
    {
        var user = await context.Users
           .Where(u => u.UserId.Equals(id))
           .Include(
               profile => profile.Profiles
                   .Where(r => r.Name.Equals(name)))
           .FirstAsync(cancellationToken);

        return user.Profiles.Any();
    }
}
