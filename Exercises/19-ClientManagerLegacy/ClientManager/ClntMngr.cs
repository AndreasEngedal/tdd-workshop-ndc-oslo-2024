namespace ClientManager;

using System;
using System.IO;

public class ClntMngr
{
    public void AddClnt(string name, string email, string filePath)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email))
        {
            throw new Exception("Name and email are required.");
        }

        if (!email.Contains("@"))
        {
            throw new Exception("Invalid email.");
        }

        if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
        {
            throw new Exception("Cannot add clients on Sundays.");
        }

        var lines = File.ReadAllLines(filePath);
        foreach (var line in lines)
        {
            var parts = line.Split(',');
            if (parts.Length != 2)
            {
                continue;
            }

            var existingName = parts[0];
            var existingEmail = parts[1];

            if (existingName == name && existingEmail == email)
            {
                throw new Exception("Client already exists.");
            }
        }

        using (var writer = new StreamWriter(filePath, true))
        {
            writer.WriteLine($"{name},{email}");
        }
    }
}