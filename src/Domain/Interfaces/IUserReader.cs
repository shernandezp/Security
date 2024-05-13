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

namespace Security.Domain.Interfaces;
public interface IUserReader
{
    Task<string> GetUserNameAsync(Guid id, CancellationToken cancellationToken = default);
    Task<UserVm> GetUserAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<UserVm>> GetUserByAccountAsync(Guid accountId, CancellationToken cancellationToken);
    Task<bool> IsInRoleAsync(Guid id, string name, CancellationToken cancellationToken = default);
    Task<bool> AuthorizeAsync(Guid id, string name, CancellationToken cancellationToken = default);
}
