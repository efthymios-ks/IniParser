using System.Linq;

namespace SharpUtilities
{
    public partial class IniParser
    {
        /// <summary>
        /// Returns FirstOrDefault Key with matching name. 
        /// Does not check Section. Use carefully. 
        /// </summary>
        public IniData this[string Key]
        {
            get
            {
                return Items.FirstOrDefault(i => i.Key.Matches(Key));
            }
        }

        /// <summary>
        /// Returns specific Key. 
        /// If Key is missing and AutoCreateKey = true, then it is created. 
        /// </summary>
        public IniData this[string Section, string Key]
        {
            get
            {
                var item = Items.FirstOrDefault(i => (i.Section.Matches(Section) && i.Key.Matches(Key)));

                //If entry does not exist 
                if (item == null)
                {
                    //If auto create keys 
                    if (AutoCreateKeys)
                    {
                        //Create entry 
                        item = new IniData(Section, Key, "");

                        //Check is section exists
                        int lastIndex = _items.FindLastIndex(i => i.Section.Matches(Section));
                        if (lastIndex < 0)
                            _items.Add(item);
                        else
                            _items.Insert(lastIndex + 1, item);
                    }
                }

                return item;
            }
        }
    }
}
