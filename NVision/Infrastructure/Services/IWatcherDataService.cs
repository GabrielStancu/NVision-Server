﻿using Infrastructure.DTOs;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IWatcherDataService
    {
        Task<WatcherDashboardDataDto> GetWatcherDashboardDataAsync(int watcherId);
    }
}