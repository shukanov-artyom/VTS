using System;
using System.Collections.Generic;
using System.IO;
using Verifier.SolutionVerifiers;

namespace Verifier
{
    public static class Program
    {
        private static readonly HashSet<ISolutionVerifier> SolutionVerifiers =
            new HashSet<ISolutionVerifier>()
            {
                new OrphanedProjectFolders(),
                new ProjectFolderNaming()
            };
        private static readonly HashSet<IProjectVerifier> ProjectVerifiers = 
            new HashSet<IProjectVerifier>();

        public static int Main(string[] args)
        {
            int result = 0;
            if (args.Length != 1)
            {
                Console.WriteLine("Project validator Error: Wrong parameters: only solution folder is expected");
                return 1;
            }
            string solutionFolder = args[0];
            if (!Directory.Exists(solutionFolder))
            {
                Console.WriteLine("Project validator Error: Provided solution directory does not exist.");
                return 1;
            }
            foreach (ISolutionVerifier solVer in SolutionVerifiers)
            {
                if (!solVer.VerifySolution(solutionFolder))
                {
                    result = 1;
                }
                /*foreach (string projectFolder in GetSolutionProjectFolders(solutionFolder))
                {
                    foreach (IProjectVerifier proVer in ProjectVerifiers)
                    {
                        if (proVer.VerifyProject(projectFolder))
                        {
                            result = 1;
                        }
                    }
                }*/
            }
            return result;
        }

        private static IEnumerable<string> GetSolutionProjectFolders(string solutionPath)
        {
            throw new NotImplementedException();
        }
    }
}
