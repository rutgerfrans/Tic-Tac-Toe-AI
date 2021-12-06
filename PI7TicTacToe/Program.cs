using System;
using System.Collections.Generic;
using System.Linq;

namespace PI7TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            Player Thelegend27 = new Player("thelegend27", false, "O ", false);
            Player Siri = new Player("siri", false, "O ", true);

            List<string> TicTacToe = new List<string>() { ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". " };
            List<List<string>> WinCondition = new List<List<string>>();

            //init      
            var TurnPlayer = DefStart(Thelegend27, Siri);
            if (!TurnPlayer.ai)
            {
                Print(TicTacToe);
            }
            WinCondition = UpdateWinCon(TicTacToe, WinCondition);

            //game
            while (TicTacToe.Contains(". "))
            {
                turn(TurnPlayer, TicTacToe, WinCondition);
                WinCondition = UpdateWinCon(TicTacToe, WinCondition);
                if (CheckWin(WinCondition, TurnPlayer))
                {
                    break;
                }
                Print(TicTacToe);
                TurnPlayer = SwapTurn(Thelegend27, Siri);
            }
            if (!TicTacToe.Contains(". "))
            {
                Console.WriteLine("Tie");
            }
        }

        static private Player SwapTurn(Player playerone, Player playertwo)
        {
            if (playerone.turn)
            {
                playerone.turn = false;
                playertwo.turn = true;
                Console.WriteLine("Ai choosing...");
                return playertwo;
            }
            else
            {
                playertwo.turn = false;
                playerone.turn = true;
                return playerone;
            }
        }

        static private Player DefStart(Player playerone, Player playertwo)
        {
            Random rand = new Random();
            int turn = rand.Next(1, 3);
            if (turn == 1)
            {
                playerone.turn = true;
                playerone.symbol = "X ";
                return playerone;
            }
            else
            {
                playertwo.turn = true;
                playertwo.symbol = "X ";
                return playertwo;
            }
        }

        static private List<List<string>> UpdateWinCon(List<string> TicTacToe, List<List<string>> WinCondition)
        {
            WinCondition.Clear();
            WinCondition.Add(new List<string>() { TicTacToe[0], TicTacToe[1], TicTacToe[2] });
            WinCondition.Add(new List<string>() { TicTacToe[3], TicTacToe[4], TicTacToe[5] });
            WinCondition.Add(new List<string>() { TicTacToe[6], TicTacToe[7], TicTacToe[8] });
            WinCondition.Add(new List<string>() { TicTacToe[0], TicTacToe[3], TicTacToe[6] });
            WinCondition.Add(new List<string>() { TicTacToe[1], TicTacToe[4], TicTacToe[7] });
            WinCondition.Add(new List<string>() { TicTacToe[2], TicTacToe[5], TicTacToe[8] });
            WinCondition.Add(new List<string>() { TicTacToe[0], TicTacToe[4], TicTacToe[8] });
            WinCondition.Add(new List<string>() { TicTacToe[2], TicTacToe[4], TicTacToe[6] });
            return WinCondition;
        }

        static private bool CheckWin(List<List<string>> WinCon, Player player)
        {
            var XSymbolCount = 0;
            var OSymbolCount = 0;
            for (int i = 0; i < WinCon.Count; i++)
            {
                var Xcount = 0;
                var Ocount = 0;

                for (int x = 0; x < WinCon[i].Count; x++)
                {
                    if (WinCon[i][x] == "X ")
                    {
                        Xcount++;
                    }
                    else if (WinCon[i][x] == "O ")
                    {
                        Ocount++;
                    }
                }

                if (Xcount > XSymbolCount)
                {
                    XSymbolCount = Xcount;
                }
                else if (Ocount > OSymbolCount)
                {
                    OSymbolCount = Ocount;
                }

                //CheckWin
                if (XSymbolCount == 3)
                {
                    if (player.symbol == "X ")
                    {
                        Console.WriteLine(player.name + " wins!");
                    }
                    return true;
                }
                else if (OSymbolCount == 3)
                {
                    if (player.symbol == "O ")
                    {
                        Console.WriteLine(player.name + " wins!");
                    }
                    return true;
                }
            }
            return false;
        }

        static private void turn(Player player, List<string> TicTacToe, List<List<string>> WinCon)
        {
            if (player.ai)
            {
                Cmove(TicTacToe, WinCon, player);
            }
            else
            {
                Pmove(TicTacToe, player);
            }
        }

        static private void Pmove(List<string> TicTacToe, Player player)
        {
            string ChooseGrid = "";
            for (int i = 0; i < TicTacToe.Count; i++)
            {
                if (TicTacToe[i] == ". ")
                {
                    ChooseGrid += 1 + i + " ";
                }
            }
            Console.WriteLine("Choose: " + ChooseGrid);

            int Choose = Convert.ToInt32(Console.ReadLine());
            if (TicTacToe[Choose-1] != ". ")
            {
                Console.WriteLine("Already Chosen!");
                Pmove(TicTacToe, player);
            }
            else
            {            
                TicTacToe[Choose - 1] = player.symbol;
            }

        }

        static private void Cmove(List<string> TicTacToe, List<List<string>> WinCon, Player player)
        {
            int PRcount = 0;
            int CRcount = 0;

            var PlayerSymbolCount = 0;
            var ComputerSymbolCount = 0;

            for (int i = 0; i < WinCon.Count; i++)
            {
                var Pcount = 0;
                var Ccount = 0;

                for (int x = 0; x < WinCon[i].Count; x++)
                {
                    if (WinCon[i][x] == player.symbol)
                    {
                        Ccount++;
                    }
                    else if (WinCon[i][x] != ". ")
                    {
                        Pcount++;
                    }
                }
                //hoogste player symbol count zoeken
                if (Pcount > PlayerSymbolCount && ((PlayerSymbolCount + ComputerSymbolCount) < 3) && ((Pcount + Ccount) < 3))//
                {
                    PlayerSymbolCount = Pcount;
                    PRcount = i;
                }
                //hoogste computer symbol count zoeken
                else if (Ccount > ComputerSymbolCount && ((PlayerSymbolCount + ComputerSymbolCount) < 3) && ((Pcount + Ccount) < 3))//
                {
                    ComputerSymbolCount = Ccount;
                    CRcount = i;
                }

            }
            List<List<int>> WCConverter = new List<List<int>>();
            WCConverter.Add(new List<int>() { 0, 1, 2 });
            WCConverter.Add(new List<int>() { 3, 4, 5 });
            WCConverter.Add(new List<int>() { 6, 7, 8 });
            WCConverter.Add(new List<int>() { 0, 3, 6 });
            WCConverter.Add(new List<int>() { 1, 4, 7 });
            WCConverter.Add(new List<int>() { 2, 5, 8 });
            WCConverter.Add(new List<int>() { 0, 4, 8 });
            WCConverter.Add(new List<int>() { 2, 4, 6 });

            var totpsc = TicTacToe.Where(x => x != ". " && x != player.symbol);
            var totcsc = TicTacToe.Where(x => x == player.symbol);
            var totesc = TicTacToe.Where(x => x == ". ");


            //attack
            if (ComputerSymbolCount >= PlayerSymbolCount)
            {
                //als de bot mag beginnen
                if (totcsc.Count() < 1)
                {
                    TicTacToe[TicTacToe.Count / 2] = player.symbol;
                    return;
                }

                //een probleem waarbij de speler alsnog kon winnen als ie in het midden begon wordt hiermee vermeden.
                //o . .          . . o
                //. x .          . x .
                //. . x          x . .
                //de bot moet in bovenstaande patronen kiezen voor een hoek ipv van een side anders kan despeler alsnog winnen.
                else if (totcsc.Count() == 1 && totpsc.Count() == 2)
                {
                    foreach (var con in WinCon)
                    {
                        if (!con.Contains(". "))
                        {
                            for (int i = 0; i < TicTacToe.Count; i++)
                            {
                                if (i % 2 == 0 && i != (TicTacToe.Count / 2) && TicTacToe[i] == ". ")
                                {
                                    TicTacToe[i] = player.symbol;
                                    return;
                                }
                            }
                        }
                    }
                }

                //een probleem waarbij de speler alsnog kon winnen als de bot in het midden begint wordt.
                //x . .          . . x
                //. o .          . o .
                //. . .          . . .
                //de bot moet in bovenstaande patronen kiezen voor een hoek ipv van een side anders kan despeler alsnog winnen.
                else if (totcsc.Count() == 1 && totpsc.Count() == 1)
                {
                    foreach (var con in WinCon)
                    {
                        if (con.Contains("X ") && con.Contains("O ") && con.Contains(". ") )
                        {
                            for (int i = 0; i < TicTacToe.Count; i++)
                            {
                                if (i % 2 == 0 && i != (TicTacToe.Count / 2) && TicTacToe[i] == ". ")
                                {
                                    TicTacToe[i] = player.symbol;
                                    return;
                                }
                            }
                        }
                    }
                }


                //wanneer de bot kan winnen waarbij de computersymbolcount groter is dan playersymbolcount
                else
                {
                    List<int> AttackRow = WCConverter[CRcount];
                    bool zet = false;

                    while (!zet)
                    {
                        for (int i = 0; i < AttackRow.Count; i++)
                        {
                            if (TicTacToe[AttackRow[i]] == ". ")
                            {
                                TicTacToe[AttackRow[i]] = player.symbol;
                                i = AttackRow.Count;
                                zet = true;
                            }
                        }
                    }
                }
            }

            //defend
            else if (ComputerSymbolCount < PlayerSymbolCount)
            {
                //wanneer de bot kan verliezen countered hij doormiddel van onderstaande functie de speler.
                if (totpsc.Count() > 1)
                {
                    List<int> DefendRow = WCConverter[PRcount];
                    bool zet = false;
                    while (!zet)
                    {
                        for (int i = 0; i < DefendRow.Count; i++)
                        {
                            if (TicTacToe[DefendRow[i]] == ". ")
                            {
                                TicTacToe[DefendRow[i]] = player.symbol;
                                i = DefendRow.Count;
                                zet = true;
                            }
                        }
                    }
                }
                //wanneer de speler is begonnen en de bot nog geen zet heeft gezet.
                else
                {
                    //wanneer de speler in het midden begint dan countered de bot met een zet in een van de hoeken.
                    if (TicTacToe[TicTacToe.Count / 2] != ". " && !TicTacToe.Contains(player.symbol))
                    {
                        List<int> Corners = new List<int>() { 0, 2, 6, 8};
                        Random rand = new Random();
                        int randcorner = rand.Next(-1, 4);
                        TicTacToe[Corners[randcorner]] = player.symbol;
                        return;
                    }
                    //wanneer de speler in de hoek of op de zijkanten begint dan countered de bot met een zet in het midden.
                    else
                    {
                        TicTacToe[TicTacToe.Count / 2] = player.symbol;
                        return;
                    }
                }
            }
        }

        static private void Print(List<string> list)
        {
            string Row1 = "";
            string Row2 = "";
            string Row3 = "";
            for (int i = 0; i < 3; i++)
            {
                Row1 += list[i];
            }
            for (int i = 3; i < 6; i++)
            {
                Row2 += list[i];
            }
            for (int i = 6; i < 9; i++)
            {
                Row3 += list[i];
            }
            Console.WriteLine(Row1 + "\n" + Row2 + "\n" + Row3);
        }     
    }
}
