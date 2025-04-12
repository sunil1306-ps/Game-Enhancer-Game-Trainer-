using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aimguard;
using Guna.UI2.WinForms;
using Memory;

namespace Game_Enhancer
{

    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private nexx32 nexx = new nexx32();
        public static string TaskId;
        private bool muteBeep = false;
        string dllmsg = "Dll Already injected.";
        string funmsg = "May be unsupported PC.";
        private string[] TaskName = { "HD-Player" };

        string[] searchPatterns = new string[]
        {
            "A5 A0 A7 AZ 90 81 FF FF 00 59 90 FF FF FF FF FF 00 00 01 01 01 B3 91",
            //"00 00 00 FF FF 91 23 AD A1 A7 2D 7B 01 00 11 FF FF ?? ?? 1D 3C 1F F2",
            //"01 00 A0 E3 1C 00 85 E5 00 70 94 E5 C0 50 96 E5 00 00 57 E3 01 00 00 1A 00 00 A0 E3 64 7F D7 EB FC 50 87 E5 00 70 94 E5 C4 50 96 E5 00 00 57 E3 01 00 00 1A 00 00 A0 E3 5D 7F D7 EB 00 51 87 E5 00 70 94 E5 C8 50 96 E5 00 00 57 E3 01 00 00 1A 00 00 A0 E3 56 7F D7 EB 94 03 9F E5 34 51 87 E5 00 00 9F E7 00 00 90 E5 BF 10 D0 E5 02 00 11 E3 06 00 00 0A 70 10 90 E5 00 00 51 E3 03 00 00 1A 7F 24 D7 EB 6C 03 9F E5 00 00 9F E7 00 00 90 E5 5C 00 90 E5 E9 0F D0 E5 00 00 50 E3 49 00 00 0A 54 03 9F E5 00 00 9F E7 00 00 90 E5 BF 10 D0 E5 02 00 11 E3 03 00 00 0A 70 10 90 E5 00 00 51 E3 00 00 00 1A 6E 24 D7 EB 00 00 A0 E3 29 5E 03 EB 00 50 A0 E1 00 00 55 E3 01 00 00 1A 00 00 A0 E3 33 7F D7 EB 05 00 A0 E1 00 10 A0 E3 6C 5E 03 EB 00 50 94 E5 00 70 A0 E1 00 00 55 E3 01 00 00 1A 00 00 A0 E3 2A 7F D7 EB 58 50 95 E5 00 00 55 E3 01 00 00 1A 00 00 A0 E3 25 7F D7 EB 00 00 95 E5 BA 10 D0 E5 00 00 51 E3 BF 10 D0 15 40 00 11 13 01 00 00 0A EE 97 D6 EB 00 00 95 E5 D8 20 90 E5 DC 10 90 E5 05 00 A0 E1 32 FF 2F E1 00 50 A0 E1 A8 02 9F E5 00 00 9F E7 00 00 90 E5 BF 10 D0 E5 02 00 11 E3 03 00 00 0A 70 10 90 E5 00 00 51 E3 00 00 00 1A 42 24 D7 EB 07 00 A0 E1 05 10 A0 E1 00 20 A0 E3 D5 41 03 EB 00 50 A0 E1 70 02 9F E5 00 00 9F E7 00 00 90 E5 BF 10 D0 E5 02 00 11 E3 03 00 00 0A 70 10 90 E5 00 00 51 E3 00 00 00 1A 33 24 D7 EB 05 00 A0 E1 00 10 A0 E3 9B 5E 03 EB 00 70 94 E5 13 00 00 EA 00 70 94 E5 00 00 57 E3 01 00 00 1A 00 00 A0 E3 F5 7E D7 EB 38 00 87 E2 00 10 A0 E3 33 3E 03 EB 00 50 A0 E1 14 02 9F E5 00 00 9F E7 00 00 90 E5 BF 10 D0 E5 02 00 11 E3 03 00 00 0A 70 10 90 E5 00 00 51 E3 00 00 00 1A 1B 24 D7 EB 05 00 A0 E1 00 10 A0 E3 86 5E 03 EB 00 00 57 E3 5D 00 00 0A DC 11 9F E5 28 01 87 E5 00 70 94 E5 01 10 9F E7 00 00 91 E5 BF 10 D0 E5 02 00 11 E3 03 00 00 0A 70 10 90 E5 00 00 51 E3 00 00 00 1A 0A 24 D7 EB 00 00 A0 E3 E3 5D 03 EB 00 50 A0 E1 00 00 55 E3 01 00 00 1A 00 00 A0 E3 CF 7E D7 EB 05 00 A0 E1 00 10 A0 E3 EA 5D 03 EB 00 50 A0 E1 00 00 57 E3 01 00 00 1A 00 00 A0 E3 C7 7E D7 EB 24 51 87 E5 00 70 94 E5 20 50 98 E5 00 00 57 E3 01 00 00 1A 00 00 A0 E3 C0 7E D7 EB 2C 51 87 E5 00 70 94 E5 24 50 98 E5 00 00 57 E3 01 00 00 1A 00 00 A0 E3 B9 7E D7 EB 08 00 A0 E1 30 51 87 E5 B9 65 00 EB 00 50 A0 E1 00 00 55 E3 0E 00 00 1A 24 01 9F E5 00 00 9F E7 00 00 90 E5 BF 10 D0 E5 02 00 11 E3 03 00 00 0A 70 10 90 E5 00 00 51 E3 00 00 00 1A DD 23 D7 EB 00 00 A0 E3 4C 5E 03 EB 00 50 A0 E1 00 00 55 E3 05 00 00 0A 00 70 94 E5 00 00 57 E3 01 00 00 1A 00 00 A0 E3 9F 7E D7 EB 3C 51 87 E5 00 70 94 E5 CC 50 96 E5 00 00 57 E3",
            //"00 48 2D E9 0D B0 A0 E1 60 D0 4D E2 11 00 4B E2",
            //"E3 1C 00 85 E5 00 70 94 E5 C0 50 96 E5 00 00 57 E3 01 00 00 1A 00 00 A0 E3 64 7F D7 EB FC 50 87 E5 00 70 94 E5 C4 50 96 E5 00 00 57 E3",
            //"0A 70 10 90 E5 00 00 51 E3 00 00 00 1A DD 23 D7 EB 00 00 A0 E3 4C 5E 03 EB 00 50 A0 E1 00 00 55 E3 05 00 00 0A 00 70 94 E5 00 00 57 E3 01 00 00 1A 00 00 A0 E3 9F 7E D7 EB 3C 51"


        };

        string[] replacePatterns = new string[]
        {
            "FF 00 00 A0 E3 1E FF 2F E1",
            //"FF 00 00 A0 E3 1E FF 2F E1",
            //"01 00 A0 E3 1C 00 85 E5 00 70 94 E5 C0 50 96 E5 00 00 57 E3 01 00 00 1A 00 00 A0 E3 64 7F D7 EB FC 50 87 E5 00 70 94 E5 C4 50 96 E5 00 00 57 E3 01 00 00 1A 00 00 A0 E3 5D 7F D7 EB 00 51 87 E5 00 70 94 E5 C8 50 96 E5 00 00 57 E3 01 00 00 1A 00 00 A0 E3 56 7F D7 EB 94 03 9F E5 34 51 87 E5 00 00 9F E7 00 00 90 E5 BF 10 D0 E5 02 00 11 E3 06 00 00 0A 70 10 90 E5 00 00 51 E3 03 00 00 1A 7F 24 D7 EB 6C 03 9F E5 00 00 9F E7 00 00 90 E5 5C 00 90 E5 E9 0F D0 E5 00 00 50 E3 49 00 00 0A 54 03 9F E5 00 00 9F E7 00 00 90 E5 BF 10 D0 E5 02 00 11 E3 03 00 00 0A 70 10 90 E5 00 00 51 E3 00 00 00 1A 6E 24 D7 EB 00 00 A0 E3 29 5E 03 EB 00 50 A0 E1 00 00 55 E3 01 00 00 1A 00 00 A0 E3 33 7F D7 EB 05 00 A0 E1 00 10 A0 E3 6C 5E 03 EB 00 50 94 E5 00 70 A0 E1 00 00 55 E3 01 00 00 1A 00 00 A0 E3 2A 7F D7 EB 58 50 95 E5 00 00 55 E3 01 00 00 1A 00 00 A0 E3 25 7F D7 EB 00 00 95 E5 BA 10 D0 E5 00 00 51 E3 BF 10 D0 15 40 00 11 13 01 00 00 0A EE 97 D6 EB 00 00 95 E5 D8 20 90 E5 DC 10 90 E5 05 00 A0 E1 32 FF 2F E1 00 50 A0 E1 A8 02 9F E5 00 00 9F E7 00 00 90 E5 BF 10 D0 E5 02 00 11 E3 03 00 00 0A 70 10 90 E5 00 00 51 E3 00 00 00 1A 42 24 D7 EB 07 00 A0 E1 05 10 A0 E1 00 20 A0 E3 D5 41 03 EB 00 50 A0 E1 70 02 9F E5 00 00 9F E7 00 00 90 E5 BF 10 D0 E5 02 00 11 E3 03 00 00 0A 70 10 90 E5 00 00 51 E3 00 00 00 1A 33 24 D7 EB 05 00 A0 E1 00 10 A0 E3 9B 5E 03 EB 00 70 94 E5 13 00 00 EA 00 70 94 E5 00 00 57 E3 01 00 00 1A 00 00 A0 E3 F5 7E D7 EB 38 00 87 E2 00 10 A0 E3 33 3E 03 EB 00 50 A0 E1 14 02 9F E5 00 00 9F E7 00 00 90 E5 BF 10 D0 E5 02 00 11 E3 03 00 00 0A 70 10 90 E5 00 00 51 E3 00 00 00 1A 1B 24 D7 EB 05 00 A0 E1 00 10 A0 E3 86 5E 03 EB 00 00 57 E3 5D 00 00 0A DC 11 9F E5 28 01 87 E5 00 70 94 E5 01 10 9F E7 00 00 91 E5 BF 10 D0 E5 02 00 11 E3 03 00 00 0A 70 10 90 E5 00 00 51 E3 00 00 00 1A 0A 24 D7 EB 00 00 A0 E3 E3 5D 03 EB 00 50 A0 E1 00 00 55 E3 01 00 00 1A 00 00 A0 E3 CF 7E D7 EB 05 00 A0 E1 00 10 A0 E3 EA 5D 03 EB 00 50 A0 E1 00 00 57 E3 01 00 00 1A 00 00 A0 E3 C7 7E D7 EB 24 51 87 E5 00 70 94 E5 20 50 98 E5 00 00 57 E3 01 00 00 1A 00 00 A0 E3 C0 7E D7 EB 2C 51 87 E5 00 70 94 E5 24 50 98 E5 00 00 57 E3 01 00 00 1A 00 00 A0 E3 B9 7E D7 EB 08 00 A0 E1 30 51 87 E5 B9 65 00 EB 00 50 A0 E1 00 00 55 E3 0E 00 00 1A 24 01 9F E5 00 00 9F E7 00 00 90 E5 BF 10 D0 E5 02 00 11 E3 03 00 00 0A 70 10 90 E5 00 00 51 E3 00 00 00 1A DD 23 D7 EB 00 00 A0 E3 4C 5E 03 EB 00 50 A0 E1 00 00 55 E3 05 00 00 0A 00 70 94 E5 00 00 57 E3 01 00 00 1A 00 00 A0 E3 9F 7E D7 EB 3C 51 87 E5 00 70 94 E5 CC 50 96 E5 00 00 57 E3",
            //"00 00 A0 E3 1E FF 2F E1",
            //"00 00",
            //"0A 70 10 90 E5 00 00 51 E3 00 00 00 1A DD 23 D7 EB 00 00 A0 E3 4C 5E 03 EB 00 50 A0 E1 00 00 55 E3 05 00 00 0A 00 70 94 E5 00 00 57 E3 01 00 00 1A 00 00 A0 E3 9F 7E D7 EB 00 00"

        };

        // Toggle bytes function starts..............

        private async void BypassBtn_Click(object sender, EventArgs e)
        {
            bool activate = true;

            try
            {
                bool success = nexx.getTask(TaskName);
                if (!success)
                {
                    Sta.Text = "Open Emulator First";
                    Sta.ForeColor = Color.Red;
                    return;
                }

                Sta.Text = activate ? "Activating..." : "Deactivating...";
                Sta.ForeColor = Color.Orange;

                if (searchPatterns.Length != replacePatterns.Length)
                {
                    Sta.Text = "Pattern arrays must have the same length!";
                    Sta.ForeColor = Color.Red;
                    return;
                }

                bool operationSuccess = false;
                bool foundAtLeastOnePattern = false;

                for (int i = 0; i < searchPatterns.Length; i++)
                {
                    string searchPattern = searchPatterns[i];
                    string replacePattern = replacePatterns[i];

                    IEnumerable<long> result = await nexx.Trace(activate ? searchPattern : replacePattern);

                    if (result == null || !result.Any())
                    {
                        Console.Beep(300, 300);
                        continue; // Skip to the next pattern instead of failing completely
                    }

                    foundAtLeastOnePattern = true;

                    foreach (long add in result)
                    {
                        bool isSuccess = nexx.SetBytes(add, activate ? replacePattern : searchPattern);
                        if (isSuccess) operationSuccess = true;
                    }
                }

                if (!foundAtLeastOnePattern)
                {
                    Sta.Text = "No matching patterns found!";
                    Sta.ForeColor = Color.Red;
                    Console.Beep(300, 300);
                    return;
                }

                if (operationSuccess)
                {
                    Sta.Text = activate ? "Activated" : "Deactivated";
                    Sta.ForeColor = activate ? Color.Green : Color.Blue;
                    Console.Beep(activate ? 400 : 300, 300);
                }
                else
                {
                    Sta.Text = "Modification failed!";
                    Sta.ForeColor = Color.Red;
                    Console.Beep(300, 300);
                }
            }
            catch (Exception ex)
            {
                Sta.Text = $"Error: {ex.Message}";
                Sta.ForeColor = Color.Red;
                Console.Beep(300, 300);
            }
        }

        // Toggle bytes ends...........

        private void closeBp_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
