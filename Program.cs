using System;
using System.Collections.Generic;

internal class Program {
    class Card {
        public string Letter { get; }
        public string Number { get; }

        public Card(string letter, string number) {
            Letter = letter;
            Number = number;
        }
        
    }

    class Deck {
        private List<Card> deck;

        public Deck(List<Card> cards) {
            deck = new List<Card>(cards);
        }

        public void Shuffle() {
            Random rnd = new Random();
            int l = deck.Count;
            for (int i = 0; i < l; i++) {
                int j = rnd.Next(0, deck.Count);
                Card temp = deck[j];
                deck.RemoveAt(j);
                deck.Add(temp);
            }
        }
    }
}