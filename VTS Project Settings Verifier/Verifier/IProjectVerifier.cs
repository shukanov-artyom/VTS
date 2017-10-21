using System;

namespace Verifier
{
    internal interface IProjectVerifier
    {
        bool VerifyProject(string folderPath);
    }
}
