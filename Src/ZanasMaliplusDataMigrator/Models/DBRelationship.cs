using ReactiveUI;

namespace ZanasMaliplusDataMigrator.Models;

public class DBRelationship : ReactiveObject
{
    private string? masterTableName;
    private string? masterColumnName;
    private string? foreignTableName;
    private string? foreignColumnName;
    private string? relationshipName;

    public string? MasterTableName { get => masterTableName; set => masterTableName = value; }
    public string? MasterColumnName { get => masterColumnName; set => masterColumnName = value; }
    public string? ForeignTableName { get => foreignTableName; set => foreignTableName = value; }
    public string? ForeignColumnName { get => foreignColumnName; set => foreignColumnName = value; }
    public string? RelationshipName { get => relationshipName; set => relationshipName = value; }
}