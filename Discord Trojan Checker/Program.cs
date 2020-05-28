using System;
using System.Linq;
using System.IO;

namespace Discord_Trojan_Checker
{
    class Program
    {
        static void Main()
        {

            Console.WriteLine("This program is made to verify that your Discord or Discord PTB installation hasn't been corrupted by the AnarchyGrabber3 version found the 25/05/2020\n" +
                "You can access the source code of this program on : https://github.com/Zl0gix/Discord-Trojan-Checker");

            Console.WriteLine("\n");
            VerificationProcess("Discord");

            Console.WriteLine("\n");
            VerificationProcess("Discord PTB");

            Console.WriteLine("\n");
            Console.WriteLine("End of program. Press any key to close it");
            Console.ReadKey(true);
        }

        static void VerificationProcess(string appName)
        {
            string appFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + $@"\{appName.Replace(" ", "").ToLower()}";
            if (Directory.Exists(appFolder))
            {
                Console.WriteLine($"{appName} seems to be installed on this computer");

                string[] folders = Directory.GetDirectories(appFolder);
                string[] foldersNames = new string[folders.Length];

                for (int i = 0; i < folders.Length; i++)
                {
                    foldersNames[i] = folders[i].Split('\\').Last();
                }

                string versionFolder = null;
                foreach (string folderName in foldersNames)
                {
                    if (int.TryParse(folderName[0].ToString(), out int temp))
                    {
                        versionFolder = folderName;
                        break;
                    }
                }

                if (versionFolder != null)
                {
                    Console.WriteLine($"{appName}'s version is normally {versionFolder}");
                    string pathToCorruptedFile = appFolder + $@"\{versionFolder}\modules\discord_desktop_core\index.js";
                    if (File.Exists(pathToCorruptedFile))
                    {
                        string[] lines = File.ReadAllLines(pathToCorruptedFile);
                        if (lines.Length > 1)
                        {
                            Console.WriteLine($"[WARNING] Your {appName} installation is probably corrupted, for your safety, uninstall discord and download it again at this link : https://discord.com/download");
                        }
                        else
                        {
                            Console.WriteLine($"Your {appName} installation seems to be clean. Please note that the verification made by the program is made to detect AnarchyGrabber3 version found the 25/05/2020");
                        }
                    }
                    else
                    {
                        Console.WriteLine("The file potentially infected doesn't exist.");
                    }
                }
                else
                {
                    Console.WriteLine($"{appName}'s version folder can't be found.");
                }
            }
            else
            {
                Console.WriteLine($"{appName} isn't installed on this computer.");
            }
        }
    }
}
