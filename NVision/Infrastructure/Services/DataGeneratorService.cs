using Core.Models;
using Core.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IDataGeneratorService
    {
        Task GenerateDataAsync();
    }

    public class DataGeneratorService : IDataGeneratorService
    {
        private readonly IWatcherRepository _watcherRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly ISensorMeasurementRepository _sensorMeasurementRepository;
        private readonly IAlertRepository _alertRepository;
        private readonly IDeviceRepository _deviceRepository;

        public DataGeneratorService(
            IWatcherRepository watcherRepository,
            ISubjectRepository subjectRepository,
            ISensorMeasurementRepository sensorMeasurementRepository,
            IAlertRepository alertRepository,
            IDeviceRepository deviceRepository)
        {
            _watcherRepository = watcherRepository;
            _subjectRepository = subjectRepository;
            _sensorMeasurementRepository = sensorMeasurementRepository;
            _alertRepository = alertRepository;
            _deviceRepository = deviceRepository;
        }
        public async Task GenerateDataAsync()
        {
            bool anyWatchers = (await _watcherRepository.SelectAllAsync()).Any();
            bool anySubjects = (await _subjectRepository.SelectAllAsync()).Any();
            bool anyMeasurements = (await _sensorMeasurementRepository.SelectAllAsync()).Any();
            bool anyAlerts = (await _alertRepository.SelectAllAsync()).Any();
            bool anyDevices = (await _deviceRepository.SelectAllAsync()).Any();

            if (!anyWatchers)
                await GenerateWatchersAsync();
            if (!anySubjects)
                await GenerateSubjectsAsync();
            if (!anyMeasurements)
                await GenerateSensorMeasurementsAsync();
            if (!anyAlerts)
                await GenerateAlertsAsync();
            if (!anyDevices)
                await GenerateDevicesAsync();
        }

        private async Task GenerateWatchersAsync()
        {
            var watcher1 = new Watcher
            {
                FirstName = "Gabriel",
                LastName = "Stancu",
                Username = "GabiS",
                Password = "Gabi_123!",
                Birthday = new DateTime(1999, 04, 16),
                PhoneNumber = "+40771699564",
                ProfilePictureSrc = "grandfather.png"
            };

            var watcher2 = new Watcher
            {
                FirstName = "Adrian",
                LastName = "Buciuman",
                Username = "AdiB",
                Password = "Adi_123!",
                Birthday = new DateTime(1999, 02, 09),
                PhoneNumber = "+40789631783",
                ProfilePictureSrc = "grandfather.png"
            };

            await _watcherRepository.InsertAsync(watcher1);
            await _watcherRepository.InsertAsync(watcher2);
        }

        private async Task GenerateSubjectsAsync()
        {
            var watchers = (await _watcherRepository.SelectAllAsync()).ToList();
            var subject1 = new Subject
            {
                FirstName = "Daniel",
                LastName = "Bancos",
                Username = "DaniB",
                Password = "Dani_123!",
                WatcherId = watchers[0].Id,
                Address = "str. Mihai Eminescu nr. 20, Baia-Mare, Satu-Mare, Romania",
                Birthday = new DateTime(2000, 7, 9),
                IsPatient = false,
                HealthStatus = "Not assigned yet",
                Sex = 'M',
                ProfilePictureSrc = "grandfather.png"
            };

            var subject2 = new Subject
            {
                FirstName = "Robert",
                LastName = "Calatoae",
                Username = "RobiC",
                Password = "Robi_123!",
                WatcherId = watchers[0].Id,
                Address = "str. George Cosbuc nr. 11, Braila, Braila, Romania",
                Birthday = new DateTime(1999, 9, 30),
                IsPatient = true,
                HealthStatus = "Not assigned yet",
                Sex = 'M',
                ProfilePictureSrc = "patient.png"
            };

            var subject3 = new Subject
            {
                FirstName = "Andrei",
                LastName = "Stirbu",
                Username = "AndreiS",
                Password = "Andrei_123!",
                WatcherId = watchers[0].Id,
                Address = "str. Eroilor nr. 14, Suceava, Suceava, Romania",
                Birthday = new DateTime(1999, 7, 5),
                IsPatient = true,
                HealthStatus = "Not assigned yet",
                Sex = 'M',
                ProfilePictureSrc = "grandfather.png"
            };

            await _subjectRepository.InsertAsync(subject1);
            await _subjectRepository.InsertAsync(subject2);
            await _subjectRepository.InsertAsync(subject3);
        }

        private async Task GenerateSensorMeasurementsAsync()
        {
            var subjects = (await _subjectRepository.SelectAllAsync()).ToList();
            var crtDate = DateTime.UtcNow;

            foreach (var subject in subjects)
            {
                foreach (SensorType sensorType in Enum.GetValues(typeof(SensorType)))
                {
                    double lastMeasurement = new Random().NextDouble() * 30;
                    DateTime startDate = new DateTime(crtDate.Year, crtDate.AddMonths(-1).Month, crtDate.Day, 12, 0, 0);
                    DateTime endDate = new DateTime(crtDate.Year, crtDate.Month, crtDate.Day, crtDate.Hour, 0, 0);
                    TimeSpan sensorMeasurementPeriod = new TimeSpan(1, 0, 0, 0);

                    while (startDate < endDate)
                    {
                        Console.WriteLine($"Generating data for subject {subject.FirstName} {subject.LastName} for sensor {sensorType} at {startDate}");
                        int sign = new Random().NextDouble() >= 0.5 ? 1 : -1;
                        var sensorMeasurement = new SensorMeasurement();

                        sensorMeasurement.SubjectId = subject.Id;
                        sensorMeasurement.Timestamp = startDate;
                        sensorMeasurement.Value = lastMeasurement + sign * new Random().NextDouble() / 2;
                        sensorMeasurement.SensorType = sensorType;

                        await _sensorMeasurementRepository.InsertAsync(sensorMeasurement);
                        startDate += sensorMeasurementPeriod;
                    }
                }
            }
        }

        private async Task GenerateAlertsAsync()
        {
            var subjects = await _subjectRepository.SelectAllAsync();

            foreach (var subject in subjects)
            {
                int alertsNumber = (int)(new Random().NextDouble() * 4) + 3;
                for (int alertIndex = 0; alertIndex < alertsNumber; alertIndex++)
                {
                    double alertResult = new Random().NextDouble();
                    var alert = new Alert
                    {
                        Message = $"Test alert #{alertIndex + 1} for subject {subject.FullName}",
                        SubjectId = subject.Id,
                        WasTrueAlert = alertResult <= 0.5 ? true : (alertResult <= 0.75 ? false : null),
                        Timestamp = DateTime.Now.AddDays(-5 * (alertsNumber - alertIndex) + 2)
                    };
                    await _alertRepository.InsertAsync(alert);
                }
            }
        }

        private async Task GenerateDevicesAsync()
        {
            var subjects = await _subjectRepository.SelectAllAsync();

            foreach (var subject in subjects)
            {
                var device = new Device
                {
                    SerialNumber = Guid.NewGuid(),
                    SubjectId = subject.Id
                };
                await _deviceRepository.InsertAsync(device);
            }
        }
    }
}
