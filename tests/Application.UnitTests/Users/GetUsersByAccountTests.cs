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

using Security.Application.Users.Queries.GetUsersByAccount;
using Security.Domain.Interfaces;
using Security.Domain.Models;

namespace Application.UnitTests.Users;

[TestFixture]
internal class GetUsersByAccountTests
{
    private Mock<IUserReader> _readerMock;

    [SetUp]
    public void Setup()
    {
        // Initialize the mock and the object under test before each test
        _readerMock = new Mock<IUserReader>();
    }

    [Test]
    public async Task Handle_ValidQuery_ReturnsUserVmCollection()
    {
        // Arrange
        var accountId = Guid.NewGuid();
        var cancellationToken = new CancellationToken();

        var users = new List<UserVm> { /* initialize with mock data */ };
        _readerMock.Setup(m => m.GetUserByAccountAsync(accountId, cancellationToken))
                  .ReturnsAsync(users);

        var handler = new GetUsersByAccountQueryHandler(_readerMock.Object);
        var query = new GetUsersByAccountQuery(accountId);

        // Act
        var result = await handler.Handle(query, cancellationToken);

        // Assert
        result.Should().NotBeNull();
        users.Should().BeSameAs(result);

        // Verify that GetUserByAccountAsync was called with the correct arguments
        _readerMock.Verify(m => m.GetUserByAccountAsync(accountId, cancellationToken), Times.Once);
    }
}
