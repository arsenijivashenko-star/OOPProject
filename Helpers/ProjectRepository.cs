using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace StartupPlatform;

public class ProjectRepository
{
    private const string DbFileName = "db.csv";
    private readonly UIConfig _ui; // Інжектований UI Config

    public ProjectRepository(UIConfig ui)
    {
        _ui = ui;
    }

    public List<Project> Load()
    {
        var list = new List<Project>();
        if (!File.Exists(DbFileName)) return list;

        string[] lines = File.ReadAllLines(DbFileName);
        for (int i = 1; i < lines.Length; i++)
        {
            var parts = lines[i].Split(';');
            if (parts.Length != 4) continue;

            string type = parts[0];
            string name = parts[1];

            // Безпечний парсинг незалежно від локалі ОС
            double req = double.Parse(parts[2], CultureInfo.InvariantCulture);
            double cur = double.Parse(parts[3], CultureInfo.InvariantCulture);

            if (type == "Startup") list.Add(new Startup(name, req, cur));
            else if (type == "Charity") list.Add(new CharityProject(name, req, cur));
        }

        Console.WriteLine(string.Format(_ui.SystemLoaded, list.Count, DbFileName));
        return list;
    }

    public void Save(List<Project> projects)
    {
        using var sw = new StreamWriter(DbFileName, false, System.Text.Encoding.UTF8);
        sw.WriteLine("Type;Name;RequiredFunding;CurrentFunding");

        foreach (var p in projects)
        {
            sw.WriteLine(p.ToCsvRow());
        }

        Console.WriteLine(string.Format(_ui.SuccessSaved, DbFileName));
    }
}