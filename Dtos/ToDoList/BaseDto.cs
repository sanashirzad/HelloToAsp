using HelloToAsp.Data;
using Humanizer;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HelloToAsp.Dtos.ToDoList
{
    public abstract class BaseDto
    {
        [Required]
        public string Task { get; set; }
        [DataType(DataType.Date)]
        public DateOnly? StartDateTime { get; set; }
        [DataType(DataType.Date)]
        [CustomValidation(typeof(BaseDto), "ValidateEndDate")]
        public DateOnly? EndDateTime { get; set; }
        [Range(1, 365)]
        public int? Duration { get; set; }
        public string? Description { get; set; }


        // ValidationContext context: Provides context about the validation, including access to the entire object
        // context.ObjectInstance: Gets the entire model object being validated
        public static ValidationResult ValidateEndDate(DateOnly? endDate, ValidationContext context)
        {
            var instance = (BaseDto)context.ObjectInstance;

            if (endDate.HasValue && instance.StartDateTime.HasValue && endDate.Value < instance.StartDateTime.Value)
            {
                return new ValidationResult("End date cannot be before start date");
            }

            return ValidationResult.Success;
        }

        // You might also want to add this to validate Duration vs Date range
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (StartDateTime.HasValue && EndDateTime.HasValue && Duration.HasValue)
            {
                var calculatedDuration = (EndDateTime.Value.ToDateTime(TimeOnly.MinValue) -
                                          StartDateTime.Value.ToDateTime(TimeOnly.MinValue)).Days + 1;

                if (calculatedDuration != Duration.Value)
                {
                    yield return new ValidationResult(
                        "Duration does not match the difference between start and end dates",
                        new[] { nameof(Duration) });
                }
            }
        }

        //This implements IValidatableObject interface (not shown in original code)
        //Runs after all property-level validations pass
        //yield return: Used to return multiple validation results from an iterator method
    }
}
