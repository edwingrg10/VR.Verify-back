namespace VF.Verify.Domain.DTOs;

public class ExcelRuleData
{
    public string EntityName { get; set; }
    public string SourceName { get; set; }
    public string RuleName { get; set; }
    public string Operator { get; set; }
    public string Value { get; set; }
    public string Result { get; set; }
}