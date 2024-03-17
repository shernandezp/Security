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

using Security.Application.Users.Commands.DeleteUser;
using Security.Application.Users.Events;
using Security.Domain.Interfaces;

namespace Application.UnitTests.Users;

[TestFixture]
internal class DeleteUserTests
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
        var userId = Guid.NewGuid();
        var cancellationToken = new CancellationToken();

        _writerMock.Setup(m => m.DeleteUserAsync(userId, cancellationToken))
                  .Returns(Task.CompletedTask); // DeleteUserAsync returns a completed task

        var handler = new DeleteUserCommandHandler(_writerMock.Object, _publisherMock.Object);
        var command = new DeleteUserCommand(userId);

        // Act
        await handler.Handle(command, cancellationToken);

        // Assert
        // Verify that DeleteUserAsync was called with the correct arguments
        _writerMock.Verify(m => m.DeleteUserAsync(userId, cancellationToken), Times.Once);

        // Verify that Publish was called with the correct arguments
        _publisherMock.Verify(m => m.Publish(It.IsAny<UserDeleted.Notification>(), cancellationToken), Times.Once);
    }
}
