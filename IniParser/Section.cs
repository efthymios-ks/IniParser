using System.Linq;

namespace SharpUtilities
{
    public partial class IniParser
    {
        /// <summary>
        /// Get sections' name. 
        /// </summary>
        public string[] GetSections()
        {
            return Items
                .Select(i => i.Section)
                .Distinct()
                .OrderBy(i => i)
                .ToArray();
        }

        /// <summary>
        /// Check if exists. 
        /// </summary>
        public bool SectionExists(string Section)
        {
            return Items.Any(i => i.Section.Matches(Section));
        }

        /// <summary>
        /// Rename section. 
        /// </summary>
        public void RenameSection(string OldName, string NewName)
        {
            var items = Items.Where(i => i.Section.Matches(OldName));
            foreach (var item in items)
                item.Section = NewName;
        }

        /// <summary>
        /// Delete section. 
        /// </summary>
        public void DeleteSection(string Section)
        {
            _items.RemoveAll(i => i.Section.Matches(Section));
        }
    }
}
