using System;
using System.Linq;
using System.Xml.Linq;
using System.Text.Json;

namespace FullStackCase
{
    internal class FilterPeopleFromXml
    {
        public static string FilterPeopleFromXmlMethod(string xmlData)
        {
            if (string.IsNullOrWhiteSpace(xmlData))  //boş veya null kontrolü
                return JsonSerializer.Serialize(new
                {
                    Names = new List<string>(),
                    TotalSalary = 0,
                    AverageSalary = 0,
                    MaxSalary = 0,
                    Count = 0
                }); //boş çıkış(output) objesi

            var doc = XDocument.Parse(xmlData); //XML pars edilir

            var persons = doc.Descendants("Person") // XML deki tüm Person ları bulur
                             .Select(p => new
                             {
                                 Name = (string)p.Element("Name"),
                                 Age = (int)p.Element("Age"),
                                 Department = (string)p.Element("Department"),
                                 Salary = (decimal)p.Element("Salary"),
                                 HireDate = DateTime.Parse((string)p.Element("HireDate"))
                             })
                             .Where(p => p.Age > 30 &&
                                         p.Department == "IT" &&
                                         p.Salary > 5000 &&
                                         p.HireDate.Year < 2019)
                             .ToList();

            var names = persons.Select(p => p.Name).OrderBy(n => n).ToList(); //isimlerin alfabetik olarak sıralanması
            var totalSalary = persons.Sum(p => p.Salary);
            var count = persons.Count;
            var averageSalary = count > 0 ? totalSalary / count : 0;
            var maxSalary = persons.Any() ? persons.Max(p => p.Salary) : 0;

            var result = new
            {
                Names = names,
                TotalSalary = totalSalary,
                AverageSalary = averageSalary,
                MaxSalary = maxSalary,
                Count = count
            };

            return JsonSerializer.Serialize(result);
        }
    }
}
