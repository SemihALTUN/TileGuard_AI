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

        private readonly LoggingService _logger = new LoggingService();
        public async Task<List<InspectionHistoryModel>> GetInspectionHistoryAsync(int limit = 15)
        {
            var list = new List<InspectionHistoryModel>();
            try
            {
                const string sql = @"
                SELECT id, user_id, shift_id, timestamp, status, detected_class, confidence, inference_time_ms, vision_analysis, image_path, created_at 
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
                    UserId = reader.IsDBNull(1) ? null : reader.GetInt32(1),
                    ShiftId = reader.IsDBNull(2) ? null : reader.GetInt32(2),
                    Timestamp = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                    Status = reader.IsDBNull(4) ? string.Empty : reader.GetString(4),
                    DetectedClass = reader.IsDBNull(5) ? string.Empty : reader.GetString(5),
                    Confidence = reader.GetDouble(6),
                    InferenceTimeMs = reader.GetDouble(7),
                    VisionAnalysis = reader.IsDBNull(8) ? string.Empty : reader.GetString(8),
                    ImagePath = reader.IsDBNull(9) ? null : reader.GetString(9),
                    CreatedAt = reader.IsDBNull(10) ? DateTime.Now : reader.GetDateTime(10)
                });
            }

            }
            catch (Exception ex)
            {
                await _logger.LogExceptionAsync("ERROR", $"GetInspectionHistoryAsync Hatası: {ex.Message}", ex.StackTrace);
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

        public async Task<ShiftStatsModel> GetCurrentShiftStatsAsync()
        {
            var stats = new ShiftStatsModel();
            try
            {
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
            }

            catch (Exception ex)
            {
                await _logger.LogExceptionAsync("ERROR", $"GetCurrentShiftStatsAsync Hatası: {ex.Message}", ex.StackTrace);
            }

            return stats;
        }
        public async Task SaveInspectionHistoryAsync(InspectionHistoryModel model)
        {
            try
            {
                const string sql = @"
                INSERT INTO inspection_history 
                (user_id, shift_id, timestamp, status, detected_class, confidence, inference_time_ms, vision_analysis, image_path, created_at)
                VALUES (@userId, @shiftId, @timestamp, @status, @detectedClass, @confidence, @inferenceTimeMs, @visionAnalysis, @imagePath, @createdAt);";

                using var conn = new NpgsqlConnection(_connectionString);
                await conn.OpenAsync();

                using var cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userId", (object?)model.UserId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@shiftId", (object?)model.ShiftId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@timestamp", model.Timestamp ?? string.Empty);
                cmd.Parameters.AddWithValue("@status", model.Status ?? string.Empty);
                cmd.Parameters.AddWithValue("@detectedClass", model.DetectedClass ?? string.Empty);
                cmd.Parameters.AddWithValue("@confidence", model.Confidence);
                cmd.Parameters.AddWithValue("@inferenceTimeMs", model.InferenceTimeMs);
                cmd.Parameters.AddWithValue("@visionAnalysis", model.VisionAnalysis ?? string.Empty);
                cmd.Parameters.AddWithValue("@imagePath", (object?)model.ImagePath ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@createdAt", DateTime.Now);

                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                await _logger.LogExceptionAsync("ERROR", $"SaveInspectionHistoryAsync Hatası: {ex.Message}", ex.StackTrace);
            }
        }
        public async Task<UserModel?> ValidateUserAsync(string username, string password)
        {
            try
            {
                using var conn = new NpgsqlConnection(_connectionString);
                await conn.OpenAsync();

                const string sql = @"
        SELECT user_id, username, password_hash, full_name, role, is_active 
        FROM users 
        WHERE TRIM(username) = TRIM(@username);";

                using var cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@username", username);

                using var reader = await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    string dbPassword = reader.GetString(2);
                    System.Diagnostics.Debug.WriteLine($"[LOGIN DEBUG] Girilen Şifre: '{password}' | DB'deki Şifre: '{dbPassword}'");

                    if (dbPassword.Trim() == password.Trim())
                    {
                        return new UserModel
                        {
                            UserId = reader.GetInt32(0),
                            Username = reader.GetString(1),
                            FullName = reader.GetString(3),
                            Role = reader.GetString(4),
                            IsActive = reader.GetBoolean(5)
                        };
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("[LOGIN DEBUG] Şifreler uyuşmuyor!");
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("[LOGIN DEBUG] Böyle bir kullanıcı adı hiç bulunamadı!");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogExceptionAsync("ERROR", $"ValidateUserAsync Hatası: {ex.Message}", ex.StackTrace);
            }
            return null;
        }
        public async Task<List<ShiftModel>> GetActiveShiftsAsync()
        {
            var list = new List<ShiftModel>();
            try
            {

                const string sql = @"
                SELECT shift_id, shift_name, start_time, end_time 
                FROM shifts 
                WHERE is_active = true 
                ORDER BY shift_id;";

                using var conn = new NpgsqlConnection(_connectionString);
                await conn.OpenAsync();

                using var cmd = new NpgsqlCommand(sql, conn);
                using var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    list.Add(new ShiftModel
                    {
                        ShiftId = reader.GetInt32(0),
                        ShiftName = reader.GetString(1),
                        StartTime = reader.GetFieldValue<TimeSpan>(2),
                        EndTime = reader.GetFieldValue<TimeSpan>(3)
                    });
                }
            }
            catch (Exception ex)
            {
                await _logger.LogExceptionAsync("ERROR", $"GetActiveShiftsAsync Hatası: {ex.Message}", ex.StackTrace);
            }
            return list;
        }
        public async Task<bool> CreateUserAsync(string username, string passwordHash, string fullName, string role)
        {
            try
            {
                const string sql = @"
            INSERT INTO users (username, password_hash, full_name, role, is_active) 
            VALUES (@username, @passwordHash, @fullName, @role, true);";

                using var conn = new NpgsqlConnection(_connectionString);
                await conn.OpenAsync();

                using var cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@username", username.Trim());
                cmd.Parameters.AddWithValue("@passwordHash", passwordHash.Trim());
                cmd.Parameters.AddWithValue("@fullName", fullName.Trim());
                cmd.Parameters.AddWithValue("@role", role.Trim());

                await cmd.ExecuteNonQueryAsync();
                return true;
            }
            catch (Exception ex)
            {
                await _logger.LogExceptionAsync("ERROR", $"CreateUserAsync Hatası: {ex.Message}", ex.StackTrace);
                return false;
            }
        }
        public async Task<List<UserModel>> GetAllUsersAsync()
        {
            var list = new List<UserModel>();
            try
            {
                const string sql = @"
            SELECT user_id, username, password_hash, full_name, role, is_active 
            FROM users 
            ORDER BY user_id;";

                using var conn = new NpgsqlConnection(_connectionString);
                await conn.OpenAsync();

                using var cmd = new NpgsqlCommand(sql, conn);
                using var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    list.Add(new UserModel
                    {
                        UserId = reader.GetInt32(0),
                        Username = reader.GetString(1),
                        FullName = reader.GetString(3),
                        Role = reader.GetString(4),
                        IsActive = reader.GetBoolean(5)
                    });
                }
            }
            catch (Exception ex)
            {
                await _logger.LogExceptionAsync("ERROR", $"GetAllUsersAsync Hatası: {ex.Message}", ex.StackTrace);
            }
            return list;
        }

        public async Task<bool> UpdateUserAsync(int userId, string fullName, string role, bool isActive)
        {
            try
            {
                const string sql = @"
            UPDATE users 
            SET full_name = @fullName, role = @role, is_active = @isActive 
            WHERE user_id = @userId;";

                using var conn = new NpgsqlConnection(_connectionString);
                await conn.OpenAsync();

                using var cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@fullName", fullName.Trim());
                cmd.Parameters.AddWithValue("@role", role.Trim());
                cmd.Parameters.AddWithValue("@isActive", isActive);
                cmd.Parameters.AddWithValue("@userId", userId);

                await cmd.ExecuteNonQueryAsync();
                return true;
            }
            catch (Exception ex)
            {
                await _logger.LogExceptionAsync("ERROR", $"UpdateUserAsync Hatası: {ex.Message}", ex.StackTrace);
                return false;
            }
        }
        public async Task<bool> UpdatePasswordAsync(int userId, string newPassword)
        {
            try
            {
                const string sql = @"
            UPDATE users 
            SET password_hash = @password 
            WHERE user_id = @userId;";

                using var conn = new NpgsqlConnection(_connectionString);
                await conn.OpenAsync();

                using var cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@password", newPassword.Trim());
                cmd.Parameters.AddWithValue("@userId", userId);

                await cmd.ExecuteNonQueryAsync();
                return true;
            }
            catch (Exception ex)
            {
                await _logger.LogExceptionAsync("ERROR", $"UpdatePasswordAsync Hatası: {ex.Message}", ex.StackTrace);
                return false;
            }
        }

    }
}