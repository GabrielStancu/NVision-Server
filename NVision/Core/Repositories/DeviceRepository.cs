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
        Task<string> GetSubjectDeviceSerialNumberAsync(int subjectId);
        Task<bool> UpdateSerialNumberAsync(int subjectId, string serialNumber);
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

        public async Task<string> GetSubjectDeviceSerialNumberAsync(int subjectId)
        {
            var device = await Context.Device
                .FirstOrDefaultAsync(d => d.SubjectId == subjectId);
            return device.SerialNumber.ToString();
        }

        public async Task<bool> UpdateSerialNumberAsync(int subjectId, string serialNumber)
        {
            var device = await Context.Device
                .FirstOrDefaultAsync(d => d.SubjectId == subjectId);
            var deviceSerialNumber = new Guid(serialNumber);

            await (device is null
                ? CreateNewDeviceAsync(subjectId, deviceSerialNumber)
                : UpdateDeviceAsync(device, deviceSerialNumber));
            return true;
        }

        private async Task CreateNewDeviceAsync(int subjectId, Guid serialNumber)
        {
            var device = new Device
            {
                SubjectId = subjectId,
                SerialNumber = serialNumber
            };

            await InsertAsync(device);
        }

        private async Task UpdateDeviceAsync(Device device, Guid serialNumber)
        {
            device.SerialNumber = serialNumber;
            await UpdateAsync(device);
        }
    }
}
