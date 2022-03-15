using Core.Models;

namespace Infrastructure.Helpers
{
    public interface ISensorNameMapper
    {
        string Map(SensorType sensorType);
    }

    public class SensorNameMapper : ISensorNameMapper
    {
        public string Map(SensorType sensorType)
        {
            switch (sensorType)
            {
                case SensorType.Temperature:
                    return "Temperature";
                case SensorType.Pulse:
                    return "Pulse (BPM)";
                case SensorType.GSR:
                    return "Galvanic Skin Response (GSR)";
                case SensorType.ECG:
                    return "Electrocardiogram (ECG)";
                case SensorType.OxygenSaturation:
                    return "Oxygen Saturation (SpO2)";
                default:
                    return "Unknown sensor";
            }
        }
    }
}
