using System;
using System.Globalization;
using System.Web.Mvc;

namespace WebApplication1.Utils
{
    public class ModelBinder
    {
        public class DecimalModelBinder : DefaultModelBinder, IModelBinder
        {
            public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
            {
                object result = null;
                
                string modelName = bindingContext.ModelName;
                string attemptedValue = bindingContext.ValueProvider.GetValue(modelName).AttemptedValue;

                // Depending on CultureInfo, the NumberDecimalSeparator can be "," or "."
                // Both "." and "," should be accepted

                string wantedSeparator = ","; // NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;
                string alternateSeparator = (wantedSeparator == "," ? "." : ",");

                if (attemptedValue.IndexOf(wantedSeparator) == -1
                    && attemptedValue.IndexOf(alternateSeparator) != -1)
                {
                    attemptedValue = attemptedValue.Replace(alternateSeparator, wantedSeparator);
                }

                try
                {
                    if (bindingContext.ModelMetadata.IsNullableValueType && string.IsNullOrWhiteSpace(attemptedValue))
                    {
                        return null;
                    }

                    result = decimal.Parse(attemptedValue, NumberStyles.Any); 
                }
                catch (FormatException e)
                {
                    bindingContext.ModelState.AddModelError(modelName, e);
                }

                return result;
            }
        }
    }
}