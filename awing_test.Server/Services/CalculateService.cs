using awing_test.Server.Models.Entities;
using Microsoft.Data.Sqlite;
using SQLitePCL;
using System.Globalization;

public class CalculateService
{
    private readonly string _connectionString;

    public CalculateService(string dbPath)
    {
        _connectionString = $"Data Source={dbPath}";
        Batteries.Init();
        Initialize();
    }

    /// <summary>
    /// Init tạo bảng cho db
    /// </summary>
    private void Initialize()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var tableCmd = connection.CreateCommand();
        tableCmd.CommandText =
        @"
        CREATE TABLE IF NOT EXISTS CalcalateRecords (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            N INTEGER NOT NULL,
            M INTEGER NOT NULL,
            P INTEGER NOT NULL,
            Matrix TEXT NOT NULL,
            CreatedAt TEXT NOT NULL
        )";
        tableCmd.ExecuteNonQuery();
    }

    /// <summary>
    /// Insert bản ghi mới vào db
    /// </summary>
    /// <param name="calculateEntity"></param>
    /// <returns></returns>
    public async Task InsertRecordAsync(CalculateEntity calculateEntity)
    {
        using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        var insertCmd = connection.CreateCommand();
        insertCmd.CommandText =
        @"
        INSERT INTO CalcalateRecords (N, M, P, Matrix, CreatedAt)
        VALUES ($n, $m, $p, $matrix, $createdAt)";

        insertCmd.Parameters.AddWithValue("$n", calculateEntity.N);
        insertCmd.Parameters.AddWithValue("$m", calculateEntity.M);
        insertCmd.Parameters.AddWithValue("$p", calculateEntity.P);
        insertCmd.Parameters.AddWithValue("$matrix", calculateEntity.Matrix);
        insertCmd.Parameters.AddWithValue("$createdAt", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")); 

        await insertCmd.ExecuteNonQueryAsync();
    }

    /// <summary>
    /// Lấy thông tin các bảng ghi trong bảng
    /// </summary>
    /// <returns></returns>
    public async Task<List<CalculateEntity>> GetAllRecordsAsync()
    {
        var records = new List<CalculateEntity>();

        using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        var selectCmd = connection.CreateCommand();
        selectCmd.CommandText = "SELECT * FROM CalcalateRecords ORDER BY Id DESC";

        using var reader = await selectCmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            records.Add(new CalculateEntity
            {
                Id = reader.GetInt32(0),
                N = reader.GetInt32(1),
                M = reader.GetInt32(2),
                P = reader.GetInt32(3),
                Matrix = reader.GetString(4),
                CreatedAt = DateTime.ParseExact(reader.GetString(5), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture)
            });
        }

        return records;
    }
}
