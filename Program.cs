using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Microsoft.VisualBasic;

internal class Program {
    class Card {
        public char Letter { get; }
        public char Number { get; }

        public Card(char letter, char number) {
            Letter = letter;
            Number = number;
        }
        public int to_num(int variant) {
            string alph = "ABCDEFGH";
            return variant == 0 ? int.Parse(alph.IndexOf(Letter) + "" + Number) : int.Parse(Number + "" + alph.IndexOf(Letter));
        }
        public override string ToString()
        {
            return Letter.ToString() + Number.ToString();
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
        public void remove(char Card) {
            var pos = findCard(Card, Card == 1 || Card == 2 || Card == 3 || Card == 4 ? 1 : 0);
        }
        public Card Draw() {
            Card card = deck[0];
            deck.RemoveAt(0);
            return card;
        }


        private void sortDeck(int variant) {
            int n = deck.Count();
            int i, j;
            Card temp;
            bool swapped;
            for (i = 0; i < n - 1; i++) {
                swapped = false;
                for (j = 0; j < n - i - 1; j++) {
                    if (deck[j].to_num(variant) > deck[j + 1].to_num(variant)) {
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
        public int findCard(char letter, int variant) {
            sortDeck(variant);
            int low = 0,  high = deck.Count - 1;
            while(low <= high) {
                int mid = low + (high - low) / 2;

                if((variant == 0 ? deck[mid].Letter : deck[mid].Number) == letter) {
                    return mid;
                }
                if((variant == 0 ? deck[mid].Letter : deck[mid].Number) < letter) {
                    low = mid + 1;
                } else{
                    high = mid - 1;
                }

            }
            return -1;

        }
    }

    static void Main(string[] args) {
        var alph = "ABCDDEFGH";
        var num = "1234";
        Deck deck = new Deck(new List<Card>());
        foreach(var i in alph) {
            foreach(var j in num) {
                Card card = new Card(i, j);
                deck.add(card);

            }
        }

        deck.Shuffle();
        Console.WriteLine(deck.findCard('1', 1));
        Console.WriteLine(deck.ToString());
    }
}