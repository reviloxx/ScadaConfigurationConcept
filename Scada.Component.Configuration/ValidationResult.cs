namespace Scada.Components.Configuration;
public class ValidationResult
{
    public bool IsValid { get; set; }
    public List<ValidationError> ValidationErrors { get; set; } = [];
}

public class ValidationError
{

}
