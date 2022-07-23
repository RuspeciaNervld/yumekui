using System.Runtime.InteropServices;

public class RusRandomer {
    [DllImport("RusRandom", EntryPoint = "hello", CallingConvention = CallingConvention.Cdecl)]
    public static extern int hello(); // 返回112

    /*  [DllImport("RusRandom", EntryPoint = "randNum", CallingConvention = CallingConvention.Cdecl)]*/
    /// <summary>
    /// 生成一个随机整数
    /// </summary>
    /// <param name="left">整数最小值</param>
    /// <param name="right">整数最大值</param>
    /// <returns>生成的随机整数</returns>
    [DllImport("RusRandom")]
    public static extern int randNum(int left, int right);

}
