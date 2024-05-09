using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SpeedrunTools
{
    internal static class SpeedrunTools
    {
        /// <summary>
        /// Features with an associated <see cref="KeyCodes"/> value for input.
        /// </summary>
        internal enum Tools { GodMode }

        /// <summary>
        /// Holds <see cref="KeyCode"/> for features that have an input key.
        /// </summary>
        internal static Dictionary<Tools, KeyCode> KeyCodes => new()
        {
            { Tools.GodMode, KeyCode.F1 }
        };

        /// <summary>
        /// Whether god mode (invincibility) is currently enabled.
        /// </summary>
        internal static bool GodMode { get; set; } = true;

    } // end class SpeedrunTools

} // end namespace
