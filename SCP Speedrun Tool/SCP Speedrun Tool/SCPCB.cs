using System.Diagnostics;
using System.Globalization;
using System.Windows;
using Memory;

namespace SCP_Speedrun_Tool
{
    public partial class SCPCB
    {
        public static string procName = "SCP - Containment Breach Speedrun Mod";
        public string moduleName;

        public IntPtr BaseAdress;
        public int Achv;
        public int Achievements;
        public int[] SingleAchievements = new int[37];
        public int Cheats;
        public int Cheats1;
        public float FogEnd;
        public int InfStamina;
        public int Noclip;
        public int Godmode;
        public int NoTarget;
        public float Injuries;
        public float Bloodloss;
        public float DeathTimer;
        public int Difficulty;
        public int GameTime;
        public int RunFinished;
        public int ShowMap;
        public int Omni;
        public int RunStart;
        public int Command;

        public int PlayerInfo;
        public int PlayerInfo1;
        public float PlayerPosX;
        public float PlayerPosY;
        public float PlayerPosZ;
        
        public Process[] procrunning;

        public string DebugText = "";

        VAMemory adr;
        public async void Update()
        {
            await Task.Delay(33);
            Update();
        }

        public async void Initialize()
        {
            VAMemory adr1 = new VAMemory(procName);
            
            adr = new VAMemory(procName);
            
            Mem mem = new Mem();
            mem.OpenProcess(procName);
            
            long achievements = (await mem.AoBScan(0x02666666, 0x03555555, "?? ?? ?? ?? 89 1E 8B 1D ?? ?? ?? ?? 8B 75 18 21 F3 21 DB", false, true)).FirstOrDefault();
            
            long cheats = (await mem.AoBScan(0x02666666, 0x03555555, "?? ?? ?? ?? E8 ?? ?? ?? ?? 89 05 ?? ?? ?? ?? E9 ?? ?? ?? ?? BB ?? ?? ?? ??", false, true)).FirstOrDefault();
            
            //string meow = adr.ReadUInt64((IntPtr)achievements).ToString("X").Substring(4).Replace("3503", ""); // gets memory address of the achievements from the code
            Achv = adr1.ReadInt32((IntPtr)achievements);
            Cheats = adr1.ReadInt32((IntPtr)cheats);
            Cheats1 = Cheats - 0x560;
            
            mem = null;
            adr1 = null;
            GC.Collect();
        }

        public void UpdateValues()
        {
            procrunning = Process.GetProcessesByName(procName);
            if (procrunning.Length > 0)
            {
                foreach (Process proc in procrunning)
                {
                    BaseAdress = proc.MainModule.BaseAddress;
                    moduleName = proc.MainModule.ModuleName;
                }

                if (adr == null)
                    Initialize();

                if (BaseAdress != null)
                {
                    Achievements = adr.ReadInt32((IntPtr)Achv + 0x0);
                    FogEnd = adr.ReadFloat((IntPtr)Cheats1 + 0xA34);
                    InfStamina = adr.ReadInt32((IntPtr)Cheats1 + 0x7C4);
                    Noclip = adr.ReadInt32((IntPtr)Cheats1 + 0x204);
                    Godmode = adr.ReadInt32((IntPtr)Cheats1 + 0x200);
                    NoTarget = adr.ReadInt32((IntPtr)Cheats1 + 0xE78);
                    Injuries = adr.ReadFloat((IntPtr)Cheats1 + 0x3C);
                    Bloodloss = adr.ReadFloat((IntPtr)Cheats1 + 0x40);
                    ShowMap = adr.ReadInt32((IntPtr)Cheats1 + 0x604);
                    Omni = adr.ReadInt32((IntPtr)Cheats1 + 0x6AC);
                    DeathTimer = adr.ReadInt32((IntPtr)Cheats1 - 0x58);

                    RunFinished = adr.ReadInt32((IntPtr)Cheats1 + 0x560 + 0x20);
                    GameTime = adr.ReadInt32((IntPtr)Cheats1 + 0x560 + 0x18);
                    RunStart = adr.ReadInt32((IntPtr)Cheats1 + 0x560);
                    Difficulty = adr.ReadByte((IntPtr)Cheats1 + 0x288);
                    Command = adr.ReadByte((IntPtr)Cheats1 + 0x60);

                    DebugText = RunStart.ToString();

                    PlayerInfo = adr.ReadInt32(BaseAdress + 0x000F795C);
                    PlayerInfo1 = adr.ReadInt32((IntPtr)PlayerInfo + 0x18);
                    PlayerPosX = adr.ReadFloat((IntPtr)PlayerInfo1 + 0x40);
                    PlayerPosY = adr.ReadFloat((IntPtr)PlayerInfo1 + 0x44);
                    PlayerPosZ = adr.ReadFloat((IntPtr)PlayerInfo1 + 0x48);

                    int offsetachv = 0x0;
                    for (int i = 0; i < SingleAchievements.Length; i++)
                    {
                        SingleAchievements[i] = adr.ReadInt32((IntPtr)Achievements + offsetachv);
                        offsetachv += 0x4;
                    }
                }
            }
            else
            {
                RunStart = 0;
            }
        }
    }
}
