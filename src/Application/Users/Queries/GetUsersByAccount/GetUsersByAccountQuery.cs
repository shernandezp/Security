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

namespace Security.Application.Users.Queries.GetUsersByAccount;

public readonly record struct GetUsersByAccountQuery(Guid AccountId) : IRequest<IReadOnlyCollection<UserVm>>;

[Authorize(Roles = Roles.Administrator)]
public class GetUsersByAccountQueryHandler(IUserReader reader) : IRequestHandler<GetUsersByAccountQuery, IReadOnlyCollection<UserVm>>
{
    public async Task<IReadOnlyCollection<UserVm>> Handle(GetUsersByAccountQuery request, CancellationToken cancellationToken)
        => await reader.GetUserByAccountAsync(request.AccountId, cancellationToken);

}
