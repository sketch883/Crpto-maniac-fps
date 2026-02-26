using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public class MarketplaceManager
{
    private List<NFTWeapon> nftWeapons;
    private Dictionary<string, double> userWallets;
    private Dictionary<string, List<Transaction>> transactionHistory;

    public MarketplaceManager()
    {
        nftWeapons = new List<NFTWeapon>();
        userWallets = new Dictionary<string, double>();
        transactionHistory = new Dictionary<string, List<Transaction>>();
    }

    public void BuyWeapon(string userId, NFTWeapon weapon, double amount)
    {
        if (userWallets.ContainsKey(userId) && userWallets[userId] >= amount)
        {
            userWallets[userId] -= amount;
            AddToTransactionHistory(userId, weapon, amount, "buy");
            Console.WriteLine($"User {userId} purchased {weapon.Name} for {amount}.");
        }
        else
        {
            Console.WriteLine("Insufficient funds or user does not exist.");
        }
    }

    public void SellWeapon(string userId, NFTWeapon weapon, double amount)
    {
        userWallets[userId] += amount;
        AddToTransactionHistory(userId, weapon, amount, "sell");
        Console.WriteLine($"User {userId} sold {weapon.Name} for {amount}.");
    }

    private void AddToTransactionHistory(string userId, NFTWeapon weapon, double amount, string transactionType)
    {
        if (!transactionHistory.ContainsKey(userId))
        {
            transactionHistory[userId] = new List<Transaction>();
        }
        transactionHistory[userId].Add(new Transaction { Weapon = weapon, Amount = amount, Type = transactionType, Date = DateTime.UtcNow });
    }

    public double GetWalletBalance(string userId)
    {
        return userWallets.ContainsKey(userId) ? userWallets[userId] : 0.0;
    }

    public List<Transaction> GetTransactionHistory(string userId)
    {
        return transactionHistory.ContainsKey(userId) ? transactionHistory[userId] : new List<Transaction>();
    }
}

public class NFTWeapon
{
    public string Name { get; set; }
    public string Id { get; set; }
    public double Price { get; set; }
}

public class Transaction
{
    public NFTWeapon Weapon { get; set; }
    public double Amount { get; set; }
    public string Type { get; set; }
    public DateTime Date { get; set; }
}