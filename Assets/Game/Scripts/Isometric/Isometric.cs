using UnityEngine;
using System.Collections;

/// <summary>
/// Class for isometric transitions
/// Author Marvin Neurath
/// version 21.11.2014
/// </summary>
public class Isometric
{

    public static Vector3 NORTH = new Vector3(1, .5f, 0);
    public static Vector3 WEST = new Vector3(-1, .5f, 0);
    public static Vector3 SOUTH = new Vector3(-1, -.5f, 0);
    public static Vector3 EAST = new Vector3(1, -.5f, 0);

    /// <summary>
    /// Converts a 2d view(e.g. top-view) to isometric view 
    /// </summary>
    /// <param name="pt">input vector</param>
    /// <returns>transformed vector</returns>
    public static Vector3 twoDToIso(Vector3 pt)
    {
        var tempPt = new Vector3(0, 0);
        tempPt.x = (pt.x - pt.y);
        tempPt.y = (pt.x + pt.y) / 2;
        tempPt.y += pt.z / 2;
        return tempPt;
    }
    /// <summary>
    /// overload method
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static Vector3 twoDToIso(int x, int y, int z)
    {
        var tempPt = new Vector3(0, 0);
        tempPt.x = (float)(x - y);
        tempPt.y = (float)(x + y) / 2;
        tempPt.y += (float)z / 2;
        return tempPt;
    }

    /// <summary>
    /// Converts a vector from isometric view into a 2d view
    /// </summary>
    /// <param name="pt"></param>
    /// <returns></returns>
    public static Vector3 isoTo2D(Vector3 pt)
    {
        var tempPt = new Vector3(0, 0);
        //tempPt.x = (pt.x - pt.y);
        tempPt.x = (2 * pt.y + pt.x) / 2;
        tempPt.y = (2 * pt.y - pt.x) / 2;
        return tempPt;
    }

    /// <summary>
    /// overload method
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    internal static Vector3 isoTo2D(int x, int y)
    {
        var tempPt = new Vector2(0, 0);
        tempPt.x = (float)(2 * y + x) / 2;
        tempPt.y = (float)(2 * y - x) / 2;
        return tempPt;
    }
}