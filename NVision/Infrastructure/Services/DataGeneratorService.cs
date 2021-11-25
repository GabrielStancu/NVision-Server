using Core.Models;
using Core.Repositories;
using Infrastructure.Convertors;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class DataGeneratorService : IDataGeneratorService
    {
        private readonly IWatcherRepository _watcherRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly ISensorMeasurementRepository _sensorMeasurementRepository;
        private readonly ISensorTypeToSensorMeasurementConvertor _convertor;

        public DataGeneratorService(
            IWatcherRepository watcherRepository,
            ISubjectRepository subjectRepository,
            ISensorMeasurementRepository sensorMeasurementRepository,
            ISensorTypeToSensorMeasurementConvertor convertor)
        {
            _watcherRepository = watcherRepository;
            _subjectRepository = subjectRepository;
            _sensorMeasurementRepository = sensorMeasurementRepository;
            _convertor = convertor;
        }
        public async Task GenerateData()
        {
            if (!(await _sensorMeasurementRepository.SelectAllAsync()).Any())
            {
                await GenerateWatchers();
                await GenerateSubjects();
                await GenerateSensorMeasurements();
            }
        }

        private async Task GenerateWatchers()
        {
            var watcher1 = new Watcher
            {
                FirstName = "Gabriel",
                LastName = "Stancu",
                Username = "GabiS",
                Password = "Gabi_123!"
            };

            var watcher2 = new Watcher
            {
                FirstName = "Adrian",
                LastName = "Buciuman",
                Username = "AdiB",
                Password = "Adi_123!"
            };

            await _watcherRepository.InsertAsync(watcher1);
            await _watcherRepository.InsertAsync(watcher2);
        }

        private async Task GenerateSubjects()
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
                IsPatient = false
            };

            var subject2 = new Subject
            {
                FirstName = "Robert",
                LastName = "Calatoae",
                Username = "RobiC",
                Password = "Robi_123!",
                WatcherId = watchers[1].Id,
                IsPatient = true
            };

            await _subjectRepository.InsertAsync(subject1);
            await _subjectRepository.InsertAsync(subject2);
        }

        private async Task GenerateSensorMeasurements()
        {
            var subjects = (await _subjectRepository.SelectAllAsync()).ToList();

            foreach (var subject in subjects)
            {
                foreach (SensorType sensorType in Enum.GetValues(typeof(SensorType)))
                {
                    double lastMeasurement = new Random().NextDouble() * 30;
                    DateTime crtDate = new DateTime(2021, 11, 24, 0, 0, 0);
                    DateTime endDate = new DateTime(2021, 11, 30, 23, 59, 59);
                    TimeSpan sensorMeasurementPeriod = new TimeSpan(0, 5, 0);

                    while (crtDate < endDate)
                    {
                        Console.WriteLine($"Generating data for subject {subject.FirstName} {subject.LastName} for sensor {sensorType.ToString()} at {crtDate}");
                        int sign = new Random().NextDouble() >= 0.5 ? 1 : -1;
                        var sensorMeasurement = _convertor.Convert(sensorType);

                        sensorMeasurement.SubjectId = subject.Id;
                        sensorMeasurement.Timestamp = crtDate;
                        sensorMeasurement.Value = lastMeasurement + sign * new Random().NextDouble() / 2;

                        await _sensorMeasurementRepository.InsertAsync(sensorMeasurement);
                        crtDate += sensorMeasurementPeriod;
                    }
                }
            }
        }
    }
}
