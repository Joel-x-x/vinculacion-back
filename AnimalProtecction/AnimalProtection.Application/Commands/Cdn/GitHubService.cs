using AnimalProtection.Domain.Shared;
using Octokit;
using Microsoft.Extensions.Options;

namespace AnimalProtection.Application.Commands.Cdn;

public class GitHubService : IGitHubService
{
    private readonly GitHubClient _client;
    private readonly GitHubSettings _settings;

    public GitHubService(IOptions<GitHubSettings> settings)
    {
        _settings = settings.Value;
        _client = new GitHubClient(new ProductHeaderValue("AnimalProtection"))
        {
            Credentials = new Credentials(_settings.Token)
        };
    }

    public async Task<string> UploadFileToGitHub(string filePath, string fileName, string directory)
    {
        var owner = _settings.Owner;
        var repo = _settings.RepoName;
        var branch = _settings.BranchName;
        var fileUrl = string.Empty;
        var url = _settings.UrlRaw;
        var heads = _settings.Heads;
        
        // Leer el archivo
        var fileContent = await File.ReadAllBytesAsync(filePath);

        try
        {
            // Obtener la referencia del branch
            var masterReference = await _client.Git.Reference.Get(owner, repo, $"heads/{branch}");
            // Obtener el último commit
            var latestCommit = await _client.Git.Commit.Get(owner, repo, masterReference.Object.Sha);

            // Crear el blob (archivo) en GitHub
            var blob = new NewBlob
            {
                Content = Convert.ToBase64String(fileContent),
                Encoding = EncodingType.Base64
            };
            var blobResult = await _client.Git.Blob.Create(owner, repo, blob);

            // Construir el path final
            var finalPath = string.IsNullOrEmpty(directory)
                ? fileName
                : $"{directory.TrimEnd('/')}/{fileName}";

            // Crear el árbol
            var tree = new NewTree
            {
                BaseTree = latestCommit.Tree.Sha
            };
            tree.Tree.Add(new NewTreeItem
            {
                Path = finalPath,
                Mode = "100644",
                Type = TreeType.Blob,
                Sha = blobResult.Sha
            });
            var newTree = await _client.Git.Tree.Create(owner, repo, tree);

            // Crear el commit
            var newCommit = new NewCommit($"Add {finalPath}", newTree.Sha, masterReference.Object.Sha);
            var commit = await _client.Git.Commit.Create(owner, repo, newCommit);

            // Actualizar el branch a apuntar al nuevo commit
            await _client.Git.Reference.Update(owner, repo, $"{heads}/{branch}", new ReferenceUpdate(commit.Sha));

            // CONSTRUIMOS LA URL RAW
            fileUrl = $"{url}/{owner}/{repo}/{branch}/{finalPath}";

            return fileUrl;
        }
        catch (NotFoundException ex)
        {
            Console.WriteLine($"Branch {branch} no encontrado en {owner}/{repo}");
        }

        return fileUrl;
    }
}
