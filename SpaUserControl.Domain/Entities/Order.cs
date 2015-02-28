﻿using SpaUserControl.Common.Resources;
using SpaUserControl.Common.Validation;
using System;
using System.Collections.Generic;

namespace SpaUserControl.Domain.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public Guid OrderGuid { get; set; }
        public DateTime Date { get; private set; }
        public decimal Total { get; private set; }
        public int Qtd { get; set; }
        public List<Item> Items { get; set; }
        public User User { get; set; }

        protected Order()
        {

        }

        public Order(User user, List<Item> items)
        {
            Date = DateTime.Now;            
            OrderGuid = new Guid();
            Items = items;
            User = user;

            foreach (var item in Items)
            {
                UpdateTotal(item);
            }

            Validate();
        }

        public void AddToItems(Item item)
        {
            Total = Total + (item.Product.Price * item.Qtd);
            Items.Add(item);

            Validate();
        }
        
        public void Validate()
        {
            AssertionConcern.AssertArgumentNotNull(Total, Notifications.NotNull);
            AssertionConcern.AssertArgumentNotNull(Qtd, Notifications.NotNull);
            AssertionConcern.AssertArgumentNotZero(Total, Notifications.IsZero);
            AssertionConcern.AssertArgumentNotZero(Qtd, Notifications.IsZero);
            AssertionConcern.AssertArgumentNotNull(Items, Notifications.NotNull);
            AssertionConcern.AssertArgumentNotNull(User, Notifications.NotNull);
        }

        private void UpdateTotal(Item item)
        {
            Total = Total + (item.Product.Price * item.Qtd);

            Validate();
        }

    }
}
