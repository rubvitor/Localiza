using DevExpress.Utils.Commands;
using FluentValidation.Results;
using System.Collections.Generic;

namespace DivisorPrimo.Domain.Models
{
    public class ValidationResultModel : ValidationResult
    {
        private bool? _isValid;
        public virtual bool IsValid
        {
            get
            {
                if (!_isValid.HasValue)
                    _isValid = base.IsValid;

                return _isValid.Value;
            }
            set
            {
                _isValid = value;
            }
        }

        private IList<ValidationFailure> _erros;
        public virtual IList<ValidationFailure> Errors
        {
            get
            {
                if (_erros == null)
                    _erros = base.Errors;

                return _erros;
            }
            set
            {
                _erros = value;
            }
        }
        private string[] _ruleSetsExecuted;
        public virtual string[] RuleSetsExecuted
        {
            get
            {
                if (_ruleSetsExecuted == null)
                    _ruleSetsExecuted = base.RuleSetsExecuted;

                return _ruleSetsExecuted;
            }
            set
            {
                _ruleSetsExecuted = value;
            }
        }
        public virtual dynamic ObjectResult { get; set; }
        public virtual string ToString()
        {
            return base.ToString();
        }
        public virtual string ToString(string separator)
        {
            return base.ToString(separator);
        }
    }
}
