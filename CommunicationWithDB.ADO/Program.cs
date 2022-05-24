using CommunicationWithDB.Common;
using System.Data.SqlClient;

Console.Title = "ADO.NET";

var configuration = new DbConfiguration();

var connectionString = configuration.GetConnectionString("ado");

if (string.IsNullOrEmpty(connectionString))
{
    throw new ArgumentNullException(connectionString);
}

Console.WriteLine($"Строка подключения для ADO: {connectionString}");

// Объявлем соединение с определенной строкой подключения.
var sqlConnection = new SqlConnection(connectionString);

try
{
    sqlConnection.Open();
    Console.WriteLine("SQL соединение открыто.");

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

    // Есть два варианта параметризации запросов
    var name = "Some Student Name";

    // Плохой вариант, так как позволяет получать и изменять данные при помощи механизма SQL-инъекций.
    var sqlString = $"INSERT INTO Student VALUES ('{name}', 1, '20220101')";

    var sqlCommandForInsertBadPractice = new SqlCommand(sqlString)
    {
        Connection = sqlConnection
    };

    affectedRows = sqlCommandForInsertBadPractice.ExecuteNonQuery();

    // Хороший вариант, добавление SQL параметров
    sqlString = $"INSERT INTO Student VALUES (@name, 1, '20220101')";
    var sqlParamForName = new SqlParameter("@name", name);
    var sqlCommandForInsertGoodPractice = new SqlCommand(sqlString);

    // Добавление параметра
    sqlCommandForInsertGoodPractice.Parameters.Add(sqlParamForName);

    affectedRows = sqlCommandForInsertBadPractice.ExecuteNonQuery();
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


