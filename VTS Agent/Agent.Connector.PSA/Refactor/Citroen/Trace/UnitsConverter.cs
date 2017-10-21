using System;
using System.Collections.Generic;
using Agent.Common.Data;
using VTS.Shared;

namespace Agent.Connector.PSA.Refactor.Citroen.Trace
{
    internal static class UnitsConverter
    {
        private static readonly IDictionary<string, Unit> Mapping =
            new Dictionary<string, Unit>();

        static UnitsConverter()
        {
            //@P2953-CITACT
            Mapping.Add(Decode("ghPN8WR/rt2tT0St+oyD3g=="), Unit.Volt);
            //@P1523-CITACT@\*
            Mapping.Add(Decode("/FKrcXt4sJSeKwQMijLk9OuZE+DEx3fvGT6Xatfgrzw="), Unit.Volt);
            //@P2953-CITACT@\*
            Mapping.Add(Decode("W1H1kvfBSvSW52Tx8S1fTxolSwEwOIuLcnw4N8UCJ/s="), Unit.Volt);
            //@TV
            Mapping.Add(Decode("Hw/zO0RR61oejJ+NQX/JfA=="), Unit.Volt);
            //@P76948-THESAU@\*
            Mapping.Add(Decode("KZBey8IQDMZAoFaV1fIsfnnf5ZsKh3I+XrPSSSQpCDQ="), Unit.Volt);
            //Volts
            Mapping.Add(Decode("0cFeJN3YG6JuWkvg6DFFeg=="), Unit.Volt);
            //Volt
            Mapping.Add(Decode("Ku8FLq7YwNujLxUfwn7h5Q=="), Unit.Volt);
            //@P2411-CITACT@\*
            Mapping.Add(Decode("LVkA0FlB2gP8zVQtKqwBnWC/bQnyQWwUg8SCaokp1OQ="), Unit.Millivolt);
            //@P6680-CITACT
            Mapping.Add(Decode("47yoRzfmKMwlpgEkuPPJ/A=="), Unit.Ohm);
            //@TmA
            Mapping.Add(Decode("vaKRSdBPEyYLe9YMnHLbAQ=="), Unit.Milliamper);
            //@P2416-CITACT
            Mapping.Add(Decode("s8cz4DGNSibeoe6H4PvN0A=="), Unit.Millisec);
            //@P106701-THESAU@\*
            Mapping.Add(Decode("i/Rlq+q4Aah/UYEq23aLhD75I7FpiKWsDxctD1GT2Eg="), Unit.Millisec);
            //@P1530-CITACT@\*
            Mapping.Add(Decode("RhZiQs6h+JEXKf8rluZHXLyFzgldx9bbFmnYMsCvNMw="), Unit.Rpm);
            //@P4460-THESAU@\*
            Mapping.Add(Decode("rP6frDZeZtTmhoy6UBafwQmcqNad1RkLlba+j25Qxk4="), Unit.Rpm);
            //@P9077-THESAU@\*
            Mapping.Add(Decode("tnN1Bt9n8w2g0nLdcsBX/CEnBYlc51RYqHFLi8qQasY="), Unit.CelsiusDegree);
            //@P6679-CITACT
            Mapping.Add(Decode("9AjdiEPkWEc2d2vaHipSMw=="), Unit.CelsiusDegree);
            //@T°
            Mapping.Add(Decode("ygXxrfttTXJ+AJ/b3SXYhw=="), Unit.Degree);
            //@P2954-CITACT
            Mapping.Add(Decode("LeNBJISCs5iZSSmWZG6b/Q=="), Unit.Bar);
            //@P13563-THESAU@\*@\*
            Mapping.Add(Decode("iOwU4EDe0iRlnzxmpnqYzqhxkVtu2yiaTG/Ds0BqGmY="), Unit.Bar);
            //@P106700-THESAU@\*
            Mapping.Add(Decode("aLf4OWrDyLgj66Aa5UVakwb3zCi9mCViQF05bL1Pp/0="), Unit.Millibar);
            //@P1517-CITACT@\*
            Mapping.Add(Decode("n8XVxK6YcuYzV2iRbAC8rQSYdtUFKeBMG90H/hIBgWE="), Unit.Millibar);
            //@P11831-CITACT
            Mapping.Add(Decode("Rk29WZ3cvXvCGVPXR9pV4w=="), Unit.Millibar);
            //@Tmb
            Mapping.Add(Decode("u7i7BsixcqHJWkfRdOp1Rg=="), Unit.Millibar);
            //@T%
            Mapping.Add(Decode("IO3X9C7OTfWdXGCjyH1vBQ=="), Unit.Percent);
            //@P1519-CITACT@\*
            Mapping.Add(Decode("nqh9lJjcLIhWC85QoB/8qCDREi2qWnW7bAkTkubbtbY="), Unit.Percent);
            //@P1534-CITACT
            Mapping.Add(Decode("ZGesJLpT4+vRau9Vym3zPQ=="), Unit.Mm3PerStroke);
            //@P85004-THESAU@\*
            Mapping.Add(Decode("/1DApQbhHigW8UbgZ1AZHqZn8NSQUNc6qxcITglC+gY="), Unit.Mm3PerStroke);
            //@P2424-CITACT
            Mapping.Add(Decode("tzPvTvaj8TopfQ+xvzZR+w=="), Unit.MgPerStroke);
            //@P1520-CITACT
            Mapping.Add(Decode("7Uwp2grqjfeD06Q2w5mQgQ=="), Unit.KmH);
            //@P112942-THESAU
            Mapping.Add(Decode("n+jxMx6EbUaMEA6eJn9DRw=="), Unit.M3PerHour);
            //@P13200-CITACT
            Mapping.Add(Decode("Aiv46UPb+4Vc3d8JInIBWg=="), Unit.M3PerHour);
            //@Tg
            Mapping.Add(Decode("N77LPFdbYc2AopIOEz3orw=="), Unit.Gramm);
            //@P24901-THESAU@\*
            Mapping.Add(Decode("T+jFkLtnTAPj6vAdgYpDQzrD2l678Gex5ehusUBxEOg="), Unit.Km);
            //@P2957-CITACT
            Mapping.Add(Decode("X0S3RxaeCoW0obRrgzR6Ww=="), Unit.Km);
            //@P33453-THESAU@\*100
            Mapping.Add(Decode("lviv8ocWqzKUafupCywf0Z62VwQ1+TxkGI49vtClU4k="), Unit.LitrePer100Km);
            //@P9103-CITACT
            Mapping.Add(Decode("YLleOlgCDLIPfLTWmKXQ1A=="), Unit.LitrePer100Km);
            //@P7974-CITACT@\*
            Mapping.Add(Decode("qff1QTV8akNiTRNsm1yaTqiSTR/RwuNVDmV+Zsid4zE="), Unit.Litre);
            //@P5669-CITACT@\*
            Mapping.Add(Decode("/+kIHHPrxSBhVG4qvf+Xr0eKGJx9zhq743ybmKiwuY4="), Unit.Lux);
            //@TA
            Mapping.Add(Decode("BclvDZvUl9ljoOd1SVDHqQ=="), Unit.Amper);
            //@T°C
            Mapping.Add(Decode("3s33zbffQ1X+2vHRwCRGpQ=="), Unit.CelsiusDegree);
            //@P408-CITACT
            Mapping.Add(Decode("7XH0K4nHVagQymfNxE5jdw=="), Unit.CelsiusDegree);
            //@P98777-THESAU@\*
            Mapping.Add(Decode("veSjmswgc1kSh0UJT/mQwTkcIxo1kBjWxYvhMzmraZk="), Unit.Percent);
            //@P1518-CITACT
            Mapping.Add(Decode("b33ZnNTJKsCTFz728D1tcA=="), Unit.Percent);
            //@P43341-THESAU@\*
            Mapping.Add(Decode("DVs+8UOsCGMiQPntcdotBYnhY1JGJab1SZ60hpW01iE="), Unit.NewtonMetre);
            //@P408-CITACT@\*
            Mapping.Add(Decode("NBQCC2A2e6wbIwkl2CS6Iw=="), Unit.CelsiusDegree);
            //@P16699-THESAU@\*
            Mapping.Add(Decode("bu00IF6R51MVML5N6bE+yr2Rh9aSregpTHKmsfu0trg="), Unit.KmH);
            //@T°/s
            Mapping.Add(Decode("RYmiy4oP3CpBHUP1M2lxXw=="), Unit.CelsiusDegreePerSecond);
            //@P2366-CITACT
            Mapping.Add(Decode("yu32TJCY/QSZjjgaBFX53g=="), Unit.Bar);
            //@P10309-CITACT@\*
            Mapping.Add(Decode("TXdQqubwi6iceYJEfTVIqh0qxibSUMB8K6XgcdWVK+I="), Unit.Gramm);
            //@P28890-THESAU@\*
            Mapping.Add(Decode("BQeowVcJ0q94M1ZOZMpIP7a3TC6YZtRFKQpk9y8mIGQ="), Unit.MgPerStroke);
            //@Ttr/mn
            Mapping.Add(Decode("AOonSK9WTBfj4ZYIpUGRpg=="), Unit.Rpm);
            //@TKm/h
            Mapping.Add(Decode("v2PPi9CxPA9sbq//2O69HQ=="), Unit.KmH);
            //@P65149-THESAU@\*
            Mapping.Add(@"@P65149-THESAU@\*", Unit.Millisec);
            //@P98558-THESAU@\\*
            Mapping.Add(@"@P98558-THESAU@\*", Unit.Millivolt);
            Mapping.Add(@"@P9530-CITACT@\+@T/@\+@\*@P7998-CITACT@\*", Unit.Mm3PerSecond);
            Mapping.Add(@"@P15085-POLUXDATA@\*", Unit.NewtonMetre);
            Mapping.Add(@"@P6867-POLUXDATA", Unit.Bar);
            Mapping.Add(@"@P14078-POLUXDATA", Unit.Rpm);
            Mapping.Add(@"@P2634-POLUXDATA", Unit.CelsiusDegree);
            Mapping.Add(@"@P4108-POLUXDATA", Unit.Millisec); // TODO : clarify
            Mapping.Add(@"@P9944-POLUXDATA", Unit.KmH);
            Mapping.Add(@"@F6867-POLUXDATA", Unit.Bar);
        }

        public static Unit Convert(string rawUnitName)
        {
            if (String.IsNullOrEmpty(rawUnitName))
            {
                return Unit.NoUnits;
            }
            if (!Mapping.ContainsKey(rawUnitName))
            {
                return Unit.Unsupported;
            }
            return Mapping[rawUnitName];
        }

        private static string Decode(string val)
        {
            return Cipher.Decrypt(val, "System.Collections.Generic");
        }
    }
}
