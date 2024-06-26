﻿// Copyright (c) 2024 Sergio Hernandez. All rights reserved.
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

using Security.Application.Users.Events;
using Security.Domain.Interfaces;

namespace Security.Application.Users.Commands.DeleteUser;

public readonly record struct DeleteUserCommand(Guid Id) : IRequest;

public class DeleteUserCommandHandler(IUserWriter writer, IPublisher publisher) : IRequestHandler<DeleteUserCommand>
{

    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        await writer.DeleteUserAsync(request.Id, cancellationToken);

        await publisher.Publish(new UserDeleted.Notification(request.Id), cancellationToken);
    }

}
