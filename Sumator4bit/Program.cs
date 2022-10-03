using System;

namespace Sumator4bit
{
    class Program
    {
        static void Main(string[] args)
        {
            bool A1 = false; bool A2 = false; bool A3 = false; bool A4 = false;
            bool B1 = false; bool B2 = false; bool B3 = false; bool B4 = false;
            bool C1; bool C2; bool C3; bool C4;
            bool D1; bool D2; bool D3; bool D4;
            bool E1; bool E2; bool E3; bool E4;
            bool F1; bool F2; bool F3; bool F4;
            bool G1; bool G2; bool G3; bool G4;
            bool W1; bool W2; bool W3; bool W4;

            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                {
                    #region WartosciA1_A4
                    if (i == 0)
                    {
                        A4 = false; A3 = false; A2 = false; A1 = false; // 0
                    }
                    else if (i == 1)
                    {
                        A4 = false; A3 = false; A2 = false; A1 = true; // 1
                    }
                    else if (i == 2)
                    {
                        A4 = false; A3 = false; A2 = true; A1 = false; // 2
                    }
                    else if (i == 3)
                    {
                        A4 = false; A3 = false; A2 = true; A1 = true; // 3
                    }
                    else if (i == 4)
                    {
                        A4 = false; A3 = true; A2 = false; A1 = false; // 4
                    }
                    else if (i == 5)
                    {
                        A4 = true; A3 = false; A2 = true; A1 = true; // 5
                    }
                    else if (i == 6)
                    {
                        A4 = true; A3 = true; A2 = false; A1 = false; // 6
                    }
                    else if (i == 7)
                    {
                        A4 = true; A3 = true; A2 = false; A1 = true; // 7
                    }
                    else if (i == 8)
                    {
                        A4 = true; A3 = true; A2 = true; A1 = false; // 8
                    }
                    else if (i == 9)
                    {
                        A4 = true; A3 = true; A2 = true; A1 = true; // 9
                    }
                    #endregion
                    #region WartosciB1_B4
                    if (j == 0)
                    {
                        B4 = false; B3 = false; B2 = false; B1 = false; // 0
                    }
                    else if (j == 1)
                    {
                        B4 = false; B3 = false; B2 = false; B1 = true; // 1
                    }
                    else if (j == 2)
                    {
                        B4 = false; B3 = false; B2 = true; B1 = false; // 2
                    }
                    else if (j == 3)
                    {
                        B4 = false; B3 = false; B2 = true; B1 = true; // 3
                    }
                    else if (j == 4)
                    {
                        B4 = false; B3 = true; B2 = false; B1 = false; // 4
                    }
                    else if (j == 5)
                    {
                        B4 = true; B3 = false; B2 = true; B1 = true; // 5
                    }
                    else if (j == 6)
                    {
                        B4 = true; B3 = true; B2 = false; B1 = false; // 6
                    }
                    else if (j == 7)
                    {
                        B4 = true; B3 = true; B2 = false; B1 = true; // 7
                    }
                    else if (j == 8)
                    {
                        B4 = true; B3 = true; B2 = true; B1 = false; // 8
                    }
                    else if (j == 9)
                    {
                        B4 = true; B3 = true; B2 = true; B1 = true; // 9
                    }
                    #endregion

                    // Konwenter A (Aiken > BCD)
                    C1 = A1;
                    C2 = NAND(NAND(NOT(A2), A4), NAND(A2, NOT(A4)));
                    C3 = NAND(NAND(A3, NOT(A2)), NAND(NOT(A3), A4));
                    C4 = NOT(NAND(A2, A3));
                    // Konwenter B (Aiken > BCD)
                    D1 = B1;
                    D2 = NAND(NAND(NOT(B2), B4), NAND(B2, NOT(B4)));
                    D3 = NAND(NAND(B3, NOT(B2)), NAND(NOT(B3), B4));
                    D4 = NOT(NAND(B2, B3));

                    // Obliczenia E1-E4
                    E1 = XOR(C1, D1);
                    E2 = XOR(C2, D2);
                    E3 = XOR(C3, D3);
                    E4 = XOR(C4, D4);
                    // Obliczenie F1-F4
                    F1 = AND(C1, D1);
                    F2 = OR(AND(F1, E2), AND(C2, D2));
                    F3 = OR(AND(F2, E3), AND(C3, D3));
                    F4 = OR(AND(F3, E4), AND(C4, D4));
                    //Obliczanie G1-G4
                    G1 = AND(ThreeIN_AND(NOT(E1), NOT(XOR(E2, F1)), NOT(XOR(E3, F2))), XOR(E4, F3));
                    G2 = AND(ThreeIN_AND(E1, NOT(XOR(E2,F1)) ,NOT(XOR(E3,F2))), XOR(E4, F3));
                    G3 = OR(G1, G2);
                    G4 = OR(AND(XOR(E4, F3), NOT(G3)), F4);
                    //Obliczenie W1-W4
                    W1 = Tri_State(E1, NOT(G4));
                    W2 = Tri_State(XOR(E2, F1), NOT(G4));
                    W3 = Tri_State(XOR(E3, F2), NOT(G4));
                    W4 = Tri_State(G3, NOT(G4));

                    if (G4)
                    {
                        Console.WriteLine("Wartość powyżej 9 :");
                        Console.WriteLine("A{0}{1}{2}{3} Wartość dziesiętna; {4} ", ToInt(A4), ToInt(A3), ToInt(A2), ToInt(A1), AidenToDecimal(A1, A2, A3, A4));
                        Console.WriteLine("B{0}{1}{2}{3} Wartość dziesiętna; {4}", ToInt(B4), ToInt(B3), ToInt(B2), ToInt(B1), AidenToDecimal(B1, B2, B3, B4));
                    }
                    else
                    {
                        Wynik(W1, W2, W3, W4, i, j);
                        Console.WriteLine("A{0}{1}{2}{3} Wartość dziesiętna; {4} ", ToInt(A4), ToInt(A3), ToInt(A2), ToInt(A1), AidenToDecimal(A1, A2, A3, A4));
                        Console.WriteLine("B{0}{1}{2}{3} Wartość dziesiętna; {4}", ToInt(B4), ToInt(B3), ToInt(B2), ToInt(B1), AidenToDecimal(B1, B2, B3, B4));
                    }
                    if (i == 9 && j == 1)
                    {
                        Console.WriteLine();
                        Console.WriteLine("A{0}", FourBoolToString(A1, A2, A3, A4));
                        Console.WriteLine("B{0}", FourBoolToString(B1, B2, B3, B4));
                        Console.WriteLine("C{0}", FourBoolToString(C1, C2, C3, C4));
                        Console.WriteLine("D{0}", FourBoolToString(D1, D2, D3, D4));
                        Console.WriteLine("E{0}", FourBoolToString(E1, E2, E3, E4));
                        Console.WriteLine("F{0}", FourBoolToString(F1, F2, F3, F4));
                        Console.WriteLine("G{0}", FourBoolToString(G1, G2, G3, G4));
                        Console.WriteLine("W{0}", FourBoolToString(W1, W2, W3, W4));
                    }


                    Console.WriteLine();
                }


        }
        static int ToInt(bool x) => Convert.ToInt32(x);
        static string FourBoolToString(bool x1, bool x2, bool x3, bool x4) => Convert.ToInt32(x4).ToString() + Convert.ToInt32(x3) + Convert.ToInt32(x2) + Convert.ToInt32(x1);
        static void Wynik(bool W1, bool W2, bool W3, bool W4, int i, int j)
        {
            int wynik = 0;
            if (!W4 && !W3 && !W2 && !W1) wynik = 0;
            else if (!W4 && !W3 && !W2 && W1) wynik = 1;
            else if (!W4 && !W3 && W2 && !W1) wynik = 2;
            else if (!W4 && !W3 && W2 && W1) wynik = 3;
            else if (!W4 && W3 && !W2 && !W1) wynik = 4;
            else if (!W4 && W3 && !W2 && W1) wynik = 5;
            else if (!W4 && W3 && W2 && !W1) wynik = 6;
            else if (!W4 && W3 && W2 && W1) wynik = 7;
            else if (W4 && !W3 && !W2 && !W1) wynik = 8;
            else if (W4 && !W3 && !W2 && W1) wynik = 9;

            Console.WriteLine("{0} + {1}  = {2}  Wynik BCD:" + Convert.ToInt32(W4) + Convert.ToInt32(W3) + Convert.ToInt32(W2) + Convert.ToInt32(W1), i, j, wynik);
        }
        static int AidenToDecimal(bool W1, bool W2, bool W3, bool W4)
        {
            if (!W4 && !W3 && !W2 && !W1) return 0;
            else if (!W4 && !W3 && !W2 && W1) return 1;
            else if (!W4 && !W3 && W2 && !W1) return 2;
            else if (!W4 && !W3 && W2 && W1) return 3;
            else if (!W4 && W3 && !W2 && !W1) return 4;
            else if (W4 && !W3 && W2 && W1) return 5;
            else if (W4 && W3 && !W2 && !W1) return 6;
            else if (W4 && W3 && !W2 && W1) return 7;
            else if (W4 && W3 && W2 && !W1) return 8;
            else if (W4 && W3 && W2 && W1) return 9;
            else return -1;
        }

        static bool NOT(bool A) => !A;
        static bool AND(bool A, bool B)
        {
            if (!A && !B) return false;
            else if (!A && B) return false;
            else if (A && !B) return false;
            else return true;
        }
        static bool NAND(bool A, bool B) => !AND(A, B);
        static bool OR(bool A, bool B)
        {
            if (!A && !B) return false;
            else if (!A && B) return true;
            else if (A && !B) return true;
            else return true;
        }
        static bool XOR(bool A, bool B)
        {
            if (!A && !B) return false;
            else if (!A && B) return true;
            else if (A && !B) return true;
            else return false;
        }
        static bool ThreeIN_AND(bool A, bool B, bool C)
        {
            if (A && B && C) return true;
            else return false;
        }
        static bool ThreeIN_NAND(bool A, bool B, bool C) => !ThreeIN_AND(A, B, C);
        static bool Tri_State(bool A, bool isEnable)
        {
            if (isEnable)
            {
                if (A) return true;
                else return false;
            }
            else return false;
        }


    }
}
