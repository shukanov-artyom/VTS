using System;

namespace VTS.Agent.BusinessObjects.Enums
{
    public class Mnemocode
    {
        private readonly string code;

        public Mnemocode(string code)
        {
            if (String.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentException("Mnemocode cannot be empty");
            }
            this.code = code;
        }

        public string Code
        {
            get
            {
                return code;
            }
        }

        public override bool Equals(object obj)
        {
            Mnemocode another = obj as Mnemocode;
            if (another == null)
            {
                throw new ArgumentException("Wrong comparison type.");
            }
            return Equals(another);
        }

        public bool Equals(Mnemocode another)
        {
            return Code.Equals(another.Code, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode()
        {
            return Code.GetHashCode();
        }

        public override string ToString()
        {
            return code;
        }
    }
}
