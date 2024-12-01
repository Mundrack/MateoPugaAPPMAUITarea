using MateoPugaAPPMAUI.Models;
using System.Collections.ObjectModel;

namespace MateoPugaAPPMAUI.Models;

internal class MPAllNotes
{
    public ObservableCollection<MPNote> MateoPugaAPPMAUI { get; set; } = new ObservableCollection<MPNote>();

    public MPAllNotes() =>
        LoadMateoPugaAPPMAUI();

    public void LoadMateoPugaAPPMAUI()
    {
        MateoPugaAPPMAUI.Clear();

        // Get the folder where the notes are stored.
        string appDataPath = FileSystem.AppDataDirectory;

        // Use Linq extensions to load the *.notes.txt files.
        IEnumerable<MPNote> notes = Directory

                                    // Select the file names from the directory
                                    .EnumerateFiles(appDataPath, "*.notes.txt")

                                    // Each file name is used to create a new Note
                                    .Select(filename => new MPNote()
                                    {
                                        Filename = filename,
                                        Text = File.ReadAllText(filename),
                                        Date = File.GetLastWriteTime(filename)
                                    })

                                    // With the final collection of notes, order them by date
                                    .OrderBy(note => note.Date);

        // Add each note into the ObservableCollection
        foreach (MPNote note in notes)
            MateoPugaAPPMAUI.Add(note);
    }

    internal void LoadNotes()
    {
        throw new NotImplementedException();
    }
}