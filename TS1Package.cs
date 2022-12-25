using System;
using System.IO.Compression;
using System.Text.Json;

namespace Sims1Pkg;

public class TS1Package : ISPackage, ISMetdata
{

    public string Name { get; set; }
    public string Author { get; set; }
    public string Date { get; set; }

    public void Compress(string file)
    {
        throw new NotImplementedException();
    }

    public void Extract(string file)
    {
        // Open the zip file in read mode
        using var zip = ZipFile.OpenRead(file);


        bool metadataFound = false;
        bool ts1Found = false;

        // Check if metadata.json and *.ts1 files are present
        foreach (var entry in zip.Entries)
        {
            if (entry.FullName == "metadata.json")
                metadataFound = true;

            if (entry.FullName.EndsWith(".ts1"))
                ts1Found = true;
        }

        if (metadataFound && ts1Found)
        {
            // Read the metadata.json file
            var metadataEntry = zip.GetEntry("metadata.json");
            using var stream = metadataEntry.Open();
            using var reader = new StreamReader(stream);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var metadata = JsonSerializer.Deserialize<TS1Package>(reader.ReadToEnd(), options);

            Name = metadata.Name;
            Date = metadata.Date;
            Author = metadata.Author;

            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Date: {Date}");
            Console.WriteLine($"Author: {Author}");


            // Extract only the *.iff and *.ts1 files
            foreach (var entry in zip.Entries)
            {
                if (entry.FullName.EndsWith(".iff"))
                {
                    // entry.ExtractToFile(entry.FullName);
                    Console.WriteLine(entry.FullName);
                }
            }
        }
        else
        {
            Console.WriteLine("Cannot extract files: required files are missing");
        }
    }
}

