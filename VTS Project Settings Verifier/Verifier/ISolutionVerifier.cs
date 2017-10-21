using System;

namespace Verifier
{
    internal interface ISolutionVerifier
    {
        bool VerifySolution(string folderPath);
    }
}
