using System.Collections.Generic;

namespace DRAP.Combo
{
    public class ComboItem {}
    public class EnemyHit : ComboItem {}
    public class EnemyKilled : ComboItem {}
    public class Grind : ComboItem {}

    public class ComboChain 
    {
        public List<ComboItem> SolidifiedChain { get; private set; }
        public ComboItem CurrentItem { get; private set ; }
    }

    public class ComboSystem 
    {
        public void BreakCombo()
        {

        }

        public void AddCombo(ComboItem comboItem)
        {

        }
    }
}