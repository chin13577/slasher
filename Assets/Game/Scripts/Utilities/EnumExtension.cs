using System;

public static class EnumExtension
{ 
    public static bool HasFlag<T>(this T value, T flag) where T : struct
    {
        long lValue = Convert.ToInt64(value);
        long lFlag = Convert.ToInt64(flag);
        return (lValue & lFlag) != 0;
    }
} 