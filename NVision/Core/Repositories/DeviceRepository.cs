using Core.Contexts;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IDeviceRepository : IGenericRepository<Device>
    {
        Task<int> GetOwnerSubjectIdAsync(Guid deviceSerialNumber);
    }

    public class DeviceRepository : GenericRepository<Device>, IDeviceRepository
    {
        public DeviceRepository(NVisionDbContext context) : base(context)
        {
        }

        public async Task<int> GetOwnerSubjectIdAsync(Guid deviceSerialNumber)
        {
            var device = await Context.Device
                .FirstOrDefaultAsync(d => d.SerialNumber == deviceSerialNumber);
            return device.SubjectId;
        }
    }
}
