using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Npgsql;
using TileGuard.UI.Models;

namespace TileGuard.UI.Services
{
    public class DatabaseService
    {
        private readonly string _connectionString =
      "Server=127.0.0.1;Port=5432;Database=tileguard_db;User Id=postgres;Password=;SSL Mode=Disable;";
        public async Task SaveInspectionHistoryAsync(InspectionHistoryModel model)
        {
            const string sql = @"
                INSERT INTO inspection_history 
                (timestamp, status, detected_class, confidence, inference_time_ms, vision_analysis)
                VALUES (@timestamp, @status, @detectedClass, @confidence, @inferenceTimeMs, @visionAnalysis);";

            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@timestamp", model.Timestamp ?? string.Empty);
            cmd.Parameters.AddWithValue("@status", model.Status ?? string.Empty);
            cmd.Parameters.AddWithValue("@detectedClass", model.DetectedClass ?? string.Empty);
            cmd.Parameters.AddWithValue("@confidence", model.Confidence);
            cmd.Parameters.AddWithValue("@inferenceTimeMs", model.InferenceTimeMs);
            cmd.Parameters.AddWithValue("@visionAnalysis", model.VisionAnalysis ?? string.Empty);

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<List<InspectionHistoryModel>> GetInspectionHistoryAsync()
        {
            var list = new List<InspectionHistoryModel>();
            const string sql = "SELECT id, timestamp, status, detected_class, confidence, inference_time_ms, vision_analysis FROM inspection_history ORDER BY id DESC;";

            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new NpgsqlCommand(sql, conn);
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                list.Add(new InspectionHistoryModel
                {
                    Id = reader.GetInt32(0),
                    Timestamp = reader.GetString(1),
                    Status = reader.GetString(2),
                    DetectedClass = reader.GetString(3),
                    Confidence = reader.GetDouble(4),
                    InferenceTimeMs = reader.GetDouble(5),
                    VisionAnalysis = reader.IsDBNull(6) ? string.Empty : reader.GetString(6)
                });
            }

            return list;
        }
    }
}