# IniParser (CSharp)
IniParser for .NET applications. 

###### Parser methods: 
- IniParser() *(Opens [ApplicationName].ini)*
- IniParser(string FilePath) *(Opens files from given filepath)*

- void IniParserInstance.**Load()**; *(Loads file from the given file path. It is also automatically called in Constructor)*
- void IniParserInstance.**Flush**(); *(Writes content to file. It is also automatically called on Dispose)*
- void IniParserInstance.**Refresh**(); *(Calls Load and Flush)*
- void IniParserInstance.**SaveTo**(string FilePath); *(Saves file to different location.)*
- string IniParserInstance.**ToString**(); *(Gets string content)*

- string[] IniParserInstance.**GetSections**(); 
- bool IniParserInstance.**SectionExists**(string Section); 
- void IniParserInstance.**RenameSection**(string OldName, string NewName); 
- void IniParserInstance.**DeleteSection**(string Section); 

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
        using (var ini = new IniParser())
        {
            ini.AutoCreateKeys = true;

            ini["DetailedLog"].Value = 2;
            ini["FTP", "backup"].Value = false;


            Console.WriteLine(ini);
        }

        Console.ReadLine();
    }        
}
  ```
