using Xunit;

namespace EmailValidation.Tests
{
#if false
    public class EmailValidationTests
    {
        private readonly EmailValidator _validator = new();

        [Fact]
        public void Return_false_when_email_is_null()
        {
            var isValid = _validator.IsValidEmail(null!);
            Assert.False(isValid);
        }

        [Fact]
        public void Return_false_when_email_is_empty()
        {
            var isValid = _validator.IsValidEmail(string.Empty);
            Assert.False(isValid);
        }

        [Fact]
        public void Return_false_when_email_has_an_invalid_format()
        {
            var isValid = _validator.IsValidEmail("invalidEmail");
            Assert.False(isValid);
        }

        [Fact]
        public void Return_true_when_email_has_a_valid_format()
        {
            var isValid = _validator.IsValidEmail("valid@email.com");
            Assert.True(isValid);
        }
    }
#endif
}