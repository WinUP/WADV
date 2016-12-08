﻿namespace WADV.Core.NETCore.Receiver
{
    /// <summary>
    /// Game loop receiver
    /// </summary>
    public interface ILooper
    {
        /// <summary>
        /// Do function of logic loop
        /// </summary>
        /// <param name="frame">Current frame count since game engine was started</param>
        /// <returns>Should this receiver still avaliable in next loop</returns>
        bool Loop(int frame);
    }
}
