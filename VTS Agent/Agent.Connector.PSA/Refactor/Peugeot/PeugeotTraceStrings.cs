using System;
using Agent.Common.Data;


namespace Agent.Connector.PSA.Refactor.Peugeot
{
    public static class PeugeotTraceStrings
    {
        private static readonly string info;
        private static readonly string workplace;
        private static readonly string address;
        private static readonly string infoMemo;
        private static readonly string dateHour;
        private static readonly string vehicle;
        private static readonly string km;
        private static readonly string tel;
        private static readonly string version;
        private static readonly string versionCd;
        private static readonly string noserieppi;
        private static readonly string vin;
        private static readonly string type;
        private static readonly string command;

        private static string card;
        private static bool cardDecrypted = false;
        private static string param;
        private static bool paramDecrypted = false;
        private static string attribut;
        private static bool attributDecrypted = false;
        private static string name;
        private static bool nameDecrypted = false;
        private static string screentitle;
        private static bool screentitleDecrypted = false;
        private static string title;
        private static bool titleDecrypted = false;
        private static string measure;
        private static bool measureDecrypted = false;
        private static string unit;
        private static bool unitDecrypted = false;

        static PeugeotTraceStrings()
        {
            
            info = "/oXnoPxdi37gq3Diiq+6Fg==";
            workplace = "TfQocUATIy8IV7pfMUahRA==";
            address = "FW6myz34t7VPifpiVJY9jA==";
            infoMemo = "s9l2tfub5JvE7s5gOxVhxQ==";
            dateHour = "fhv2l1d5NSWM0J0XF1OHjA==";
            vehicle = "YGqfJz8xftufRYyv9tzYMQ==";
            km = "0dbrvfWwfl0uzj7BGjIG/A==";
            tel = "FeVUdJJcaxLSY4W8d30h7A==";
            version = "VwaVmazKpe7m36jgaMYFBw==";
            versionCd = "U4gzOLphbJf4GNm4vWZVkw==";
            noserieppi = "2gZkRclLFa4OnFJPv2H7LA==";
            vin = "BdDy5+hEpQzVDO3OrDn81g==";
            type = "rIfx1dJ0RvH8jSqbsxK/Rw==";
            command = "6giBKIgZLGO6QplA+sEEUQ==";
            card = "kfkIDAKDg3GyTLuTtX1SfA==";
            param = "Vh39EuKSzd00SLjHBbQpFw==";
            attribut = "bssjAixeJ4NmfoFTz9kewA==";
            name = "+kJ+ERPoKta2cjEXeGXgBg==";
            screentitle = "tdGMzir9V15mwO3YKCfc2w==";
            title = "YEaRH/1ls2eWBg9t6XI45w==";
            measure = "2Ej0URRoUE9rtNwd0fH6sg==";
            unit = "4rLUnLGr27XlrAhT6NdI5g==";

            /*
            info = "INFO";
            workplace = "WORKPLACE";
            address = "ADDRESS";
            infoMemo = "INFO_MEMO";
            dateHour = "DATE_HOUR";
            vehicle = "VEHICLE";
            km = "KM";
            tel = "TEL";
            version = "VERSION";
            versionCd = "VERSION_CD";
            noserieppi = "NOSERIEPPI";
            vin = "VIN";
            type = "TYPE";
            command = "COMMAND";
            card = "CARD";
            param = "PARAM";
            attribut = "ATTRIBUT";
            name = "NAME";
            screentitle = "SCREENTITLE";
            title = "TITLE";
            measure = "MEASURE";
            unit = "UNIT";
            */
        }

        public static string Unit
        {
            get
            {
                if (!unitDecrypted)
                {
                    unit = Decrypt(unit);
                    unitDecrypted = true;
                }
                return unit;
            }
        }

        public static string Measure
        {
            get
            {
                if (!measureDecrypted)
                {
                    measure = Decrypt(measure);
                    measureDecrypted = true;
                }
                return measure;
            }
        }

        public static string Title
        {
            get
            {
                if (!titleDecrypted)
                {
                    title = Decrypt(title);
                    titleDecrypted = true;
                }
                return title;
            }
        }

        public static string Screentitle
        {
            get
            {
                if (!screentitleDecrypted)
                {
                    screentitle = Decrypt(screentitle);
                    screentitleDecrypted = true;
                }
                return screentitle;
            }
        }

        public static string Name
        {
            get
            {
                if (!nameDecrypted)
                {
                    name = Decrypt(name);
                    nameDecrypted = true;
                }
                return name;
            }
        }

        public static string Attribut
        {
            get
            {
                if (!attributDecrypted)
                {
                    attribut = Decrypt(attribut);
                    attributDecrypted = true;
                }
                return attribut;
            }
        }

        public static string Param
        {
            get
            {
                if (!paramDecrypted)
                {
                    param = Decrypt(param);
                    paramDecrypted = true;
                }
                return param;
            }
        }

        public static string Card
        {
            get
            {
                if (!cardDecrypted)
                {
                    card = Decrypt(card);
                    cardDecrypted = true;
                }
                return card;
            }
        }

        public static string Command
        {
            get
            {
                return Decrypt(command);
            }
        }

        public static string Type
        {
            get
            {
                return Decrypt(type);
            }
        }

        public static string Vin
        {
            get
            {
                return Decrypt(vin);
            }
        }

        public static string Noserieppi
        {
            get
            {
                return Decrypt(noserieppi);
            }
        }

        public static string VersionCd
        {
            get
            {
                return Decrypt(versionCd);
            }
        }

        public static string Version
        {
            get
            {
                return Decrypt(version);
            }
        }

        public static string Info
        {
            get
            {
                return Decrypt(info);
            }
        }

        public static string Workplace
        {
            get
            {
                return Decrypt(workplace);
            }
        }

        public static string Address
        {
            get
            {
                return Decrypt(address);
            }
        }

        public static string InfoMemo
        {
            get
            {
                return Decrypt(infoMemo);
            }
        }

        public static string DateHour
        {
            get
            {
                return Decrypt(dateHour);
            }
        }

        public static string Vehicle
        {
            get
            {
                return Decrypt(vehicle);
            }
        }

        public static string Km
        {
            get
            {
                return Decrypt(km);
            }
        }

        public static string Tel
        {
            get
            {
                return Decrypt(tel);
            }
        }

        private static string Decrypt(string s)
        {
            return Cipher.Decrypt(s, "System.IO.FileInfo");
            //return s;
        }
    }
}
