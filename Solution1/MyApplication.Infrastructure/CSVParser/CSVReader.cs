using CsvHelper;
using MyApplication.Domain.Entities;
using System.Globalization;

namespace MyApplication.Infrastructure.CSVParser;
public class CSVReader
{
    public IEnumerable<Person> ReadPersonsFromCsv(string filePath)
    {
        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        return csv.GetRecords<Person>().ToList();
    }
}
