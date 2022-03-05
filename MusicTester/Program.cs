using System.Diagnostics;
string aDirectory = "C:\\Users\\nickl\\Music\\Distance Music\\";
string[] file = Directory.GetFiles(aDirectory);
int total = file.Length;
for (int i = 0; i < total; i++)
{
    file[i] = $"\"{file[i]}\"";
    Console.WriteLine(file[i]);
    var open = new Process();
    open.StartInfo.FileName = "ffplay";
    open.StartInfo.Arguments = file[i];
    open.Start();
    open.WaitForExit();
    Console.WriteLine("Delete file?");
    string delete = Console.ReadLine();
    if (delete == "y")
    {
        File.Delete(file[i].Trim('"'));
    }
}
Console.ReadKey();