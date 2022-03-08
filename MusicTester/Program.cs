using Microsoft.Data.Sqlite;
using System.Diagnostics;

string aDirectory = "C:\\Users\\nickl\\Music\\Distance Music\\";
string[] file = Directory.GetFiles(aDirectory);
int total = file.Length;
for (int i = 0; i < total; i++)
{
    var connection = new SqliteConnection("Data Source=C:\\Users\\nickl\\Music\\Distance Music\\AllowedSongs.db");
    var addpath = connection.CreateCommand();
    var checkpath = connection.CreateCommand();
    addpath.CommandText =
    @"
    INSERT INTO Songs (Path)
    VALUES ($Path)
";
    checkpath.CommandText =
    @"
    SELECT SongID
    FROM Songs
    WHERE Path == $Path
";
    connection.Open();
    file[i] = $"\"{file[i]}\"";
    checkpath.Parameters.AddWithValue("$Path", file[i]);
    var ID = "";
    try
    {
        using var reader = checkpath.ExecuteReader();
        while (reader.Read())
        {
            ID = reader.GetString(0);
        }
    }
    catch (Exception)
    {
        var open = new Process();
        open.StartInfo.FileName = "ffplay";
        open.StartInfo.Arguments = file[i];
        open.Start();
        open.WaitForExit();
        Console.WriteLine("Delete file?");
        string delete = Console.ReadLine();
        if (delete.ToUpper() == "Y")
        {
            File.Delete(file[i].Trim('"'));
        }
        else
        {
            addpath.Parameters.AddWithValue("$Path", file[i]);
            try
            {
                addpath.ExecuteNonQuery();
            }
            catch (Exception)
            {

            }
        }
    }
    if (ID == "")
    {
        var open = new Process();
        open.StartInfo.FileName = "ffplay";
        open.StartInfo.Arguments = file[i];
        open.Start();
        open.WaitForExit();
        Console.WriteLine("Delete file?");
        string delete = Console.ReadLine();
        if (delete.ToUpper() == "Y")
        {
            File.Delete(file[i].Trim('"'));
        }
        else
        {
            addpath.Parameters.AddWithValue("$Path", file[i]);
            try
            {
                addpath.ExecuteNonQuery();
            }
            catch (Exception)
            {

            }
        }
    }
        
}
Console.ReadKey();