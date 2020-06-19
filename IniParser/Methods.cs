using System;
using System.IO;
using System.Text;
using System.Linq;

namespace SharpUtilities
{
    public partial class IniParser
    { 
        /// <summary>
        /// Read data from file. 
        /// </summary>
        public void Load()
        {
            _items = ParseFile(FilePath).ToList();
        }

        /// <summary>
        /// Write data to file. 
        /// </summary>
        public void Flush()
        {
            SaveTo(FilePath);
        }

        /// <summary>
        /// Flushes data to file and reads data back from file. 
        /// </summary>
        public void Refresh()
        {
            Flush();
            Load();
        }

        /// <summary>
        /// Save to specific file path. 
        /// </summary>
        public void SaveTo(string FilePath)
        {
            string content = this.ToString();
            File.WriteAllText(FilePath, content);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            string splitter = string.Empty;
            string section = string.Empty;
            foreach (var entry in _items)
            {
                //Section  
                if (entry.Section != section)
                {
                    //If there was a previous Section 
                    if (!String.IsNullOrWhiteSpace(section))
                    {
                        //Close previous Section 
                        builder.AppendFormatLine(splitter);
                        builder.AppendLine();
                        builder.AppendLine();
                    }

                    //Append new Section 
                    section = entry.Section;
                    splitter = SectionSplitterStart + new String(SectionSplitterCharacter, 2 * section.Length);
                    builder.AppendFormatLine(Culture, "[{0}]", entry.Section);
                    builder.AppendLine(splitter);
                }
                else
                {
                    //Empty line 
                    builder.AppendLine();
                }

                //Comment 
                if (!String.IsNullOrWhiteSpace(entry.Comment))
                    builder.AppendFormatLine("{0} {1} ", CommentCharacter, entry.Comment);

                //Key = Value 
                builder.AppendFormatLine(Culture, "{0} = {1}", entry.Key, entry.Value);
            }

            //Closing lastly openeded Section
            if (splitter != "")
                builder.AppendLine(splitter);

            return builder.ToString();
        }

    }
}
