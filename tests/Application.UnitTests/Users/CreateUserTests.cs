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

using Security.Application.Users.Commands.CreateUser;
using Security.Application.Users.Events;
using Security.Domain.Interfaces;
using Security.Domain.Models;
using Security.Domain.Records;

namespace Application.UnitTests.Users;

[TestFixture]
internal class Tests
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
    public async Task Handle_ValidCommand_ReturnsUserVm()
    {
        var userId = Guid.NewGuid();
        // Arrange
        var userDto = new UserDto { UserId = userId };
        var userVm = new UserVm { UserId = userId };

        _writerMock.Setup(m => m.CreateUserAsync(userDto, It.IsAny<CancellationToken>()))
                  .ReturnsAsync(userVm);

        var handler = new CreateUserCommandHandler(_writerMock.Object, _publisherMock.Object);
        var command = new CreateUserCommand { User = userDto };
        var cancellationToken = new CancellationToken();

        // Act
        var result = await handler.Handle(command, cancellationToken);

        // Assert
        result.Should().NotBeNull();
        result.UserId.Should().Be(userDto.UserId);

        // Verify that CreateUserAsync was called with the correct arguments
        _writerMock.Verify(m => m.CreateUserAsync(userDto, cancellationToken), Times.Once);

        // Verify that Publish was called with the correct arguments
        _publisherMock.Verify(m => m.Publish(It.IsAny<UserCreated.Notification>(), cancellationToken), Times.Once);
    }
}
