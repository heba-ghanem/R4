/*namespace RealTask
{
    public class RealEvaluator
    {
        public string GetLoanEligibility(int income, bool hasJob, int creditScore, int dependents, bool ownsHouse)
        {
            if (income < 2000)
                return "Not Eligible";

            if (hasJob)
            {
                if (creditScore >= 700)
                {
                    if (dependents == 0)
                        return "Eligible";
                    else if (dependents < 2)
                        return "Review Manually";
                    else
                        return "Not Eligible";
                }
                else if (creditScore >= 600)
                {
                    if (ownsHouse)
                        return "Review Manually";
                    else
                        return "Not Eligible";
                }
                else
                    return "Not Eligible";
            }
            else
            {
                if (creditScore >= 750 && income > 5000 && ownsHouse)
                    return "Eligible";
                else if (creditScore >= 650 && dependents == 0)
                    return "Review Manually";
                else
                    return "Not Eligible";
            }
        }
    }
}*/

namespace LoanApp
{
    public class LoanEvaluator
    {
        public string GetLoanEligibility(int income, bool hasJob, int creditScore, int dependents, bool ownsHouse)
        {
            if (!hasJob || income < 50000)
                return "Not Eligible";

            if (IsHighCreditEligible(creditScore, dependents, ownsHouse))
                return "Eligible";

            if (NeedsManualReview(creditScore, dependents, ownsHouse))
                return "Review Manually";

            if (IsMidRangeEligible(creditScore, income, dependents))
                return "Eligible";

            return "Not Eligible";
        }

        public bool IsHighCreditEligible(int creditScore, int dependents, bool ownsHouse)
        {
            return creditScore > 700 && dependents <= 2 && ownsHouse;
        }

        public bool NeedsManualReview(int creditScore, int dependents, bool ownsHouse)
        {
            return creditScore > 700 && dependents > 2 && !ownsHouse;
        }

        public bool IsMidRangeEligible(int creditScore, int income, int dependents)
        {
            return creditScore >= 700 && income > 50000 && dependents <= 1;
        }
    }
}
