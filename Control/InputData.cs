using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Control
{
    /// <summary>
    /// The input data.
    /// </summary>
    public struct InputData
    {
        /// <summary>
        /// The modified axes since last update.
        /// </summary>
        public Dictionary<string, float> Axes;

        /// <summary>
        /// The buttons pressed since last update.
        /// </summary>
        public Dictionary<string, bool> Buttons;

        /// <summary>
        /// The keyboard input since last update.
        /// </summary>
        public string InputString;

        /// <summary>
        /// Creates a new input data object from a list of axis and button names.
        /// </summary>
        /// <param name="axisNames"></param>
        /// <param name="buttonNames"></param>
        /// <param name="inputString"></param>
        public InputData(IEnumerable<string> axisNames, IEnumerable<string> buttonNames, string inputString = "")
        {
            Axes = axisNames.ToDictionary(name => name, name => 0f);
            Buttons = buttonNames.ToDictionary(name => name, name => false);
            InputString = inputString;
        }

        /// <summary>
        /// Clears the input data.
        /// </summary>
        public void Clear()
        {
            Axes = Axes.ToDictionary(pair => pair.Key, pair => 0f);
            Buttons = Buttons.ToDictionary(pair => pair.Key, pair => false);
            InputString = string.Empty;
        }

        public override string ToString()
        {
            return string.Format("Axes: {0}\nButtons: {1}\nInputString: {2}",
                string.Join("\n", Axes.Select(pair => string.Format("\t{0}: {1}", pair.Key, pair.Value)).ToArray()), 
                string.Join("\n", Buttons.Select(pair => string.Format("\t{0}: {1}", pair.Key, pair.Value)).ToArray()), 
                InputString
            );
        }
    }
}