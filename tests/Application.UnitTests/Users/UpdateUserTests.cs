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

using Security.Application.Users.Commands.UpdateUser;
using Security.Application.Users.Events;
using Security.Domain.Interfaces;
using Security.Domain.Records;

namespace Application.UnitTests.Users;

[TestFixture]
internal class UpdateUserTests
{
    private Mock<IUserWriter> _writerMock;
    private Mock<IPublisher> _publisherMock;

    [SetUp]
    public void Setup()
    {
        // Initialize the mock and the object under test before each test
        _writerMock = new Mock<IUserWriter>();
        _publisherMock = new Mock<IPublisher>();
    }

    [Test]
    public async Task Handle_ValidCommand_UpdatesUserAndPublishesNotification()
    {
        // Arrange
        var userDto = new UserDto { UserId = Guid.NewGuid() };
        var userId = userDto.UserId; // Assuming UserDto has a UserId property
        var cancellationToken = new CancellationToken();

        _writerMock.Setup(m => m.UpdateUserAsync(userDto, cancellationToken))
                  .Returns(Task.CompletedTask); // UpdateUserAsync returns a completed task

        var handler = new UpdateUserCommandHandler(_writerMock.Object, _publisherMock.Object);
        var command = new UpdateUserCommand { User = userDto };

        // Act
        await handler.Handle(command, cancellationToken);

        // Assert
        // Verify that UpdateUserAsync was called with the correct arguments
        _writerMock.Verify(m => m.UpdateUserAsync(userDto, cancellationToken), Times.Once);

        // Verify that Publish was called with the correct arguments
        _publisherMock.Verify(m => m.Publish(It.IsAny<UserUpdated.Notification>(), cancellationToken), Times.Once);
    }
}
