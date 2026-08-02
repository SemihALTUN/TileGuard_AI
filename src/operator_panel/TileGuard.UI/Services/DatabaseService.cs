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

        // int limit = 15 yapıyoruz: Varsayılan olarak 15 çeker. 
        // Parametre verilirse (örn: 100000) o kadar çeker!
        public async Task<List<InspectionHistoryModel>> GetInspectionHistoryAsync(int limit = 15)
        {
            var list = new List<InspectionHistoryModel>();

            const string sql = @"
        SELECT id, timestamp, status, detected_class, confidence, inference_time_ms, vision_analysis 
        FROM inspection_history 
        ORDER BY id DESC 
        LIMIT @limit;";

            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@limit", limit);

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
        public static DateTime GetCurrentShiftStartTime()
        {
            DateTime now = DateTime.Now;

            // Vardiya Saatleri: 08:00 - 16:00, 16:00 - 00:00, 00:00 - 08:00
            if (now.Hour >= 8 && now.Hour < 16)
                return new DateTime(now.Year, now.Month, now.Day, 8, 0, 0);  // 1. Vardiya
            else if (now.Hour >= 16)
                return new DateTime(now.Year, now.Month, now.Day, 16, 0, 0); // 2. Vardiya
            else
                return new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);  // 3. Vardiya
        }

        // 2. Aktif vardiyaya ait kayıtları created_at kolonuna göre çeken metot
        public async Task<ShiftStatsModel> GetCurrentShiftStatsAsync()
        {
            var stats = new ShiftStatsModel();
            DateTime shiftStart = GetCurrentShiftStartTime();

            const string sql = @"
        SELECT id, timestamp, status, detected_class, confidence, inference_time_ms, vision_analysis 
        FROM inspection_history 
        ORDER BY id DESC 
        LIMIT @limit;";

            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@shiftStart", shiftStart);

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                stats.TotalInspected = Convert.ToInt32(reader.GetInt64(0));
                stats.OkCount = Convert.ToInt32(reader.GetInt64(1));
                stats.NokCount = Convert.ToInt32(reader.GetInt64(2));
            }

            return stats;
        }

        // 3. Yeni tespit geldiğinde created_at ile kaydetme metodu
        public async Task SaveInspectionHistoryAsync(InspectionHistoryModel model)
        {
            const string sql = @"
        INSERT INTO inspection_history 
        (timestamp, status, detected_class, confidence, inference_time_ms, vision_analysis, created_at)
        VALUES (@timestamp, @status, @detectedClass, @confidence, @inferenceTimeMs, @visionAnalysis, @createdAt);";

            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@timestamp", model.Timestamp ?? string.Empty);
            cmd.Parameters.AddWithValue("@status", model.Status ?? string.Empty);
            cmd.Parameters.AddWithValue("@detectedClass", model.DetectedClass ?? string.Empty);
            cmd.Parameters.AddWithValue("@confidence", model.Confidence);
            cmd.Parameters.AddWithValue("@inferenceTimeMs", model.InferenceTimeMs);
            cmd.Parameters.AddWithValue("@visionAnalysis", model.VisionAnalysis ?? string.Empty);
            cmd.Parameters.AddWithValue("@createdAt", DateTime.Now);

            await cmd.ExecuteNonQueryAsync();
        }
    }
}