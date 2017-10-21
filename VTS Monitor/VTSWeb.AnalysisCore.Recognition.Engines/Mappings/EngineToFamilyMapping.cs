using System.Collections.Generic;
using VTS.Shared;

namespace VTSWeb.AnalysisCore.Recognition.Engines.Mappings
{
    public class EngineToFamilyMapping
    {
        private static IDictionary<EngineFamilyType, IList<EngineType>> 
            mapping = new Dictionary<EngineFamilyType,IList<EngineType>>();

        static EngineToFamilyMapping()
        {
            mapping[EngineFamilyType.Douvrin] = new List<EngineType>();
            mapping[EngineFamilyType.Douvrin].Add(EngineType.Douvrin22);
            mapping[EngineFamilyType.Douvrin].Add(EngineType.Douvrin20);
            mapping[EngineFamilyType.Douvrin].Add(EngineType.Douvrin21Diesel);

            mapping[EngineFamilyType.X] = new List<EngineType>();
            mapping[EngineFamilyType.X].Add(EngineType.XV8);
            mapping[EngineFamilyType.X].Add(EngineType.XW3);
            mapping[EngineFamilyType.X].Add(EngineType.XW7);
            mapping[EngineFamilyType.X].Add(EngineType.XW3S);
            mapping[EngineFamilyType.X].Add(EngineType.XZ5);
            mapping[EngineFamilyType.X].Add(EngineType.XZ6);
            mapping[EngineFamilyType.X].Add(EngineType.XZ7R);
            mapping[EngineFamilyType.X].Add(EngineType.XY);
            mapping[EngineFamilyType.X].Add(EngineType.XY6B);
            mapping[EngineFamilyType.X].Add(EngineType.XY6A);
            mapping[EngineFamilyType.X].Add(EngineType.XY7);
            mapping[EngineFamilyType.X].Add(EngineType.XY8);
            mapping[EngineFamilyType.X].Add(EngineType.XYR);

            mapping[EngineFamilyType.TU] = new List<EngineType>();
            mapping[EngineFamilyType.TU].Add(EngineType.TU9MZ);
            mapping[EngineFamilyType.TU].Add(EngineType.TU9K);
            mapping[EngineFamilyType.TU].Add(EngineType.TU1F2K);
            mapping[EngineFamilyType.TU].Add(EngineType.TU1JP);
            mapping[EngineFamilyType.TU].Add(EngineType.TU1MZ);
            mapping[EngineFamilyType.TU].Add(EngineType.TU1K);
            mapping[EngineFamilyType.TU].Add(EngineType.TU2A);
            mapping[EngineFamilyType.TU].Add(EngineType.TU24AK);
            mapping[EngineFamilyType.TU].Add(EngineType.TU3A);
            mapping[EngineFamilyType.TU].Add(EngineType.TU3AK);
            mapping[EngineFamilyType.TU].Add(EngineType.TU3F2K);
            mapping[EngineFamilyType.TU].Add(EngineType.TU3FJ2K);
            mapping[EngineFamilyType.TU].Add(EngineType.TU3FJ2Z);
            mapping[EngineFamilyType.TU].Add(EngineType.TU3JP);
            mapping[EngineFamilyType.TU].Add(EngineType.TU3MZ);
            mapping[EngineFamilyType.TU].Add(EngineType.TU3S);
            mapping[EngineFamilyType.TU].Add(EngineType.ET3J4);
            mapping[EngineFamilyType.TU].Add(EngineType.TU5J2L3);
            mapping[EngineFamilyType.TU].Add(EngineType.TU5J4);
            mapping[EngineFamilyType.TU].Add(EngineType.TU5JP);
            mapping[EngineFamilyType.TU].Add(EngineType.TU5JPPlus);
            mapping[EngineFamilyType.TU].Add(EngineType.TU5JP_);
            mapping[EngineFamilyType.TU].Add(EngineType.TU5JP4);
            mapping[EngineFamilyType.TU].Add(EngineType.TU5JP4S);

            mapping[EngineFamilyType.EC] = new List<EngineType>();
            mapping[EngineFamilyType.EC].Add(EngineType.EC5);
            mapping[EngineFamilyType.EC].Add(EngineType.EC8);

            mapping[EngineFamilyType.Prince] = new List<EngineType>();
            mapping[EngineFamilyType.Prince].Add(EngineType.EP3);
            mapping[EngineFamilyType.Prince].Add(EngineType.EP6);
            mapping[EngineFamilyType.Prince].Add(EngineType.EP6C);
            mapping[EngineFamilyType.Prince].Add(EngineType.EP6DT);
            mapping[EngineFamilyType.Prince].Add(EngineType.EP6DT140);
            mapping[EngineFamilyType.Prince].Add(EngineType.EP6DTS);
            mapping[EngineFamilyType.Prince].Add(EngineType.EP6CDTX);

            mapping[EngineFamilyType.TUD] = new List<EngineType>();
            mapping[EngineFamilyType.TUD].Add(EngineType.TUD3);
            mapping[EngineFamilyType.TUD].Add(EngineType.TUD5);

            mapping[EngineFamilyType.XU] = new List<EngineType>();
            mapping[EngineFamilyType.XU].Add(EngineType.XU5J);
            mapping[EngineFamilyType.XU].Add(EngineType.XU51C);
            mapping[EngineFamilyType.XU].Add(EngineType.XU52C);
            mapping[EngineFamilyType.XU].Add(EngineType.XU5C);
            mapping[EngineFamilyType.XU].Add(EngineType.XU5M3Z);
            mapping[EngineFamilyType.XU].Add(EngineType.XU7JB);
            mapping[EngineFamilyType.XU].Add(EngineType.XU7JP);
            mapping[EngineFamilyType.XU].Add(EngineType.XU7JP4);
            mapping[EngineFamilyType.XU].Add(EngineType.XU8T200);
            mapping[EngineFamilyType.XU].Add(EngineType.XU8T300);
            mapping[EngineFamilyType.XU].Add(EngineType.XU8TEvo1);
            mapping[EngineFamilyType.XU].Add(EngineType.XU8TEvo2);
            mapping[EngineFamilyType.XU].Add(EngineType.XU92C105);
            mapping[EngineFamilyType.XU].Add(EngineType.XU92C110);
            mapping[EngineFamilyType.XU].Add(EngineType.XU94C);
            mapping[EngineFamilyType.XU].Add(EngineType.XU9J1Z);
            mapping[EngineFamilyType.XU].Add(EngineType.XU9J2);
            mapping[EngineFamilyType.XU].Add(EngineType.XU9JAK);
            mapping[EngineFamilyType.XU].Add(EngineType.XU9J4D6CL);
            mapping[EngineFamilyType.XU].Add(EngineType.XU9J4ZDFW);
            mapping[EngineFamilyType.XU].Add(EngineType.XU102C);
            mapping[EngineFamilyType.XU].Add(EngineType.XU10J2CLRFX);
            mapping[EngineFamilyType.XU].Add(EngineType.XU10J2);
            mapping[EngineFamilyType.XU].Add(EngineType.XU10J2TERGY);
            mapping[EngineFamilyType.XU].Add(EngineType.XU10J2TERGX);
            mapping[EngineFamilyType.XU].Add(EngineType.XU10J4DZ);
            mapping[EngineFamilyType.XU].Add(EngineType.XU10J4RRFV);
            mapping[EngineFamilyType.XU].Add(EngineType.XU10J4RL3RFV);
            mapping[EngineFamilyType.XU].Add(EngineType.XU10J4RSL3FS);
            mapping[EngineFamilyType.XU].Add(EngineType.XU10J4TE);
            mapping[EngineFamilyType.XU].Add(EngineType.XU10J4LZRFY);
            mapping[EngineFamilyType.XU].Add(EngineType.XU10M);

            mapping[EngineFamilyType.XUD] = new List<EngineType>();
            mapping[EngineFamilyType.XUD].Add(EngineType.XUD7TK);
            mapping[EngineFamilyType.XUD].Add(EngineType.XUD7TE);
            mapping[EngineFamilyType.XUD].Add(EngineType.XUD7K);
            mapping[EngineFamilyType.XUD].Add(EngineType.XUD7Z);
            mapping[EngineFamilyType.XUD].Add(EngineType.XUD9);
            mapping[EngineFamilyType.XUD].Add(EngineType.XUD9A);
            mapping[EngineFamilyType.XUD].Add(EngineType.XUD9TEL);
            mapping[EngineFamilyType.XUD].Add(EngineType.XUD9SD);
            mapping[EngineFamilyType.XUD].Add(EngineType.XUD9TEY);
            mapping[EngineFamilyType.XUD].Add(EngineType.XUD9Z);
            mapping[EngineFamilyType.XUD].Add(EngineType.XUD11A);
            mapping[EngineFamilyType.XUD].Add(EngineType.XUD11ATE);
            mapping[EngineFamilyType.XUD].Add(EngineType.XUD11BTE);

            mapping[EngineFamilyType.EWDW] = new List<EngineType>();
            mapping[EngineFamilyType.EWDW].Add(EngineType.EW7J4);
            mapping[EngineFamilyType.EWDW].Add(EngineType.EW10D);
            mapping[EngineFamilyType.EWDW].Add(EngineType.EW10J4RFN);
            mapping[EngineFamilyType.EWDW].Add(EngineType.EW10J4RFR);
            mapping[EngineFamilyType.EWDW].Add(EngineType.EW10J4S);
            mapping[EngineFamilyType.EWDW].Add(EngineType.EW10A);
            mapping[EngineFamilyType.EWDW].Add(EngineType.EW12J4);
            mapping[EngineFamilyType.EWDW].Add(EngineType.DW8);
            mapping[EngineFamilyType.EWDW].Add(EngineType.DW8B);
            mapping[EngineFamilyType.EWDW].Add(EngineType.DW10ATED);
            mapping[EngineFamilyType.EWDW].Add(EngineType.DW10ATED4);
            mapping[EngineFamilyType.EWDW].Add(EngineType.DW10TD);
            mapping[EngineFamilyType.EWDW].Add(EngineType.DW10BTED4);
            mapping[EngineFamilyType.EWDW].Add(EngineType.DW10C);
            mapping[EngineFamilyType.EWDW].Add(EngineType.DW12UTED);
            mapping[EngineFamilyType.EWDW].Add(EngineType.DW12TED4);
            mapping[EngineFamilyType.EWDW].Add(EngineType.DW12MTED4);
            mapping[EngineFamilyType.EWDW].Add(EngineType.DW12C);

            mapping[EngineFamilyType.DK] = new List<EngineType>();
            mapping[EngineFamilyType.DK].Add(EngineType.DK5);

            mapping[EngineFamilyType.DV] = new List<EngineType>();
            mapping[EngineFamilyType.DV].Add(EngineType.DV4TD);
            mapping[EngineFamilyType.DV].Add(EngineType.DV4TED4);
            mapping[EngineFamilyType.DV].Add(EngineType.DV6ATED4);
            mapping[EngineFamilyType.DV].Add(EngineType.DV6B);
            mapping[EngineFamilyType.DV].Add(EngineType.DV6TED4);
            mapping[EngineFamilyType.DV].Add(EngineType.DV6C);
            mapping[EngineFamilyType.DV].Add(EngineType.DV6D);

            mapping[EngineFamilyType.PRV] = new List<EngineType>();
            mapping[EngineFamilyType.PRV].Add(EngineType.ZP);
            mapping[EngineFamilyType.PRV].Add(EngineType.ZN);
            mapping[EngineFamilyType.PRV].Add(EngineType.ZM);

            mapping[EngineFamilyType.ES] = new List<EngineType>();
            mapping[EngineFamilyType.ES].Add(EngineType.ES9J4L7X);
            mapping[EngineFamilyType.ES].Add(EngineType.ES9J4S);
            mapping[EngineFamilyType.ES].Add(EngineType.ES9IA);

            mapping[EngineFamilyType.XB] = new List<EngineType>();

            mapping[EngineFamilyType.XC] = new List<EngineType>();
            // TODO: fill it
            mapping[EngineFamilyType.XD] = new List<EngineType>();

            mapping[EngineFamilyType.XM] = new List<EngineType>();

            mapping[EngineFamilyType.XN] = new List<EngineType>();
            mapping[EngineFamilyType.XN].Add(EngineType.XN1);
            mapping[EngineFamilyType.XN].Add(EngineType.XN2);
            mapping[EngineFamilyType.XN].Add(EngineType.XN6);
            mapping[EngineFamilyType.XN].Add(EngineType.XN8);

            mapping[EngineFamilyType.EB] = new List<EngineType>();
            mapping[EngineFamilyType.EB].Add(EngineType.EB2M);
        }

        public static IList<EngineType> GetFamilyMembers(EngineFamilyType familyType)
        {
            return mapping[familyType];
        }
    }
}
