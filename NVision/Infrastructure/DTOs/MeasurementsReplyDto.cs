using System.Collections.Generic;

namespace Infrastructure.DTOs
{
    public class MeasurementsReplyDto
    {
        public SubjectSummarizedDataDto SummarizedDataDto { get; set; }
        public IEnumerable<SensorMeasurementDto> Measurements { get; set; }
    }
}
