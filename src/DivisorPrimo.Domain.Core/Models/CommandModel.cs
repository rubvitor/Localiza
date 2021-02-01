using FluentValidation.Results;
using System;

namespace DivisorPrimo.Domain.Models
{
    public class CommandModel : NetDevPack.Messaging.Command
    {
        private ValidationResult _validationResult;
        public virtual ValidationResult ValidationResult
        {
            get
            {
                if (_validationResult == null)
                    _validationResult = base.ValidationResult;

                return _validationResult;
            }
            set
            {
                _validationResult = value;
            }
        }
        private ValidationResultModel _validationResultModel;
        public virtual ValidationResultModel ValidationResultModel
        {
            get
            {
                if (_validationResultModel == null)
                {
                    _validationResultModel = new ValidationResultModel
                    {
                        Errors = base.ValidationResult.Errors,
                        IsValid = base.ValidationResult.IsValid,
                        RuleSetsExecuted = base.ValidationResult.RuleSetsExecuted
                    };
                }

                return _validationResultModel;
            }
            set
            {
                _validationResultModel = value;
            }
        }
        protected CommandModel()
        { }

        public virtual DateTime Timestamp
        {
            get
            {
                return base.Timestamp;
            }
        }

        public virtual bool IsValid()
        {
            return base.IsValid();
        }
    }
}
