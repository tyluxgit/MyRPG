using Engine.Models;
using Engine.Factories;
using Engine.EventArgs;

namespace Engine.ViewModels;

public static class CombatService
{
    public static event EventHandler<GameMessageEventArgs>? OnMessageRaised;

    public static void AttackMonster(GameSession session)
    {
        if (session.CurrentWeapon == null || session.CurrentMonster == null)
        {
            RaiseMessage("You must select a weapon and have a monster to attack.");
            return;
        }

        int damageToMonster = CalculateDamage(session.CurrentWeapon);
        ProcessPlayerAttack(session, damageToMonster);

        if (session.CurrentMonster.HitPoints <= 0)
        {
            HandleMonsterDeath(session);
        }
        else
        {
            ProcessMonsterAttack(session);
        }
    }

    private static int CalculateDamage(Weapon weapon) =>
        RandomNumberGenerator.NumberBetween(weapon.MinimumDamage, weapon.MaximumDamage);

    public static void ProcessPlayerAttack(GameSession session, int damage)
    {
        if (damage == 0)
        {
            RaiseMessage($"You missed the {session.CurrentMonster?.Name}.");
        }
        else
        {
            session.CurrentMonster!.HitPoints -= damage;
            RaiseMessage($"You hit the {session.CurrentMonster.Name} for {damage} points.");
        }
    }

    public static void HandleMonsterDeath(GameSession session)
    {
        var monster = session.CurrentMonster!;
        RaiseMessage($"\nYou defeated the {monster.Name}!");
        session.CurrentPlayer.ExperiencePoints += monster.RewardExperiencePoints;
        session.CurrentPlayer.Gold += monster.RewardGold;

        RaiseMessage($"You receive {monster.RewardExperiencePoints} XP and {monster.RewardGold} gold.");

        foreach (ItemQuantity itemQuantity in monster.Inventory)
        {
            GameItem item = ItemFactory.CreateGameItem(itemQuantity.ItemID);
            session.CurrentPlayer.AddItemToInventory(item);
            RaiseMessage($"You receive {itemQuantity.Quantity} {item.Name}.");
        }

        session.GetMonsterAtLocation();
    }

    public static void ProcessMonsterAttack(GameSession session)
    {
        var monster = session.CurrentMonster!;
        int damageToPlayer = RandomNumberGenerator.NumberBetween(monster.MinimumDamage, monster.MaximumDamage);

        if (damageToPlayer == 0)
        {
            RaiseMessage("The monster attacks, but misses you.");
        }
        else
        {
            session.CurrentPlayer.HitPoints -= damageToPlayer;
            RaiseMessage($"The {monster.Name} hit you for {damageToPlayer} points.");
        }

        if (session.CurrentPlayer.HitPoints <= 0)
        {
            HandlePlayerDeath(session);
        }
    }

    public static void HandlePlayerDeath(GameSession session)
    {
        // Safely access CurrentMonster's Name
        if (session.CurrentMonster != null)
        {
            RaiseMessage($"The {session.CurrentMonster.Name} killed you.");
        }
        else
        {
            RaiseMessage("You died.");
        }

        // Ensure CurrentWorld is not null before accessing LocationAt
        if (session.CurrentWorld != null)
        {
            session.CurrentLocation = session.CurrentWorld.LocationAt(0, -1); // Home
        }
        else
        {
            // Handle the scenario where CurrentWorld is unexpectedly null
            throw new InvalidOperationException("CurrentWorld is not set.");
        }

        // Set CurrentMonster to null after death
        session.CurrentMonster = null;

        // Ensure CurrentPlayer is not null before accessing its properties
        if (session.CurrentPlayer != null)
        {
            session.CurrentPlayer.HitPoints = session.CurrentPlayer.Level * 10; // Full heal
        }
        else
        {
            // Handle the scenario where CurrentPlayer is unexpectedly null
            throw new InvalidOperationException("CurrentPlayer is not set.");
        }
    }



    public static void RaiseMessage(string message) =>
    OnMessageRaised?.Invoke(typeof(CombatService), new GameMessageEventArgs(message));

}
