using System;
using System.Threading;

namespace Pizza
{
    public class PizzaSlice
    {
        public int Size { get; }
        
        public int TopRow { get; }
        public int LeftColumn { get; }
        
        public int BottomRow { get; }
        public int RightColumn { get; }

        public PizzaSlice(int topRow, int leftColumn, int bottomRow, int rightColumn)
        {
            TopRow = topRow;
            LeftColumn = leftColumn;
            BottomRow = bottomRow;
            RightColumn = rightColumn;
            Size = (bottomRow - topRow + 1) * (rightColumn - leftColumn + 1);
        }

        public bool IsValid(PizzaInstance instance)
        {
            if (TopRow > BottomRow) return false;
            if (LeftColumn > RightColumn) return false;
            if (Size > instance.MaximumCellsInSlice) return false;

            int m = 0, t = 0;
            for (var row = TopRow; row <= BottomRow; row++)
            {
                for (var col = LeftColumn; col <= RightColumn; col++)
                {
                    if (instance.Pizza[row, col] == PizzaIngredient.Mushroom)
                        m++;
                    else
                        t++;
                }
            }

            if (m < instance.MinimumOfEachIngredient || t < instance.MinimumOfEachIngredient) return false;

            return true;
        }

        public bool Overlaps(PizzaSlice other)
        {
            return (LeftColumn < other.RightColumn && RightColumn > other.LeftColumn && BottomRow > other.TopRow && TopRow < other.BottomRow) ||
                   (other.LeftColumn < LeftColumn && RightColumn < other.RightColumn && other.TopRow < TopRow && BottomRow < other.BottomRow);
        }

        public override string ToString()
        {
            return $"{TopRow} {LeftColumn} {BottomRow} {RightColumn}";
        }
    }
}