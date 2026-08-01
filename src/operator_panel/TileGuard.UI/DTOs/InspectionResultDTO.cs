using System.Collections.Generic;
using Newtonsoft.Json;

namespace TileGuard.UI.DTOs
{
    public class DetectionItemDto
    {
        [JsonProperty("class_name")]
        public string ClassName { get; set; } = string.Empty;

        [JsonProperty("confidence")]
        public double Confidence { get; set; }

        [JsonProperty("bbox")]
        public List<float> BoundingBox { get; set; } = new List<float>();
        [JsonProperty("vision_analysis")]
        public string VisionAnalysis { get; set; } = string.Empty;
    }

    public class InspectionResultDto
    {
        [JsonProperty("timestamp")]
        public string Timestamp { get; set; } = string.Empty;

        [JsonProperty("inference_time_ms")]
        public double InferenceTimeMs { get; set; }

        [JsonProperty("total_defects")]
        public int TotalDefects { get; set; }

        [JsonProperty("detections")]
        public List<DetectionItemDto> Detections { get; set; } = new List<DetectionItemDto>();

        [JsonProperty("error")]
        public string? Error { get; set; }
    }
}