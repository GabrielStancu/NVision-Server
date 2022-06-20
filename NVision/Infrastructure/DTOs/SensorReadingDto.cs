﻿using System;

namespace Infrastructure.DTOs
{
    public class SensorReadingDto
    {
        public string Type { get; set; }
        public double Value { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
