// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Running;
using CommunicationWithDB.Tests;

Console.WriteLine("Hello, World!");


BenchmarkRunner.Run<BenchmarkEF>();
BenchmarkRunner.Run<BenchmarkAdo>(); 
BenchmarkRunner.Run<BenchmarkDapper>();