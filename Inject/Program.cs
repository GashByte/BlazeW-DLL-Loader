using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Inject
{
    /// <summary>
    /// 一个适用于 Unity 游戏的 DLL 注入器
    /// </summary>
    internal class Program
    {
        public static string? TargetPath;
        public static string? objective;
        public static string configPath = $@"{Environment.CurrentDirectory}\config.ini";

        private static bool AutoReadConfig;

        /// <summary>
        /// main
        /// </summary>
        /// <param name="args">args</param>
        static void Main(string[]? args)
        {
            Console.Title = "BlazeW Dll Injector";
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("欢迎使用 BlazeW Dll Injector!");
            Console.WriteLine("这是一款免费且开源的 DLL 注入器");
            Console.WriteLine("此软件用于将一些打包好的 DLL 注入到 我团队制作的 Unity Game 中");
            Console.WriteLine("仅供学习！请不要将此软件用于非法用途！");
            Console.WriteLine("开源地址：https://");
            Console.WriteLine("----------------------------------------------------------------\n");

            string AutoReadConfig_temp = ReadConfig("AutoReadConfig", "Read");
            if (string.IsNullOrWhiteSpace(AutoReadConfig_temp)) AutoReadConfig = false;
            else AutoReadConfig = Convert.ToBoolean(AutoReadConfig_temp);

            if (!AutoReadConfig)
            {
            InputTargetPath:
                Console.WriteLine("输入Target路径");
                Console.WriteLine("Path: ");
                var targetPath = Console.ReadLine()!;
                Console.WriteLine("确定是这个路径吗？(y/n)");
                var verifytargetpath = Console.ReadLine()!;
                if (verifytargetpath == "y" || verifytargetpath == "Y")
                {
                    if (string.IsNullOrWhiteSpace(targetPath))
                    {
                        Console.WriteLine("你输入了一个空的 Traget 路径. 是否重新来过？(y/n)");
                        if (Console.ReadLine() == "y" || Console.ReadLine() == "Y") goto InputTargetPath;
                        else Environment.Exit(-1);
                    }
                    else TargetPath = targetPath;
                }
                else goto InputTargetPath;

            InputDllPath:
                Console.WriteLine("输入Dll名称 将会调用同级目录下的dll文件 如果您不想用同级目录的dll 按下回车后输入对应的路径");
                Console.WriteLine("Path: ");

                var target = Console.ReadLine()!;

                Console.WriteLine("确定是这个路径吗？(y/n)");
                var verifydllpath = Console.ReadLine()!;
                if (verifydllpath == "y" || verifydllpath == "Y")
                {
                    if (string.IsNullOrWhiteSpace(target))
                    {
                        Console.WriteLine("输入Dll路径");
                        Console.WriteLine("Path: ");
                        objective = $@"{Environment.CurrentDirectory}\{Console.ReadLine()!}";
                    }
                    else objective = $@"{Environment.CurrentDirectory}\{target}";

                    if (string.IsNullOrWhiteSpace(objective))
                    {
                        Console.WriteLine("你输入了一个空的 Dll 路径. 是否重新来过？(y/n)");
                        if (Console.ReadLine() == "y" || Console.ReadLine() == "Y") goto InputDllPath;
                        else Environment.Exit(-1);
                    }
                }
                else goto InputDllPath;

                Console.WriteLine("是否自动读取？(y/n)");
                if (Console.ReadLine()! == "y")
                {
                    if (!File.Exists(configPath)) File.Create(configPath).Close();

                    SaveConfig(true, TargetPath, objective);
                }
                else SaveConfig(false, TargetPath, objective);
            }
            else ReadConfig();

            Console.WriteLine($"正在注入...\n目标文件：{Path.GetFileName(TargetPath)}\n注入文件{Path.GetFileName(objective)}");

            Inject();

            Thread.Sleep(TimeSpan.FromSeconds(30));
            Environment.Exit(0);
        }

        /// <summary>
        /// 读取配置
        /// </summary>
        static void ReadConfig()
        {
            if(ReadConfig("Settings", "TargetPath") == ReadConfig("Settings", "Objective"))
            {
                File.Delete(configPath);
                Main(null);
            }

            if (string.IsNullOrWhiteSpace(TargetPath = ReadConfig("Settings", "TargetPath")))
            {
            InputTargetPath:
                Console.WriteLine("输入Target路径");
                Console.WriteLine("Path: ");
                var targetPath = Console.ReadLine()!;
                Console.WriteLine("确定是这个路径吗？(y/n)");
                var verifytargetpath = Console.ReadLine()!;
                if (verifytargetpath == "y" || verifytargetpath == "Y")
                {
                    if (string.IsNullOrWhiteSpace(targetPath))
                    {
                        Console.WriteLine("你输入了一个空的 Traget 路径. 是否重新来过？(y/n)");
                        if (Console.ReadLine() == "y" || Console.ReadLine() == "Y") goto InputTargetPath;
                        else Environment.Exit(-1);
                    }
                    else TargetPath = targetPath;
                }
                else goto InputTargetPath;
            }

            if (string.IsNullOrWhiteSpace(objective = ReadConfig("Settings", "Objective")))
            {
            InputDllPath:
                Console.WriteLine("输入Dll名称 将会调用同级目录下的dll文件 如果您不想用同级目录的dll 按下回车后输入对应的路径");
                Console.WriteLine("Path: ");

                var target = Console.ReadLine()!;

                Console.WriteLine("确定是这个路径吗？(y/n)");
                var verifydllpath = Console.ReadLine()!;
                if (verifydllpath == "y" || verifydllpath == "Y")
                {
                    if (string.IsNullOrWhiteSpace(target))
                    {
                        Console.WriteLine("输入Dll路径");
                        Console.WriteLine("Path: ");
                        objective = $@"{Environment.CurrentDirectory}\{Console.ReadLine()!}";
                    }
                    else objective = $@"{Environment.CurrentDirectory}\{target}";

                    if (string.IsNullOrWhiteSpace(objective))
                    {
                        Console.WriteLine("你输入了一个空的 Dll 路径. 是否重新来过？(y/n)");
                        if (Console.ReadLine() == "y" || Console.ReadLine() == "Y") goto InputDllPath;
                        else Environment.Exit(-1);
                    }
                }
                else goto InputDllPath;
            }

            SaveConfig(true, TargetPath, objective);
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        static void SaveConfig(bool AutoReadConfig_, string? TargetPath_, string? Objective_)
        {
            WriteConfig("AutoReadConfig", "Read", AutoReadConfig_);
            WriteConfig("Settings", "TargetPath", TargetPath_!);
            WriteConfig("Settings", "Objective", Objective_!);
        }

        /// <summary>
        /// 读取配置
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="key">关键字</param>
        /// <returns>配置内容</returns>
        static string ReadConfig(object name, object key)
        {
            if (!File.Exists(configPath)) return string.Empty;
            if (name == null || key == null) return string.Empty;
            return Utils.InIUtils.GetValue(name.ToString()!, key.ToString()!, string.Empty, configPath);
        }

        /// <summary>
        /// 写入配置
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="key">关键字</param>
        /// <param name="value">值</param>
        static void WriteConfig(object name, object key, object value)
        {
            if (!File.Exists(configPath)) File.Create(configPath).Close();
            if (name == null || key == null || value == null) return;
            Utils.InIUtils.SetValue(name.ToString()!, key.ToString()!, value.ToString()!, iniFilePath: configPath);
        }

        /// <summary>
        /// 注入模块
        /// </summary>
        static void Inject()
        {
            if (!File.Exists(objective))
            {
                return;
            }
            bool tokenRet = DllUtils.OpenProcessToken(DllUtils.GetCurrentProcess(), 0xF00FF, out IntPtr hToken);
            DllUtils.StartUpInfoEx si = default;
            si.StartupInfo.Cb = Marshal.SizeOf(si);
            if (hToken == IntPtr.Zero)
            {
                Console.WriteLine("提权失败！提权失败！请尝试以管理员模式打开 本软件 后再试！");
                Console.ReadLine();
                return;
            }
            Console.WriteLine("[>] 检查提权成功\t\t\t\tStep(1/9)");

            Process pExporer = Process.GetProcessesByName("explorer")[0];
            if (pExporer == null)
            {
                Console.WriteLine("未找到Explorer！注入失败！未找到Explorer！");
                Console.ReadLine();
                return;
            }
            Console.WriteLine("[>] Explorer 存在\t\t\t\tStep(2/9)");

            IntPtr handle = DllUtils.OpenProcess(0xF0000 | 0x100000 | 0xFFFF, false, (uint)pExporer.Id);
            nint lpSize = IntPtr.Zero;
            DllUtils.InitializeProcThreadAttributeList(IntPtr.Zero, 1, 0, ref lpSize);
            si.LpAttributeList = Marshal.AllocHGlobal(lpSize);
            DllUtils.InitializeProcThreadAttributeList(si.LpAttributeList, 1, 0, ref lpSize);
            if (DllUtils.UpdateProcThreadAttribute(si.LpAttributeList, 0, (IntPtr)0x00020004, handle, (IntPtr)IntPtr.Size, IntPtr.Zero, IntPtr.Zero))
            {
                Console.WriteLine("线程更新失败！注入失败！线程更新失败！请尝试以管理员模式打开 本软件 后再试!");
                Console.ReadLine();
                return;
            }
            Console.WriteLine("[>] 线程更新 成功\t\t\t\tStep(3/9)");

            DirectoryInfo path = new DirectoryInfo(TargetPath!);
            DllUtils.Process_Infomation pi = default;
            
            // 工作目录
            string TargetDir = $"{path.Parent!}";
            bool result = DllUtils.CreateProcessAsUser(hToken, Path.Combine(TargetPath!).ToString(), ReadConfig("Custom", "args")!, IntPtr.Zero, IntPtr.Zero, false, 0x00080000 | 0x00000004, IntPtr.Zero, TargetDir.ToString(), ref si.StartupInfo, out pi);
            if (!result)
            {
                Console.WriteLine("Target线程暂停失败！注入失败！Target线程暂停失败 请尝试以管理员模式打开 本软件 后再试!");
                Console.ReadLine();
                return;
            }
            Console.WriteLine("[>] 目标线程暂停 成功\t\t\t\tStep(4/9)");

            DllUtils.DeleteProcThreadAttributeList(si.LpAttributeList);
#pragma warning disable VSTHRD101
            new Thread(() =>
            {
                IntPtr hProc = pi.HProcess;

                IntPtr hKernel = DllUtils.GetModuleHandle("kernel32.dll");
                if (hKernel == IntPtr.Zero)
                {
                    Console.WriteLine("kernel32.dll模块地址寻找失败！注入失败！kernel32.dll模块地址寻找失败！");
                    Console.ReadLine();
                    return;
                }
                Console.WriteLine("[>] kernel32模块寻找 成功\t\tStep(5/9)");

                IntPtr pLoadLibrary = DllUtils.GetProcAddress(hKernel, "LoadLibraryA");
                if (pLoadLibrary == IntPtr.Zero)
                {
                    Console.WriteLine("LoadLibraryA模块地址未找到！注入失败！kernel32.dll模块地址寻找失败！");
                    Console.ReadLine();
                    return;
                }
                Console.WriteLine("[>] LoadLibraryA模块寻找 成功\tStep(6/9)");

                IntPtr pDllPath = DllUtils.VirtualAllocEx(hProc, IntPtr.Zero, (uint)((objective.Length + 1) * Marshal.SizeOf(typeof(char))), 0x1000 | 0x2000, 0x4);
                if (pDllPath == IntPtr.Zero)
                {
                    Console.WriteLine("申请内存地址失败！注入失败！申请内存地址失败！");
                    Console.ReadLine();
                    return;
                }
                Console.WriteLine("[>] 内存地址申请 成功\tStep(7/9)");

                bool writeResult = DllUtils.WriteProcessMemory(hProc, pDllPath, Encoding.Default.GetBytes(objective), (uint)((objective.Length + 1) * Marshal.SizeOf(typeof(char))), out _);
                if (!writeResult)
                {
                    Console.WriteLine("写入线程失败！注入失败！写入线程失败！");
                    Console.ReadLine();
                    return;
                }
                Console.WriteLine("[>] 线程写入 成功\tStep(8/9)");

                IntPtr hThread = DllUtils.CreateRemoteThread(hProc, IntPtr.Zero, 0, pLoadLibrary, pDllPath, 0, IntPtr.Zero);
                if (hThread == IntPtr.Zero)
                {
                    Console.WriteLine("创建远程线程失败！注入失败！创建远程线程失败！");
                    Console.ReadLine();
                    DllUtils.VirtualFreeEx(hProc, pDllPath, 0, 0x8000);
                    return;
                }
                Console.WriteLine("[>] 远程线程创建 成功\tStep(9/9)");

                if (DllUtils.WaitForSingleObject(hThread, 2000) == IntPtr.Zero)
                {
                    DllUtils.VirtualFreeEx(hProc, pDllPath, 0, 0x8000);
                }

                DllUtils.CloseHandle(hThread);
                Thread.Sleep(2000);
                DllUtils.ResumeThread(pi.HThread);
                DllUtils.CloseHandle(pi.HProcess);
            }).Start();

            Console.WriteLine("[>] 完成注入... 感谢使用\n5s 后自动退出");
            Thread.Sleep(TimeSpan.FromSeconds(5));
            Environment.Exit(0);
        }
    }

    internal class Utils
    {
        public static class InIUtils
        {
            [DllImport("kernel32")]
            private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
            [DllImport("kernel32")]
            private static extern long GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

            /// <summary>
            /// 读取ini文件
            /// </summary>
            /// <param name="Section">名称</param>
            /// <param name="Key">关键字</param>
            /// <param name="defaultText">默认值</param>
            /// <param name="iniFilePath">ini文件地址</param>
            /// <returns></returns>
            public static string GetValue(string Section, string Key, string defaultText, string iniFilePath)
            {
                if (File.Exists(iniFilePath))
                {
                    StringBuilder temp = new StringBuilder(1024);
                    GetPrivateProfileString(Section, Key, defaultText, temp, 1024, iniFilePath);
                    return temp.ToString();
                }
                else
                {
                    return defaultText;
                }
            }

            /// <summary>
            /// 写入ini文件
            /// </summary>
            /// <param name="Section">名称</param>
            /// <param name="Key">关键字</param>
            /// <param name="defaultText">默认值</param>
            /// <param name="iniFilePath">ini文件地址</param>
            /// <returns></returns>
            public static bool SetValue(string Section, string Key, string Value, string iniFilePath)
            {
                var pat = Path.GetDirectoryName(iniFilePath);
                if (Directory.Exists(pat) == false)
                {
                    Directory.CreateDirectory(pat!);
                }
                if (File.Exists(iniFilePath) == false)
                {
                    File.Create(iniFilePath).Close();
                }
                long OpStation = WritePrivateProfileString(Section, Key, Value, iniFilePath);
                if (OpStation == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}