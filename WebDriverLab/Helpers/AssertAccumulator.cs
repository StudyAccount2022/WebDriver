using System;
using System.Text;
using NUnit.Framework;

namespace WebDriverLab.Helpers
{
    public class AssertAccumulator
    {
        private StringBuilder Errors { get; set; }
        private bool AssertsPassed { get; set; }

        private String AccumulatedErrorMessage
        {
            get
            {
                return Errors.ToString();
            }
        }

        public AssertAccumulator()
        {
            Errors = new StringBuilder();
            AssertsPassed = true;
        }

        private void RegisterError(string exceptionMessage)
        {
            AssertsPassed = false;
            Errors.AppendLine(exceptionMessage);
        }

        public void Accumulate(Action assert)
        {
            try
            {
                assert.Invoke();
            }
            catch (Exception exception)
            {
                RegisterError(exception.Message);
            }
        }

        public void Release()
        {
            if (!AssertsPassed)
            {
                throw new AssertionException(AccumulatedErrorMessage);
            }
        }

    }
}
