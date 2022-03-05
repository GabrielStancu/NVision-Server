using System.Runtime.Serialization;

namespace Core.Models
{
    public enum SensorType
    {
        [EnumMember(Value = "TMP")]
        Temperature,
        [EnumMember(Value = "ECG")]
        ECG,
        [EnumMember(Value = "BPM")]
        Pulse,
        [EnumMember(Value = "OXY")]
        OxygenSaturation,
        [EnumMember(Value = "GSR")]
        GSR
    }
}
