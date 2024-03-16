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

using Common.Application.Security;
using Common.Domain.Constants;
using Security.Domain.Interfaces;
using Security.Domain.Models;

namespace Security.Application.Users.Queries.GetUser;

[Authorize(Roles = Roles.Administrator)]
public record GetUserQuery() : IRequest<UserVm>
{
    public required Guid Id { get; init; }
}

public class GetUsersQueryHandler(IUserReader reader) : IRequestHandler<GetUserQuery, UserVm>
{
    public async Task<UserVm> Handle(GetUserQuery request, CancellationToken cancellationToken)
        => await reader.GetUserAsync(request.Id, cancellationToken);

}
