using Xunit;
using LoanEvaluator;

namespace LoanEvaluator.Tests
{
    public class LoanEvaluatorTests
    {
        private readonly LoanEvaluatorTests _loanEvaluator;

        public LoanEvaluatorTests()
        {
            _loanEvaluator = new LoanEvaluatorTests();
        }

        [Theory]
        [InlineData(60000, true, 750, 1, true, "Eligible")] // High credit eligible
        [InlineData(55000, true, 800, 2, true, "Eligible")] // High credit eligible
        [InlineData(51000, true, 710, 1, false, "Eligible")] // Mid range eligible
        [InlineData(60000, true, 720, 0, false, "Eligible")] // Mid range eligible
        [InlineData(60000, true, 750, 3, false, "Review Manually")] // Needs manual review
        [InlineData(60000, true, 680, 2, false, "Not Eligible")] // Not eligible
        [InlineData(40000, true, 800, 1, true, "Not Eligible")] // Income too low
        [InlineData(60000, false, 800, 1, true, "Not Eligible")] // No job
        public void GetLoanEligibility_ShouldReturnCorrectResult(
            int income, bool hasJob, int creditScore, int dependents, bool ownsHouse, string expected)
        {
            // Act
            var result = LoanEvaluatorr.GetLoanEligibility(income, hasJob, creditScore, dependents, ownsHouse);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(750, 1, true, true)] // Eligible
        [InlineData(800, 2, true, true)] // Eligible
        [InlineData(750, 3, true, false)] // Too many dependents
        [InlineData(750, 1, false, false)] // Doesn't own house
        [InlineData(700, 1, true, false)] // Credit score not > 700
        public void IsHighCreditEligible_ShouldReturnCorrectResult(
            int creditScore, int dependents, bool ownsHouse, bool expected)
        {
            // Act
            var result = _loanEvaluator.IsHighCreditEligible(creditScore, dependents, ownsHouse);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(750, 3, false, true)] // Needs review
        [InlineData(800, 4, false, true)] // Needs review
        [InlineData(750, 2, false, false)] // Not enough dependents
        [InlineData(750, 3, true, false)] // Owns house
        [InlineData(650, 3, false, false)] // Credit score too low
        public void NeedsManualReview_ShouldReturnCorrectResult(
            int creditScore, int dependents, bool ownsHouse, bool expected)
        {
            // Act
            var result = _loanEvaluator.NeedsManualReview(creditScore, dependents, ownsHouse);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(710, 55000, 1, true)] // Eligible
        [InlineData(700, 60000, 0, true)] // Eligible
        [InlineData(710, 50000, 1, false)] // Income not > 50000
        [InlineData(690, 55000, 1, false)] // Credit score < 700
        [InlineData(710, 55000, 2, false)] // Too many dependents
        public void IsMidRangeEligible_ShouldReturnCorrectResult(
            int creditScore, int income, int dependents, bool expected)
        {
            // Act
            var result = _loanEvaluator.IsMidRangeEligible(creditScore, income, dependents);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}