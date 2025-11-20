var notesFile = "notes.txt";
var argsList = args.ToList();
var command = argsList.FirstOrDefault();

switch (command)
{
    case "add":
        if (argsList.Count < 2)
        {
            Console.WriteLine("Usage: dotnet run QuickNotes.cs add <note>");
            return;
        }
        var noteText = string.Join(" ", argsList.Skip(1));
        var notes = LoadNotes();
        var newId = notes.Any() ? notes.Max(n => n.Id) + 1 : 1;
        var newNote = new Note(newId, noteText, DateTime.Now);
        notes.Add(newNote);
        SaveNotes(notes);
        Console.WriteLine($"Note added: {noteText}");
        break;

    case "list":
        var currentNotes = LoadNotes();
        if (!currentNotes.Any())
        {
            Console.WriteLine("No notes found.");
        }
        else
        {
            Console.WriteLine("Your Notes:");
            Console.WriteLine("ID  | Time             | Content");
            Console.WriteLine("----|------------------|-------------------");
            foreach (var note in currentNotes)
            {
                Console.WriteLine($"{note.Id,-3} | {note.Timestamp:g} | {note.Content}");
            }
        }
        break;

    case "remove":
        if (argsList.Count < 2 || !int.TryParse(argsList[1], out int id))
        {
            Console.WriteLine("Usage: dotnet run QuickNotes.cs remove <id>");
            return;
        }
        var notesToRemove = LoadNotes();
        var noteToRemove = notesToRemove.FirstOrDefault(n => n.Id == id);
        if (noteToRemove != null)
        {
            notesToRemove.Remove(noteToRemove);
            SaveNotes(notesToRemove);
            Console.WriteLine($"Note {id} removed.");
        }
        else
        {
            Console.WriteLine($"Note {id} not found.");
        }
        break;

    case "clear":
        SaveNotes(new List<Note>());
        Console.WriteLine("All notes cleared.");
        break;

    case "help":
    default:
        Console.WriteLine("QuickNotes CLI");
        Console.WriteLine("--------------");
        Console.WriteLine("Usage:");
        Console.WriteLine("  dotnet run QuickNotes.cs add <note>   - Add a new note");
        Console.WriteLine("  dotnet run QuickNotes.cs list         - List all notes");
        Console.WriteLine("  dotnet run QuickNotes.cs remove <id>  - Remove a note by ID");
        Console.WriteLine("  dotnet run QuickNotes.cs clear        - Clear all notes");
        Console.WriteLine("  dotnet run QuickNotes.cs help         - Show this help message");
        break;
}

List<Note> LoadNotes()
{
    if (!File.Exists(notesFile))
    {
        return new List<Note>();
    }
    var lines = File.ReadAllLines(notesFile);
    var notes = new List<Note>();
    foreach (var line in lines)
    {
        var parts = line.Split('|');
        if (parts.Length >= 3 && int.TryParse(parts[0], out int id) && DateTime.TryParse(parts[1], out DateTime ts))
        {
            notes.Add(new Note(id, parts[2], ts));
        }
    }
    return notes;
}

void SaveNotes(List<Note> notes)
{
    var lines = notes.Select(n => $"{n.Id}|{n.Timestamp:O}|{n.Content}");
    File.WriteAllLines(notesFile, lines);
}

record Note(int Id, string Content, DateTime Timestamp);