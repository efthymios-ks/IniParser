using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace SharpUtilities
{
    public partial class IniParser
    {
        /// <summary>
        /// Parse file to list of IniData. 
        /// </summary>
        private IEnumerable<IniData> ParseFile(string FilePath)
        {
            var items = new List<IniData>();

            if (String.IsNullOrWhiteSpace(FilePath))
                return items;
            else if (!File.Exists(FilePath))
                return items;
            else
            {
                //Read lines 
                var lines = File
                            //Read lines 
                            .ReadLines(FilePath)
                            //Empty lines or section brackets are discarded
                            .Where(i => !(String.IsNullOrWhiteSpace(i) || LineIsSectionBracket(i, SectionSplitterStart)))
                            //Trimm
                            .Select(i => i.Trim());

                //Scan lines 
                {
                    string section = string.Empty;
                    string comment = string.Empty;

                    string cc = CommentCharacter.ToString(); //Convert temporarily for iterations 

                    foreach (var line in lines)
                    {
                        //Section 
                        if (LineIsSection(line))
                        {
                            section = GetSectionName(line);
                        }
                        //Comment 
                        else if (LineIsComment(line, CommentCharacter))
                        {
                            comment = GetCommentContent(line, CommentCharacter);
                        }
                        //Check for data 
                        else if (LineIsVariable(line))
                        {
                            var fields = SplitLineVariableFields(line);
                            string key = fields.Key;
                            object value = fields.Value;

                            //Add item to collection 
                            var item = new IniData(section, comment, key, value);
                            items.Add(item);

                            //Reset values
                            comment = string.Empty;
                        }
                    }
                }

                return items;
            }
        }

        /// <summary>
        /// Check if line is [Section] 
        /// </summary>
        private bool LineIsSection(string Line)
        {
            if (!Line.StartsWith("["))
                return false;
            else if (!Line.EndsWith("]"))
                return false;
            else
                return true;
        }

        /// <summary>
        /// Check if line is section bracket. 
        /// </summary>
        private bool LineIsSectionBracket(string Line, string SectionSplitterStart)
        {
            return Line.StartsWith(SectionSplitterStart);
        }

        /// <summary>
        /// Check if line is #Comment. 
        /// </summary>
        private bool LineIsComment(string Line, char CommentCharacter)
        {
            string _commentCharacter = CommentCharacter.ToString();
            return Line.StartsWith(_commentCharacter);
        }

        /// <summary>
        /// Check if line is variable entry. 
        /// </summary>
        private bool LineIsVariable(string Line)
        {
            return Line.Contains("=");
        }

        /// <summary>
        /// Get [Section] name. 
        /// </summary>
        private string GetSectionName(string Line)
        {
            return Line.TrimStart('[').TrimEnd(']').Trim();
        }

        /// <summary>
        /// Get #Comment content. 
        /// </summary>
        private string GetCommentContent(string Line, char CommentCharacter)
        {
            return Line.TrimStart(CommentCharacter).Trim();
        }

        /// <summary>
        /// Split line variable to fields. 
        /// Key = Value 
        /// </summary>
        private KeyValuePair<string, object> SplitLineVariableFields(string Line)
        {
            string key = string.Empty;
            object value = string.Empty;

            //Get two fields (Key, Value) 
            var fields = Line
                .Split('=')
                .Select(field => field.Trim());

            key = fields.ElementAt(0);
            if (fields.Count() >= 2)
            {
                string tmp = fields.ElementAt(1);
                value = GetRealValue(tmp);
            }

            return (new KeyValuePair<string, object>(key, value));
        }

        /// <summary>
        /// Converts string to real value (int, decimal, bool or string). 
        /// </summary>
        private object GetRealValue(string Value)
        {
            //Null 
            if ((String.IsNullOrWhiteSpace(Value)))
                Value = string.Empty;

            //Integer 
            {
                int result;
                if (int.TryParse(Value, NumberStyles.Any, Culture, out result))
                    return result;
            }

            //Decimal 
            {
                decimal result;
                if (decimal.TryParse(Value, NumberStyles.Any, Culture, out result))
                    return result;
            }

            //Bool 
            {
                if (String.Equals(Value, "true", StringComparison.InvariantCultureIgnoreCase))
                    return true;
                else if (String.Equals(Value, "yes", StringComparison.InvariantCultureIgnoreCase))
                    return true;
                else if (String.Equals(Value, "false", StringComparison.InvariantCultureIgnoreCase))
                    return false;
                else if (String.Equals(Value, "no", StringComparison.InvariantCultureIgnoreCase))
                    return false;
            }

            //String 
            return Value;
        }
    }
}
