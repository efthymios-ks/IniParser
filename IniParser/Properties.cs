using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace SharpUtilities
{
    public partial class IniParser
    {
        private string DebuggerDisplay
        {
            get
            {
                int sections = _items.DistinctBy(i => i.Section).Count();
                int keys = _items.Count();
                return String.Format(Culture, "{0} (Sections={1}, Keys={2})", Title, sections, keys);
            }
        }

        /// <summary>
        /// Get ini file title. 
        /// </summary>
        public string Title
        {
            get
            {
                return Path.GetFileNameWithoutExtension(FilePath);
            }
        }
        /// <summary>
        /// File path. 
        /// </summary>
        public string FilePath { get; private set; } = string.Empty;
        /// <summary>
        /// Entries read from file. 
        /// </summary>
        private List<IniData> _items { get; set; } = null;
        public IniData[] Items
        {
            get
            {
                return _items.ToArray();
            }
            private set
            {
                _items = value.ToList();
            }
        }
        /// <summary>
        /// CultureInfo for variables. 
        /// </summary>
        public CultureInfo Culture { get; set; } = CultureInfo.InvariantCulture;
        /// <summary>
        /// Create keys if they do not exist. 
        /// </summary>
        public bool AutoCreateKeys { get; set; } = false;
        /// <summary>
        /// Character to indicate comment line. 
        /// </summary>
        public char CommentCharacter { get; set; } = '#';

        /// <summary>
        /// Section delimiter starts with "===". 
        /// </summary>
        private char SectionSplitterCharacter { get; set; } = '=';
        /// <summary>
        /// Section spliter start "#===". 
        /// </summary>
        private string SectionSplitterStart
        {
            get
            {
                return String.Format("{0}{1}", CommentCharacter, SectionSplitterCharacter);
            }
        }
    }
}
