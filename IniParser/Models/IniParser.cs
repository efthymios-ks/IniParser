using System;
using System.IO;
using System.Diagnostics;

namespace SharpUtilities
{
    [DebuggerDisplayAttribute("{DebuggerDisplay,nq}")]
    public partial class IniParser : IDisposable
    {
        //Constructors 
        public IniParser() : this(Process.GetCurrentProcess().MainModule.FileName)
        {

        }

        public IniParser(string FilePath)
        {
            //Filepath 
            FilePath = Path.ChangeExtension(FilePath, "ini");
            this.FilePath = FilePath;

            Load();
        }
               
        #region IDisposable implementation 
        private bool _isDisposed = false;
        public void Dispose()
        {
            Dispose(true);
        }

        public virtual void Dispose(bool Disposing)
        {
            if (_isDisposed)
                return;

            //Flush data 
            Flush();

            if (Disposing)
            {
                //Dispose managed objects 
            }

            //Free unmanaged resources (unmanaged objects) and override a finalizer below 

            //Set large fields to null 
            _items = null;

            _isDisposed = true;
        }
        #endregion
    }
}
