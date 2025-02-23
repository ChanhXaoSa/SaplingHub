using Duende.IdentityServer.EntityFramework.Options;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SH_BusinessObjects.Common.Interface;
using SH_DataAccessObjects.Context.Interceptor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_DataAccessObjects.Context
{
    public class SaplingHubContextFactory : IDesignTimeDbContextFactory<SaplingHubContext>
    {
        public SaplingHubContext CreateDbContext(string[] args)
        {
            // Tạo configuration để đọc chuỗi kết nối từ appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../UI")) // Thư mục SH_DataAccessObjects
                .AddJsonFile("appsettings.json", optional: true) // optional nếu file ở SH_UI
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? "Server=(local);Database=SaplingHubDb;uid=sa;pwd=Trieu123!;TrustServerCertificate=True;MultipleActiveResultSets=true";

            var optionsBuilder = new DbContextOptionsBuilder<SaplingHubContext>();
            optionsBuilder.UseSqlServer(connectionString);

            // Tạo các dependency
            var mediator = new MockMediator();
            var currentUserService = new MockCurrentUserService();
            var dateTime = new MockDateTime();
            var interceptor = new AuditableEntitySaveChangesInterceptor(currentUserService, dateTime);
            var operationalStoreOptions = Options.Create(new OperationalStoreOptions());

            return new SaplingHubContext(
                optionsBuilder.Options,
                operationalStoreOptions,
                mediator,
                interceptor
            );
        }
    }
    public class MockMediator : IMediator
    {
        public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
            => Task.FromResult(default(TResponse));

        public Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest
            => Task.CompletedTask;

        public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
            => Task.CompletedTask;

        public Task Publish(object notification, CancellationToken cancellationToken = default)
            => Task.CompletedTask;
        public Task<object?> Send(object request, CancellationToken cancellationToken = default)
            => Task.FromResult<object?>(null); // Trả về null vì đây là mock

        public IAsyncEnumerable<TResponse> CreateStream<TResponse>(IStreamRequest<TResponse> request, CancellationToken cancellationToken = default)
            => AsyncEnumerable.Empty<TResponse>(); // Trả về một luồng rỗng

        public IAsyncEnumerable<object?> CreateStream(object request, CancellationToken cancellationToken = default)
            => AsyncEnumerable.Empty<object?>();
    }

    public class MockCurrentUserService : ICurrentUserService
    {
        public string UserId => "DesignTimeUser";
    }

    public class MockDateTime : IDateTime
    {
        public DateTime Now => DateTime.UtcNow;
    }
}

static class AsyncEnumerable
{
    public static IAsyncEnumerable<T> Empty<T>()
    {
        return new EmptyAsyncEnumerable<T>();
    }

    private class EmptyAsyncEnumerable<T> : IAsyncEnumerable<T>
    {
        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new EmptyAsyncEnumerator<T>();
        }
    }

    private class EmptyAsyncEnumerator<T> : IAsyncEnumerator<T>
    {
        public T Current => default;

        public ValueTask<bool> MoveNextAsync() => new ValueTask<bool>(false);

        public ValueTask DisposeAsync() => default;
    }
}
