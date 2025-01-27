﻿public struct Article
{
    public string Nom { get; set; }
    public decimal Prix { get; set; }
    public int Quantite { get; set; }

    public Article(string nom, decimal prix, int quantite)
    {
        Nom = nom;
        Prix = prix;
        Quantite = quantite;
    }

    public void Afficher()
    {
        Console.WriteLine($"Nom: {Nom}, Prix: {Prix}, Quantité: {Quantite}");
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
}
class Program
{
    static void Main(string[] args)
    {
        // Création de quelques articles
        Article article1 = new Article("Stylo", 1.50m, 100);
        Article article2 = new Article("Cahier", 2.75m, 50);
        Article article3 = new Article("Gomme", 0.99m, 200);

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

        // Création et affichage du nouvel article
        Article nouvelArticle = new Article(nom, prix, quantite);
        nouvelArticle.Afficher();
    }
}