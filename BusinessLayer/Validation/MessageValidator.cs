using FluentValidation;

using Models;

namespace BusinessLayer.Validation
{
    public class MessageValidator : AbstractValidator<MessageModelRequest>, IValidator<MessageModelRequest>
    {
        private void ValidateMobileNumber ()
        {
            RuleFor ( sub => sub.MobileNumber ).NotNull ().NotEmpty ()/*.Matches ("REGEX")*/;
        }
        private void ValidateKeyWord ()
        {
            RuleFor ( sub => sub.MobileNumber ).NotNull ().NotEmpty ();
        }
        private void ApplyAllRules ()
        {
            ValidateMobileNumber ();
            ValidateKeyWord ();
        }
        public bool IsValid ( MessageModelRequest message )
        {
            ApplyAllRules ();
            return Validate ( message ).IsValid;
        }

    }
}
