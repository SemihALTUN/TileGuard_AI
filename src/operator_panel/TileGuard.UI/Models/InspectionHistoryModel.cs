using System;

namespace TileGuard.UI.Models
{
    public class InspectionHistoryModel
    {
        public int Id { get; set; }
        public string Timestamp { get; set; } = string.Empty;
        public string Status { get; set; } = "OK";
        public string DetectedClass { get; set; } = "good";
        public double Confidence { get; set; }
        public double InferenceTimeMs { get; set; }
        public string VisionAnalysis { get; set; } = string.Empty;
    }
}