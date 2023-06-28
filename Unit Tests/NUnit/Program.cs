using NUnit.Framework;
using NUnitLite;

namespace Unit_Tests.NUnit
{
    internal class Program
    {
        public static int Main(string[] args)
        {
            // Загрузка всех сборок с тестами
            var testAssembly = typeof(Program).Assembly;
            var runner = new AutoRun(testAssembly);

            // Запуск тестов
            var result = runner.Execute(args);

            // Возвращаемый код завершения
            return result;
        }
    }
}
