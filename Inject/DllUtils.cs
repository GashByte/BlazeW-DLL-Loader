using System.Runtime.InteropServices;

namespace Inject
{
    /// <summary>
    /// Dll工具
    /// </summary>
    internal static class DllUtils
    {
        /// <summary>
        /// 启动信息
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct StartUpInfo
        {
            /// <summary>
            /// cb
            /// </summary>
            public int Cb;

            /// <summary>
            /// LpReserved
            /// </summary>
            public string LpReserved;

            /// <summary>
            /// LpDesktop
            /// </summary>
            public string LpDesktop;

            /// <summary>
            /// Lptitle
            /// </summary>
            public string LpTitle;

            /// <summary>
            /// dwx
            /// </summary>
            public int DwX;

            /// <summary>
            /// dwy
            /// </summary>
            public int DwY;

            /// <summary>
            /// dwxsize
            /// </summary>
            public int DwXSize;

            /// <summary>
            /// dwysize
            /// </summary>
            public int DwYSize;

            /// <summary>
            /// dwxcountchars
            /// </summary>
            public int DwXCountChars;

            /// <summary>
            /// DwYCountChars
            /// </summary>
            public int DwYCountChars;

            /// <summary>
            /// DwFillAttribute
            /// </summary>
            public int DwFillAttribute;

            /// <summary>
            /// DwFlags
            /// </summary>
            public int DwFlags;

            /// <summary>
            /// WShowWindow
            /// </summary>
            public int WShowWindow;

            /// <summary>
            /// CbReserved2
            /// </summary>
            public int CbReserved2;

            /// <summary>
            /// LpReserved2
            /// </summary>
            public IntPtr LpReserved2;

            /// <summary>
            /// HStdInput
            /// </summary>
            public IntPtr HStdInput;

            /// <summary>
            /// HStdOutput
            /// </summary>
            public IntPtr HStdOutput;

            /// <summary>
            /// HStdError
            /// </summary>
            public IntPtr HStdError;
        }

        /// <summary>
        /// StartUpInfoEx
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct StartUpInfoEx
        {
            /// <summary>
            /// Start up info
            /// </summary>
            public StartUpInfo StartupInfo;

            /// <summary>
            /// LpAttributeList
            /// </summary>
            public IntPtr LpAttributeList;
        }

        /// <summary>
        /// Process_Infomation
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct Process_Infomation
        {
            /// <summary>
            /// HProcess
            /// </summary>
            public IntPtr HProcess;

            /// <summary>
            /// HThread
            /// </summary>
            public IntPtr HThread;

            /// <summary>
            /// DwProcessId
            /// </summary>
            public uint DwProcessId;

            /// <summary>
            /// DwThreadId
            /// </summary>
            public uint DwThreadId;
        }

        /// <summary>
        /// 获得模块id
        /// </summary>
        /// <param name="lpModuleName">模块名称</param>
        /// <returns>模块id</returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        /// <summary>
        /// 打开模块线程
        /// </summary>
        /// <param name="dwDesiredAccess">模块地址</param>
        /// <param name="bInheritHandle">句柄</param>
        /// <param name="dwProcessId">线程ID</param>
        /// <returns>线程id</returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, uint dwProcessId);

        /// <summary>
        /// 获得线程地址
        /// </summary>
        /// <param name="hModule">模块</param>
        /// <param name="procName">线程名称</param>
        /// <returns>线程地址</returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        /// <summary>
        /// 获得错误
        /// </summary>
        /// <param name="hThread">线程</param>
        /// <returns>错误</returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern uint ResumeThread(IntPtr hThread);

        /// <summary>
        /// 虚函数
        /// </summary>
        /// <param name="hProcess">线程</param>
        /// <param name="lpAddress">地址</param>
        /// <param name="dwSize">Data size</param>
        /// <param name="flAllocationType">Alloction type</param>
        /// <param name="flProtect">protect</param>
        /// <returns>intptr函数</returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

        /// <summary>
        /// 虚函数
        /// </summary>
        /// <param name="hProcess">线程</param>
        /// <param name="lpAddress">地址</param>
        /// <param name="dwSize">data size</param>
        /// <param name="dwFreeType">free type</param>
        /// <returns>是否free</returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint dwFreeType);

        /// <summary>
        /// 写入线程内存
        /// </summary>
        /// <param name="hProcess">线程</param>
        /// <param name="lpBaseAddress">地址</param>
        /// <param name="lpBuffer">IO流量</param>
        /// <param name="nSize">大小</param>
        /// <param name="lpNumberOfBytesWritten">写入字节</param>
        /// <returns>是否写入成功</returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out int lpNumberOfBytesWritten);

        /// <summary>
        /// 创建远程线程
        /// </summary>
        /// <param name="hProcess">线程</param>
        /// <param name="lpThreadAttributes">线程属性</param>
        /// <param name="dwStackSize">什么大小</param>
        /// <param name="lpStartAddress">地址</param>
        /// <param name="lpParameter">parameter</param>
        /// <param name="dwCreationFlags">不知道</param>
        /// <param name="lpThreadId">线程id</param>
        /// <returns>线程地址</returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

        /// <summary>
        /// 等待什么东西
        /// </summary>
        /// <param name="hProcess">线程</param>
        /// <param name="dwMilliseconds">等待时间</param>
        /// <returns>等待是否成功？</returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr WaitForSingleObject(IntPtr hProcess, uint dwMilliseconds);

        /// <summary>
        /// 关闭句柄
        /// </summary>
        /// <param name="handle">句柄</param>
        /// <returns>是否成功</returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool CloseHandle(IntPtr handle);

        /// <summary>
        /// 获得上次错误
        /// </summary>
        /// <returns>错误</returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern uint GetLastError();

        /// <summary>
        /// 获得当前线程
        /// </summary>
        /// <returns>线程地址</returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetCurrentProcess();

        /// <summary>
        /// 打开线程的token
        /// </summary>
        /// <param name="processHandle">线程句柄</param>
        /// <param name="desiredAccess">不知道啥</param>
        /// <param name="tokenHandle">token句柄</param>
        /// <returns>是否成功</returns>
        [DllImport("advapi32.dll")]
        public static extern bool OpenProcessToken(IntPtr processHandle, uint desiredAccess, out IntPtr tokenHandle);

        /// <summary>
        /// 加载线程属性
        /// </summary>
        /// <param name="lpAttributeList">一个list</param>
        /// <param name="dwAttributeCount">list的数量</param>
        /// <param name="dwFlags">不知道</param>
        /// <param name="lpSize">什么的大小</param>
        /// <returns>是否加载成功</returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool InitializeProcThreadAttributeList(IntPtr lpAttributeList, uint dwAttributeCount, uint dwFlags, ref IntPtr lpSize);

        /// <summary>
        /// 也不知道
        /// </summary>
        /// <param name="lpAttributeList">也不知道啊</param>
        /// <param name="dwFlags">还是不知道</param>
        /// <param name="attribute">属性</param>
        /// <param name="lpValue">值</param>
        /// <param name="cbSize">cb的大小</param>
        /// <param name="lpPreviousValue">又是一个值</param>
        /// <param name="lpReturnSize">阿哲 不知道啥</param>
        /// <returns>不知道会返回什么</returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UpdateProcThreadAttribute(IntPtr lpAttributeList, uint dwFlags, IntPtr attribute, IntPtr lpValue, IntPtr cbSize, IntPtr lpPreviousValue, IntPtr lpReturnSize);

        /// <summary>
        /// 着什么不知道
        /// </summary>
        /// <param name="lpAttributeList">不知道</param>
        /// <returns>不知道啊</returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteProcThreadAttributeList(IntPtr lpAttributeList);

        /// <summary>
        /// 以成员模式创建线程
        /// </summary>
        /// <param name="hToken">一个token</param>
        /// <param name="lpApplicationName">一个名称</param>
        /// <param name="lpCommandLine">启动命令</param>
        /// <param name="lpProcessAttributes">线程属性1</param>
        /// <param name="lpThreadAttributes">线程属性2</param>
        /// <param name="bInheritHandle">句柄</param>
        /// <param name="dwCreationFlags">这什么</param>
        /// <param name="lpEnvironment">环境</param>
        /// <param name="lpCurrentDirectory">绝对路径</param>
        /// <param name="lpStartupInfo">启动任务</param>
        /// <param name="lpProcessInformation">线程信息</param>
        /// <returns>是否创建成功</returns>
        [DllImport("advapi32.dll", EntryPoint = "CreateProcessAsUser", SetLastError = true, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern bool CreateProcessAsUser(IntPtr hToken, string lpApplicationName, string lpCommandLine, IntPtr lpProcessAttributes, IntPtr lpThreadAttributes, bool bInheritHandle, uint dwCreationFlags, IntPtr lpEnvironment, string lpCurrentDirectory, ref StartUpInfo lpStartupInfo, out Process_Infomation lpProcessInformation);

        /// <summary>
        /// 阿哲我也不知道是什么
        /// </summary>
        /// <param name="hProc">线程</param>
        /// <param name="pDllPath">dll位置</param>
        /// <param name="chars">chars？</param>
        /// <param name="v">指针</param>
        /// <returns>任务函数</returns>
        /// <exception cref="NotImplementedException">未完成函数</exception>
        internal static bool WriteProcessMemory(IntPtr hProc, IntPtr pDllPath, char[] chars, uint v)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 我还是不知道是啥
        /// </summary>
        /// <param name="hProc">线程</param>
        /// <param name="pDllPath">Dll路径</param>
        /// <param name="value">什么值</param>
        /// <param name="v">指针</param>
        /// <returns>任务函数</returns>
        /// <exception cref="NotImplementedException">未完成函数</exception>
        internal static bool WriteProcessMemory(IntPtr hProc, IntPtr pDllPath, object value, uint v)
        {
            throw new NotImplementedException();
        }
    }
}
