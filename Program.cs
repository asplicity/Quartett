internal class Program
{
    private static void Main(string[] args)
    {
        string Stapel = "A1A2A3A4B1B2B3B4C1C2C3C4D1D2D3D4E1E2E3E4F1F2F3F4G1G2G3G4H1H2H3H4";
        int KartenImBlatt = Stapel.Length / 2;
        var StapelPlayer = "";
        var StapelBot = "";
        Random rnd = new Random();
        string zwischen = "";
        for (int i = 0; i < 32; i++) {
            var RandomKarte = rnd.Next(0, KartenImBlatt);
            var Karte = Stapel.Substring(RandomKarte * 2, 2);
            Stapel = Stapel.Replace(Karte, "");
            KartenImBlatt -= 1;
            zwischen += Karte;
        }
        Stapel = zwischen;
        for (int i = 0; i < 13; i++) {
            var Karte = Stapel.Substring(0, 2);
            if (i % 2 == 0) {
                StapelPlayer += Karte;
            } else{
                StapelBot += Karte;
            }
            Stapel = Stapel.Replace(Karte, "");
        }


    }
}