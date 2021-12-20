namespace Infrastructure.DTOs
{
    public class SubjectWithMeasurementsRequestDto
    {
        public int SubjectId { get; set; }
        public SensorMeasurementSpecificationDto SensorMeasurementSpecificationDto { get; set; }
    }
}
