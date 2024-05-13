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

namespace Security.Application.Identity.Queries.IsInRole;

public readonly record struct IsInRoleQuey(Guid UserId, string Role) : IRequest<bool>
{
}

public class GetUsersQueryHandler(IUserReader reader) : IRequestHandler<IsInRoleQuey, bool>
{
    public async Task<bool> Handle(IsInRoleQuey request, CancellationToken cancellationToken)
        => await reader.IsInRoleAsync(request.UserId, request.Role, cancellationToken);

}
