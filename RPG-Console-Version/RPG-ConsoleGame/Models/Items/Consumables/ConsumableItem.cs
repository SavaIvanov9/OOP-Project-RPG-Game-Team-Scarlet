﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_ConsoleGame.Exceptions;
using RPG_ConsoleGame.Interfaces;

namespace RPG_ConsoleGame.Models.Items.Consumables
{
    [Serializable()]
    public class ConsumableItem : Item, INonConsumable
    {
        
        public ConsumableItem(ItemType type, int level) : base(type, level)
        {
            this.InitializeItem();
            this.Initializelevel();
        }
    
        private void InitializeItem()
        {
            switch (this.Type)
            {
                case ItemType.PotionHealth:
                    this.health = 50;
                    break;
                case ItemType.PotionEnergy:
                    this.energy = 50;
                    break;
                case ItemType.ScrollGuardian:
                    this.health = 100;
                    this.defence = 10;
                    this.energy = 50;
                    break;
                case ItemType.ScrollDestruction:
                    this.damage = 100;
                    this.energy = 50;
                    this.reflexes = 10;
                    break;
                default:
                    throw new IncorrectTypeException("Incorrect item type");
            }
        }

        private void Initializelevel()
        {
            switch (this.Level)
            {
                case 1:
                    this.health *= 1;
                    this.damage *= 1;
                    this.defence *= 1;
                    this.energy *= 1;
                    this.reflexes *= 1;
                    break;
                case 2:
                    this.health *= 2;
                    this.damage *= 2;
                    this.defence *= 2;
                    this.energy *= 2;
                    this.reflexes *= 2;
                    break;
                case 3:
                    this.health *= 3;
                    this.damage *= 3;
                    this.defence *= 3;
                    this.energy *= 3;
                    this.reflexes *= 3;
                    break;
                default:
                    throw new IncorrectLevelException("Invalid level. Level must be in range of [1:3]");
            }
        }

        public override void UseItem(int health, int damage, int defence, int energy, int reflexes)
        {                            
            if (!used)               
            {                        
                damage += this.damage;
                defence += this.defence;
                energy += this.energy;
                health += this.health;
                reflexes += this.reflexes;

                this.used = true;
            }
            else
            {
                throw new OutOfAmountException("Item alredy used");
            }
        }
    }
}
