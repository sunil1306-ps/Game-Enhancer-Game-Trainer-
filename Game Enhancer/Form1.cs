using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Memory;
using System.IO;
using Gma.System.MouseKeyHook;
using Aimguard;


namespace Game_Enhancer
{
    public partial class Form1 : Form
    {

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int SW_HIDE = 0;
        private const int SW_SHOW = 5;

        private NotifyIcon trayIcon;
        private IKeyboardMouseEvents globalHook;

        Mem memory = new Mem();
        private nexx32 nexx = new nexx32();
        public static string TaskId;
        private bool muteBeep = false;
        string dllmsg = "Dll Already injected.";
        string funmsg = "May be unsupported PC.";
        private string[] TaskName = { "HD-Player" };


        Form2 form2 = new Form2();

        private bool isAutorized = true;
        

        private DateTime targetDestructionTime = new DateTime(2028, 2, 10, 23, 59, 59);
        private Timer destructionTimer;

        private string[] scanPatterns = new string[]
        {
            "13 40 00 00 F0 3F 00 00 80 3F 01 00 00 00 00 00 00 00 ?? ?? ?? ?? 00 00 00 00 ?? ?? ?? ?? ?? ?? ?? 00 09 8A",
            "00 60 40 CD CC 8C 3F 8F C2 F5 3C CD CC CC 3D 06 00 00 00 00 00 00 00 00 00 00 00 00 00 F0 41",
            "00 C0 3F 00 00 00 3F 00 00 80 3F 00 00 00 40",
            "00 00 A5 43 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 ?? 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 80 BF"
        };


        public Form1()
        {
            InitializeComponent();
            disableButtons();
            InitializeDestructionTimer();
            HookGlobalHotKeys();
            
            // Hide the form window
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            this.Visible = false;

            // Create the tray icon
            trayIcon = new NotifyIcon
            {
                Icon = IconFromImage("Images/icon.png"),
                Visible = true,
                Text = "FreeFire"
            };
            trayIcon.ContextMenuStrip = CreateContextMenu();
            
        }

        private Icon IconFromImage(string imagePath)
        {
            using (var stream = new System.IO.FileStream(imagePath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
            {
                using (var bitmap = new Bitmap(stream))
                {
                    // Convert the Bitmap to an Icon
                    return Icon.FromHandle(bitmap.GetHicon());
                }
            }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            ShowWindow(this.Handle, SW_HIDE); // Hide the application window
        }

        private void HookGlobalHotKeys()
        {
            // Subscribe to global keyboard events
            globalHook = Hook.GlobalEvents();

            // Register CTRL + A hotkey
            globalHook.KeyDown += OnGlobalKeyDown;

            //HookGlobalMouse();
        }

        private void OnGlobalKeyDown(object sender, KeyEventArgs e)
        {
            // Handle CTRL + A for Activate AimBot
            if (e.Control && e.KeyCode == Keys.A)
            {
                Console.WriteLine("CTRL + A pressed!");
                AimBotBtn.PerformClick(); // Trigger the button click event
            }

            // Handle CTRL + B for Toggle Aim
            else if (e.Control && e.KeyCode == Keys.F)
            {
                Console.WriteLine("CTRL + B pressed!");
                guna2Button1.PerformClick(); // Trigger the AimBot button click event
            }

            // CTRL + 1 for Scope
            else if (e.Control && e.KeyCode == Keys.NumPad1)
            {
                Console.WriteLine("CTRL + B pressed!");
                guna2Button2.PerformClick(); // Trigger the AimBot button click event
            }

            // CTRL + 2 for Switch
            else if (e.Control && e.KeyCode == Keys.NumPad2)
            {
                Console.WriteLine("CTRL + B pressed!");
                guna2Button3.PerformClick(); // Trigger the AimBot button click event
            }

            // CTRL + C for Chams
            else if (e.Control && e.KeyCode == Keys.C)
            {
                Console.WriteLine("CTRL + B pressed!");
                guna2Button4.PerformClick(); // Trigger the AimBot button click event
            }

            // CTRL + 3 for GlitchFire
            else if (e.Control && e.KeyCode == Keys.NumPad3)
            {
                Console.WriteLine("CTRL + B pressed!");
                GlitchFireBtn.PerformClick(); // Trigger the AimBot button click event
            }

            // CTRL + 4 for No Recoil
            else if (e.Control && e.KeyCode == Keys.NumPad4)
            {
                Console.WriteLine("CTRL + B pressed!");
                NoRecoilBtn.PerformClick(); // Trigger the AimBot button click event
            }

            // CTRL + 5 for Scope Track
            else if (e.Control && e.KeyCode == Keys.NumPad5)
            {
                Console.WriteLine("CTRL + B pressed!");
                ScopeTrackBtn.PerformClick(); // Trigger the AimBot button click event
            }

           /* else if (e.Control && e.KeyCode == Keys.B)
            {
                Console.WriteLine("CTRL + B pressed!");
                guna2Button6.PerformClick(); // Trigger the AimBot button click event
            }*/



        }


        private ContextMenuStrip CreateContextMenu()
        {
            var contextMenu = new ContextMenuStrip();

            // Add "Show" option
            contextMenu.Items.Add("Show", null, (s, e) =>
            {
                this.Visible = true;
                this.ShowInTaskbar = true;
                this.WindowState = FormWindowState.Normal;
            });

            // Add "Hide" option
            contextMenu.Items.Add("Hide", null, (s, e) =>
            {
                this.Visible = false;
                this.ShowInTaskbar = false;
                this.WindowState = FormWindowState.Minimized;
            });

            // Add "Exit" option
            contextMenu.Items.Add("Exit", null, (s, e) =>
            {
                trayIcon.Visible = false;
                globalHook?.Dispose();
                globalHook = null;
                Environment.Exit(0);

            });

            return contextMenu;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Prevent the form from being closed, minimize instead
            e.Cancel = true;
            this.Visible = false;
            this.ShowInTaskbar = false;
            this.WindowState = FormWindowState.Minimized;
            globalHook?.Dispose();
            globalHook = null;
            Environment.Exit(0);
        }


        private void guna2Button5_Click(object sender, EventArgs e)
        {
            globalHook?.Dispose();
            globalHook = null;
            Environment.Exit(0);
        }


        private void disableButtons()
        {
            guna2Button1.Enabled = isAutorized;
            guna2Button2.Enabled = isAutorized;
            guna2Button3.Enabled = isAutorized;
            guna2Button4.Enabled = isAutorized;
            guna2Button6.Enabled = isAutorized;
            AimBotBtn.Enabled = isAutorized;
            ScopeTrackBtn.Enabled = isAutorized;
            GlitchFireBtn.Enabled = isAutorized;
            NoRecoilBtn.Enabled = isAutorized;
        }

        Dictionary<long, byte[]> cachedValues = new Dictionary<long, byte[]>();
        Dictionary<long, byte[]> originalValues = new Dictionary<long, byte[]>();
        private async void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                //nexx.OpenProcess(Convert.ToInt32(TaskId));

                if (guna2Button1.Text == "ON") // Turn OFF (Revert Changes)
                {
                    if (originalValues.Count == 0)
                    {
                        Sta.Text = "No Injection Found to Revert";
                        Sta.ForeColor = Color.Red;
                        return;
                    }

                    foreach (var entity in originalValues)
                    {
                        long address = entity.Key;
                        byte[] originalValue = entity.Value;

                        // Restore original value
                        nexx.SetHeadBytes((address + 40).ToString("X"), "int", BitConverter.ToInt32(originalValue, 0).ToString());
                    }

                    Sta.Text = "Aimbot Uninjected Successfully";
                    Sta.ForeColor = Color.Green;
                    guna2Button1.Text = "OFF";
                    Console.Beep(400, 300);
                }
                else // Turn ON (Inject)
                {
                    if (cachedValues.Count == 0)
                    {
                        Sta.Text = "No Aimbot Data Found to Inject";
                        Sta.ForeColor = Color.Red;
                        return;
                    }

                    foreach (var entity in cachedValues)
                    {
                        long address = entity.Key;
                        byte[] modifiedValue = entity.Value;

                        if (modifiedValue == null || modifiedValue.Length < 4)
                        {
                            Sta.Text = $"Invalid data at address {address:X}";
                            Sta.ForeColor = Color.Red;
                            continue; // Skip this iteration instead of crashing
                        }

                        // Convert byte array to int safely
                        int newValue = BitConverter.ToInt32(modifiedValue, 0);

                        // Inject modified value
                        nexx.SetHeadBytes((address + 40).ToString("X"), "int", newValue.ToString());
                    }

                    Sta.Text = "Injected Aimbot Successfully";
                    Sta.ForeColor = Color.Green;
                    guna2Button1.Text = "ON";
                    Console.Beep(300, 300);
                }

                //nexx.CloseProcess();
            }
            catch (Exception ex)
            {
                Sta.Text = ex.Message;
                Sta.ForeColor = Color.Red;
            }
        }


        private async void AimBotBtn_Click(object sender, EventArgs e)
        {
            originalValues.Clear();
            cachedValues.Clear();
            try
            {
                bool success = nexx.getTask(TaskName);
                if (!success)
                {
                    MessageBox.Show("Process not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Int32 proc = Process.GetProcessesByName("HD-Player")[0].Id;
                nexx.OpenProcess(proc);
                nexx.OpenProcess(Convert.ToInt32(TaskId));
                Sta.Text = "Activating...";

                IEnumerable<long> longs = await nexx.Trace(scanPatterns[3]);
                bool k = false;

                if (longs == null || !longs.Any())
                {
                    Console.WriteLine("Only Work Ingame. No Entities Found");
                    Sta.Text = "Failed";
                    if (!muteBeep) Console.Beep(400, 500);
                    return;
                }

                foreach (long num in longs)
                {
                    string address38 = (num + 44).ToString("X");
                    string address42 = (num + 40).ToString("X");

                    // Store original values before modification
                    Byte[] originalBytes = nexx.TraceHead(address38, 4);
                    Byte[] valueBytes = nexx.TraceHead(address42, 4);

                    if (originalBytes != null && valueBytes != null)
                    {
                        originalValues[num] = originalBytes;
                        cachedValues[num] = valueBytes;

                        // Modify memory
                        nexx.SetHeadBytes(address42, "int", BitConverter.ToInt32(originalBytes, 0).ToString());
                        nexx.SetHeadBytes(address38, "int", BitConverter.ToInt32(valueBytes, 0).ToString());
                        k = true;
                    }
                }

                if (k)
                {
                    if (!muteBeep) Console.Beep(400, 500);
                    guna2Button1.Text = "OFF";
                    Sta.Text = "Activated";
                }
                else
                {
                    if (!muteBeep) Console.Beep(400, 500);
                    Sta.Text = "Failed";
                }
            }
            catch (Exception ex)
            {
                Sta.Text = ex.Message;
                Sta.ForeColor = Color.White;
            }
        }

        private async void toggleBytes(string[] searchPatterns, string[] replacePatterns, bool activate)
        {
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


        private void guna2Button2_Click(object sender, EventArgs e)
        {

            String[] searchPatterns = new String[]
            {
                "00 60 40 CD CC 8C 3F 8F C2 F5 3C CD CC CC 3D 06 00 00 00 00 00 00 00 00 00 00 00 00 00 F0 41",
                //"00 00 00 00 41 00 00 00 01 00 00 00 00 00 00 00 00 00 00 00 01 00 00 00 00 00 00 00"
            };

            String[] replacePatterns = new string[]
            {
                "00 60 40 CD CC 8C 3F 8F C2 F5 3C CD CC CC 3D 06 00 00 00 00 00 F0 FF 00 00",
                //"00 00 00 00 41 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 01 00 00 00 00 00 00 00"
            };

            if (guna2Button2.Text == "ON")
            {

                toggleBytes(searchPatterns, replacePatterns, true);
                guna2Button2.Text = "OFF";

            }
            else
            {

                toggleBytes(searchPatterns, replacePatterns, false);
                guna2Button2.Text = "ON";

            }

        }


        private async void guna2Button3_Click(object sender, EventArgs e)
        {
            String[] searchPatterns = new String[]
            {
                "3F 00 00 80 3E 00 00 00 00 04 00 00 00 00 00 80 3F 00 00 20 41 00 00 34 42 01 00 00 00 01 00 00 00 00 00 00 00 00 00 00 00 00 00 80 3F 0A D7 23 3F 9A 99 99 3F 00 00 80 3F 00 00 00 00 00 00 80 3F 00 00 80 3F 00 00 80 3F 00 00 00 00 00 00 00 00 00 00 00 3F 00 00",
                "3F 00 00 80 3E 00 00 00 00 04 00 00 00 00 00 80 3F 00 00 20 41 00 00 34 42 01 00 00 00 01 00 00 00 00 00 00 00 00 00 00 00 00 00 80 3F 8F C2 35 3F 9A 99 99 3F 00 00 80 3F 00 00 00 00 00 00 80 3F 00 00 80 3F 00 00",
                "3F 00 00 80 3E 00 00 00 00 04 00 00 00 00 00 80 3F 00 00 20 41 00 00 34 42 01 00 00 00 01 00 00 00 00 00 00 00 00 00 00 00 00 00 80 3F 9A 99 19 3F CD CC 8C 3F 00 00 80 3F 00 00 00 00 66 66 66 3F 00 00 80 3F 00 00 80 3F 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 80 3F 00 00 80 3F 00 00 80 3F 00 00 00 00 01 00 00 00 0A",
                "00 00 3F 00 00 80 3E 00 00 00 00 06 00 00 00 CD CC 4C 3F 00 00 20 41 00 00 34 42 01 00 00 00 01 00 00 00 00 00 00 00 00 00 00 00 00 00 80 3F 66 66 66 3F 9A 99 99 3F 00 00 80 3F 00 00 00 00 33 33 93 3F 00 00 80 3F 00 00 80 3F 00 00 00 00 00 00 00 00 00 00 80 3F 00 00 00 00 00 00 80 3E 00 00 00 00 00 00 80 3F 00 00 80 3F 00 00 80",
                "42 00 00 70 42 7f 00 00 00 00 00 00 00 00 00 00 3f 00 00 80 3e 00 00 00 00 04 00 00 00 00 00 80 3f 00 00 20 41 00 00 34 42 01"
            };

            String[] replacePatterns = new String[]
            {
                "3F 00 00 80 3E 00 00 00 3D",
                "3F 00 00 80 3E 00 00 00 3D",
                "3C 00 00 80 3C 00 00 00 00 04 00 00 00 00 00 80 3F 00 00 20 41 00 00 34 42 01 00 00 00 01 00 00 00 00 00 00 00 00 00 00 00 00 00 80",
                "00 00 3C 00 00 80 3C 00 00 00 00 06 00 00 00 CD CC 4C 3F 00 00 20 41 00 00 34 42 01 00 00 00 01 00 00 00 00 00 00 00 00 00 00 00 00 00 80 3F 66 66 66 3F",
                "42 00 00 3f 42 3f 00 00 00 00 00 00 00 3f 3f 3f 3b 00 00 29 3d 00 00 00 00 04 00 00 00 00 00 80 3f 00 00 20 41 00 00 34 42 01"
            };
            
            if (guna2Button3.Text == "ON")
            {
                
                 toggleBytes(searchPatterns, replacePatterns, true);
                 guna2Button3.Text = "OFF";

            } else
            {

                toggleBytes(searchPatterns, replacePatterns, false);
                guna2Button3.Text = "ON";

            }

        }

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);
        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        static extern IntPtr GetProcAddress(IntPtr hModule, string procName);
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress,
        uint dwSize, uint flAllocationType, uint flProtect);
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out UIntPtr lpNumberOfBytesWritten);
        [DllImport("kernel32.dll")]
        static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);
        const int PROCESS_CREATE_THREAD = 0x0002;
        const int PROCESS_QUERY_INFORMATION = 0x0400;
        const int PROCESS_VM_OPERATION = 0x0008;
        const int PROCESS_VM_WRITE = 0x0020;
        const int PROCESS_VM_READ = 0x0010;
        const uint MEM_COMMIT = 0x00001000;
        const uint MEM_RESERVE = 0x00002000;
        const uint PAGE_READWRITE = 4;

        private WebClient webclient = new WebClient();

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (Process.GetProcessesByName("HD-Player").Length == 0)
                {
                    Sta.Text = "Open Emulator First";
                    Console.Beep(240, 300);
                }
                else
                {
                    Sta.Text = "Waiting...";
                    string dllName = "Aimbot.Exe Chams.dll";
                    ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                    string adress = "https://cdn.glitch.global/74002823-d235-4cf1-ba34-36967b91f68e/ChamsRed.dll?v=1717190149207";
                    //string adress = "https://cdn.glitch.global/74002823-d235-4cf1-ba34-36967b91f68e/ChamsRed.dll?v=1717190149207";

                    string fileName = $"C:\\Windows\\System32\\{dllName}";
                    bool flag = File.Exists(fileName);

                    if (flag)
                    {
                        File.Delete(fileName);
                    }
                    this.webclient.DownloadFile(adress, fileName);
                    Process targetProcess = Process.GetProcessesByName("HD-Player").FirstOrDefault();
                    if (targetProcess != null)
                    {
                        Sta.Text = "Done";
                        Console.Beep(400, 300);
                    }
                    IntPtr procHandle = OpenProcess(PROCESS_CREATE_THREAD | PROCESS_QUERY_INFORMATION | PROCESS_VM_OPERATION | PROCESS_VM_WRITE | PROCESS_VM_READ, false, targetProcess.Id);
                    IntPtr loadLibraryAddr = GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA");
                    IntPtr allocMemAddress = VirtualAllocEx(procHandle, IntPtr.Zero, (uint)((dllName.Length + 1) * Marshal.SizeOf(typeof(char))), MEM_COMMIT | MEM_RESERVE, PAGE_READWRITE);
                    UIntPtr bytesWritten;
                    WriteProcessMemory(procHandle, allocMemAddress, Encoding.Default.GetBytes(dllName), (uint)((dllName.Length + 1) * Marshal.SizeOf(typeof(char))), out bytesWritten);
                    CreateRemoteThread(procHandle, IntPtr.Zero, 0, loadLibraryAddr, allocMemAddress, 0, IntPtr.Zero);
                }
            }
            catch
            {
                Sta.Text = "Failed";
                Console.Beep(240, 300);
            }
        }


        bool isShowing = false;
        private void guna2Button6_Click(object sender, EventArgs e)
        {
            /* if (isShowing) { 
                 form2.Hide();
                 isShowing = false;
             } else
             {
                 form2.Show();
                 isShowing = true;
             }*/

            String[] searchPatterns = new String[]
            {
                "10 4C 2D E9 08 B0 8D E2 40 D0 4D E2 00 30",
                "10 4C 2D E9 08 B0 8D E2 40 D0 4D E2 0C",
                "10 4C 2D E9 08 B0 8D E2 40 D0 4D E2 0C",
                "10 4C 2D E9 08 B0 8D E2 40 D0 4D E2 E8",
                "10 4C 2D E9 08 B0 8D E2 40 D0 4D E2 E0",
                "30 48 2D E9 08 B0 8D E2 18 D0 4D E2 00 50 A0 E1 84 00 9F E5 01 40 A0 E1 00 00 8F E0 00 00 D0 E5 00 00 50 E3 06 00 00 1A 70 00 9F E5 00 00 9F E7 00 00 90 E5",
                "0A 00 A0 E3 00 10 A0 E3 08 00 85 E5 04 00 A0 E1",
                "0A 00 A0 E3 1C 30 8D E2 24 00 8D E5 04 00 A0 E1",
                "0A 00 A0 E3 28 00 8D E5 28 00 8D E2 04 10 A0 E1",
                "0A 00 A0 E3 B2 01 C4 E1 01 60 9F E7 04 10 A0 E3",
                "0A 00 A0 E3 7C FB FF EB 04 00 8D E2 04 10 A0 E1",
                "0A 00 A0 E3 02 3C A0 E3 00 C0 94 E5 04 C3 8D E5",
                "0A 00 A0 E3 FC B4 FF EB 48 4B 16 E5 04 A0 8B E2",
                "0A 00 A0 E3 AF 8D 8D E2 44 1B 16 E5 04 A0 88 E2",
                "49 44 48 48 42 47 42 4E 48 4D 44 00 49 4C 48 4E",
                "49 44 48 48 42 47 42 4E 48 4D 44 00 49 4C 48 4E",
                "3C 51 87 E5 00 70 94 E5 CC 50 96 E5 00 00 57 E3 01 00",
                "28 01 87 E5 00 70 94 E5 01 10 9F E7 00 00 91 E5 00 00",
                "28 01 87 E5 00 70 94 E5 01 10 9F E7 00 00 91 E5 BF 10",
                "28 01 87 E5 00 70 94 E5 01 10 9F E7 00 00 91 E5 10 B0",
                "1C 00 85 E5 00 70 94 E5 C0 50 96 E5 00 00 57 E3 01 00"
            };

            String[] replacePatterns = new string[]
            {
                "00 00 A0 E3 1E FF 2F E1 40 D0 4D E2 00 30",
                "00 00 A0 E3 1E FF 2F E1 40 D0 4D E2 0C",
                "00 00 A0 E3 1E FF 2F E1 40 D0 4D E2 0C",
                "00 00 A0 E3 1E FF 2F E1 40 D0 4D E2 E8",
                "00 00 A0 E3 1E FF 2F E1 40 D0 4D E2 E0",
                "00 00 A0 E3 1E FF 2F E1 18 D0 4D E2 00 50 A0 E1 84 00 9F E5 01 40 A0 E1 00 00 8F E0 00 00 D0 E5 00 00 50 E3 06 00 00 1A 70 00 9F E5 00 00 9F E7 00 00 90 E5 81",
                "00 F0 20 E3 00 10 A0 E3 08 00 85 E5 04 00 A0 E1",
                "00 F0 20 E3 1C 30 8D E2 24 00 8D E5 04 00 A0 E1",
                "00 F0 20 E3 28 00 8D E5 28 00 8D E2 04 10 A0 E1",
                "00 F0 20 E3 B2 01 C4 E1 01 60 9F E7 04 10 A0 E3",
                "00 F0 20 E3 7C FB FF EB 04 00 8D E2 04 10 A0 E1",
                "00 F0 20 E3 02 3C A0 E3 00 C0 94 E5 04 C3 8D E5",
                "00 F0 20 E3 FC B4 FF EB 48 4B 16 E5 04 A0 8B E2",
                "00 F0 20 E3 AF 8D 8D E2 44 1B 16 E5 04 A0 88 E2",
                "50 4B 45 4A 42 4C 4E 42 41 48 48 00 49 4C 48 4E",
                "50 4B 45 4A 42 4C 4E 42 41 48 48 00 49 4C 48 4E",
                "40 51 87 E5 00 70 94 E5",
                "30 01 87 E5 00 70 94 E5",
                "30 01 87 E5 00 70 94 E5",
                "30 01 87 E5 00 70 94 E5",
                "1D 00 85 E5 00 70 94 E5"
            };

            toggleBytes(searchPatterns, replacePatterns, true);

        }

        bool toggleStateGlitch = false;
        List<long> storedGlitchFireAddresses = new List<long>();
        private async void GlitchFireBtn_Click(object sender, EventArgs e)
        {

            String[] searchPatterns = new String[]
            {
                "00 C0 3F 00 00 00 3F 00 00 80 3F 00 00 00 40"
            };

            String[] replacePatterns = new string[]
            {
                "00 C0 30 00"
            };

            if (GlitchFireBtn.Text == "ON")
            {

                toggleBytes(searchPatterns, replacePatterns, true);
                GlitchFireBtn.Text = "OFF";

            }
            else
            {

                toggleBytes(searchPatterns, replacePatterns, false);
                GlitchFireBtn.Text = "ON";

            }

        }


        bool toggleStateRecoil = false;
        List<long> storedNoRecoilAddresses = new List<long>();
        private async void NoRecoilBtn_Click(object sender, EventArgs e)
        {
            String[] searchPatterns = new String[]
            {
                "00 0A 81 EE 10 0A 10 EE 10 8C BD E8 00 00 7A 44 F0 48 2D E9 10 B0 8D E2 02 8B 2D ED"
            };

            String[] replacePatterns = new string[]
            {
                "00 0A 81 EE 10 0A 10 EE 10 8C BD E8 00 80 89 44"
            };

            if (NoRecoilBtn.Text == "ON")
            {

                toggleBytes(searchPatterns, replacePatterns, true);
                NoRecoilBtn.Text = "OFF";

            }
            else
            {

                toggleBytes(searchPatterns, replacePatterns, false);
                NoRecoilBtn.Text = "ON";

            }
        }


        bool toggleStateTrack = false;
        List<long> storedScopeTrackAddresses = new List<long>();
        private async void ScopeTrackBtn_Click(object sender, EventArgs e)
        {

            String[] searchPatterns = new String[]
            {
                "33 33 13 40 00 00 F0 3F 00 00 80 3F"
            };

            String[] replacePatterns = new string[]
            {
                "FF B1 FF FF 01 01 FF 3F 0F 0F 99 5F"
            };

            if (ScopeTrackBtn.Text == "ON")
            {

                toggleBytes(searchPatterns, replacePatterns, true);
                ScopeTrackBtn.Text = "OFF";

            }
            else
            {

                toggleBytes(searchPatterns, replacePatterns, false);
                ScopeTrackBtn.Text = "ON";

            }

        }


        bool toggleStateReload = false;
        List<long> storedNoReloadAddresses = new List<long>();
        private async void NoReloadBtn_Click(object sender, EventArgs e)
        {
            if (Process.GetProcessesByName("HD-Player").Length == 0)
            {
                Sta.Text = "Open Emulator First";
                Console.Beep(240, 300);
            }
            else
            {
                // Define search and replace patterns
                string searchPattern = "6D 00 00 EB 00 0A B7 EE 10 0A 01 EE 00 0A 31 EE 10 5A 01 EE 00 0A 21 EE 10 0A 10 EE 30 88 BD E8 F0 48 2D E9 10 B0 8D E2 02 8B 2D ED 00 40 A0 E1 74 01 9F E5 00 00 8F E0 00 00 D0 E5 00 00 50 E3 06 00 00 1A 64 01 9F E5 00 00 9F E7 00 00 90 E5 16 3F D4 EB 58 01 9F E5 01 10 A0 E3 00 10 CF E7 71 02 02 E3 00 10 A0 E3 FA 7A 01 EB 01";
                string replacePattern = "FF 02 44 E3 00 0A B7 EE 10 0A";


                Sta.Text = toggleStateReload ? "Reverting Changes . . ." : "Applying Changes . . .";

                memory.OpenProcess("HD-Player");

                if (!toggleStateReload) // Applying changes
                {
                    if (!storedNoReloadAddresses.Any()) // Perform search only if no addresses are stored
                    {
                        Sta.Text = "Scanning...";
                        IEnumerable<long> results = await memory.AoBScan(searchPattern, writable: true);
                        if (results.Any())
                        {
                            storedNoReloadAddresses = results.ToList(); // Store addresses for future use
                        }
                        else
                        {
                            Sta.Text = "Apply Failed: Pattern not found";
                            Console.Beep(240, 300);
                            return;
                        }
                    }

                    // Apply the replace pattern to stored addresses
                    foreach (var address in storedNoReloadAddresses)
                    {
                        memory.WriteMemory(address.ToString("X"), "bytes", replacePattern);
                    }

                    Console.Beep(400, 300);
                    Sta.Text = "Done";
                    toggleStateReload = true;
                }
                else // Reverting changes
                {
                    if (storedNoReloadAddresses.Any())
                    {
                        // Revert the changes at stored addresses
                        foreach (var address in storedNoReloadAddresses)
                        {
                            memory.WriteMemory(address.ToString("X"), "bytes", searchPattern);
                        }

                        Console.Beep(400, 300);
                        Sta.Text = "Reverted";
                        toggleStateReload = false;
                    }
                    else
                    {
                        Sta.Text = "No stored addresses to revert";
                        Console.Beep(240, 300);
                    }
                }
            }
        }


        bool toggleStateVision = false;
        List<long> storedVisionAddresses = new List<long>();
        private async void VisionBtn_Click(object sender, EventArgs e)
        {
            if (Process.GetProcessesByName("HD-Player").Length == 0)
            {
                Sta.Text = "Open Emulator First";
                Console.Beep(240, 300);
            }
            else
            {
                // Define search and replace patterns
                string searchPattern = "DB 0F 49 40 10 2A 00 EE 00 10 80 E5 10 3A 01 EE 14 10 80 E5";
                string replacePattern = "00 00 A9 40 00 00 A0 40 00 00 A0 40 10 3A 99 EE 14 10 80 E5";


                Sta.Text = toggleStateVision ? "Reverting Changes . . ." : "Applying Changes . . .";

                memory.OpenProcess("HD-Player");

                if (!toggleStateVision) // Applying changes
                {
                    if (!storedVisionAddresses.Any()) // Perform search only if no addresses are stored
                    {
                        Sta.Text = "Scanning...";
                        IEnumerable<long> results = await memory.AoBScan(searchPattern, writable: true);
                        if (results.Any())
                        {
                            storedVisionAddresses = results.ToList(); // Store addresses for future use
                        }
                        else
                        {
                            Sta.Text = "Apply Failed: Pattern not found";
                            Console.Beep(240, 300);
                            return;
                        }
                    }

                    // Apply the replace pattern to stored addresses
                    foreach (var address in storedVisionAddresses)
                    {
                        memory.WriteMemory(address.ToString("X"), "bytes", replacePattern);
                    }

                    Console.Beep(400, 300);
                    Sta.Text = "Done";
                    toggleStateVision = true;
                }
                else // Reverting changes
                {
                    if (storedVisionAddresses.Any())
                    {
                        // Revert the changes at stored addresses
                        foreach (var address in storedVisionAddresses)
                        {
                            memory.WriteMemory(address.ToString("X"), "bytes", searchPattern);
                        }

                        Console.Beep(400, 300);
                        Sta.Text = "Reverted";
                        toggleStateVision = false;
                    }
                    else
                    {
                        Sta.Text = "No stored addresses to revert";
                        Console.Beep(240, 300);
                    }
                }
            }
        }

        private void AuthorizeBtn_Click(object sender, EventArgs e)
        {
            /*string password = PassBox.Text;
            switch (password)
            {
                case "aimbot12":
                    AimBotBtn.Enabled = true;
                    guna2Button1.Enabled = true;
                    break;

                case "scope12":
                    guna2Button2.Enabled = true;
                    guna2Button3.Enabled = true;
                    break;

                case "chams12":
                    guna2Button4.Enabled = true;
                    break;

                case "bypass12":
                    guna2Button6.Enabled = true;
                    break;

                case "extra12":
                    GlitchFireBtn.Enabled = true;
                    ScopeTrackBtn.Enabled = true;
                    NoRecoilBtn.Enabled = true;
                    NoReloadBtn.Enabled = true;
                    VisionBtn.Enabled = true;
                    break;

                case "allbtns12": 
                    isAutorized = true;
                    disableButtons();
                    break;

                case "vicky":
                    isAutorized = true;
                    disableButtons();
                    guna2Button6.Enabled = false;
                    break;

                default:
                    Sta.Text = "Invalid code!!!";
                    break;

            }*/
        }

        // Self-destruction method
        private void SelfDestruct()
        {
            string batchFile = Path.Combine(Path.GetTempPath(), "SelfDestruct.bat");
            string exePath = Application.ExecutablePath;

            // Batch script to delete the executable after application closes
            string batchContent = $@"
            @echo off
            timeout /t 2 > nul
            del ""{exePath}"" > nul
            del ""%~f0"" > nul";

            // Write the batch file to the temp directory
            File.WriteAllText(batchFile, batchContent);

            // Start the batch file and close the application
            Process.Start(new ProcessStartInfo
            {
                FileName = batchFile,
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
            });
            globalHook?.Dispose();
            globalHook = null; 
            Environment.Exit(0);
        }

        // Initialize and start the destruction timer
        private void InitializeDestructionTimer()
        {
            destructionTimer = new Timer
            {
                Interval = 1000, // Check every second
                Enabled = true
            };
            destructionTimer.Tick += DestructionTimer_Tick;
        }

        // Check system time and trigger self-destruction
        private void DestructionTimer_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now >= targetDestructionTime)
            {
                destructionTimer.Stop(); // Stop the timer to avoid repeated triggers
                MessageBox.Show(
                    "This application has expired and will now self-destruct.",
                    "Self-Destruct Triggered",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                SelfDestruct();
            }
        }

    }

}
