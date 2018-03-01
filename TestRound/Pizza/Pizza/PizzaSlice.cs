namespace Pizza
{
    public class PizzaSlice
    {
        public int TopRow { get; set; }
        public int LeftColumn { get; set; }
        
        public int BottomRow { get; set; }
        public int RightColumn { get; set; }

        public bool IsValid(PizzaInstance instance)
        {
            if (TopRow > BottomRow) return false;
            if (LeftColumn > RightColumn) return false;
            if ((BottomRow - TopRow) * (LeftColumn - RightColumn) > instance.MaximumCellsInSlice) return false;

            int m = 0, t = 0;
            for (var row = TopRow; row < BottomRow; row++)
            {
                for (var col = LeftColumn; col < RightColumn; col++)
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

        public override string ToString()
        {
            return $"{TopRow} {LeftColumn} {BottomRow} {RightColumn}";
        }
    }
}