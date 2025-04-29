namespace AnimalProtection.Domain.Shared;

public class GitHubSettings
{
    public string Token { get; set; }
    public string Owner { get; set; }
    public string RepoName { get; set; }
    public string BranchName { get; set; }
    public string Heads { get; set; }
    public string UrlRaw { get; set; }
    public GitHubSettings() {}
}