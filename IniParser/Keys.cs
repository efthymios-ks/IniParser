using System.Linq;

namespace SharpUtilities
{
    public partial class IniParser
    {
        /// <summary>
        /// Check if key exists in any section available. 
        /// </summary>
        public bool KeyExists(string Key)
        {
            return Items.Any(i => i.Key.Matches(Key));
        }

        /// <summary>
        /// Check if key exists. 
        /// </summary>
        public bool KeyExists(string Section, string Key)
        {
            return Items.Any(i => (i.Section.Matches(Section) && i.Key.Matches(Key)));
        }

        /// <summary>
        /// Delete matching keys in any section. 
        /// </summary>
        public void DeleteKeys(string Key)
        {
            _items.RemoveAll(i => i.Key.Matches(Key));
        }

        /// <summary>
        /// Delete key. 
        /// </summary>
        public void DeleteKey(string Section, string Key)
        {
            _items.RemoveAll(i => (i.Section.Matches(Section) && i.Key.Matches(Key)));
        }

    }
}
