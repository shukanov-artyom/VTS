using System;
using System.Globalization;
using System.IO;
using System.Xml.Linq;
using Agent.Common.Data;

namespace Agent.Metadata.Psa
{
    public class PsaMetadataPersistencyObject
    {
        private readonly string RootElementName =
            Cipher.Decrypt("1M/IVFLxFoDw0+Xp6zYncw==", "DateTime.Now");
        private readonly string MileageElementName =
            Cipher.Decrypt("CMVNYrmxdS3FSOfpZA89/w==", "DateTime.Now");
        private readonly string IsSynchronizedElementName =
            "IsSynchronized";

        private const string HiddenElementName = "Hidden";

        private XDocument doc;
        private FileStream file;

        public PsaMetadataPersistencyObject(FileStream file)
        {
            if (file.Length == 0)
            {
                doc = new XDocument();
            }
            else
            {
                doc = XDocument.Load(file);
            }
            this.file = file;
        }

        public void SetMileage(int mileage)
        {
            XElement root = doc.Element(RootElementName);
            if (root == null)
            {
                XElement newRoot = new XElement(RootElementName);
                doc.Add(newRoot);
                root = newRoot;
            }
            XElement mileageElement = root.Element(MileageElementName);
            if (mileageElement == null)
            {
                XElement newMileageElement = new XElement(MileageElementName);
                newMileageElement.Value = 
                    mileage.ToString(CultureInfo.InvariantCulture);
                root.Add(newMileageElement);
            }
            else
            {
                mileageElement.Value =
                    mileage.ToString(CultureInfo.InvariantCulture);
            }
            file.Position = 0;
            doc.Save(file);
        }

        public int GetMileage()
        {
            XElement root = doc.Element(RootElementName);
            if (root == null)
            {
                return 0;
            }
            XElement mileageElement = root.Element(MileageElementName);
            if (mileageElement != null)
            {
                return Int32.Parse(mileageElement.Value);
            }
            return 0;
        }

        public void MarkAsSynchronized(bool isSynchronized)
        {
            XElement root = doc.Element(RootElementName);
            if (root == null)
            {
                XElement newRoot = new XElement(RootElementName);
                doc.Add(newRoot);
                root = newRoot;
            }
            XElement xElement = root.Element(IsSynchronizedElementName);
            if (xElement == null)
            {
                XElement element = new XElement(IsSynchronizedElementName);
                element.Value =
                    isSynchronized.ToString(CultureInfo.InvariantCulture);
                root.Add(element);
            }
            else
            {
                xElement.Value =
                    isSynchronized.ToString(CultureInfo.InvariantCulture);
            }
            file.Position = 0;
            doc.Save(file);
        }

        public bool IsSynchronized()
        {
            XElement root = doc.Element(RootElementName);
            if (root == null)
            {
                return false;
            }
            XElement mileageElement = root.Element(IsSynchronizedElementName);
            if (mileageElement != null)
            {
                return Boolean.Parse(mileageElement.Value);
            }
            return false;
        }

        public bool IsHidden
        {
            get
            {
                return doc.Element(RootElementName).Element(HiddenElementName).Value.
                    Equals("true", StringComparison.OrdinalIgnoreCase);
            }
            set
            {
                doc.Element(RootElementName).Element(HiddenElementName).Value = value.ToString();
                doc.Save(file);
            }
        }
    }
}
