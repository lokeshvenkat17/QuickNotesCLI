# QuickNotes

A simple, file-based .NET CLI application for managing notes.

## How to Run

Ensure you have the .NET SDK installed.

Run the application using `dotnet run`:

```bash
dotnet run QuickNotes.cs [command] [arguments]
```

## Commands

- `add <note>`: Add a new note.
  ```bash
  dotnet run QuickNotes.cs add "Buy milk"
  ```

- `list`: List all notes.
  ```bash
  dotnet run QuickNotes.cs list
  ```

- `remove <id>`: Remove a note by its ID.
  ```bash
  dotnet run QuickNotes.cs remove 1
  ```

- `clear`: Clear all notes.
  ```bash
  dotnet run QuickNotes.cs clear
  ```

- `help`: Show help message.
  ```bash
  dotnet run QuickNotes.cs help
  ```

## Data

Notes are stored in a local `notes.txt` file.

