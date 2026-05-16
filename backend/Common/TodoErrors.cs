namespace backend.Common;

public static class TodoErrors
{
    public static readonly Error NotFound = 
        new("todo.not_found", "La todo demandee n'existe pas.");

    public static Error DeleteBlocked(List<long> blockedIds) =>
        new("todo.delete_blocked", $"Suppression refusée pour les todos high/critical : {string.Join(", ", blockedIds)}.");
}