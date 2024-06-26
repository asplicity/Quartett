internal class Program {
    class Card(string Letter, string number) {
        string letter = Letter;
        string number = number;
    }
    class Deck(Card[] Deck){
        Card[] deck = Deck;

        public void shuffle() {
            Random rnd = new Random();
            int l = deck.Length;
            for(int i = 0; i < l; i++) {
                
            }
        }
    }
}
