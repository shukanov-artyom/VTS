using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows.Controls;
using System.Xml.Linq;
using VTS.Shared;

namespace Agent.Common.Presentation.RegistryUser
{
    public static class AgentIsolatedStorageHelper
    {
        private const string FileName = "agentSettings.xml";
        private const string DefaultLang = "DefaultLang";
        private const string LoginElement = "Login";
        private const string PasswordHashElement = "PasswordHash";

        private static object locker = new object();

        public static SupportedLanguage Language 
        {
            get
            {
                lock (locker)
                {
                    if (!Storage.FileExists(FileName))
                    {
                        InitializeFile();
                        return SupportedLanguage.English;
                    }
                    try
                    {
                        using (IsolatedStorageFileStream fs = Storage.OpenFile(
                        FileName,
                        FileMode.Open,
                        FileAccess.Read,
                        FileShare.ReadWrite))
                        {
                            XDocument doc = XDocument.Load(fs);
                            string langLine = doc.Root.Element(DefaultLang).Value;
                            SupportedLanguage result = (SupportedLanguage)Enum.Parse(typeof(SupportedLanguage), langLine);
                            return result;
                        }
                    }
                    catch (Exception)
                    {
                        CleanStorage();
                        throw;
                    }
                }
            }
            set
            {
                lock (locker)
                {
                  if (!Storage.FileExists(FileName))
                    {
                        InitializeFile();
                    }
                    try
                    {
                        using (IsolatedStorageFileStream fs = Storage.OpenFile(
                        FileName,
                        FileMode.Open,
                        FileAccess.ReadWrite,
                        FileShare.ReadWrite))
                        {
                            XDocument doc = XDocument.Load(fs);
                            XElement root = doc.Root;
                            root.Element(DefaultLang).Value = value.ToString();
                            fs.Position = 0;
                            doc.Save(fs);
                        }
                    }
                    catch (Exception)
                    {
                        CleanStorage();
                        throw;
                    }
                }
            }
        }

        public static string Login
        {
            get
            {
                lock (locker)
                {
                    if (!Storage.FileExists(FileName))
                    {
                        InitializeFile();
                    }
                    try
                    {
                        using (IsolatedStorageFileStream fs = Storage.OpenFile(
                        FileName,
                        FileMode.Open,
                        FileAccess.Read,
                        FileShare.ReadWrite))
                        {
                            XDocument doc = XDocument.Load(fs);
                            return doc.Root.Element(LoginElement).Value;
                        }
                    }
                    catch (Exception)
                    {
                        CleanStorage();
                        throw;
                    }
                }
            }
            set
            {
                lock (locker)
                {
                    if (!Storage.FileExists(FileName))
                    {
                        InitializeFile();
                    }
                    try
                    {
                        using (IsolatedStorageFileStream fs = Storage.OpenFile(
                        FileName,
                        FileMode.Open,
                        FileAccess.ReadWrite,
                        FileShare.ReadWrite))
                        {
                            XDocument doc = XDocument.Load(fs);
                            doc.Root.Element(LoginElement).Value = value;
                            fs.Position = 0;
                            doc.Save(fs);
                        }
                    }
                    catch (Exception)
                    {
                        CleanStorage();
                        throw;
                    }
                }
            }
        }

        public static string PasswordHash
        {
            get
            {
                lock (locker)
                {
                    if (!Storage.FileExists(FileName))
                    {
                        InitializeFile();
                    }
                    try
                    {
                        using (IsolatedStorageFileStream fs = Storage.OpenFile(
                            FileName,
                            FileMode.Open,
                            FileAccess.Read,
                            FileShare.ReadWrite))
                            {
                                XDocument doc = XDocument.Load(fs);
                                return doc.Root.Element(PasswordHashElement).Value;
                            }
                    }
                    catch (Exception)
                    {
                        CleanStorage();
                        throw;
                    }
                }
            }
            set
            {
                lock (locker)
                {
                    if (!Storage.FileExists(FileName))
                    {
                        InitializeFile();
                    }
                    try
                    {
                        using (IsolatedStorageFileStream fs = Storage.OpenFile(
                        FileName,
                        FileMode.Open,
                        FileAccess.ReadWrite,
                        FileShare.ReadWrite))
                        {
                            XDocument doc = XDocument.Load(fs);
                            doc.Root.Element(PasswordHashElement).Value = value;
                            fs.Position = 0;
                            doc.Save(fs);
                        }
                    }
                    catch (Exception)
                    {
                        CleanStorage();
                        throw;
                    }
                }
            }
        }

        private static IsolatedStorageFile Storage
        {
            get
            {
                return IsolatedStorageFile.GetStore(
                    IsolatedStorageScope.User | IsolatedStorageScope.Assembly,
                    null,
                    null);
            }
        }

        private static void InitializeFile()
        {
            lock (locker)
            {
                using (IsolatedStorageFileStream fs = Storage.CreateFile(FileName))
                {
                    XDocument doc = new XDocument();
                    XElement root = new XElement("root");
                    root.Add(new XElement(DefaultLang)
                        {
                            Value = SupportedLanguage.English.ToString()
                        });
                    root.Add(new XElement(LoginElement)
                             {
                                 Value = String.Empty
                             });
                    root.Add(new XElement(PasswordHashElement)
                        {
                            Value = String.Empty
                        });
                    doc.Add(root);
                    fs.Position = 0;
                    doc.Save(fs);
                }
            }
        }

        private static void CleanStorage()
        {
            Storage.Remove();
        }
    }
}
