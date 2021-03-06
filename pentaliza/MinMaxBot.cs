﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace Bots
{
    public class MinMaxBot : Bot//bot that uses minimax algorithm (i sould use a tree data structure for all the moves and then search the best. Now it creats all the moves(up to the max deapth) every time)
    {
        private List<Point> pointEmptyList;
        private int maxDeapth = -1;
        public int MaxDeapth { set{ maxDeapth=value; } }
        
        
        public MinMaxBot()
        {
        }
        public MinMaxBot(int maxDeapth)
        {
            this.maxDeapth = maxDeapth;
        }
        public MinMaxBot(int ID, int EnemyId, int EmptyId, int maxDeapth=-1)
        {
            id = ID;
            enemyId = EnemyId;
            emptyId = EmptyId;
            this.maxDeapth = maxDeapth;
        }
        override public Point CalculateMove(int[,] Board)//will return the best move using minimax
        {
            currentPlayer = id;
            pointEmptyList = pointsEmpty(Board);
            if (maxDeapth < 0|| maxDeapth > (int)Math.Ceiling((double)(9 * 9) / Board.Length)) {
                maxDeapth = (int) Math.Ceiling((double)(9 * 9) / Board.Length);
                if (maxDeapth < 1)
                {
                    maxDeapth = 1;
                }
            }
            return minMax(Board, pointEmptyList, id, maxDeapth).location;
        }
        private Move minMax(int[,] Board, List<Point> pointEmptyList, int Player, int maxDeapth, int deapth = 0)//the starting function of minimax (creates the 1st deapth) and returns the best move
        {
            Move move;
            Move tempMove;
            List<Move> moves = new List<Move>();
            foreach (Point p in pointEmptyList)
            {
                int i = p.X, j = p.Y;
                if (Board[i, j] == emptyId)
                {
                    move = new Move(Board, pointEmptyList, new Point(i, j), Player, deapth);
                    move.score = calculateScore(move, deapth);
                    if (move.pointEmptyList.Count>0 && move.score == 0)
                    {
                        if (deapth < maxDeapth)
                        {
                            tempMove = minMax(move, nextTurn(Player), maxDeapth, deapth + 1);
                            if (tempMove.Player == Player)
                            {
                                move.score = tempMove.score;
                            }
                            else
                            {
                                move.score = -tempMove.score;
                            }
                        }
                        else
                        {
                            
                        }
                    }
                    
                    moves.Add(move);
                }
            }
            Move bestMove = BestMove(moves);
            return bestMove;
        }
        private Move minMax(Move pmove, int Player, int maxDeapth, int deapth = 0)//returns the best move based on the previus move
        {
            List<Point> pointEmptyList = pmove.pointEmptyList;
            int[,] Board = pmove.Board;
            Move tempMove, move;
            List<Move> moves = new List<Move>();
            double totalScore = 0;
            foreach (Point p in pointEmptyList ) {
                int i=p.X, j=p.Y;
                if (Board[i, j] == emptyId)
                {
                    move = new Move(Board, pointEmptyList, new Point(i, j), Player, deapth);
                    move.score = calculateScore(move, deapth);
                    if (move.pointEmptyList.Count > 0 && move.score == 0)
                    {
                        if (deapth < maxDeapth)
                        {
                            tempMove = minMax(move, nextTurn(Player), maxDeapth, deapth + 1);
                            move.score = -tempMove.score;
                        }
                        else
                        {
                            
                        }
                    }

                    totalScore += move.score;
                    moves.Add(move);
                    
                }
            }
            Move bestMove = BestMove(moves);
            return bestMove;
        }
        

        private List<Point> pointsEmpty(int[,] Board)//returns a list of empty points
        {
            List<Point> points = new List<Point>();
            for (int i = 0; i < Board.GetLength(0); i++)
            {
                for (int j = 0; j < Board.GetLength(1); j++)
                {
                    if (Board[i, j] == emptyId)
                    {
                        points.Add(new Point(i, j));
                    }
                }
            }
            return points;
        }
        private double calculateScore(Move move, int deapth)//will return a score if it won based on the deapth
        {
            int[,] Board = move.Board;
            Point m = move.location;
            int p= 0;
            if (nextTurn(move.Player) == currentPlayer)
            {
                p = 1;
            }
            int rank = Board.Length;
            if (checkWin(move))
            {
                return Math.Pow((rank), (rank + p - deapth)) ;
            }
            else
            {
                return 0;
            }
        }
        
    }
}
