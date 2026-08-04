using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Npgsql;
using TileGuard.UI.Models;

namespace TileGuard.UI.Services
{
    public class LoggingService
    {
        private readonly string _connectionString ="Server=127.0.0.1;Port=5432;Database=tileguard_db;User Id=postgres;Password=;SSL Mode=Disable;";

        public async Task LogExceptionAsync(string logLevel, string message, string? stackTrace)
        {
            try
            {
                using var conn = new NpgsqlConnection(_connectionString);
                await conn.OpenAsync();

                const string sql = @"
                    INSERT INTO system_logs (log_level, message, stack_trace) 
                    VALUES (@level, @msg, @stack);";

                using var cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@level", logLevel);
                cmd.Parameters.AddWithValue("@msg", message);
                cmd.Parameters.AddWithValue("@stack", (object?)stackTrace ?? DBNull.Value);

                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[LOGGING SERVICE CRITICAL ERROR]: {ex.Message}");
            }
        }
        public async Task<List<LogModel>> GetAllLogsAsync()
        {
            var logs = new List<LogModel>();

            try
            {
                using var conn = new NpgsqlConnection(_connectionString);
                await conn.OpenAsync();

                const string sql = "SELECT log_id, log_level, message, stack_trace, created_at FROM system_logs ORDER BY created_at DESC;";
                using var cmd = new NpgsqlCommand(sql, conn);
                using var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    logs.Add(new LogModel
                    {
                        LogId = reader.GetInt32(0),
                        LogLevel = reader.GetString(1),
                        Message = reader.GetString(2),
                        StackTrace = reader.IsDBNull(3) ? null : reader.GetString(3),
                        CreatedAt = reader.GetDateTime(4)
                    });
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Loglar çekilirken hata: " + ex.Message);
            }

            return logs;
        }
    }
}