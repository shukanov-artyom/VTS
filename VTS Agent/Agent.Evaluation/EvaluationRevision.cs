using System;
using System.Collections.Generic;
using VTS.Shared;

namespace Agent.Evaluation
{
    public class EvaluationRevision
    {
        private readonly Dictionary<AnalyticRuleType, EvaluationMark> marks = 
            new Dictionary<AnalyticRuleType,EvaluationMark>();

        public Dictionary<AnalyticRuleType, EvaluationMark> Marks
        {
            get
            {
                return marks;
            }
        }

        public void AddParameterMark(AnalyticRuleType type, EvaluationMark mark)
        {
            if (marks.ContainsKey(type))
            {
                throw new NotSupportedException("Rule type already added!");
            }
            marks[type] = mark;
        }
    }
}
