using CommunicationWithDB.Common;
using System.Data.SqlClient;

Console.Title = "ADO.NET";

var configuration = new DbConfiguration();

var connectionString = configuration.GetConnectionString("ado");

if (string.IsNullOrEmpty(connectionString))
{
    throw new ArgumentNullException(connectionString);
}

Console.WriteLine($"Connection string for ADO is: {connectionString}");

// Объявлем соединение с определенной строкой подключения.
var sqlConnection = new SqlConnection(connectionString);

try
{
    sqlConnection.Open();
    Console.WriteLine("SQL Connection open.");

    // Добавление (аналогичный код для обновления / удаления
    var sqlCommand = sqlConnection.CreateCommand();
    sqlCommand.CommandText = "INSERT INTO Student VALUES ('TestUser', 1, '20220101')";
    var affectedRows = sqlCommand.ExecuteNonQuery();
    Console.WriteLine($"Число затронутых строк: {affectedRows}");

    // Чтение
    var sqlCommandForRead = sqlConnection.CreateCommand();
    sqlCommandForRead.CommandText = "SELECT * FROM Student";
    SqlDataReader reader = sqlCommandForRead.ExecuteReader();

    if (reader.HasRows)
    {
        while (reader.Read())
        {
            // При использовании reader[""] - мы получаем object,
            // если хотим конкретный тип,
            // то используем reader.GetString() / reader.GetInt() и т.д.
            Console.WriteLine($"Студент с Id: {reader["Id"]}, " +
                $"с курсом: {reader["Course"]}, " +
                $"с именем: {reader["Name"]}, " +
                $"с датой рождения: {reader["BirthDate"]}");
        }
    }

    reader.Close();

    // Получение результата агрегатной функции
    var sqlCommandForCount = sqlConnection.CreateCommand();
    sqlCommandForCount.CommandText = "SELECT COUNT(*) FROM Student";
    var count = sqlCommandForCount.ExecuteScalar();
    Console.WriteLine($"Полное число студентов: {count}");
}
catch (Exception ex)
{
    Console.WriteLine($"Ошибка: {ex.Message}");
    throw;
}
finally
{
    sqlConnection.Close();
    Console.WriteLine("SQL соединение закрыто.");
}


