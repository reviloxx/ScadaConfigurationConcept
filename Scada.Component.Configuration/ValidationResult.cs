namespace Scada.Component.Configuration;

public class ValidationResult
{
    public bool IsValidConfiguration { get; set; }
    public List<ValidationError> ValidationErrors { get; set; } = [];
}

public class ValidationError
{

}
