using System;
using System.Collections.Generic;
using System.Security.Cryptography;

internal class Program {
    class Card {
        public string Letter { get; }
        public string Number { get; }

        public Card(string letter, string number) {
            Letter = letter;
            Number = number;
        }
        public int to_num() {
            string alph = "ABCDEFGH";
            return int.Parse(alph.IndexOf(Letter) + "" + Number);
        }
        public override string ToString()
        {
            return Letter + Number;
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
        public void add(Card card) {
            deck.Add(card);
        }
        public void remove(Card card) {
            deck.Remove(card);
        }
        public Card Draw() {
            return deck[0];
        }


        public void sortDeck() {
            int n = deck.Count();
            int i, j;
            Card temp;
            bool swapped;
            for (i = 0; i < n - 1; i++) {
                swapped = false;
                for (j = 0; j < n - i - 1; j++) {
                    if (deck[j].to_num() > deck[j + 1].to_num()) {
                        temp = deck[j];
                        deck[j] = deck[j + 1];
                        deck[j + 1] = temp;
                        swapped = true;
                    }
                }

                if (swapped == false) {
                    break;
                }
            }
        }

        public override string ToString() {
            string output = "";
            foreach (Card card in deck) {
                output += card.ToString() + ", ";
            }
            return output;
        }
    }

    static void Main(string[] args) {
        var alph = "ABCDDEFGH";
        var num = "1234";
        Deck deck = new Deck(new List<Card>());
        foreach(var i in alph) {
            foreach(var j in num) {
                Card card = new Card(i.ToString(), j.ToString());
                deck.add(card);

            }
        }

        deck.Shuffle();
        Console.WriteLine(deck.ToString());
        deck.sortDeck();
        Console.WriteLine(deck.ToString());
    }
}