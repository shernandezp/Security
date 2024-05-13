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

using Common.Application.Interfaces;
using Security.Domain.Interfaces;

namespace Security.Infrastructure.Identity;
public class IdentityService(IUserReader userReader) : IIdentityService
{
    public async Task<string> GetUserNameAsync(Guid userId, CancellationToken token)
        => await userReader.GetUserNameAsync(userId, token);

    public async Task<bool> IsInRoleAsync(Guid userId, string role, CancellationToken token)
        => await userReader.IsInRoleAsync(userId, role, token);

    public async Task<bool> AuthorizeAsync(Guid userId, string policyName, CancellationToken token)
        => await userReader.AuthorizeAsync(userId, policyName, token);
}
