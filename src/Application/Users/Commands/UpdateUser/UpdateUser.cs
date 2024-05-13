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

using Security.Application.Users.Events;
using Security.Domain.Interfaces;
using Security.Domain.Records;

namespace Security.Application.Users.Commands.UpdateUser;

public readonly record struct UpdateUserCommand : IRequest
{
    public UserDto User { get; init; }
}

public class UpdateUserCommandHandler(IUserWriter writer, IPublisher publisher) : IRequestHandler<UpdateUserCommand>
{

    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        await writer.UpdateUserAsync(request.User, cancellationToken);

        await publisher.Publish(new UserUpdated.Notification(request.User.UserId), cancellationToken);
    }
}
