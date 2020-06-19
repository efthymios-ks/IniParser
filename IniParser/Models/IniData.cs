using System;
using System.Diagnostics;
using System.Globalization;

namespace SharpUtilities
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public partial class IniData
    {
        private string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture,
                  "[{0}] {1} = {2}",
                  Section, Key, Value);
            }
        }

        //Properties 
        /// <summary>
        /// Variable section. 
        /// </summary>
        public string Section { get; internal set; } = string.Empty;
        /// <summary>
        /// Variable comment. Only one line allowed. 
        /// </summary>
        public string Comment { get; set; } = string.Empty;
        /// <summary>
        /// Variable name. 
        /// </summary>
        public string Key { get; private set; } = string.Empty;
        /// <summary>
        /// Variable value. 
        /// </summary>
        public object Value { get; set; } = null;

        //Constructors
        public IniData(string Section, string Key, object Value) :
            this(Section, string.Empty, Key, Value)
        {

        }

        public IniData(string Section, string Comment, string Key, object Value)
        {
            this.Section = Section;
            this.Comment = Comment;
            this.Key = Key;
            this.Value = Value;
        }

        //Methods
        public override string ToString()
        {
            return DebuggerDisplay;
        }
    }
}
