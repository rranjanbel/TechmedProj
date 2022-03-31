using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace TechMedAPI.JwtInfra
{
    public class JwtRefreshTokenCache : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly IJwtAuthManager _jwtAuthManager;

#pragma warning disable CS8618 // Non-nullable field '_timer' must contain a non-null value when exiting constructor. Consider declaring the field as nullable.
        public JwtRefreshTokenCache(IJwtAuthManager jwtAuthManager)
#pragma warning restore CS8618 // Non-nullable field '_timer' must contain a non-null value when exiting constructor. Consider declaring the field as nullable.
        {
            _jwtAuthManager = jwtAuthManager;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            // remove expired refresh tokens from cache every minute
#pragma warning disable CS8622 // Nullability of reference types in type of parameter 'state' of 'void JwtRefreshTokenCache.DoWork(object state)' doesn't match the target delegate 'TimerCallback' (possibly because of nullability attributes).
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
#pragma warning restore CS8622 // Nullability of reference types in type of parameter 'state' of 'void JwtRefreshTokenCache.DoWork(object state)' doesn't match the target delegate 'TimerCallback' (possibly because of nullability attributes).
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            _jwtAuthManager.RemoveExpiredRefreshTokens(DateTime.Now);
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
