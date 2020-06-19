# IniParser (CSharp)
IniParser for .NET applications.  
The parser is **not** case-sensitive.  

###### Constructors: 
- **IniParser**() *(Opens [ApplicationName].ini)*
- **IniParser**(string FilePath) *(Opens files from given filepath)*

###### Properties: 
- string **Title**; *(File name without extension)*
- string **FilePath**; *(Full file path)*
- IniData[] **Items**; *(Keys read from file)*
- CultureInfo **Culture**; *(CultureInfo used for string conversions. Default value: **CultureInfo.InvariantCulture**. Do not set if not sure)*
- char **CommentCharacter**; *(Character indicator for comment lines. Default value: **'#'**)*



###### Indexing: 
- **IniParserInstance["Section", "Key"]**; *(Retrieves Key based on Section and Key value)*
- **IniParserInstance["Key"]**; *(Retrieves first Key found on any of the Sections. Use with caution and only if sure that there are no duplicates)*

###### General methods: 
- void IniParserInstance.**Load()**; *(Loads file from the given file path. It is automatically called in Constructor)*
- void IniParserInstance.**Flush**(); *(Writes content to file. It is automatically called on Disposal)*
- void IniParserInstance.**Refresh**(); *(Calls **Load** and **Flush**)*
- void IniParserInstance.**SaveTo**(string FilePath); *(Saves file to different location)*
- string IniParserInstance.**ToString**(); *(Gets string content)*

###### Section methods: 
- string[] IniParserInstance.**GetSections**(); 
- bool IniParserInstance.**SectionExists**(string Section); 
- void IniParserInstance.**RenameSection**(string OldName, string NewName); 
- void IniParserInstance.**DeleteSection**(string Section); 

###### Key methods: 
- bool IniParserInstance.**KeyExists**(string Key); 
- bool IniParserInstance.**KeyExists**(string Section, string Key); 
- void IniParserInstance.**DeleteKeys**(string Key); 
- void IniParserInstance.**DeleteKey**(string Section, string Key); 

###### Demo code: 
```
class Program
{
    static void Main(string[] args)
    {
        //Dispoing the object saves changes back to file 
        using (var ini = new IniParser())
        {
            //If Key is not found while indexing, create a new one 
            ini.AutoCreateKeys = true;
            
            //Read values using indeces (int, decimal, bool, string) 
            bool backupMode = (bool)(ini["GlobalSettings", "BackupMode").Value);
            string backupPath = ini["GlobalSettings", "Path").Value as string;
            int interval = (int)(ini["FTP", "Millis").Value);
            decimal size = (decimal)(ini["FTP", "Size").value); 
            
            //Write values directly (int, decimal, bool, string) 
            ini["DetailedLog"].Value = 2;
            ini["FTP", "backup"].Value = false;

            Console.WriteLine(ini);
        }

        Console.ReadLine();
    }        
}
  ```
