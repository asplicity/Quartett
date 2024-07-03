using System;
using System.Collections.Generic;

internal class Program {
    class Card {
        public char Letter { get; }
        public char Number { get; }

        public Card(char letter, char number) {
            Letter = letter;
            Number = number;
        }
        public int to_num(int variant){
            string alph = "ABCDEFGH";
            return variant == 0 ? int.Parse(alph.IndexOf(Letter) + "" + Number) : int.Parse(Number + "" + alph.IndexOf(Letter));
        }
        public override string ToString() {
            return Letter.ToString() + Number.ToString();
        }
    }

    class Deck {
        #pragma warning disable
        private List<Card> deck;

        public Deck(List<Card> cards) {
            deck = new List<Card>(cards);
        }

        public void Shuffle() {
            Random rnd = new Random();
            int n = deck.Count;
            while (n > 1) {
                n--;
                int k = rnd.Next(n + 1);
                Card temp = deck[k];
                deck[k] = deck[n];
                deck[n] = temp;
            }
        }

        public void add(Card card) {
            deck.Add(card);
        }

        public Card remove(char letter, char number) {
            var pos = findCard(letter, number);
            if (pos != -1) {
                Card card = deck[pos];
                deck.RemoveAt(pos);
                return card;
            }
            return null;
        }

        public Card remove(char character, int variant) {
            var pos = findCard(character, variant);
            if (pos != -1) {
                Card card = deck[pos];
                deck.RemoveAt(pos);
                return card;
            }
            return null;
        }

        public Card Choose() {
            Shuffle();
            return deck[0];
        }

        public Card Draw() {
            if (deck.Count == 0) return null;
            Card card = deck[0];
            deck.RemoveAt(0);
            return card;
        }

        public char has_Quartett() {
            foreach (var letter in "ABCDEFGH".ToCharArray()) {
                int count = 0;
                foreach (var card in deck) {
                    if (card.Letter == letter) {
                        count++;
                    }
                }
                if (count == 4) {
                    return letter;
                }
            }
            return '-';
        }

        private void sortDeck(int variant) {
            deck.Sort((x, y) => x.to_num(variant).CompareTo(y.to_num(variant)));
        }

        public override string ToString() {
            string output = "";
            foreach (Card card in deck) {
                output += card.ToString() + ", ";
            }
            return output.TrimEnd(' ', ',');
        }

        public int findCard(char letter, int variant) {
            sortDeck(variant);
            int low = 0, high = deck.Count - 1;
            while (low <= high) {
                int mid = low + (high - low) / 2;
                char compare = variant == 0 ? deck[mid].Letter : deck[mid].Number;
                if (compare == letter) {
                    return mid;
                }
                if (compare < letter) {
                    low = mid + 1;
                }
                else {
                    high = mid - 1;
                }
            }
            return -1;
        }

        public int findCard(char letter, char number) {
            for (int i = 0; i < deck.Count; i++) {
                if (deck[i].Letter == letter && deck[i].Number == number) {
                    return i;
                }
            }
            return -1;
        }
    }

    #pragma warning enable
    static void Main(string[] args) {
        var alph = "ABCDEFGH";
        var num = "1234";
        Deck deck = new Deck(new List<Card>());
        foreach (var i in alph) {
            foreach (var j in num) {
                Card card = new Card(i, j);
                deck.add(card);
            }
        }
        deck.Shuffle();

        Deck bot = new Deck(new List<Card>());
        Deck player = new Deck(new List<Card>());

        for (int i = 0; i < 7; i++) {
            bot.add(deck.Draw());
            player.add(deck.Draw());
        }

        var bot_quartetts = 0;
        var player_quartetts = 0;

        while (true) {
            Console.WriteLine($"Du hast:\n{player}\nNach welchem Wert möchtest du fragen?");
            var input = Console.ReadLine();
            char Wert = string.IsNullOrWhiteSpace(input) ? '1' : input[0];
            int variant = "1234".Contains(Wert) ? 1 : 0;
            Card Karte = bot.remove(Wert, variant);
            if (player.findCard(Wert, variant) != -1) {
            
                if (Karte != null) {
                    player.add(Karte);
                    Console.WriteLine($"Der Bot hatte die Karte: {Karte}");
                }
                else {
                    Card newCard = deck.Draw();
                    bot.add(newCard);
                    Console.WriteLine("Der Bot hatte die Karte nicht, er hat eine Karte gezogen");
                }
            } else {
                Console.WriteLine("Du hast die Karte nicht wähle einen anderen Wert");
            }
            Card WKarte = bot.Choose();
            Random rnd = new Random();
            var Wahl = rnd.Next(2) == 0 ? WKarte.Letter : WKarte.Number;
            Console.WriteLine($"Der Bot fragt ob du ein {Wahl} hast");
            variant = "1234".Contains(Wahl) ? 1 : 0;

            Karte = player.remove(Wahl, variant);
            if (Karte != null) {
                bot.add(Karte);
                Console.WriteLine($"Du hast die Karte {Karte} dem Bot gegeben");
            }
            else {
                Card newCard = deck.Draw();
                player.add(newCard);
                Console.WriteLine($"Du hattest die Karte nicht, du hast die Karte {newCard} gezogen");
            }

            var Quartett = player.has_Quartett();
            if (Quartett != '-') {
                
                player.remove(Quartett, '1'); 
                player.remove(Quartett, '2'); 
                player.remove(Quartett, '3'); 
                player.remove(Quartett, '4'); 
                player_quartetts++;
                Console.WriteLine($"Du hast ein Quartett von {Quartett} bekommen das ist dein {player_quartetts}. Quartett");
                
            }

            Quartett = bot.has_Quartett();
            if (Quartett != '-') {
                Console.WriteLine(bot.ToString());
                bot.remove(Quartett, '1');
                bot.remove(Quartett, '2');
                bot.remove(Quartett, '3');
                bot.remove(Quartett, '4');    
                bot_quartetts++;
                Console.WriteLine($"Der Bot hat jetzt sein {bot_quartetts}. Quartett");
                
            }
            if (player_quartetts == 4) {
                Console.WriteLine("Du hast gewonnen");
                break;
            }

            if (bot_quartetts == 4) {
                Console.WriteLine("Du hast verloren");
                break;
            }
        }
    }
}