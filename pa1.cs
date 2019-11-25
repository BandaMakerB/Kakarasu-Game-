/* Author: Eknoor Singh
 * Last Date Modified: 09-29-19
 * The purpose of this game is to simulate the game of Kakarusu without the winning conditions 
 **/
using System;
using static System.Console;

namespace Bme121
{
    static class Program
    {
        static bool useBoxDrawingChars = true;
        static string[ ] letters = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l" };
        static int boardSize = 8; // must be in the range 1..12.

        static Random rGen = new Random( );
        /*@param char letter
         *@return int index
         * This method takes in the character input from the user and converts it into it's respective index for the arrarys of the program
         */
        public static int conversion (char letter){
            
            //Switch statement which equates 1 & a into the index of 0, 2 & b into 1, e.t.c
            switch (letter){
                case '1':
                case 'a':
                    return 0;
                case 'b':
                case '2':
                    return 1;
                case 'c':
                case '3':
                    return 2;
                case 'd':
                case '4':
                    return 3;
                case 'e':
                case '5':
                    return 4;
                case 'f':
                case '6':
                    return 5;
                case 'g':
                case '7':
                    return 6;
                case 'h':
                case '8':
                    return 7;
                //Incorrect input returns a -1, which is later accounts for invalid input
                default:
                    return -1;
                }
                
            }
        static void Main( )
        {

            //Intializing all arrays needed for the game
            int [] sumOfHiddenRows = new int[boardSize];
            int [] sumOfHiddenCols = new int[boardSize];
            int [] sumOfPlayerRows = new int[boardSize];
            int [] sumOfPlayerCols = new int[boardSize];
            bool [,] marked = new bool[boardSize,boardSize];
            string [,] stringMark = new string[boardSize,boardSize];
            int [,] hiddenNumbers = new int[boardSize,boardSize];
           
            //Marking all the hidden cells with the probability of 20% of it being marked
            for (int a = 0; a < boardSize; a++)
                for(int b = 0; b < boardSize; b++){
                    if (rGen.NextDouble() <= 0.2){
                        hiddenNumbers[a,b] = 1;
                    }
                    else 
                        hiddenNumbers[a,b] = 0;
                    }
                    
            //Summing the hidden cells into their respective rows and coloum sums
            for (int a = 0; a < boardSize; a++)
                for(int b = 0; b < boardSize; b++){
                sumOfHiddenRows[a] = sumOfHiddenRows[a] + (hiddenNumbers[a,b]*(b+1));
                sumOfHiddenCols[b] = sumOfHiddenCols[b] + (hiddenNumbers[a,b]*(a+1));
            }
            
            //Main game loop without resetting the intialized variables
            //Every for loop not commented on is strictly formatting and the creation of the board
            bool gameNotQuit = true;
            while( gameNotQuit )
            {
                Console.Clear( );
                WriteLine( );
                WriteLine( "    Play Kakurasu!" );
                WriteLine( );


                if( useBoxDrawingChars )
                {
                    for( int row = 0; row < boardSize; row ++ )
                    {
                        if( row == 0 )
                        {
                            Write( "        " );
                            for( int col = 0; col < boardSize; col ++ )
                                Write( "  {0} ", letters[ col ] );
                            WriteLine( );

                            Write( "        " );
                            for( int col = 0; col < boardSize; col ++ )
                                Write( " {0,2} ", col + 1 );
                            WriteLine( );

                            Write( "        \u250c" );
                            for( int col = 0; col < boardSize - 1; col ++ )
                                Write( "\u2500\u2500\u2500\u252c" );
                            WriteLine( "\u2500\u2500\u2500\u2510" );
                        }

                        Write( "   {0} {1,2} \u2502", letters[ row ], row + 1 );
                        
                        //Any marked cell is marked with an X, marked[,] is the boolean array that holds the marked cells 
                        for( int col = 0; col < boardSize; col ++ )
                        {
                        if( marked[row,col] ) Write( " X \u2502" );
                        else                        Write( "   \u2502" );
                            
                        }
                        
                        //Write out the sum of the rows, hidden and player inputted values
                        WriteLine(sumOfPlayerRows[row]+ " " + sumOfHiddenRows[row] );

                        if( row < boardSize - 1 )
                        {
                            Write( "        \u251c" );
                            for( int col = 0; col < boardSize - 1; col ++ )
                                Write( "\u2500\u2500\u2500\u253c" );
                            WriteLine( "\u2500\u2500\u2500\u2524" );
                        }
                        else
                        {
                            Write( "        \u2514" );
                            for( int col = 0; col < boardSize - 1; col ++ )
                                Write( "\u2500\u2500\u2500\u2534" );
                            WriteLine( "\u2500\u2500\u2500\u2518" );

                            //Write out the sum of the rows, hidden and player inputted values
                            Write( "         " );
                            for( int col = 0; col < boardSize; col ++ )
                                Write(" {0,2} ", sumOfPlayerCols[col]);
                            WriteLine( );

                            Write( "         " );
                            for( int col = 0; col < boardSize; col ++ )
                                Write(" {0,2} ", sumOfHiddenCols[col]);
                            WriteLine( );
                        }
                    }
                }
                

                WriteLine( );
                WriteLine( "   Toggle cells to match the row and column sums." );
                Write(     "   Enter a row-column letter pair or 'quit': " );
                WriteLine();

                string response = ReadLine( );
        

                if( response == "quit" ) gameNotQuit = false;
                else
                {
                    //ToCharArray is a string method that seperates a string into an array of chars
                    char[] coordinates = response.ToCharArray();
                    
                    //Using method conversion to find the index values of the inputted chars
                    int xCoord = conversion(coordinates[0]);
                    int yCoord = conversion(coordinates[1]);
                    
                    //Accounting for incorrect input from the user, continue runs the main game loop again 
                    if (xCoord == -1 || yCoord == -1 || response.Length != 2){
                        continue;
                    }
                    
                    //If the cell that the user inputted was already marked, it becomes unmarked and the sums are subtracted
                    if(marked[xCoord,yCoord]){
                        marked[xCoord, yCoord] = false;
                        sumOfPlayerRows[xCoord]= sumOfPlayerRows[xCoord] - (yCoord+1);
                        sumOfPlayerCols[yCoord]= sumOfPlayerCols[yCoord] - (xCoord+1);
                    }
                    //If the cell that the user inputted was already marked, it becomes unmarked and the sums are subtracted
                    else{
                        marked[xCoord, yCoord] = true;
                        sumOfPlayerRows[xCoord]= sumOfPlayerRows[xCoord] + (yCoord+1);
                        sumOfPlayerCols[yCoord]= sumOfPlayerCols[yCoord] + (xCoord+1);
                    
                    //Accounts for a won game, a counter variable is incremented each time a rows sum matches the hidden sum, if all rows and coloums match, the game is won
                    int counter = 0;
                    for(int a = 0; a < boardSize; a++) {
                        if (sumOfPlayerRows[a] == sumOfHiddenRows[a]) counter++;
                        if (sumOfPlayerCols[a] == sumOfHiddenCols[a]) counter++;
                        }
                    if (counter == 16){
                        gameNotQuit = false;
                        WriteLine("You won the game!");
                    }
                }
            }
            //WriteLine is used for spacing
            WriteLine( );
        }
    }
}
}
