using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace FullStackCase
{
    public static class FilterEmployees
    {
        public static string FilterEmployeesMethod(IEnumerable
            <(string Name, int Age, string Department, decimal Salary, DateTime HireDate)> employees)
        {
            //boş veya null kontrolü
            if (employees == null)
            {
                employees = new List<(string, int, string, decimal, DateTime)>();
            }

            // filtreleme kriterleri
            var filteredEmployees = employees
                .Where(emp => emp.Age >= 25 && emp.Age <= 40)
                .Where(emp => emp.Department == "IT" || emp.Department == "Finance")
                .Where(emp => emp.Salary >= 5000 && emp.Salary <= 9000)
                .Where(emp => emp.HireDate.Year > 2017)
                .ToList();

            // filtrelenmiş çalışan yoksa boş sonuç döndür
            if (!filteredEmployees.Any())
            {
                return JsonSerializer.Serialize(new
                {
                    Names = new List<string>(),
                    TotalSalary = 0,
                    AverageSalary = 0,
                    MinSalary = 0,
                    MaxSalary = 0,
                    Count = 0
                });
            }

            // isimleri sırala:[ 1) isim uzunluğuna göre azalan, 2) alfabetik olarak artan
            var sortedNames = filteredEmployees
                .Select(emp => emp.Name)
                .OrderByDescending(name => name.Length)
                .ThenBy(name => name)
                .ToList();

            // maaş hesaplamaları
            var totalSalary = filteredEmployees.Sum(emp => emp.Salary);
            var averageSalary = filteredEmployees.Average(emp => emp.Salary);
            var minSalary = filteredEmployees.Min(emp => emp.Salary);
            var maxSalary = filteredEmployees.Max(emp => emp.Salary);
            var count = filteredEmployees.Count;

            var result = new
            {
                Names = sortedNames,
                TotalSalary = totalSalary,
                AverageSalary = Math.Round(averageSalary, 2),
                MinSalary = minSalary,
                MaxSalary = maxSalary,
                Count = count
            };

            return JsonSerializer.Serialize(result);
        }
    }
}