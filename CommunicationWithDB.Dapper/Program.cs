using CommunicationWithDB.Common;
using CommunicationWithDB.Dapper;
using Dapper;
using System.Data.SqlClient;

Console.Title = "Dapper";

var configuration = new DbConfiguration();

var connectionString = configuration.GetConnectionString("connString");

if (string.IsNullOrEmpty(connectionString))
{
    throw new ArgumentNullException(connectionString);
}

Console.WriteLine($"Строка подключения для Dapper: {connectionString}");

using (var sqlConnection = new SqlConnection(connectionString))
{
    // Добавление (аналогичный код для обновления / удаления).
    sqlConnection.Execute("INSERT INTO Student VALUES ('TestUserDapper', 1, '20220101')");

    // Чтение данных.
    var students = sqlConnection.Query<Student>("SELECT * FROM Student").ToList();

    foreach (var student in students)
    {
        Console.WriteLine($"Студент с Id: {student.Id}, " +
            $"с курсом: {student.Course}, " +
            $"с именем: {student.Name}, " +
            $"с датой рождения: {student.BirthDate}");
    }

    // Получение результата агрегатной функции
    // В данном случае необходимо использование .FirstOrDefault(), так как
    // .Query<T> возвращает IEnumerable<T>, что является коллекцией.
    // И так как мы знаем что результатом будет 1 запись, то без зазрений совести
    // можем применить .FirstOrDefault(), чтобы получить число записей.
    var count = sqlConnection.Query<int>("SELECT COUNT(*) FROM Student").FirstOrDefault();

    Console.WriteLine($"Общее число записей в таблице студентов: {count}");

    // Использование параметров.
    sqlConnection.Execute("INSERT INTO Student VALUES (@Name, @Course, @BirthDate)", 
        new Student 
        { 
            Name = "SomeParamName", 
            Course = 2, 
            BirthDate = new DateTime(2022, 04, 04)
        });

    // Анонимные объекты new { }.
    sqlConnection.Execute("DELETE FROM Student WHERE Name = @name", 
        new { name = "TestUserDapper" });
}