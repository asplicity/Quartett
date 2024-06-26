internal class Program
{
    private static void Main(string[] args)
    {
        string Stapel = "A1A2A3A4B1B2B3B4C1C2C3C4D1D2D3D4E1E2E3E4F1F2F3F4G1G2G3G4H1H2H3H4";
        int KartenImBlatt = Stapel.Length / 2;
        var Stapel_player = "";
        var Stapel_bot = "";
        Random rnd = new Random();
        for (int i = 0; i < 13; i++) {
            var RandomKarte = rnd.Next(0, KartenImBlatt);
            var Karte = Stapel.Substring(RandomKarte * 2, 2);
            Stapel = Stapel.Replace(Karte, "");
            KartenImBlatt -= 1;
            if (i % 2 == 0) {
                Stapel_bot += Karte;
            } else {
                Stapel_player += Karte;
            }
        }
        Console.WriteLine($"Du hast die Karten: {Stapel_player}, welchen Wert möchtest du checken?");
        var Wert = Console.ReadKey();
    }
}