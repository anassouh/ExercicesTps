using System;
using System.Text.Json;

public enum TypeArticle
{
    Alimentaire,
    Droguerie,
    Habillement,
    Loisir
}

public struct Article
{
    public string Nom { get; set; }
    public decimal Prix { get; set; }
    public int Quantite { get; set; }
    public TypeArticle Type { get; set; }

    public Article(string nom, decimal prix, int quantite, TypeArticle type)
    {
        Nom = nom;
        Prix = prix;
        Quantite = quantite;
        Type = type;
    }

    public void Afficher()
    {
        Console.WriteLine($"Nom: {Nom}, Prix: {Prix}, Quantité: {Quantite}, Type: {Type}");
    }

    public void Ajouter(int quantite)
    {
        if (quantite > 0)
        {
            Quantite += quantite;
        }
        else
        {
            Console.WriteLine("La quantité à ajouter doit être un entier positif.");
        }
    }

    public void Retirer(int quantite)
    {
        if (quantite > 0)
        {
            if (Quantite >= quantite)
            {
                Quantite -= quantite;
            }
            else
            {
                Console.WriteLine("Quantité insuffisante pour retirer.");
            }
        }
        else
        {
            Console.WriteLine("La quantité à retirer doit être un entier positif.");
        }
    }
    public static void AfficherTous(this IEnumerable<Article> articles)
    {
        foreach (var article in articles)
        {
            Console.WriteLine($"Nom : {article.Nom}, Prix : {article.Prix:C}");
        }
    }
}

public static class Extensions
{
    public static void AfficherTous(this IEnumerable<Article> articles)
    {
        foreach (var article in articles)
        {
            Console.WriteLine($"Nom : {article.Nom}, Prix : {article.Prix:C}, Quantité : {article.Quantite}, Type : {article.Type}");
        }
    }
}
class Program
{
    static void Main(string[] args)
    {
        // Création de quelques articles
        Article article1 = new Article("Stylo", 1.50m, 100, TypeArticle.Loisir);
        Article article2 = new Article("Cahier", 2.75m, 50, TypeArticle.Habillement);
        Article article3 = new Article("Gomme", 0.99m, 200, TypeArticle.Droguerie);

        // Affichage des articles
        article1.Afficher();
        article2.Afficher();
        article3.Afficher();

        // Modification des quantités
        article1.Ajouter(20);
        article2.Retirer(10);
        article3.Ajouter(50);

        // Affichage des articles après modification
        Console.WriteLine("\nAprès modification des quantités :");
        article1.Afficher();
        article2.Afficher();
        article3.Afficher();

        // Saisie des informations pour un nouvel article
        Console.WriteLine("\nSaisir les informations pour un nouvel article :");
        Console.Write("Nom : ");
        string nom = Console.ReadLine();
        Console.Write("Prix : ");
        decimal prix = Convert.ToDecimal(Console.ReadLine());
        Console.Write("Quantité : ");
        int quantite = Convert.ToInt32(Console.ReadLine());
        Console.Write("Type (Alimentaire 0, Droguerie 1, Habillement 2, Loisir 3) : ");
        TypeArticle type = (TypeArticle)Enum.Parse(typeof(TypeArticle), Console.ReadLine(), true);

        // Création et affichage du nouvel article
        Article nouvelArticle = new Article(nom, prix, quantite, type);
        nouvelArticle.Afficher();

        #region Linq

        List<Article> articles = new List<Article> {
            new Article("Pomme", 2.5m, 50, TypeArticle.Alimentaire),
            new Article("Savon", 3.2m, 30, TypeArticle.Droguerie),
            new Article("T-shirt", 15.0m, 20, TypeArticle.Habillement)
        };

        //-1
        var articlesAlimentaires = articles.Where(a => a.Type == TypeArticle.Alimentaire);

        articlesAlimentaires = articles.OrderByDescending(a => a.Prix);

        int stockTotal = articles.Sum(x => x.Quantite);



        foreach (var article in articlesAlimentaires)
        {
            Console.WriteLine($"{article.Nom} - {article.Prix}€ - {article.Type} unités");
        }

        //-2 
        var list = new List<Object>
        {
            articles,"objet diff"
        };


        var OnlyArticles = list.OfType<Article>;

        var articlesSimplifiés = articles.Select(a => new
        {
            a.Nom,
            a.Prix
        });

        foreach (var article in articlesSimplifiés)
        {
            Console.WriteLine($"{article.Nom} - {article.Prix}€ ");
        }

        //-3
        articles.AfficherTous();

        decimal valeurTotaleStock = articles.Sum(a => a.Prix * a.Quantite);

        // Affichage de la valeur totale
        Console.WriteLine($"La valeur totale du stock est : {valeurTotaleStock:C}");

        //-4
        string filePath = "articles.json";

        // 1. Exporter (Sérialisation JSON)
        Console.WriteLine("Export des articles en JSON...");
        string json = JsonSerializer.Serialize(articles, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filePath, json);
        Console.WriteLine($"Fichier JSON créé : {filePath}");

        // 2. Importer (Désérialisation JSON)
        Console.WriteLine("\nImport des articles depuis le fichier JSON...");
        if (File.Exists(filePath))
        {
            string importedJson = File.ReadAllText(filePath);
            var importedArticles = JsonSerializer.Deserialize<List<Article>>(importedJson);

            // Affichage des articles importés
            Console.WriteLine("Articles importés :");
            foreach (var article in importedArticles)
            {
                article.Afficher();
            }
        }
        else
        {
            Console.WriteLine("Le fichier JSON n'existe pas.");
        }

        #endregion

    }
}

