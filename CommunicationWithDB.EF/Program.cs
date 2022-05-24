using CommunicationWithDB.EF;
using CommunicationWithDB.EF.Models;
using Microsoft.EntityFrameworkCore;

Console.Title = "EF";

using (var dbContext = new UniversityContext())
{
    // Добавление данных
    // Для обновления используем Update, для удаления Remove.
    // Для любой из этих операций есть поддержка bulk операций (AddRange, UpdateRange, DeleteRange).
    dbContext.Student.Add(
        new Student() 
        { 
            Name = "SomeStudent For EF Test", 
            Course = 3, 
            BirthDate = new DateTime(2022, 4, 7) 
        });

    // Любое изменение данных требует сохранения.
    dbContext.SaveChanges();

    // Чтение данных.
    var students = dbContext.Student.ToList();

    foreach (var student in students)
    {
        Console.WriteLine($"Студент с Id: {student.Id}, " +
            $"с курсом: {student.Course}, " +
            $"с именем: {student.Name}, " +
            $"с датой рождения: {student.BirthDate}");
    }

    // Вывод количества студентов.
    var count = dbContext.Student.Count();

    Console.WriteLine($"Общее число записей в таблице студентов: {count}");

    // AsNoTracking
    var studentVladzimir = dbContext.Student
                            .Where(student => student.Name.Contains("Vladzimir"))
                            .FirstOrDefault();

    studentVladzimir.Course = 999;
    dbContext.SaveChanges();

    var studentVladzimirWithoutTracking = dbContext.Student
                                            .Where(student => student.Name.Contains("Vladzimir"))
                                            .AsNoTracking()
                                            .FirstOrDefault();

    studentVladzimirWithoutTracking.Course = 777;
    dbContext.SaveChanges();

    // Результат будет Course = 999.
}