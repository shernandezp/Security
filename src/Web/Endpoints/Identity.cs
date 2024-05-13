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

using Security.Application.Identity.Queries.Authorize;
using Security.Application.Identity.Queries.GetUsername;
using Security.Application.Identity.Queries.IsInRole;

namespace Security.Web.Endpoints;

public sealed class Identity : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetUserName, "UserName/{userId}")
            .MapGet(IsInRole, "IsInRole/{userId}/{role}")
            .MapGet(Authorize, "Authorize/{userId}/{policyName}");
    }

    public async Task<string> GetUserName(ISender sender, [AsParameters] GetUserNameQuery query)
        => await sender.Send(query);

    public async Task<bool> IsInRole(ISender sender, Guid userId, [AsParameters] IsInRoleQuey query)
        => await sender.Send(query);

    public async Task<bool> Authorize(ISender sender, Guid userId, [AsParameters] AuthorizeQuery query)
        => await sender.Send(query);
}
