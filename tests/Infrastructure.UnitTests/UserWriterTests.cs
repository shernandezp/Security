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

using Microsoft.EntityFrameworkCore.ChangeTracking;
using Security.Domain.Records;
using Security.Infrastructure.Entities;
using Security.Infrastructure.Interfaces;
using Security.Infrastructure.Writers;

namespace Infrastructure.UnitTests;

[TestFixture]
internal class UserWriterTests : Context
{
    // Declare necessary fields
    private Mock<IApplicationDbContext> _dbContextMock;
    private UserWriter _userWriter;

    [SetUp]
    public void Setup()
    {
        // Initialize the mock and the object under test before each test
        _dbContextMock = new Mock<IApplicationDbContext>();
        _userWriter = new UserWriter(_dbContextMock.Object);
    }

    [Test]
    public async Task CreateUserAsync_ValidUserDto_ReturnsUserVm()
    {
        // Arrange
        var userDto = new UserDto
        {
            Password = "password"
        };

        // Setup mock behavior for adding user and saving changes
        _dbContextMock.Setup(m => m.Users.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                      .Returns((User model, CancellationToken token) => new ValueTask<EntityEntry<User>>());
        _dbContextMock.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()))
                      .ReturnsAsync(1);

        // Act
        var result = await _userWriter.CreateUserAsync(userDto);

        // Assert
        result.Should().NotBeNull(); // Ensure that a non-null result is returned
    }

    [Test]
    public async Task UpdateUserAsync_ValidUserDto_UpdatesUser()
    {
        // Arrange
        var userDto = new UserDto
        {
            // Provide necessary user details
        };
        var cancellationToken = new CancellationToken();

        // Setup mock behavior for finding user and saving changes
        _dbContextMock.Setup(m => m.Users.FindAsync(It.IsAny<object[]>(), cancellationToken))
                      .ReturnsAsync(GetUser()); // Return a mock user for testing

        _dbContextMock.Setup(m => m.SaveChangesAsync(cancellationToken))
                      .ReturnsAsync(1); // Return a completed task

        // Act
        await _userWriter.UpdateUserAsync(userDto, cancellationToken);

        // Assert
        // Add assertions to check if the user is updated properly
    }

    [Test]
    public async Task UpdatePasswordAsync_ValidUserPasswordDto_UpdatesPassword()
    {
        // Arrange
        var userPasswordDto = new UserPasswordDto
        {
            // Provide necessary user password details
        };
        var cancellationToken = new CancellationToken();

        // Setup mock behavior for finding user and saving changes
        _dbContextMock.Setup(m => m.Users.FindAsync(It.IsAny<object[]>(), cancellationToken))
                      .ReturnsAsync(GetUser()); // Return a mock user for testing

        _dbContextMock.Setup(m => m.SaveChangesAsync(cancellationToken))
                      .ReturnsAsync(1); // Return a completed task

        // Act
        await _userWriter.UpdatePasswordAsync(userPasswordDto, cancellationToken);

        // Assert
        // Add assertions to check if the password is updated properly
    }

    [Test]
    public async Task DeleteUserAsync_ValidUserId_DeletesUser()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var cancellationToken = new CancellationToken();

        // Setup mock behavior for finding user and saving changes
        _dbContextMock.Setup(m => m.Users.FindAsync(It.IsAny<object[]>(), cancellationToken))
                      .ReturnsAsync(GetUser()); // Return a mock user for testing

        _dbContextMock.Setup(m => m.SaveChangesAsync(cancellationToken))
                      .ReturnsAsync(1); // Return a completed task

        // Act
        await _userWriter.DeleteUserAsync(userId, cancellationToken);

        // Assert
        // Add assertions to check if the user is deleted properly
    }
}
